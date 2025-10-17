using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using SysFin_2CTDS.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SysFin_2CTDS.Controller
{
    public class FornecedorController
    {
        public List<Fornecedor> GetAll(string orderBy = "nome", string direction = "ASC")
        {
            var fornecedores = new List<Fornecedor>();
            var allowedColumns = new List<string> { "id", "nome", "cnpj", "email", "telefone" };
            var allowedDirections = new List<string> { "ASC", "DESC" };

            if (!allowedColumns.Contains(orderBy.ToLower()))
            {
                orderBy = "nome";
            }
            if (!allowedDirections.Contains(direction.ToUpper()))
            {
                direction = "ASC";
            }

            using (var connection = Database.GetConnection())
            {
                var query = $"SELECT * FROM fornecedores ORDER BY {orderBy} {direction}";
                var command = new SqlCommand(query, connection);
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
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email")),
                            Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? "" : reader.GetString(reader.GetOrdinal("telefone"))
                        });
                    }
                }
            }
            return fornecedores;
        }

        public List<string> Save(Fornecedor fornecedor)
        {
            var errors = new List<string>();

            var validationContext = new ValidationContext(fornecedor, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(fornecedor, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    errors.Add(validationResult.ErrorMessage);
                }
                return errors;
            }

            try
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    SqlCommand command;

                    if (fornecedor.Id > 0)
                    {
                        command = new SqlCommand("UPDATE fornecedores SET nome = @nome, cnpj = @cnpj, email = @email, telefone = @telefone WHERE id = @id", connection);
                        command.Parameters.AddWithValue("@id", fornecedor.Id);
                    }
                    else
                    {
                        command = new SqlCommand("INSERT INTO fornecedores (nome, cnpj, email, telefone) VALUES (@nome, @cnpj, @email, @telefone)", connection);
                    }

                    command.Parameters.AddWithValue("@nome", fornecedor.Nome);
                    command.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                    command.Parameters.AddWithValue("@email", fornecedor.Email);
                    command.Parameters.AddWithValue("@telefone", fornecedor.Telefone);

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
                errors.Add($"Ocorreu um erro inesperado ao salvar fornecedor: {ex.Message}");
            }

            return errors;
        }

        public bool Delete(int id)
        {
            try
            {
                using (var connection = Database.GetConnection())
                {
                    var command = new SqlCommand("DELETE FROM fornecedores WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                System.Console.WriteLine($"Erro de banco de dados ao excluir fornecedor: {ex.Message}");
                return false;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Ocorreu um erro inesperado ao excluir fornecedor: {ex.Message}");
                return false;
            }
        }
    }
}
