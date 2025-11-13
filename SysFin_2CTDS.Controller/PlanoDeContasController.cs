using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Controller
{
    public class PlanoDeContasController
    {
        /// <summary>
        /// Busca todas as contas (Receita e Despesa).
        /// </summary>
        public async Task<List<PlanoDeContas>> GetAllAsync()
        {
            var lista = new List<PlanoDeContas>();
            using (var connection = Database.GetConnection())
            {
                var sql = "SELECT id, descricao, tipo FROM plano_de_contas ORDER BY descricao";
                var command = new SqlCommand(sql, connection);
                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(MapearPlano(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar plano de contas: " + ex.Message);
                }
            }
            return lista;
        }

        /// <summary>
        /// MUDANÇA (Passo 55): Busca contas filtrando por tipo (R ou D).
        /// </summary>
        public async Task<List<PlanoDeContas>> GetPorTipoAsync(char tipo)
        {
            var lista = new List<PlanoDeContas>();
            using (var connection = Database.GetConnection())
            {
                var sql = "SELECT id, descricao, tipo FROM plano_de_contas WHERE tipo = @tipo ORDER BY descricao";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@tipo", tipo);
                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(MapearPlano(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar plano de contas por tipo: " + ex.Message);
                }
            }
            return lista;
        }


        /// <summary>
        /// Salva (Insere ou Atualiza) uma conta.
        /// </summary>
        public async Task<bool> SaveAsync(PlanoDeContas conta)
        {
            if (string.IsNullOrWhiteSpace(conta.Descricao))
            {
                throw new Exception("A Descrição é obrigatória.");
            }
            if (conta.Tipo != 'R' && conta.Tipo != 'D')
            {
                throw new Exception("O Tipo é inválido (deve ser 'R' ou 'D').");
            }

            using (var connection = Database.GetConnection())
            {
                SqlCommand command;
                if (conta.Id > 0)
                {
                    command = new SqlCommand("UPDATE plano_de_contas SET descricao = @descricao, tipo = @tipo WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", conta.Id);
                }
                else
                {
                    command = new SqlCommand("INSERT INTO plano_de_contas (descricao, tipo) VALUES (@descricao, @tipo)", connection);
                }

                command.Parameters.AddWithValue("@descricao", conta.Descricao);
                command.Parameters.AddWithValue("@tipo", conta.Tipo);

                try
                {
                    await connection.OpenAsync();
                    int linhasAfetadas = await command.ExecuteNonQueryAsync();
                    return linhasAfetadas > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao salvar conta: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Exclui uma conta.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = Database.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM plano_de_contas WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    await connection.OpenAsync();
                    int linhasAfetadas = await command.ExecuteNonQueryAsync();
                    return linhasAfetadas > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // Conflito de FK
                    {
                        throw new Exception("Não é possível excluir esta conta, pois ela já está sendo usada em lançamentos do Caixa.");
                    }
                    throw new Exception("Erro ao excluir conta: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Mapeador auxiliar.
        /// </summary>
        private PlanoDeContas MapearPlano(SqlDataReader reader)
        {
            return new PlanoDeContas
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Descricao = reader.GetString(reader.GetOrdinal("descricao")),
                Tipo = reader.GetString(reader.GetOrdinal("tipo"))[0]
            };
        }
    }
}