using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SysFin_2CTDS.Models; // Para ItemVenda

namespace SysFin_2CTDS.Controller
{
    public class VendaController
    {
        /// <summary>
        /// Registra uma Venda completa, atualiza estoque e lança no caixa.
        /// </summary>
        public async Task RegistrarVendaAsync(int idCliente, BindingList<ItemVenda> itensVenda, decimal valorTotal)
        {
            const int ID_PLANO_CONTAS_RECEITA_VENDA = 1;

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();
                using (var transaction = (SqlTransaction)await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 1. Inserir o cabeçalho na tabela 'vendas'
                        var sqlVenda = "INSERT INTO vendas (id_cliente, data_venda, valor_total) OUTPUT INSERTED.id VALUES (@id_cliente, @data_venda, @valor_total)";

                        int vendaId;
                        using (var cmdVenda = new SqlCommand(sqlVenda, connection, transaction))
                        {
                            cmdVenda.Parameters.AddWithValue("@id_cliente", idCliente);
                            cmdVenda.Parameters.AddWithValue("@data_venda", DateTime.Now);
                            cmdVenda.Parameters.AddWithValue("@valor_total", valorTotal);
                            var result = await cmdVenda.ExecuteScalarAsync();
                            vendaId = Convert.ToInt32(result);
                        }

                        // 2. Inserir itens e atualizar estoque
                        foreach (var item in itensVenda)
                        {
                            var sqlItem = "INSERT INTO itens_venda (id_venda, id_produto, quantidade, valor_unitario) VALUES (@id_venda, @id_produto, @quantidade, @valor_unitario)";
                            using (var cmdItem = new SqlCommand(sqlItem, connection, transaction))
                            {
                                cmdItem.Parameters.AddWithValue("@id_venda", vendaId);
                                cmdItem.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                                cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                                cmdItem.Parameters.AddWithValue("@valor_unitario", item.ValorUnitario);
                                await cmdItem.ExecuteNonQueryAsync();
                            }

                            var sqlEstoque = "UPDATE produtos SET estoque_atual = estoque_atual - @quantidade WHERE id = @id_produto AND estoque_atual >= @quantidade";
                            using (var cmdEstoque = new SqlCommand(sqlEstoque, connection, transaction))
                            {
                                cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                                cmdEstoque.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                                int linhasAfetadas = await cmdEstoque.ExecuteNonQueryAsync();

                                if (linhasAfetadas == 0)
                                {
                                    throw new Exception($"Estoque insuficiente para o produto '{item.ProdutoNome}'. Venda cancelada.");
                                }
                            }
                        }

                        // 3. Lançar no Movimento de Caixa
                        var sqlCaixa = @"
                            INSERT INTO movimento_caixa
                            (data_movimento, descricao, id_plano_de_contas, tipo, valor, id_venda)
                            VALUES (@data, @descricao, @id_plano, 'E', @valor, @id_venda)";

                        using (var cmdCaixa = new SqlCommand(sqlCaixa, connection, transaction))
                        {
                            cmdCaixa.Parameters.AddWithValue("@data", DateTime.Now);
                            cmdCaixa.Parameters.AddWithValue("@descricao", $"Receita referente à Venda ID {vendaId}");
                            cmdCaixa.Parameters.AddWithValue("@id_plano", ID_PLANO_CONTAS_RECEITA_VENDA);
                            cmdCaixa.Parameters.AddWithValue("@valor", valorTotal);
                            cmdCaixa.Parameters.AddWithValue("@id_venda", vendaId);
                            await cmdCaixa.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("Erro ao registrar a venda: " + ex.Message);
                    }
                }
            }
        }

        // --- MÉTODO ATUALIZADO (Tarefa 13) ---
        /// <summary>
        /// Busca o relatório de Vendas (volume) por período.
        /// USA O MODEL 'RelatorioVenda'
        /// </summary>
        public async Task<List<RelatorioVenda>> GetVendasPorPeriodoAsync(DateTime dataInicial, DateTime dataFinal)
        {
            var lista = new List<RelatorioVenda>(); // MUDANÇA: Usa RelatorioVenda
            using (var connection = Database.GetConnection())
            {
                string sql = @"
                    SELECT 
                        v.data_venda,
                        c.nome AS nome_cliente,
                        v.valor_total
                    FROM vendas v
                    JOIN clientes c ON v.id_cliente = c.id
                    WHERE 
                        v.data_venda BETWEEN @dataInicial AND @dataFinal
                    ORDER BY 
                        v.data_venda DESC";

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
                            lista.Add(new RelatorioVenda // MUDANÇA: Usa RelatorioVenda
                            {
                                DataVenda = reader.GetDateTime(reader.GetOrdinal("data_venda")),
                                NomeCliente = reader.GetString(reader.GetOrdinal("nome_cliente")),
                                ValorTotal = reader.GetDecimal(reader.GetOrdinal("valor_total"))
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar relatório de vendas: " + ex.Message);
                }
            }
            return lista;
        }
    }
}