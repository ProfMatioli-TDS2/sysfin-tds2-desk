// Importações necessárias para o PDF e para acessar arquivos do sistema
using iTextSharp.text;
using iTextSharp.text.pdf;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.IO; // Essencial para lidar com arquivos e pastas

namespace SysFin_2CTDS.Controller
{
    public class RelatorioController
    {
        public string GerarRelatorioProdutos()
        {
            // 1. Obter a lista de produtos
            ProdutoController produtoController = new ProdutoController();
            List<Produto> listaDeProdutos = produtoController.ListarProdutos();

            // 2. Definir o nome e o caminho do arquivo
            // Vamos salvar o PDF na Área de Trabalho (Desktop) do usuário
            string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string nomeArquivo = "RelatorioDeProdutos_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
            string caminhoCompleto = Path.Combine(caminhoDesktop, nomeArquivo);

            // 3. Criar o documento PDF
            Document doc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);

            try
            {
                // Cria o arquivo PDF no caminho especificado
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminhoCompleto, FileMode.Create));

                // Abre o documento para edição
                doc.Open();

                // 4. Adicionar o Título
                Paragraph titulo = new Paragraph("Relatório de Produtos\n\n", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                // 5. Criar a Tabela
                PdfPTable tabela = new PdfPTable(5); // 5 colunas: Id, Nome, Descrição, Preço, Estoque
                tabela.WidthPercentage = 100; // A tabela ocupa 100% da largura da página

                // Adiciona os cabeçalhos das colunas
                tabela.AddCell("ID");
                tabela.AddCell("Nome do Produto");
                tabela.AddCell("Descrição");
                tabela.AddCell("Preço (R$)");
                tabela.AddCell("Estoque");

                // 6. Adicionar os dados dos produtos na tabela
                foreach (var produto in listaDeProdutos)
                {
                    tabela.AddCell(produto.Id.ToString());
                    tabela.AddCell(produto.Nome);
                    tabela.AddCell(produto.Descricao);
                    tabela.AddCell(produto.PrecoVenda.ToString("F2")); // Formata o preço com 2 casas decimais
                    tabela.AddCell(produto.Estoque.ToString());
                }

                // Adiciona a tabela ao documento
                doc.Add(tabela);

                // Retorna o caminho onde o arquivo foi salvo
                return caminhoCompleto;
            }
            catch (Exception ex)
            {
                // Se der erro, retorna a mensagem de erro
                return "ERRO: " + ex.Message;
            }
            finally
            {
                // 7. Fechar o documento (MUITO IMPORTANTE!)
                if (doc.IsOpen())
                {
                    doc.Close();
                }
            }
        }
    }
}