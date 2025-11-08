using SysFin_2CTDS.Controller;
<<<<<<< HEAD
using SysFin_2CTDS.Model;
=======
>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // Adicionado
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmListagemProdutos : Form
    {
        // MUDANÇA: Controllers movidos para campos da classe
        private readonly ProdutoController _produtoController;
        private readonly RelatorioController _relatorioController;

        public frmListagemProdutos()
        {
            InitializeComponent();
            // MUDANÇA: Inicializa os controllers
            _produtoController = new ProdutoController();
            _relatorioController = new RelatorioController();
        }

<<<<<<< HEAD
        // MUDANÇA: 'async void'
        private async void CarregarProdutos()
        {
            try
            {
                dgvProdutos.DataSource = null;
                // MUDANÇA: Chamada Async
                dgvProdutos.DataSource = await _produtoController.ListarProdutosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
=======
        private void CarregarProdutos()
        {
            ProdutoController controller = new ProdutoController();
            dgvProdutos.DataSource = null;
            dgvProdutos.DataSource = controller.ListarProdutos();
>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
        }

        private void frmListagemProdutos_Load(object? sender, EventArgs e)
        {
<<<<<<< HEAD
=======

>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
            CarregarProdutos();
        }

        private void btnNovo_Click(object? sender, EventArgs e)
        {
<<<<<<< HEAD
            // O formulário de cadastro (modal) não precisa ser async
            using (frmCadastroProduto telaCadastro = new frmCadastroProduto())
            {
                telaCadastro.ShowDialog();
            }
=======
>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
            CarregarProdutos();
        }

        // MUDANÇA: 'async void'
        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
<<<<<<< HEAD
            try
            {
                string termoBusca = txtBusca.Text;
                // MUDANÇA: Chamada Async
                List<Model.Produto> resultados = await _produtoController.ListarProdutosPorNomeAsync(termoBusca);
                dgvProdutos.DataSource = null;
                dgvProdutos.DataSource = resultados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MUDANÇA: 'async void'
        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
=======
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
>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultadoConfirmacao = MessageBox.Show("Tem certeza que deseja excluir o produto selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultadoConfirmacao == DialogResult.Yes)
            {
<<<<<<< HEAD
                try
                {
                    // MUDANÇA: Convert.ToInt32 (para nulos)
                    int idSelecionado = Convert.ToInt32(dgvProdutos.SelectedRows[0].Cells["Id"].Value);

                    // MUDANÇA: Chamada Async
                    bool sucesso = await _produtoController.ExcluirProdutoAsync(idSelecionado);

                    if (sucesso)
                    {
                        MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarProdutos();
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível encontrar o produto para excluir.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
=======
                int idSelecionado = (int)dgvProdutos.SelectedRows[0].Cells["Id"].Value;
                ProdutoController controller = new ProdutoController();
                bool sucesso = controller.ExcluirProduto(idSelecionado);

                if (sucesso)
                {
                    MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarProdutos();
>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

<<<<<<< HEAD
        private void btnEditar_Click(object? sender, EventArgs e)
=======

        private void btnEditar_Click(object sender, EventArgs e)
>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
        {
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um produto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

<<<<<<< HEAD
            // MUDANÇA: Convert.ToInt32 (para nulos)
            int idSelecionado = Convert.ToInt32(dgvProdutos.SelectedRows[0].Cells["Id"].Value);

            using (frmCadastroProduto telaEdicao = new frmCadastroProduto(idSelecionado))
            {
                telaEdicao.ShowDialog();
            }

=======
            int idSelecionado = (int)dgvProdutos.SelectedRows[0].Cells["Id"].Value;

            frmCadastroProduto telaEdicao = new frmCadastroProduto(idSelecionado);
            telaEdicao.ShowDialog();
>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
            CarregarProdutos();
        }

        // MUDANÇA: 'async void'
        private async void btnRelatorio_Click(object? sender, EventArgs e)
        {
<<<<<<< HEAD
            SaveFileDialog salvar = new SaveFileDialog();
            salvar.Filter = "Arquivo PDF (*.pdf)|*.pdf";
            salvar.FileName = "Relatorio_Produtos.pdf";

            if (salvar.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // MUDANÇA: Chamada Async
                    await _relatorioController.GerarRelatorioProdutosAsync(salvar.FileName);

                    MessageBox.Show("Relatório gerado com sucesso!\nSalvo em: " + salvar.FileName, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Opcional: Tenta abrir o arquivo PDF gerado
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(salvar.FileName) { UseShellExecute = true });
                    }
                    catch (Exception exOpen)
                    {
                        MessageBox.Show("Não foi possível abrir o arquivo PDF automaticamente.\nErro: " + exOpen.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
=======
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
>>>>>>> d3d0a430218cc3631d6a87b01dcb4ac0609cb1f7
                }
                catch (Exception ex)
                {
                    // Captura o erro do Controller (ex: "Não há produtos")
                    MessageBox.Show(ex.Message, "Erro ao Gerar Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBusca_TextChanged(object? sender, EventArgs e)
        {
            // Se o texto for apagado, busca tudo
            if (string.IsNullOrWhiteSpace(txtBusca.Text))
            {
                CarregarProdutos();
            }
        }

        private void txtBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}

