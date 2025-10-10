using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using SysFin_2CTDS.Models;

namespace SysFin_2CTDS.Controller {
    /// <summary>
    /// Classe responsável pela lógica de negócio e acesso a dados para a entidade Cliente.
    /// </summary>
    public class ClienteController {
        /// <summary>
        /// Obtém todos os clientes do banco de dados, ordenados por nome.
        /// </summary>
        /// <returns>Uma lista de objetos Cliente.</returns>
        public List<Cliente> GetAll() {
            var clientes = new List<Cliente>();
            // 'using' garante que a conexão com o banco será fechada automaticamente
            using(var connection = Database.GetConnection()) {
                var command = new SqlCommand("SELECT * FROM clientes ORDER BY nome", connection);
                connection.Open();
                using(var reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        clientes.Add(new Cliente {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Nome = reader.GetString(reader.GetOrdinal("nome")),
                            CpfCnpj = reader.GetString(reader.GetOrdinal("cpf_cnpj")),
                            // Verifica se o campo no banco é nulo antes de tentar ler
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email")),
                            Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? "" : reader.GetString(reader.GetOrdinal("telefone"))
                        });
                    }
                }
            }
            return clientes;
        }

        /// <summary>
        /// Salva um novo cliente ou atualiza um existente.
        /// </summary>
        /// <param name="cliente">O objeto Cliente a ser salvo.</param>
        /// <returns>Verdadeiro se a operação foi bem-sucedida, falso caso contrário.</returns>
        public bool Save(Cliente cliente) {
            using(var connection = Database.GetConnection()) {
                connection.Open();
                SqlCommand command;

                // Se o ID for maior que 0, significa que é uma atualização (UPDATE)
                if(cliente.Id > 0) {
                    command = new SqlCommand("UPDATE clientes SET nome = @nome, cpf_cnpj = @cpf_cnpj, email = @email, telefone = @telefone WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", cliente.Id);
                }
                // Caso contrário, é uma inserção (INSERT)
                else {
                    command = new SqlCommand("INSERT INTO clientes (nome, cpf_cnpj, email, telefone) VALUES (@nome, @cpf_cnpj, @email, @telefone)", connection);
                }

                // Adiciona os parâmetros para evitar SQL Injection
                command.Parameters.AddWithValue("@nome", cliente.Nome);
                command.Parameters.AddWithValue("@cpf_cnpj", cliente.CpfCnpj);
                command.Parameters.AddWithValue("@email", cliente.Email);
                command.Parameters.AddWithValue("@telefone", cliente.Telefone);

                // ExecuteNonQuery retorna o número de linhas afetadas
                return command.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Exclui um cliente do banco de dados com base no seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente a ser excluído.</param>
        /// <returns>Verdadeiro se a exclusão foi bem-sucedida, falso caso contrário.</returns>
        public bool Delete(int id) {
            using(var connection = Database.GetConnection()) {
                var command = new SqlCommand("DELETE FROM clientes WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}

