using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.IO;
using System.Threading.Tasks; // MUDANÇA: Adicionado

namespace SysFin_2CTDS.Controller
{
    public class FornecedorController
    {
        // MUDANÇA: Assinatura agora é async Task<List<Fornecedor>>
        public async Task<List<Fornecedor>> GetAllAsync(string orderBy = "nome", string direction = "ASC")
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
                await connection.OpenAsync(); // MUDANÇA: Async
                // MUDANÇA: SELECT * removido
                var query = $"SELECT Id, Nome, Cnpj, Email, Telefone FROM Fornecedores ORDER BY {orderBy} {direction}";
                var command = new SqlCommand(query, connection);

                using (var reader = await command.ExecuteReaderAsync()) // MUDANÇA: Async
                {
                    while (await reader.ReadAsync()) // MUDANÇA: Async
                    {
                        fornecedores.Add(new Fornecedor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Cnpj = reader.GetString(reader.GetOrdinal("Cnpj")),
                            Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                            Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString(reader.GetOrdinal("Telefone"))
                        });
                    }
                }
            }
            return fornecedores;
        }

        // MUDANÇA: Assinatura agora é async Task<List<string>>
        public async Task<List<string>> SaveAsync(Fornecedor fornecedor)
        {
            var errors = new List<string>();

            var validationContext = new ValidationContext(fornecedor, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(fornecedor, validationContext, validationResults, validateAllProperties: true);

            // MUDANÇA: Validação do CNPJ agora permite nulo
            if (fornecedor.Cnpj != null && !fornecedor.CnpjValido())
            {
                errors.Add("O CNPJ informado é inválido.");
            }

            if (string.IsNullOrWhiteSpace(fornecedor.Email))
            {
                errors.Add("O campo de e-mail é obrigatório.");
            }
            else if (!new EmailAddressAttribute().IsValid(fornecedor.Email))
            {
                errors.Add("O e-mail informado não é válido.");
            }

            // MUDANÇA: Validação de telefone agora permite nulo
            if (!string.IsNullOrEmpty(fornecedor.Telefone))
            {
                var telefoneNumerico = new string(fornecedor.Telefone.Where(char.IsDigit).ToArray());
                if (telefoneNumerico.Length < 10 || telefoneNumerico.Length > 11)
                {
                    errors.Add("O telefone deve conter entre 10 e 11 dígitos numéricos.");
                }
            }

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    // MUDANÇA: Checagem de nulidade
                    if (validationResult.ErrorMessage != null)
                        errors.Add(validationResult.ErrorMessage);
                }
            }

            if (errors.Any())
            {
                return errors; // Retorna erros de validação antes de ir ao banco
            }

            try
            {
                using (var connection = Database.GetConnection())
                {
                    await connection.OpenAsync(); // MUDANÇA: Async
                    SqlCommand command;

                    if (fornecedor.Id > 0)
                    {
                        command = new SqlCommand("UPDATE Fornecedores SET Nome = @Nome, Cnpj = @Cnpj, Email = @Email, Telefone = @Telefone WHERE Id = @Id", connection);
                        command.Parameters.AddWithValue("@Id", fornecedor.Id);
                    }
                    else
                    {
                        command = new SqlCommand("INSERT INTO Fornecedores (Nome, Cnpj, Email, Telefone) VALUES (@Nome, @Cnpj, @Email, @Telefone)", connection);
                    }

                    command.Parameters.AddWithValue("@Nome", (object)fornecedor.Nome ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Cnpj", (object)fornecedor.Cnpj ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Email", (object)fornecedor.Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Telefone", (object)fornecedor.Telefone ?? DBNull.Value);

                    if (await command.ExecuteNonQueryAsync() <= 0) // MUDANÇA: Async
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

        // MUDANÇA: Assinatura agora é async Task<bool>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var connection = Database.GetConnection())
                {
                    await connection.OpenAsync(); // MUDANÇA: Async
                    var command = new SqlCommand("DELETE FROM Fornecedores WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    int rows = await command.ExecuteNonQueryAsync(); // MUDANÇA: Async
                    return rows > 0;
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

