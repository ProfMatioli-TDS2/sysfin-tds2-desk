using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using SysFin_2CTDS.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq; // Adicionado para usar LINQ

<<<<<<< Updated upstream
namespace SysFin_2CTDS.Controller {
    /// <summary>
    /// Classe responsável pela lógica de negócio e acesso a dados para a entidade Fornecedor.
    /// </summary>
    public class FornecedorController {
=======
namespace SysFin_2CTDS.Controller
{
    /// <summary>
    /// Classe responsável pela lógica de negócio e acesso a dados para a entidade Fornecedor.
    /// </summary>
    public class FornecedorController
    {
>>>>>>> Stashed changes
        /// <summary>
        /// Obtém todos os fornecedores do banco de dados, ordenados por nome.
        /// </summary>
        /// <returns>Uma lista de objetos Fornecedor.</returns>
<<<<<<< Updated upstream
        public List<Fornecedor> GetAll() {
            var fornecedores = new List<Fornecedor>();
            // 'using' garante que a conexão com o banco será fechada automaticamente
            using(var connection = Database.GetConnection()) {
=======
        public List<Fornecedor> GetAll()
        {
            var fornecedores = new List<Fornecedor>();
            // 'using' garante que a conexão com o banco será fechada automaticamente
            using (var connection = Database.GetConnection())
            {
>>>>>>> Stashed changes
                var command = new SqlCommand("SELECT * FROM fornecedores ORDER BY nome", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fornecedores.Add(new Fornecedor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Nome = reader.GetString(reader.GetOrdinal("nome")),
                            Cnpj = reader.GetString(reader.GetOrdinal("cnpj")),
                            // Verifica se o campo no banco é nulo antes de tentar ler
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email")),
                            Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? "" : reader.GetString(reader.GetOrdinal("telefone"))
                        });
                    }
                }
            }
            return fornecedores;
        }

        /// <summary>
        /// Salva um novo fornecedor ou atualiza um existente.
        /// </summary>
        /// <param name="fornecedor">O objeto Fornecedor a ser salvo.</param>
        /// <returns>Uma lista de strings contendo mensagens de erro de validação. Se a lista estiver vazia, a operação foi bem-sucedida.</returns>
<<<<<<< Updated upstream
        public List<string> Save(Fornecedor fornecedor) {
=======
        public List<string> Save(Fornecedor fornecedor)
        {
>>>>>>> Stashed changes
            var errors = new List<string>();

            // Validação do modelo antes de tentar salvar no banco de dados
            var validationContext = new ValidationContext(fornecedor, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(fornecedor, validationContext, validationResults, validateAllProperties: true);

<<<<<<< Updated upstream
            if(!isValid) {
                foreach(var validationResult in validationResults) {
=======
            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
>>>>>>> Stashed changes
                    errors.Add(validationResult.ErrorMessage);
                }
                return errors;
            }

<<<<<<< Updated upstream
            try {
                using(var connection = Database.GetConnection()) {
=======
            try
            {
                using (var connection = Database.GetConnection())
                {
>>>>>>> Stashed changes
                    connection.Open();
                    SqlCommand command;

                    // Se o ID for maior que 0, significa que é uma atualização (UPDATE)
<<<<<<< Updated upstream
                    if(fornecedor.Id > 0) {
=======
                    if (fornecedor.Id > 0)
                    {
>>>>>>> Stashed changes
                        command = new SqlCommand("UPDATE fornecedores SET nome = @nome, cnpj = @cnpj, email = @email, telefone = @telefone WHERE id = @id", connection);
                        command.Parameters.AddWithValue("@id", fornecedor.Id);
                    }
                    // Caso contrário, é uma inserção (INSERT)
<<<<<<< Updated upstream
                    else {
=======
                    else
                    {
>>>>>>> Stashed changes
                        command = new SqlCommand("INSERT INTO fornecedores (nome, cnpj, email, telefone) VALUES (@nome, @cnpj, @email, @telefone)", connection);
                    }

                    // Adiciona os parâmetros para evitar SQL Injection
                    command.Parameters.AddWithValue("@nome", fornecedor.Nome);
                    command.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                    command.Parameters.AddWithValue("@email", fornecedor.Email);
                    command.Parameters.AddWithValue("@telefone", fornecedor.Telefone);

                    // ExecuteNonQuery retorna o número de linhas afetadas
<<<<<<< Updated upstream
                    if(command.ExecuteNonQuery() <= 0) {
                        errors.Add("Falha ao salvar o fornecedor no banco de dados.");
                    }
                }
            } catch(SqlException ex) {
                errors.Add($"Erro de banco de dados ao salvar fornecedor: {ex.Message}");
            } catch(System.Exception ex) {
=======
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        errors.Add("Falha ao salvar o fornecedor no banco de dados.");
                    }
                }
            }
            catch (SqlException ex)
            {
                errors.Add($"Erro de banco de dados ao salvar fornecedor: {ex.Message}");
            }
            catch (System.Exception ex)
            {
>>>>>>> Stashed changes
                errors.Add($"Ocorreu um erro inesperado ao salvar fornecedor: {ex.Message}");
            }

            return errors;
        }

        /// <summary>
        /// Exclui um fornecedor do banco de dados com base no seu ID.
        /// </summary>
        /// <param name="id">O ID do fornecedor a ser excluído.</param>
        /// <returns>Verdadeiro se a exclusão foi bem-sucedida, falso caso contrário.</returns>
<<<<<<< Updated upstream
        public bool Delete(int id) {
            try {
                using(var connection = Database.GetConnection()) {
=======
        public bool Delete(int id)
        {
            try
            {
                using (var connection = Database.GetConnection())
                {
>>>>>>> Stashed changes
                    var command = new SqlCommand("DELETE FROM fornecedores WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
<<<<<<< Updated upstream
            } catch(SqlException ex) {
                System.Console.WriteLine($"Erro de banco de dados ao excluir fornecedor: {ex.Message}");
                return false;
            } catch(System.Exception ex) {
=======
            }
            catch (SqlException ex)
            {
                System.Console.WriteLine($"Erro de banco de dados ao excluir fornecedor: {ex.Message}");
                return false;
            }
            catch (System.Exception ex)
            {
>>>>>>> Stashed changes
                System.Console.WriteLine($"Ocorreu um erro inesperado ao excluir fornecedor: {ex.Message}");
                return false;
            }
        }
    }
}