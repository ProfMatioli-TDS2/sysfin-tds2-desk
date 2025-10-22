using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

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
                connection.Open();
                var query = $"SELECT * FROM Fornecedores ORDER BY {orderBy} {direction}";
                var command = new SqlCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
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

        public List<string> Save(Fornecedor fornecedor)
        {
            var errors = new List<string>();

            var validationContext = new ValidationContext(fornecedor, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(fornecedor, validationContext, validationResults, validateAllProperties: true);
            if (!fornecedor.CnpjValido())
            {
                errors.Add("O CNPJ informado é inválido.");
            }

            if (!string.IsNullOrWhiteSpace(fornecedor.Email) && !new EmailAddressAttribute().IsValid(fornecedor.Email))
            {
                errors.Add("O e-mail informado não é válido.");
            }

            var telefoneNumerico = new string((fornecedor.Telefone ?? "").Where(char.IsDigit).ToArray());
            if (telefoneNumerico.Length < 10 || telefoneNumerico.Length > 11)
            {
                errors.Add("O telefone deve conter entre 10 e 11 dígitos numéricos.");
            }



            if (!isValid || errors.Any())
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
                        command = new SqlCommand("UPDATE Fornecedores SET Nome = @Nome, Cnpj = @Cnpj, Email = @Email, Telefone = @Telefone WHERE Id = @Id", connection);
                        command.Parameters.AddWithValue("@Id", fornecedor.Id);
                    }
                    else
                    {
                        command = new SqlCommand("INSERT INTO Fornecedores (Nome, Cnpj, Email, Telefone) VALUES (@Nome, @Cnpj, @Email, @Telefone)", connection);
                    }

                    command.Parameters.AddWithValue("@Nome", fornecedor.Nome ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Cnpj", fornecedor.Cnpj ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", fornecedor.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefone", fornecedor.Telefone ?? (object)DBNull.Value);

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
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM Fornecedores WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
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



        public string GerarRelatorioPDF(string caminho)
        {
            var fornecedores = GetAll("nome", "ASC");

            Document doc = new Document(PageSize.A4);
            PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
            doc.Open();

            var fonteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var fonteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
            var fonteCorpo = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            // Título centralizado
            Paragraph titulo = new Paragraph("Relatório de Fornecedores", fonteTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);

            doc.Add(new Paragraph("\n")); // Espaço entre título e tabela

            PdfPTable tabela = new PdfPTable(5);
            tabela.WidthPercentage = 100;
            tabela.SpacingBefore = 10f;
            tabela.SpacingAfter = 10f;
            tabela.SetWidths(new float[] { 10, 25, 25, 25, 15 });

            // Cabeçalhos
            tabela.AddCell(new PdfPCell(new Phrase("ID", fonteSubtitulo)));
            tabela.AddCell(new PdfPCell(new Phrase("Nome", fonteSubtitulo)));
            tabela.AddCell(new PdfPCell(new Phrase("CNPJ", fonteSubtitulo)));
            tabela.AddCell(new PdfPCell(new Phrase("E-mail", fonteSubtitulo)));
            tabela.AddCell(new PdfPCell(new Phrase("Telefone", fonteSubtitulo)));

            foreach (var f in fornecedores)
            {
                string cnpjFormatado = string.IsNullOrWhiteSpace(f.Cnpj) || f.Cnpj.Length != 14
                    ? f.Cnpj ?? ""
                    : Convert.ToUInt64(f.Cnpj).ToString(@"00\.000\.000\/0000\-00");

                string telefoneFormatado = string.IsNullOrWhiteSpace(f.Telefone)
                    ? ""
                    : f.Telefone.Length == 11
                        ? Convert.ToUInt64(f.Telefone).ToString(@"(00) 00000\-0000")
                        : Convert.ToUInt64(f.Telefone).ToString(@"(00) 0000\-0000");

                tabela.AddCell(new PdfPCell(new Phrase(f.Id.ToString(), fonteCorpo)));
                tabela.AddCell(new PdfPCell(new Phrase(f.Nome ?? "", fonteCorpo)));
                tabela.AddCell(new PdfPCell(new Phrase(cnpjFormatado, fonteCorpo)));
                tabela.AddCell(new PdfPCell(new Phrase(f.Email ?? "", fonteCorpo)));
                tabela.AddCell(new PdfPCell(new Phrase(telefoneFormatado, fonteCorpo)));
            }

            doc.Add(tabela);

            // Rodapé com data de geração, alinhado à direita
            Paragraph rodape = new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fonteCorpo);
            rodape.Alignment = Element.ALIGN_RIGHT;
            doc.Add(new Paragraph("\n")); // Espaço extra
            doc.Add(rodape);

            doc.Close();

            return caminho;
        }



    }
}
