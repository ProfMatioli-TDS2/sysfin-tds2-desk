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
        public async Task<List<PlanoDeContas>> GetAllAsync()
        {
            var lista = new List<PlanoDeContas>();
            using (var connection = Database.GetConnection())
            {
                var sql = "SELECT id, descricao, tipo FROM plano_de_contas ORDER BY tipo, descricao";
                var command = new SqlCommand(sql, connection);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new PlanoDeContas
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString(reader.GetOrdinal("descricao")),
                                Tipo = reader.GetString(reader.GetOrdinal("tipo"))[0] // Pega o primeiro char
                            });
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

        public async Task<bool> SaveAsync(PlanoDeContas conta)
        {
            if (string.IsNullOrWhiteSpace(conta.Descricao))
            {
                throw new Exception("A Descrição é obrigatória.");
            }
            if (conta.Tipo != 'R' && conta.Tipo != 'D')
            {
                throw new Exception("O Tipo deve ser 'R' (Receita) ou 'D' (Despesa).");
            }

            using (var connection = Database.GetConnection())
            {
                SqlCommand command;
                if (conta.Id > 0)
                {
                    // Update
                    command = new SqlCommand("UPDATE plano_de_contas SET descricao = @descricao, tipo = @tipo WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", conta.Id);
                }
                else
                {
                    // Insert
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

        public async Task<bool> DeleteAsync(int id)
        {
            // Proteção: Não permite excluir as contas padrão (ID 1 e 2)
            if (id == 1 || id == 2)
            {
                throw new Exception("Não é permitido excluir as contas padrão (Receita de Vendas / Compra de Mercadorias).");
            }

            using (var connection = Database.GetConnection())
            {
                // Proteção: Verifica se a conta está em uso no movimento_caixa
                var sqlCheck = "SELECT COUNT(*) FROM movimento_caixa WHERE id_plano_de_contas = @id";
                var cmdCheck = new SqlCommand(sqlCheck, connection);
                cmdCheck.Parameters.AddWithValue("@id", id);

                try
                {
                    await connection.OpenAsync();
                    int count = (int)await cmdCheck.ExecuteScalarAsync();
                    if (count > 0)
                    {
                        throw new Exception("Não é possível excluir esta conta, pois ela já está sendo usada em lançamentos de caixa.");
                    }

                    // Se não estiver em uso, exclui
                    var sqlDelete = "DELETE FROM plano_de_contas WHERE id = @id";
                    var cmdDelete = new SqlCommand(sqlDelete, connection);
                    cmdDelete.Parameters.AddWithValue("@id", id);

                    int linhasAfetadas = await cmdDelete.ExecuteNonQueryAsync();
                    return linhasAfetadas > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao excluir conta: " + ex.Message);
                }
            }
        }
    }
}