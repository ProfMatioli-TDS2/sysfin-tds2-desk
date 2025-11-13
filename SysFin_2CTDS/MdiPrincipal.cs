using SysFin_2CTDS.Model;
using System;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class MdiPrincipal : Form
    {
        public MdiPrincipal()
        {
            InitializeComponent();
        }

        private void MdiPrincipal_Load(object? sender, EventArgs e)
        {
            AplicarSeguranca();

            if (SessionManager.CurrentUser != null)
            {
                tssUsuarioLogado.Text = $"Usuário: {SessionManager.CurrentUser.Nome} (Perfil: {string.Join(", ", SessionManager.CurrentUser.Perfis)})";
            }

            AbrirDashboard();
        }

        private void AbrirDashboard()
        {
            foreach (var form in this.MdiChildren)
            {
                form.Close();
            }

            var dashboard = new frmDashboard
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            dashboard.Show();
        }

        private void AplicarSeguranca()
        {
            var usuario = SessionManager.CurrentUser;

            if (usuario == null)
            {
                MessageBox.Show("Erro de sessão. O aplicativo será fechado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            // --- 1. Definir Visibilidade dos BOTÕES ---

            // Cadastros
            btnClientes.Visible = usuario.HasRole("Administrador");
            btnFornecedores.Visible = usuario.HasRole("Administrador");
            btnProdutos.Visible = usuario.HasRole("Administrador");
            btnPlanoContas.Visible = usuario.HasRole("Administrador") || usuario.HasRole("Tesoureiro");
            btnUsuarios.Visible = usuario.HasRole("Administrador");

            // Financeiro (Registros)
            btnFluxoCaixa.Visible = usuario.HasRole("Administrador") || usuario.HasRole("Tesoureiro");
            btnCompras.Visible = usuario.HasRole("Administrador");
            btnVendas.Visible = usuario.HasRole("Administrador") || usuario.HasRole("Vendedor");

            // Relatórios
            btnContasPagar.Visible = usuario.HasRole("Administrador") || usuario.HasRole("Tesoureiro");
            btnRelatorioCompras.Visible = usuario.HasRole("Administrador");
            btnRelatorioVendas.Visible = usuario.HasRole("Administrador") || usuario.HasRole("Tesoureiro");
            btnEstoque.Visible = usuario.HasRole("Administrador") || usuario.HasRole("Vendedor");

            // Dashboard (Logo)
            pnlLogo.Visible = true;

            // --- 2. Definir Visibilidade dos TÍTULOS (Separadores) ---
            // Um título só aparece se pelo menos UM botão do grupo estiver visível.

            lblMenuCadastros.Visible = btnClientes.Visible ||
                                      btnFornecedores.Visible ||
                                      btnProdutos.Visible ||
                                      btnPlanoContas.Visible ||
                                      btnUsuarios.Visible;

            lblMenuFinanceiro.Visible = btnFluxoCaixa.Visible ||
                                        btnCompras.Visible ||
                                        btnVendas.Visible;

            lblMenuRelatorios.Visible = btnContasPagar.Visible ||
                                        btnRelatorioCompras.Visible ||
                                        btnRelatorioVendas.Visible ||
                                        btnEstoque.Visible;
        }

        // --- Eventos de Clique ---

        private void pnlLogo_Click(object? sender, EventArgs e)
        {
            AbrirDashboard();
        }

        private void btnClientes_Click(object? sender, EventArgs e)
        {
            var form = new ClienteForm();
            form.MdiParent = this;
            form.Show();
        }

        private void btnFornecedores_Click(object? sender, EventArgs e)
        {
            var form = new FornecedorForm();
            form.MdiParent = this;
            form.Show();
        }

        private void btnProdutos_Click(object? sender, EventArgs e)
        {
            var form = new frmListagemProdutos();
            form.MdiParent = this;
            form.Show();
        }

        private void btnPlanoContas_Click(object? sender, EventArgs e)
        {
            var form = new frmPlanoDeContas();
            form.MdiParent = this;
            form.Show();
        }

        private void btnUsuarios_Click(object? sender, EventArgs e)
        {
            var form = new frmCadastroUsuarios();
            form.MdiParent = this;
            form.Show();
        }

        private void btnFluxoCaixa_Click(object? sender, EventArgs e)
        {
            var form = new frmFluxoCaixa();
            form.MdiParent = this;
            form.Show();
        }

        private void btnContasPagar_Click(object? sender, EventArgs e)
        {
            var form = new frmContasPagar();
            form.MdiParent = this;
            form.Show();
        }

        private void btnRelatorioCompras_Click(object? sender, EventArgs e)
        {
            var form = new frmRelatorioCompras();
            form.MdiParent = this;
            form.Show();
        }

        private void btnRelatorioVendas_Click(object? sender, EventArgs e)
        {
            var form = new frmRelatorioVendas();
            form.MdiParent = this;
            form.Show();
        }

        private void btnCompras_Click(object? sender, EventArgs e)
        {
            var form = new frmRegistroCompras();
            form.MdiParent = this;
            form.Show();
        }

        private void btnVendas_Click(object? sender, EventArgs e)
        {
            var form = new frmRegistroVendas();
            form.MdiParent = this;
            form.Show();
        }

        private void btnEstoque_Click(object? sender, EventArgs e)
        {
            var form = new EstoqueForm();
            form.MdiParent = this;
            form.Show();
        }

        private void btnSair_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair e voltar para a tela de Login?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.Logout();
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }
    }
}