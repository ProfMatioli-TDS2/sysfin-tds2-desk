using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using SysFin_2CTDS.Models;
using System;
using System.IO; // Necessário para FileStream
using iTextSharp.text; // Necessário para Document, Paragraph, etc.
using iTextSharp.text.pdf; // Necessário para PdfWriter e PdfPTable

namespace SysFin_2CTDS.Controller
{
    /// <summary>
    /// Classe responsável pela lógica de negócio e acesso a dados para a entidade Cliente.
    /// </summary>
    public class ClienteController
    {
        /// <summary>
        /// Obtém todos os clientes do banco de dados, podendo filtrar por nome (busca parcial 'LIKE').
        /// </summary>
        public List<Cliente> GetAll(string filtroNome = null)
        {
            var clientes = new List<Cliente>();
            using (var connection = Database.GetConnection())
            {
                string sql = "SELECT * FROM clientes";
                if (!string.IsNullOrWhiteSpace(filtroNome))
                {
                    sql += " WHERE nome LIKE @nome";
                }
                sql += " ORDER BY nome";

                var command = new SqlCommand(sql, connection);

                if (!string.IsNullOrWhiteSpace(filtroNome))
                {
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
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email")),
                            Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? "" : reader.GetString(reader.GetOrdinal("telefone"))
                        });
                    }
                }
            }
            return clientes;
        }

        /// <summary>
        /// Obtém clientes que correspondem EXATAMENTE ao nome fornecido, ignorando maiúsculas/minúsculas.
        /// </summary>
        public List<Cliente> GetByExactName(string nome)
        {
            var clientes = new List<Cliente>();
            using (var connection = Database.GetConnection())
            {
                // Usamos UPPER() em ambos os lados para fazer uma busca exata, mas sem diferenciar maiúsculas/minúsculas
                string sql = "SELECT * FROM clientes WHERE UPPER(nome) = UPPER(@nome) ORDER BY nome";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@nome", nome);

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
        public bool Save(Cliente cliente)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
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

                command.Parameters.AddWithValue("@nome", cliente.Nome);
                command.Parameters.AddWithValue("@cpf_cnpj", cliente.CpfCnpj);
                command.Parameters.AddWithValue("@email", cliente.Email);
                command.Parameters.AddWithValue("@telefone", cliente.Telefone);

                return command.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Exclui um cliente do banco de dados com base no seu ID.
        /// </summary>
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
        public bool CpfCnpjExists(string cpfCnpj, int currentId)
        {
            using (var connection = Database.GetConnection())
            {
                var command = new SqlCommand("SELECT COUNT(1) FROM clientes WHERE cpf_cnpj = @cpf_cnpj AND id <> @id", connection);
                command.Parameters.AddWithValue("@cpf_cnpj", cpfCnpj);
                command.Parameters.AddWithValue("@id", currentId);

                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        // --- NOVA LÓGICA DE RELATÓRIO MOVIDA PARA O CONTROLLER ---

        /// <summary>
        /// Gera um documento PDF com a lista de clientes fornecida.
        /// </summary>
        /// <param name="clientes">A lista de clientes para o relatório.</param>
        /// <param name="caminhoArquivo">O caminho completo onde o PDF deve ser salvo.</param>
        /// <returns>Verdadeiro se o PDF foi gerado com sucesso, falso caso contrário.</returns>
        public bool GerarRelatorioPDF(List<Cliente> clientes, string caminhoArquivo)
        {
            try
            {
                // 1. Cria o documento PDF
                Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 30f, 20f);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminhoArquivo, FileMode.Create));

                doc.Open();

                // 2. Adiciona o Título
                var fonteTitulo = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD);
                var titulo = new Paragraph("Relatório de Clientes\n\n", fonteTitulo);
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                // 3. Cria a Tabela
                PdfPTable tabela = new PdfPTable(5); // 5 colunas
                tabela.WidthPercentage = 100;
                tabela.SetWidths(new float[] { 0.5f, 2f, 1.5f, 2f, 1f });

                // 4. Adiciona os Cabeçalhos da Tabela
                AdicionarCabecalhoTabela(tabela, "ID");
                AdicionarCabecalhoTabela(tabela, "Nome");
                AdicionarCabecalhoTabela(tabela, "CPF/CNPJ");
                AdicionarCabecalhoTabela(tabela, "E-mail");
                AdicionarCabecalhoTabela(tabela, "Telefone");

                // 5. Adiciona os Dados (Linhas)
                var fonteDados = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);
                foreach (var cliente in clientes)
                {
                    tabela.AddCell(new Phrase(cliente.Id.ToString(), fonteDados));
                    tabela.AddCell(new Phrase(cliente.Nome, fonteDados));
                    tabela.AddCell(new Phrase(cliente.CpfCnpj, fonteDados));
                    tabela.AddCell(new Phrase(cliente.Email, fonteDados));
                    tabela.AddCell(new Phrase(cliente.Telefone, fonteDados));
                }

                // 6. Adiciona a tabela ao documento
                doc.Add(tabela);
                doc.Close();

                return true; // Sucesso
            }
            catch (Exception ex)
            {
                // Em um app real, você logaria o erro (ex. Console.WriteLine(ex.Message))
                return false; // Falha
            }
        }

        /// <summary>
        /// Método auxiliar (privado) para formatar e adicionar células de cabeçalho à tabela PDF.
        /// </summary>
        private void AdicionarCabecalhoTabela(PdfPTable tabela, string texto)
        {
            var fonteCabecalho = new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.WHITE);
            PdfPCell celula = new PdfPCell(new Phrase(texto, fonteCabecalho));
            celula.BackgroundColor = new BaseColor(70, 130, 180); // Cor "SteelBlue"
            celula.HorizontalAlignment = Element.ALIGN_CENTER;
            celula.VerticalAlignment = Element.ALIGN_MIDDLE;
            celula.Padding = 6;
            tabela.AddCell(celula);
        }
    }
}
