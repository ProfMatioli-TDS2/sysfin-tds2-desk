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
        private int? _idProdutoParaEdicao = null;

        public frmCadastroProduto()
        {
            InitializeComponent();
        }
        public frmCadastroProduto(int idProduto)
        {
            InitializeComponent();

            _idProdutoParaEdicao = idProduto;
            CarregarDadosParaEdicao();
        }
 
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ProdutoController controller = new ProdutoController();

            try
            {
                string nome = txtNome.Text;
                string descricao = txtDescricao.Text;
                decimal preco = numPrecoVenda.Value;

                string resultado;

                if (_idProdutoParaEdicao.HasValue)
                {
                    resultado = controller.AtualizarProduto(_idProdutoParaEdicao.Value, nome, descricao, preco);
                }

                else
                {
                    int estoque = (int)numEstoque.Value;
                    resultado = controller.CadastrarProduto(nome, descricao, preco, estoque);
                }

                MessageBox.Show(resultado, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarDadosParaEdicao()
        {
            ProdutoController controller = new ProdutoController();
            Produto produto = controller.BuscarProdutoPorId(_idProdutoParaEdicao.Value);

            if (produto != null)
            {
                txtNome.Text = produto.Nome;
                txtDescricao.Text = produto.Descricao;
                numPrecoVenda.Value = produto.PrecoVenda;
                numEstoque.Value = produto.EstoqueAtual;
                numEstoque.Enabled = false;

                this.Text = "Editar Produto";
                btnSalvar.Text = "Salvar Alterações";
            }
        }
    }
}
