using iTextSharp.text;
using iTextSharp.text.pdf;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace SysFin_2CTDS.Controller
{
    public class RelatorioController
    {
        public string GerarRelatorioProdutos()
        {
            ProdutoController produtoController = new ProdutoController();
            List<Produto> listaDeProdutos = produtoController.ListarProdutos();

            string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string nomeArquivo = "RelatorioDeProdutos_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
            string caminhoCompleto = Path.Combine(caminhoDesktop, nomeArquivo);

            Document doc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminhoCompleto, FileMode.Create));

                doc.Open();

                Paragraph titulo = new Paragraph("Relatório de Produtos\n\n", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                PdfPTable tabela = new PdfPTable(5);
                tabela.WidthPercentage = 100;

                tabela.AddCell("ID");
                tabela.AddCell("Nome do Produto");
                tabela.AddCell("Descrição");
                tabela.AddCell("Preço (R$)");
                tabela.AddCell("Estoque");

                foreach (var produto in listaDeProdutos)
                {
                    tabela.AddCell(produto.Id.ToString());
                    tabela.AddCell(produto.Nome);
                    tabela.AddCell(produto.Descricao);
                    tabela.AddCell(produto.PrecoVenda.ToString("F2"));
                    tabela.AddCell(produto.EstoqueAtual.ToString());
                }

                doc.Add(tabela);

                return caminhoCompleto;
            }

            catch (Exception ex)
            {
                return "ERRO: " + ex.Message;
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