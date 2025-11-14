using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Controller
{
    public class MovimentoCaixaController
    {
        /// <summary>
        /// Busca o extrato de caixa (Entradas e Saídas) por período.
        /// </summary>
        public async Task<List<MovimentoCaixa>> GetMovimentosPorPeriodoAsync(DateTime dataInicial, DateTime dataFinal)
        {
            var movimentos = new List<MovimentoCaixa>();
            using (var connection = Database.GetConnection())
            {
                // Query SQL que junta movimento e plano de contas
                string sql = @"
                    SELECT 
                        m.id, m.data_movimento, m.descricao, 
                        m.id_plano_de_contas, p.descricao AS plano_conta_descricao, 
                        m.tipo, m.valor, m.id_venda, m.id_compra
                    FROM 
                        movimento_caixa m
                    JOIN 
                        plano_de_contas p ON m.id_plano_de_contas = p.id
                    WHERE 
                        m.data_movimento BETWEEN @dataInicial AND @dataFinal
                    ORDER BY 
                        m.data_movimento DESC";

                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@dataInicial", dataInicial.Date);
                command.Parameters.AddWithValue("@dataFinal", dataFinal.Date.AddDays(1).AddSeconds(-1)); // Pega o dia final inteiro

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            movimentos.Add(MapearMovimento(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar movimentos do caixa: " + ex.Message);
                }
            }
            return movimentos;
        }

        /// <summary>
        /// Busca o relatório de Contas a Pagar (apenas Saídas) por período.
        /// </summary>
        public async Task<List<MovimentoCaixa>> GetContasPagarAsync(DateTime dataInicial, DateTime dataFinal)
        {
            var movimentos = new List<MovimentoCaixa>();
            using (var connection = Database.GetConnection())
            {
                // Query similar, mas filtrando apenas Tipo 'S' (Saída)
                string sql = @"
                    SELECT 
                        m.id, m.data_movimento, m.descricao, 
                        m.id_plano_de_contas, p.descricao AS plano_conta_descricao, 
                        m.tipo, m.valor, m.id_venda, m.id_compra
                    FROM 
                        movimento_caixa m
                    JOIN 
                        plano_de_contas p ON m.id_plano_de_contas = p.id
                    WHERE 
                        m.data_movimento BETWEEN @dataInicial AND @dataFinal
                        AND m.tipo = 'S'  -- Filtra apenas Saídas
                    ORDER BY 
                        m.data_movimento DESC";

                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@dataInicial", dataInicial.Date);
                command.Parameters.AddWithValue("@dataFinal", dataFinal.Date.AddDays(1).AddSeconds(-1));

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            movimentos.Add(MapearMovimento(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar contas a pagar: " + ex.Message);
                }
            }
            return movimentos;
        }

        /// <summary>
        /// Calcula o saldo total (soma de todas as entradas e saídas).
        /// </summary>
        public async Task<decimal> GetSaldoTotalAsync()
        {
            using (var connection = Database.GetConnection())
            {
                // SUM(CASE...) converte Saídas (S) para negativo e Entradas (E) para positivo
                string sql = @"
                    SELECT 
                        ISNULL(SUM(CASE WHEN tipo = 'E' THEN valor ELSE -valor END), 0) 
                    FROM 
                        movimento_caixa";

                var command = new SqlCommand(sql, connection);

                try
                {
                    await connection.OpenAsync();
                    var resultado = await command.ExecuteScalarAsync();
                    return Convert.ToDecimal(resultado);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao calcular saldo total: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Salva um novo lançamento manual (Entrada ou Saída).
        /// </summary>
        public async Task SalvarLancamentoManualAsync(MovimentoCaixa lancamento)
        {
            // Validação
            if (lancamento.IdPlanoDeContas <= 0)
                throw new Exception("O Plano de Contas é obrigatório.");
            if (string.IsNullOrWhiteSpace(lancamento.Descricao))
                throw new Exception("A Descrição é obrigatória.");
            if (lancamento.Valor <= 0)
                throw new Exception("O Valor deve ser maior que zero.");
            if (lancamento.Tipo != 'E' && lancamento.Tipo != 'S')
                throw new Exception("O Tipo é inválido (deve ser 'E' ou 'S').");

            using (var connection = Database.GetConnection())
            {
                string sql = @"
                    INSERT INTO movimento_caixa
                    (data_movimento, descricao, id_plano_de_contas, tipo, valor, id_venda, id_compra)
                    VALUES 
                    (@data, @descricao, @id_plano, @tipo, @valor, NULL, NULL)";

                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@data", lancamento.DataMovimento);
                command.Parameters.AddWithValue("@descricao", lancamento.Descricao);
                command.Parameters.AddWithValue("@id_plano", lancamento.IdPlanoDeContas);
                command.Parameters.AddWithValue("@tipo", lancamento.Tipo);
                command.Parameters.AddWithValue("@valor", lancamento.Valor);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao salvar lançamento manual: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método auxiliar para mapear o resultado do banco para o Model.
        /// </summary>
        private MovimentoCaixa MapearMovimento(SqlDataReader reader)
        {
            return new MovimentoCaixa
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                DataMovimento = reader.GetDateTime(reader.GetOrdinal("data_movimento")),
                Descricao = reader.GetString(reader.GetOrdinal("descricao")),
                IdPlanoDeContas = reader.GetInt32(reader.GetOrdinal("id_plano_de_contas")),
                PlanoContaDescricao = reader.GetString(reader.GetOrdinal("plano_conta_descricao")),
                Tipo = reader.GetString(reader.GetOrdinal("tipo"))[0],
                Valor = reader.GetDecimal(reader.GetOrdinal("valor")),
                IdVenda = reader.IsDBNull(reader.GetOrdinal("id_venda")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_venda")),
                IdCompra = reader.IsDBNull(reader.GetOrdinal("id_compra")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_compra"))
            };
        }
    }
}