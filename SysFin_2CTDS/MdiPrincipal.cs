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

            // Preenche o rodapé com as informações do usuário logado
            if (SessionManager.CurrentUser != null)
            {
                // Ex: "Usuário: Administrador do Sistema | Perfis: Administrador"
                tssUsuarioLogado.Text = $"Usuário: {SessionManager.CurrentUser.Nome} | Perfis: {string.Join(", ", SessionManager.CurrentUser.Perfis)}";
            }
        }

        private void AplicarSeguranca()
        {
            var usuario = SessionManager.CurrentUser;

            if (usuario == null)
            {
                pnlSideMenu.Enabled = false; // Desabilita o novo menu
                MessageBox.Show("Erro de sessão. O aplicativo será fechado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            // Define a visibilidade dos Menus E dos Botões da Toolbar
            bool adminOuTesoureiro = usuario.HasRole("Administrador") || usuario.HasRole("Tesoureiro");
            bool acessoVendas = adminOuTesoureiro || usuario.HasRole("Vendedor");

            // Aplica segurança aos novos botões do painel lateral

            // Regra 1: Cadastros (Admin/Tesoureiro)
            lblMenuCadastros.Visible = adminOuTesoureiro;
            btnClientes.Visible = adminOuTesoureiro;
            btnFornecedores.Visible = adminOuTesoureiro;
            btnProdutos.Visible = adminOuTesoureiro;
            btnPlanoDeContas.Visible = adminOuTesoureiro;

            // Regra 2: Movimentações
            lblMenuMovimentacoes.Visible = true; // Visível para todos
            btnCompras.Visible = adminOuTesoureiro; // Só Admin/Tesoureiro compra
            btnVendas.Visible = acessoVendas;       // Vendedor pode vender
            btnEstoque.Visible = true;              // Todos podem ver estoque
        }

        // --- Eventos de Clique (Menus) ---

        private void btnClientes_Click(object? sender, EventArgs e)
        {
            var clienteForm = new ClienteForm();
            clienteForm.MdiParent = this;
            clienteForm.Show();
        }

        private void btnFornecedores_Click(object? sender, EventArgs e)
        {
            var fornecedorForm = new FornecedorForm();
            fornecedorForm.MdiParent = this;
            fornecedorForm.Show();
        }

        private void btnProdutos_Click(object? sender, EventArgs e)
        {
            var form = new frmListagemProdutos();
            form.MdiParent = this;
            form.Show();
        }

        private void btnEstoque_Click(object? sender, EventArgs e)
        {
            var estoqueForm = new EstoqueForm();
            estoqueForm.MdiParent = this;
            estoqueForm.Show();
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

        private void btnPlanoDeContas_Click(object? sender, EventArgs e)
        {
            var form = new frmPlanoDeContas();
            form.MdiParent = this;
            form.Show();
        }

        // --- Lógica de Sair (Logout) ---
        private void btnSair_Click(object? sender, EventArgs e)
        {
            // 1. Limpa a sessão atual
            SessionManager.Logout();

            // 2. Define o DialogResult como 'Retry' (Tentar Novamente)
            // Este é o sinal para o Program.cs (que mudamos no Passo 45)
            // de que queremos voltar ao login.
            this.DialogResult = DialogResult.Retry;

            // 3. Fecha este formulário (MdiPrincipal)
            this.Close();
        }
    }
}