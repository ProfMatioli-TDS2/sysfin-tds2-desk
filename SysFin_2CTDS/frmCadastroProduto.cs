using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;

namespace SysFin_2CTDS.View
{
    public partial class frmCadastroProduto : Form
    {
        // Variável para controlar se estamos em modo de edição
        private int? _idProdutoParaEdicao = null;

        public frmCadastroProduto()
        {
            InitializeComponent();
        }
        // Adicione este NOVO CONSTRUTOR abaixo do construtor padrão

        // Construtor para o modo EDIÇÃO
        public frmCadastroProduto(int idProduto)
        {
            InitializeComponent(); // Sempre necessário para construir a tela

            _idProdutoParaEdicao = idProduto; // Guarda o ID que recebemos

            // Carrega os dados do produto para preencher a tela
            CarregarDadosParaEdicao();
        }
        // ... resto do código

        // SUBSTITUA o seu método btnSalvar_Click por este

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ProdutoController controller = new ProdutoController();

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
                    resultado = controller.AtualizarProduto(_idProdutoParaEdicao.Value, nome, descricao, preco);
                }
                // Senão, estamos em modo de CADASTRO
                else
                {
                    int estoque = (int)numEstoque.Value;
                    resultado = controller.CadastrarProduto(nome, descricao, preco, estoque);
                }

                MessageBox.Show(resultado, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Fecha a tela de cadastro/edição após o sucesso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Adicione este MÉTODO dentro da classe frmCadastroProduto

        private void CarregarDadosParaEdicao()
        {
            ProdutoController controller = new ProdutoController();
            Produto produto = controller.BuscarProdutoPorId(_idProdutoParaEdicao.Value);

            if (produto != null)
            {
                // Preenche os campos com os dados do produto
                txtNome.Text = produto.Nome;
                txtDescricao.Text = produto.Descricao;
                numPrecoVenda.Value = produto.PrecoVenda;
                numEstoque.Value = produto.Estoque;

                // ---- APLICA A REGRA DE NEGÓCIO IMPORTANTE ----
                numEstoque.Enabled = false; // Desabilita o campo de estoque

                // Altera o título da janela e o texto do botão
                this.Text = "Editar Produto";
                btnSalvar.Text = "Salvar Alterações";
            }
        }
    }
}
