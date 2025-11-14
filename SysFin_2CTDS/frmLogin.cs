using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmLogin : Form
    {
        private readonly UsuarioController _usuarioController;

        public frmLogin()
        {
            InitializeComponent();
            _usuarioController = new UsuarioController();
        }

        private async void btnEntrar_Click(object? sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string senha = txtSenha.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Por favor, preencha o login e a senha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tenta fazer o login
                Usuario? usuarioLogado = await _usuarioController.LoginAsync(login, senha);

                if (usuarioLogado != null)
                {
                    // 1. Salva o usuário na sessão global (conforme o guia PDF)
                    SessionManager.Login(usuarioLogado);

                    // 2. Define o resultado como OK para o Program.cs
                    this.DialogResult = DialogResult.OK;

                    // 3. Fecha a tela de login
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login ou senha inválidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar logar: " + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}