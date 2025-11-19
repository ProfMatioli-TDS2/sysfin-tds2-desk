// Importações necessárias para o PDF e para acessar arquivos do sistema
using iTextSharp.text;
using iTextSharp.text.pdf;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.IO; // Essencial para lidar com arquivos e pastas
using System.Linq; // Para .Any()
using System.Threading.Tasks; // Para Async

namespace SysFin_2CTDS.Controller
{
    public class RelatorioController
    {
        // MUDANÇA: 'void' para 'async Task' e nome '...Async'
        public async Task GerarRelatorioProdutosAsync(string caminhoCompleto)
        {
            // 1. Obter a lista de produtos (Async)
            ProdutoController produtoController = new ProdutoController();
            // MUDANÇA: Chamada Async
            List<Produto> listaDeProdutos = await produtoController.ListarProdutosAsync();

            if (listaDeProdutos == null || !listaDeProdutos.Any())
            {
                throw new Exception("Não há produtos para gerar o relatório.");
            }

            // 3. Criar o documento PDF
            Document doc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminhoCompleto, FileMode.Create));
                doc.Open();

                Paragraph titulo = new Paragraph("Relatório de Produtos\n\n", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                PdfPTable tabela = new PdfPTable(5); // 5 colunas
                tabela.WidthPercentage = 100;

                tabela.AddCell("ID");
                tabela.AddCell("Nome do Produto");
                tabela.AddCell("Descrição");
                tabela.AddCell("Preço (R$)");
                tabela.AddCell("Estoque");

                foreach (var produto in listaDeProdutos)
                {
                    tabela.AddCell(produto.Id.ToString());
                    tabela.AddCell(produto.Nome ?? ""); // Tratamento de nulo
                    tabela.AddCell(produto.Descricao ?? ""); // Tratamento de nulo
                    tabela.AddCell(produto.PrecoVenda.ToString("F2"));
                    tabela.AddCell(produto.EstoqueAtual.ToString());
                }

                doc.Add(tabela);
            }
            catch (Exception ex)
            {
                // Se der erro, lança a exceção para a View (Form)
                throw new Exception("ERRO ao gerar PDF: " + ex.Message);
            }
            finally
            {
                if (doc.IsOpen())
                {
                    doc.Close();
                }
            }
        }

        // MUDANÇA: 'void' para 'async Task' e nome '...Async'
        public async Task GerarRelatorioFornecedoresAsync(string caminho)
        {
            FornecedorController fornecedorController = new FornecedorController();
            // MUDANÇA: Chamada Async
            var fornecedores = await fornecedorController.GetAllAsync("nome", "ASC");

            if (fornecedores == null || !fornecedores.Any())
            {
                throw new Exception("Não há fornecedores para gerar o relatório.");
            }

            Document doc = new Document(PageSize.A4);
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
                doc.Open();

                var fonteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                var fonteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
                var fonteCorpo = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                Paragraph titulo = new Paragraph("Relatório de Fornecedores", fonteTitulo);
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(new Paragraph("\n"));

                PdfPTable tabela = new PdfPTable(5);
                tabela.WidthPercentage = 100;
                tabela.SetWidths(new float[] { 10, 25, 25, 25, 15 });

                tabela.AddCell(new PdfPCell(new Phrase("ID", fonteSubtitulo)));
                tabela.AddCell(new PdfPCell(new Phrase("Nome", fonteSubtitulo)));
                tabela.AddCell(new PdfPCell(new Phrase("CNPJ", fonteSubtitulo)));
                tabela.AddCell(new PdfPCell(new Phrase("E-mail", fonteSubtitulo)));
                tabela.AddCell(new PdfPCell(new Phrase("Telefone", fonteSubtitulo)));

                foreach (var f in fornecedores)
                {
                    // Remove caracteres não numéricos antes de formatar
                    string cnpjNumerico = new string((f.Cnpj ?? "").Where(char.IsDigit).ToArray());
                    string telefoneNumerico = new string((f.Telefone ?? "").Where(char.IsDigit).ToArray());

                    string cnpjFormatado = f.Cnpj ?? "";
                    if (cnpjNumerico.Length == 14)
                        cnpjFormatado = Convert.ToUInt64(cnpjNumerico).ToString(@"00\.000\.000\/0000\-00");

                    string telefoneFormatado = f.Telefone ?? "";
                    if (telefoneNumerico.Length == 11)
                        telefoneFormatado = Convert.ToUInt64(telefoneNumerico).ToString(@"(00) 00000\-0000");
                    else if (telefoneNumerico.Length == 10)
                        telefoneFormatado = Convert.ToUInt64(telefoneNumerico).ToString(@"(00) 0000\-0000");


                    tabela.AddCell(new PdfPCell(new Phrase(f.Id.ToString(), fonteCorpo)));
                    tabela.AddCell(new PdfPCell(new Phrase(f.Nome ?? "", fonteCorpo)));
                    tabela.AddCell(new PdfPCell(new Phrase(cnpjFormatado, fonteCorpo)));
                    tabela.AddCell(new PdfPCell(new Phrase(f.Email ?? "", fonteCorpo)));
                    tabela.AddCell(new PdfPCell(new Phrase(telefoneFormatado, fonteCorpo)));
                }

                doc.Add(tabela);

                Paragraph rodape = new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fonteCorpo);
                rodape.Alignment = Element.ALIGN_RIGHT;
                doc.Add(new Paragraph("\n"));
                doc.Add(rodape);
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO ao gerar PDF: " + ex.Message);
            }
            finally
            {
                if (doc.IsOpen())
                {
                    doc.Close();
                }
            }
        }
    }
}

