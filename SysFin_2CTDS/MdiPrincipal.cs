using SysFin_2CTDS.Model;
using System;
using System.Linq;
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
                tssUsuarioLogado.Text = $"Usuário: {SessionManager.CurrentUser.Nome} | Perfis: {string.Join(", ", SessionManager.CurrentUser.Perfis)}";
            }

            AbrirDashboard();
        }

        private void AplicarSeguranca()
        {
            var usuario = SessionManager.CurrentUser;

            if (usuario == null)
            {
                pnlSideMenu.Enabled = false;
                MessageBox.Show("Erro de sessão. O aplicativo será fechado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            bool adminOuTesoureiro = usuario.HasRole("Administrador") || usuario.HasRole("Tesoureiro");
            bool acessoVendas = adminOuTesoureiro || usuario.HasRole("Vendedor");

            // Regra 1: Cadastros
            lblMenuCadastros.Visible = adminOuTesoureiro;
            btnClientes.Visible = adminOuTesoureiro;
            btnFornecedores.Visible = adminOuTesoureiro;
            btnProdutos.Visible = adminOuTesoureiro;
            btnPlanoDeContas.Visible = adminOuTesoureiro;
            btnUsuarios.Visible = usuario.HasRole("Administrador");

            // Regra 2: Movimentações
            lblMenuMovimentacoes.Visible = true;
            btnCompras.Visible = adminOuTesoureiro;
            btnVendas.Visible = acessoVendas;
            btnEstoque.Visible = true;
            btnFluxoCaixa.Visible = adminOuTesoureiro;

            // Regra 3: Relatórios
            lblMenuRelatorios.Visible = true;
            btnRelatorioCompras.Visible = usuario.HasRole("Administrador");
            btnRelatorioVendas.Visible = adminOuTesoureiro;
            btnContasPagar.Visible = adminOuTesoureiro;
        }

        // --- MÉTODO CENTRALIZADO PARA ABRIR FORMULÁRIOS ---
        private void AbrirFormulario(Form form)
        {
            // Fecha outros formulários filhos abertos para não acumular janelas
            foreach (var child in this.MdiChildren)
            {
                child.Close();
            }

            form.MdiParent = this;

            // --- MUDANÇA SOLICITADA ---
            // Faz o formulário preencher todo o espaço do MDI
            form.WindowState = FormWindowState.Maximized;

            // Opcional: Remove a borda do formulário filho para parecer
            // que ele faz parte da janela principal (sem barra de título extra).
            // Se você não gostar disso, pode comentar a linha abaixo.
            form.FormBorderStyle = FormBorderStyle.None;

            // Opcional: Faz o formulário preencher o espaço restante (Dock)
            // Isso às vezes funciona melhor que Maximized em MDIs complexos.
            form.Dock = DockStyle.Fill;

            form.Show();
        }

        private void AbrirDashboard()
        {
            // O Dashboard também usa a nova lógica
            AbrirFormulario(new frmDashboard());
        }

        // --- EVENTOS DE CLIQUE (Navegação) ---

        private void pnlLogo_Click(object? sender, EventArgs e) => AbrirDashboard();

        // Cadastros
        private void btnClientes_Click(object? sender, EventArgs e) => AbrirFormulario(new ClienteForm());
        private void btnFornecedores_Click(object? sender, EventArgs e) => AbrirFormulario(new FornecedorForm());
        private void btnProdutos_Click(object? sender, EventArgs e) => AbrirFormulario(new frmListagemProdutos());
        private void btnUsuarios_Click(object? sender, EventArgs e) => AbrirFormulario(new frmCadastroUsuarios());
        private void btnPlanoDeContas_Click(object? sender, EventArgs e) => AbrirFormulario(new frmPlanoDeContas());

        // Movimentações
        private void btnFluxoCaixa_Click(object? sender, EventArgs e) => AbrirFormulario(new frmFluxoCaixa());
        private void btnCompras_Click(object? sender, EventArgs e) => AbrirFormulario(new frmRegistroCompras());
        private void btnVendas_Click(object? sender, EventArgs e) => AbrirFormulario(new frmRegistroVendas());
        private void btnEstoque_Click(object? sender, EventArgs e) => AbrirFormulario(new EstoqueForm());

        // Relatórios
        private void btnContasPagar_Click(object? sender, EventArgs e) => AbrirFormulario(new frmContasPagar());
        private void btnRelatorioCompras_Click(object? sender, EventArgs e) => AbrirFormulario(new frmRelatorioCompras());
        private void btnRelatorioVendas_Click(object? sender, EventArgs e) => AbrirFormulario(new frmRelatorioVendas());

        // Sair
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