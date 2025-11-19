using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model.Data;
using SysFin_2CTDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions; // Para Validação
using iTextSharp.text; // Para Relatório PDF
using iTextSharp.text.pdf; // Para Relatório PDF
using System.IO; // Para Relatório PDF
using System.Linq; // Para Relatório PDF

namespace SysFin_2CTDS.Controller
{
    public class ClienteController
    {
        // --- MÉTODOS DE ACESSO A DADOS (ASYNC) ---

        /// <summary>
        /// Busca todos os clientes, opcionalmente filtrando por nome.
        /// </summary>
        public async Task<List<Cliente>> GetAllAsync(string filtroNome)
        {
            var clientes = new List<Cliente>();
            using (var connection = Database.GetConnection())
            {
                // Query dinâmica para adicionar o filtro
                string sql = "SELECT id, nome, cpf_cnpj, email, telefone FROM clientes";

                if (!string.IsNullOrWhiteSpace(filtroNome))
                {
                    sql += " WHERE nome LIKE @filtroNome";
                }
                sql += " ORDER BY nome";

                var command = new SqlCommand(sql, connection);

                if (!string.IsNullOrWhiteSpace(filtroNome))
                {
                    command.Parameters.AddWithValue("@filtroNome", $"%{filtroNome}%");
                }

                try
                {
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
                    throw new Exception("Erro ao buscar clientes: " + ex.Message);
                }
            }
            return clientes;
        }

        /// <summary>
        /// Busca clientes pelo nome exato.
        /// </summary>
        public async Task<List<Cliente>> GetByExactNameAsync(string nomeExato)
        {
            var clientes = new List<Cliente>();
            using (var connection = Database.GetConnection())
            {
                string sql = "SELECT id, nome, cpf_cnpj, email, telefone FROM clientes WHERE nome = @nomeExato ORDER BY nome";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@nomeExato", nomeExato);

                try
                {
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
                    throw new Exception("Erro ao buscar cliente por nome: " + ex.Message);
                }
            }
            return clientes;
        }

        /// <summary>
        /// Verifica se um CPF/CNPJ já existe, ignorando o ID do cliente atual (em caso de edição).
        /// </summary>
        public async Task<bool> CpfCnpjExistsAsync(string cpfCnpj, int idClienteAtual)
        {
            using (var connection = Database.GetConnection())
            {
                // Verifica se existe outro cliente (ID != idClienteAtual) com o mesmo CPF/CNPJ
                var sql = "SELECT COUNT(*) FROM clientes WHERE cpf_cnpj = @cpfCnpj AND id != @idClienteAtual";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cpfCnpj", cpfCnpj);
                command.Parameters.AddWithValue("@idClienteAtual", idClienteAtual);

                try
                {
                    await connection.OpenAsync();
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao verificar duplicidade de CPF/CNPJ: " + ex.Message);
                }
            }
        }


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

                command.Parameters.AddWithValue("@nome", (object)cliente.Nome ?? DBNull.Value);
                command.Parameters.AddWithValue("@cpf_cnpj", (object)cliente.CpfCnpj ?? DBNull.Value);
                command.Parameters.AddWithValue("@email", (object)cliente.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@telefone", (object)cliente.Telefone ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    int linhasAfetadas = await command.ExecuteNonQueryAsync();
                    return linhasAfetadas > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao salvar cliente: " + ex.Message);
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = Database.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM clientes WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    await connection.OpenAsync();
                    int linhasAfetadas = await command.ExecuteNonQueryAsync();
                    return linhasAfetadas > 0;
                }
                catch (SqlException ex)
                {
                    // Tratamento de erro de chave estrangeira (FK)
                    if (ex.Number == 547) // Código de erro SQL Server para conflito de FK
                    {
                        throw new Exception("Não é possível excluir este cliente, pois ele já está vinculado a uma ou mais Vendas.");
                    }
                    throw new Exception("Erro ao excluir cliente: " + ex.Message);
                }
            }
        }

        // --- MÉTODOS DE VALIDAÇÃO (Como o seu Form espera) ---

        public static bool IsValidCpfCnpj(string? cpfCnpj)
        {
            if (string.IsNullOrWhiteSpace(cpfCnpj))
                return false;

            // O seu MaskedTextBox já remove os literais, então só verificamos o tamanho
            return cpfCnpj.Length == 11 || cpfCnpj.Length == 14;
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true; // E-mail é opcional

            // Regex simples para validação de email
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidTelefone(string? telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return true; // Telefone é opcional

            // O seu MaskedTextBox já remove os literais
            return telefone.Length == 10 || telefone.Length == 11;
        }

        // --- GERAÇÃO DE RELATÓRIO (Movido do RelatorioController) ---

        public async Task<bool> GerarRelatorioPDFAsync(List<Cliente> clientes, string caminho)
        {
            // O Task.Run() move o processamento pesado (geração de PDF) 
            // para uma thread separada, mantendo a UI responsiva.
            return await Task.Run(() =>
            {
                try
                {
                    Document doc = new Document(PageSize.A4.Rotate()); // Página deitada
                    PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
                    doc.Open();

                    var fonteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                    var fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                    var fonteCorpo = FontFactory.GetFont(FontFactory.HELVETICA, 9);

                    Paragraph titulo = new Paragraph("Relatório de Clientes", fonteTitulo)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    doc.Add(titulo);

                    PdfPTable tabela = new PdfPTable(5); // 5 Colunas
                    tabela.WidthPercentage = 100;
                    tabela.SetWidths(new float[] { 0.8f, 2.5f, 1.5f, 2.5f, 1.5f }); // Larguras relativas

                    // Cabeçalhos
                    tabela.AddCell(new PdfPCell(new Phrase("ID", fonteCabecalho)));
                    tabela.AddCell(new PdfPCell(new Phrase("Nome", fonteCabecalho)));
                    tabela.AddCell(new PdfPCell(new Phrase("CPF/CNPJ", fonteCabecalho)));
                    tabela.AddCell(new PdfPCell(new Phrase("E-mail", fonteCabecalho)));
                    tabela.AddCell(new PdfPCell(new Phrase("Telefone", fonteCabecalho)));

                    // Dados
                    foreach (var cliente in clientes)
                    {
                        tabela.AddCell(new PdfPCell(new Phrase(cliente.Id.ToString(), fonteCorpo)));
                        tabela.AddCell(new PdfPCell(new Phrase(cliente.Nome ?? "", fonteCorpo)));
                        tabela.AddCell(new PdfPCell(new Phrase(FormatarCpfCnpj(cliente.CpfCnpj), fonteCorpo)));
                        tabela.AddCell(new PdfPCell(new Phrase(cliente.Email ?? "", fonteCorpo)));
                        tabela.AddCell(new PdfPCell(new Phrase(FormatarTelefone(cliente.Telefone), fonteCorpo)));
                    }

                    doc.Add(tabela);

                    // Rodapé
                    Paragraph rodape = new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Total de Clientes: {clientes.Count}", fonteCorpo)
                    {
                        Alignment = Element.ALIGN_RIGHT,
                        SpacingBefore = 10
                    };
                    doc.Add(rodape);

                    doc.Close();
                    return true;
                }
                catch (Exception)
                {
                    // Retorna false se falhar (ex: arquivo bloqueado)
                    return false;
                }
            });
        }

        // Métodos auxiliares privados para o relatório
        private static string FormatarCpfCnpj(string? numeros)
        {
            if (string.IsNullOrEmpty(numeros)) return "";
            if (numeros.Length == 11)
                return Convert.ToUInt64(numeros).ToString(@"000\.000\.000\-00");
            if (numeros.Length == 14)
                return Convert.ToUInt64(numeros).ToString(@"00\.000\.000\/0000\-00");
            return numeros; // Retorna sem formatar se o tamanho for inválido
        }

        private static string FormatarTelefone(string? numeros)
        {
            if (string.IsNullOrEmpty(numeros)) return "";
            if (numeros.Length == 10)
                return Convert.ToUInt64(numeros).ToString(@"(00) 0000\-0000");
            if (numeros.Length == 11)
                return Convert.ToUInt64(numeros).ToString(@"(00) 00000\-0000");
            return numeros;
        }
    }
}