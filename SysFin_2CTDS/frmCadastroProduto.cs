using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // MUDANÇA: Adicionado
using System.Windows.Forms;
using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;

namespace SysFin_2CTDS.View
{
    public partial class frmCadastroProduto : Form
    {
        private int? _idProdutoParaEdicao = null;
        private readonly ProdutoController _controller; // MUDANÇA: Controller como campo

        public frmCadastroProduto()
        {
            InitializeComponent();
            _controller = new ProdutoController(); // MUDANÇA: Inicializado
        }

        // Construtor para o modo EDIÇÃO
        public frmCadastroProduto(int idProduto)
        {
            InitializeComponent(); // Sempre necessário para construir a tela
            _controller = new ProdutoController(); // MUDANÇA: Inicializado

            _idProdutoParaEdicao = idProduto; // Guarda o ID que recebemos
            CarregarDadosParaEdicao();
        }

        // MUDANÇA: Método agora é 'async void'
        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Captura os dados da tela
                string nome = txtNome.Text;
                string descricao = txtDescricao.Text;
                decimal preco = numPrecoVenda.Value;

                string resultado;

                // Se _idProdutoParaEdicao NÃO for nulo, estamos em modo de EDIÇÃO
                if (_idProdutoParaEdicao.HasValue)
                {
                    // MUDANÇA: Chamando método Async
                    resultado = await _controller.AtualizarProdutoAsync(_idProdutoParaEdicao.Value, nome, descricao, preco);
                }
                // Senão, estamos em modo de CADASTRO
                else
                {
                    int estoque = (int)numEstoque.Value;
                    // MUDANÇA: Chamando método Async
                    resultado = await _controller.CadastrarProduto(nome, descricao, preco, estoque);
                }

                MessageBox.Show(resultado, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Fecha a tela de cadastro/edição após o sucesso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MUDANÇA: Método agora é 'async void'
        private async void CarregarDadosParaEdicao()
        {
            try
            {
                // MUDANÇA: Chamando método Async
                Produto? produto = await _controller.BuscarProdutoPorIdAsync(_idProdutoParaEdicao.Value);

                if (produto != null)
                {
                    // Preenche os campos com os dados do produto
                    txtNome.Text = produto.Nome;
                    txtDescricao.Text = produto.Descricao;
                    numPrecoVenda.Value = produto.PrecoVenda;
                    numEstoque.Value = produto.EstoqueAtual;

                    // ---- APLICA A REGRA DE NEGÓCIO IMPORTANTE ----
                    numEstoque.Enabled = false; // Desabilita o campo de estoque

                    // Altera o título da janela e o texto do botão
                    this.Text = "Editar Produto";
                    btnSalvar.Text = "Salvar Alterações";
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}