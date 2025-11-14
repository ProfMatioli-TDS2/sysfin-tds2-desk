using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmCadastroUsuarios : Form
    {
        private readonly UsuarioController _controller;
        private Usuario? _usuarioSelecionado;
        private List<Perfil>? _listaDePerfis; // Armazena todos os perfis

        public frmCadastroUsuarios()
        {
            InitializeComponent();
            _controller = new UsuarioController();
        }

        private async void frmCadastroUsuarios_Load(object? sender, EventArgs e)
        {
            await ConfigurarGrid();
            await CarregarUsuariosAsync();
            await CarregarPerfisAsync();
            LimparFormulario();
        }

        private async Task ConfigurarGrid()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.Columns.Add("Id", "ID");
            dgvUsuarios.Columns.Add("Nome", "Nome");
            dgvUsuarios.Columns.Add("Login", "Login");
            dgvUsuarios.Columns.Add("Ativo", "Ativo");

            dgvUsuarios.Columns["Id"].DataPropertyName = "Id";
            dgvUsuarios.Columns["Nome"].DataPropertyName = "Nome";
            dgvUsuarios.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsuarios.Columns["Login"].DataPropertyName = "Login";
            dgvUsuarios.Columns["Ativo"].DataPropertyName = "Ativo";
        }

        private async Task CarregarUsuariosAsync()
        {
            try
            {
                // Pega o ID do usuário logado (não pode editar a si mesmo)
                int idUsuarioLogado = SessionManager.CurrentUser?.Id ?? 0;

                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = await _controller.GetAllUsuariosAsync(idUsuarioLogado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CarregarPerfisAsync()
        {
            try
            {
                // Carrega a lista de perfis (Admin, Vendedor...) para o CheckedListBox
                _listaDePerfis = await _controller.GetAllPerfisAsync();
                clbPerfis.DataSource = _listaDePerfis;
                clbPerfis.DisplayMember = "Nome";
                clbPerfis.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparFormulario()
        {
            _usuarioSelecionado = null;
            txtNome.Clear();
            txtLogin.Clear();
            txtSenha.Clear(); // Limpa o campo senha
            chkAtivo.Checked = true; // Padrão é ativo

            // Desmarca todos os perfis
            for (int i = 0; i < clbPerfis.Items.Count; i++)
            {
                clbPerfis.SetItemChecked(i, false);
            }

            dgvUsuarios.ClearSelection();
            txtNome.Focus();
        }

        private async void dgvUsuarios_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                _usuarioSelecionado = dgvUsuarios.SelectedRows[0].DataBoundItem as Usuario;
                if (_usuarioSelecionado != null)
                {
                    txtNome.Text = _usuarioSelecionado.Nome;
                    txtLogin.Text = _usuarioSelecionado.Login;
                    chkAtivo.Checked = _usuarioSelecionado.Ativo;
                    txtSenha.Clear(); // Senha nunca é exibida
                    txtSenha.PlaceholderText = "Deixe em branco para não alterar";

                    // Marca os perfis do usuário
                    await MarcarPerfisDoUsuarioAsync(_usuarioSelecionado.Id);
                }
            }
            else
            {
                _usuarioSelecionado = null;
            }
        }

        private async Task MarcarPerfisDoUsuarioAsync(int idUsuario)
        {
            if (_listaDePerfis == null) return;

            // Desabilita o evento de seleção para evitar flickering
            clbPerfis.ItemCheck -= clbPerfis_ItemCheck;

            try
            {
                // 1. Busca os IDs dos perfis deste usuário
                var perfisDoUsuario = await _controller.GetPerfisDoUsuarioAsync(idUsuario);

                // 2. Marca os checkboxes
                for (int i = 0; i < clbPerfis.Items.Count; i++)
                {
                    if (clbPerfis.Items[i] is Perfil perfil)
                    {
                        // Verifica se o ID deste perfil está na lista de perfis do usuário
                        bool hasRole = perfisDoUsuario.Contains(perfil.Id);
                        clbPerfis.SetItemChecked(i, hasRole);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar perfis", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Reabilita o evento
                clbPerfis.ItemCheck += clbPerfis_ItemCheck;
            }
        }

        private void btnNovo_Click(object? sender, EventArgs e)
        {
            LimparFormulario();
            txtSenha.PlaceholderText = "";
        }

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            try
            {
                var usuario = _usuarioSelecionado ?? new Usuario();
                usuario.Nome = txtNome.Text;
                usuario.Login = txtLogin.Text;
                usuario.Ativo = chkAtivo.Checked;

                string novaSenha = txtSenha.Text; // Pode estar vazia (se for edição)

                // Pega os IDs dos perfis marcados
                var perfisIds = clbPerfis.CheckedItems
                                        .OfType<Perfil>()
                                        .Select(p => p.Id)
                                        .ToList();

                if (await _controller.SaveUsuarioAsync(usuario, novaSenha, perfisIds))
                {
                    MessageBox.Show("Usuário salvo com sucesso!");
                    await CarregarUsuariosAsync();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Falha ao salvar o usuário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_usuarioSelecionado == null)
            {
                MessageBox.Show("Selecione um usuário para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Tem certeza que deseja excluir o usuário '{_usuarioSelecionado.Nome}'?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (await _controller.DeleteUsuarioAsync(_usuarioSelecionado.Id))
                    {
                        MessageBox.Show("Usuário excluído com sucesso!");
                        await CarregarUsuariosAsync();
                        LimparFormulario();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao excluir o usuário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Impede que o usuário desmarque o último item, garantindo que "Administrador"
        // não possa remover o seu próprio perfil de Admin (se for o único que ele tem).
        private void clbPerfis_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked && clbPerfis.CheckedItems.Count <= 1)
            {
                // Se este é o último item marcado, impede de desmarcar
                e.NewValue = CheckState.Checked;
                MessageBox.Show("O usuário deve ter pelo menos um perfil.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}