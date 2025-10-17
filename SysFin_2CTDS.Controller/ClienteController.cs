using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using SysFin_2CTDS.Models;
using System; // Adicionado para Convert.ToInt32

namespace SysFin_2CTDS.Controller
{
    /// <summary>
    /// Classe responsável pela lógica de negócio e acesso a dados para a entidade Cliente.
    /// </summary>
    public class ClienteController
    {
        /// <summary>
        /// Obtém todos os clientes do banco de dados, podendo filtrar por nome.
        /// </summary>
        /// <param name="filtroNome">O nome (ou parte dele) para filtrar. Se nulo ou vazio, retorna todos.</param>
        /// <returns>Uma lista de objetos Cliente.</returns>
        public List<Cliente> GetAll(string filtroNome = null)
        {
            var clientes = new List<Cliente>();
            // 'using' garante que a conexão com o banco será fechada automaticamente
            using (var connection = Database.GetConnection())
            {
                // SQL inicial
                string sql = "SELECT * FROM clientes";

                // Se o filtroNome NÃO for nulo ou vazio, adicionamos a cláusula WHERE
                if (!string.IsNullOrWhiteSpace(filtroNome))
                {
                    sql += " WHERE nome LIKE @nome";
                }

                sql += " ORDER BY nome"; // Adiciona a ordenação no final

                var command = new SqlCommand(sql, connection);

                // Adiciona o parâmetro SOMENTE se ele for usado
                if (!string.IsNullOrWhiteSpace(filtroNome))
                {
                    // O '%' é um curinga. Significa "qualquer texto antes ou depois"
                    command.Parameters.AddWithValue("@nome", "%" + filtroNome + "%");
                }

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
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
        public bool Save(Cliente cliente)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command;

                // Se o ID for maior que 0, significa que é uma atualização (UPDATE)
                if (cliente.Id > 0)
                {
                    command = new SqlCommand("UPDATE clientes SET nome = @nome, cpf_cnpj = @cpf_cnpj, email = @email, telefone = @telefone WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", cliente.Id);
                }
                // Caso contrário, é uma inserção (INSERT)
                else
                {
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
        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM clientes WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Verifica se um CPF/CNPJ já existe no banco de dados, ignorando um ID de cliente específico.
        /// </summary>
        /// <param name="cpfCnpj">O CPF/CNPJ a ser verificado.</param>
        /// <param name="currentId">O ID do cliente atual (usado para ignorá-lo na busca, permitindo a edição).</param>
        /// <returns>Verdadeiro se o CPF/CNPJ já existir para outro cliente.</returns>
        public bool CpfCnpjExists(string cpfCnpj, int currentId)
        {
            using (var connection = Database.GetConnection())
            {
                // A consulta verifica se existe algum cliente com o mesmo CPF/CNPJ,
                // mas EXCLUI o cliente que tem o ID atual (para permitir a edição do mesmo cliente sem acusar duplicidade)
                var command = new SqlCommand("SELECT COUNT(1) FROM clientes WHERE cpf_cnpj = @cpf_cnpj AND id <> @id", connection);
                command.Parameters.AddWithValue("@cpf_cnpj", cpfCnpj);
                command.Parameters.AddWithValue("@id", currentId); // Se currentId for 0 (novo cliente), o 'AND' não vai excluir ninguém relevante

                connection.Open();

                // ExecuteScalar é ideal para consultas que retornam um único valor (como um COUNT)
                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
        }
    }
}

