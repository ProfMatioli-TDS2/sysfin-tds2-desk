using SysFin_2CTDS.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SysFin_2CTDS.View
{
    public partial class frmListagemProdutos : Form
    {
        public frmListagemProdutos()
        {
            InitializeComponent();
        }

        private void CarregarProdutos()
        {
            ProdutoController controller = new ProdutoController();
            dgvProdutos.DataSource = null;
            dgvProdutos.DataSource = controller.ListarProdutos();
        }

        private void frmListagemProdutos_Load(object sender, EventArgs e)
        {

            CarregarProdutos();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarProdutos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroProduto telaCadastro = new frmCadastroProduto();
            telaCadastro.ShowDialog();
            CarregarProdutos();
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBusca.Text;
            ProdutoController controller = new ProdutoController();
            List<Model.Produto> resultados = controller.ListarProdutosPorNome(termoBusca);

            dgvProdutos.DataSource = null;
            dgvProdutos.DataSource = resultados;
        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultadoConfirmacao = MessageBox.Show("Tem certeza que deseja excluir o produto selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultadoConfirmacao == DialogResult.Yes)
            {
                int idSelecionado = (int)dgvProdutos.SelectedRows[0].Cells["Id"].Value;
                ProdutoController controller = new ProdutoController();
                bool sucesso = controller.ExcluirProduto(idSelecionado);

                if (sucesso)
                {
                    MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarProdutos();
                }
                else
                {
                    MessageBox.Show("Não foi possível encontrar o produto para excluir.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um produto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idSelecionado = (int)dgvProdutos.SelectedRows[0].Cells["Id"].Value;

            frmCadastroProduto telaEdicao = new frmCadastroProduto(idSelecionado);
            telaEdicao.ShowDialog();
            CarregarProdutos();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            RelatorioController relatorioController = new RelatorioController();
            string resultado = relatorioController.GerarRelatorioProdutos();

            if (resultado.StartsWith("ERRO:"))
            {
                MessageBox.Show(resultado, "Erro ao Gerar Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Relatório gerado com sucesso!\nSalvo em: " + resultado, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = resultado,
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível abrir o arquivo PDF automaticamente.\nErro: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {

        }
    }
}