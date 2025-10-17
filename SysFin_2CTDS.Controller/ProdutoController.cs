using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Controller
{
    public class ProdutoController
    {
        // 1. SIMULAÇÃO DO BANCO DE DADOS
        // Esta lista estática guardará todos os produtos enquanto o programa estiver rodando.
        private static List<Produto> _bancoDeDadosSimulado = new List<Produto>();
        private static int _idCounter = 1; // Para gerar IDs únicos

        public string CadastrarProduto(string nome, string descricao, decimal precoVenda, int estoqueInicial)
        {
            // Validações (futuras)

            Produto novoProduto = new Produto();
            novoProduto.Id = _idCounter; // 2. Atribui um ID único
            novoProduto.Nome = nome;
            novoProduto.Descricao = descricao;
            novoProduto.PrecoVenda = precoVenda;
            novoProduto.Estoque = estoqueInicial;

            // 3. Adiciona o novo produto na nossa lista simulada
            _bancoDeDadosSimulado.Add(novoProduto);
            _idCounter++; // Incrementa o contador para o próximo produto

            return $"Produto '{nome}' cadastrado com sucesso!";
        }

        // 4. NOVO MÉTODO PARA LISTAR OS PRODUTOS
        public List<Produto> ListarProdutos()
        {
            // Simplesmente retorna a lista completa de produtos
            return _bancoDeDadosSimulado;
        }

        // Adicione este método dentro da classe ProdutoController

        public List<Produto> ListarProdutosPorNome(string termoBusca)
        {
            // Se o termo de busca estiver vazio ou nulo, retorna a lista completa
            if (string.IsNullOrWhiteSpace(termoBusca))
            {
                return ListarProdutos();
            }

            // Usa LINQ para filtrar a lista
            // Onde (Where) o Nome do produto, convertido para minúsculas (ToLower),
            // contém (Contains) o termo de busca, também em minúsculas.
            return _bancoDeDadosSimulado
                .Where(p => p.Nome.ToLower().Contains(termoBusca.ToLower()))
                .ToList();
        }

        // Adicione este método dentro da classe ProdutoController

        public bool ExcluirProduto(int id)
        {
            // Procura na lista o produto que tem o ID correspondente
            Produto produtoParaExcluir = _bancoDeDadosSimulado.FirstOrDefault(p => p.Id == id);

            // Se encontrou o produto (não é nulo)
            if (produtoParaExcluir != null)
            {
                // Regra de negócio importante (será implementada no futuro):
                // Antes de remover, deveríamos verificar se este produto não está em nenhuma venda ou compra.
                // Como ainda não temos essas funcionalidades, vamos permitir a exclusão direta.

                _bancoDeDadosSimulado.Remove(produtoParaExcluir);
                return true; // Retorna true indicando que a exclusão foi bem-sucedida
            }

            // Se não encontrou o produto, retorna false
            return false;
        }

        // Adicione estes dois métodos dentro da classe ProdutoController

        public Produto BuscarProdutoPorId(int id)
        {
            // Usa o FirstOrDefault para encontrar o produto com o ID correspondente.
            // Retorna o objeto Produto encontrado ou null se não encontrar.
            return _bancoDeDadosSimulado.FirstOrDefault(p => p.Id == id);
        }

        public string AtualizarProduto(int id, string nome, string descricao, decimal precoVenda)
        {
            // Busca o produto existente na nossa lista
            Produto produtoParaAtualizar = BuscarProdutoPorId(id);

            // Se o produto não for encontrado, retorna uma mensagem de erro
            if (produtoParaAtualizar == null)
            {
                return "Produto não encontrado. A atualização falhou.";
            }

            // Atualiza apenas as propriedades permitidas.
            // NOTE que o ESTOQUE não está sendo modificado aqui.
            produtoParaAtualizar.Nome = nome;
            produtoParaAtualizar.Descricao = descricao;
            produtoParaAtualizar.PrecoVenda = precoVenda;

            return $"Produto '{nome}' atualizado com sucesso!";
        }
    }
}