using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model.Data;
using SysFin_2CTDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // Adicionado

namespace SysFin_2CTDS.Controller
{
    public class ClienteController
    {
        // MUDANÇA: 'List<Cliente>' alterado para 'Task<List<Cliente>>'
        public async Task<List<Cliente>> GetAllAsync()
        {
            var clientes = new List<Cliente>();
            // 'using' garante que a conexão com o banco será fechada automaticamente
            using (var connection = Database.GetConnection())
            {
                // MUDANÇA: 'SELECT *' removido
                var command = new SqlCommand("SELECT id, nome, cpf_cnpj, email, telefone FROM clientes ORDER BY nome", connection);

                try
                {
                    // MUDANÇA: Chamadas Async
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            clientes.Add(new Cliente
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.IsDBNull(reader.GetOrdinal("nome")) ? null : reader.GetString(reader.GetOrdinal("nome")),
                                CpfCnpj = reader.IsDBNull(reader.GetOrdinal("cpf_cnpj")) ? null : reader.GetString(reader.GetOrdinal("cpf_cnpj")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString(reader.GetOrdinal("telefone"))
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // "Lança" a exceção para a View (Form) tratar
                    throw new Exception("Erro ao buscar clientes no banco de dados: " + ex.Message);
                }
            }
            return clientes;
        }

        // MUDANÇA: 'bool' alterado para 'Task<bool>'
        public async Task<bool> SaveAsync(Cliente cliente)
        {
            using (var connection = Database.GetConnection())
            {
                SqlCommand command;

                if (cliente.Id > 0)
                {
                    command = new SqlCommand("UPDATE clientes SET nome = @nome, cpf_cnpj = @cpf_cnpj, email = @email, telefone = @telefone WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", cliente.Id);
                }
                else
                {
                    command = new SqlCommand("INSERT INTO clientes (nome, cpf_cnpj, email, telefone) VALUES (@nome, @cpf_cnpj, @email, @telefone)", connection);
                }

                // MUDANÇA: Tratamento de Nulos
                command.Parameters.AddWithValue("@nome", (object)cliente.Nome ?? DBNull.Value);
                command.Parameters.AddWithValue("@cpf_cnpj", (object)cliente.CpfCnpj ?? DBNull.Value);
                command.Parameters.AddWithValue("@email", (object)cliente.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@telefone", (object)cliente.Telefone ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    // MUDANÇA: 'ExecuteNonQueryAsync' agora retorna o número de linhas
                    int linhasAfetadas = await command.ExecuteNonQueryAsync();
                    return linhasAfetadas > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao salvar cliente: " + ex.Message);
                }
            }
        }

        // MUDANÇA: 'bool' alterado para 'Task<bool>'
        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = Database.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM clientes WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    await connection.OpenAsync();
                    // MUDANÇA: 'ExecuteNonQueryAsync' agora retorna o número de linhas
                    int linhasAfetadas = await command.ExecuteNonQueryAsync();
                    return linhasAfetadas > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao excluir cliente: " + ex.Message);
                }
            }
        }
    }
}

