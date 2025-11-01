using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Models; // Corrigido
using System;
using System.Threading.Tasks; // Adicionado
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    // MUDANÇA: Herda de 'Form', não de 'FrmCadastroBase'
    public partial class ClienteForm : Form
    {
        private readonly ClienteController _clienteController;
        private Cliente? _clienteSelecionado; // MUDANÇA: '?' para nulidade

        public ClienteForm()
        {
            InitializeComponent();
            _clienteController = new ClienteController();
        }

        // MUDANÇA: Evento 'Load' (era da base)
        private async void ClienteForm_Load(object? sender, EventArgs e)
        {
            await ConfigurarEListarClientesAsync();
        }

        // MUDANÇA: Lógica de configuração e carregamento
        private async Task ConfigurarEListarClientesAsync()
        {
            try
            {
                // Configura a grade (era 'ConfigurarColunasGrid')
                dgvClientes.AutoGenerateColumns = false;
                dgvClientes.Columns.Clear();
                dgvClientes.Columns.Add("Id", "ID");
                dgvClientes.Columns.Add("Nome", "Nome");
                dgvClientes.Columns.Add("CpfCnpj", "CPF/CNPJ");
                dgvClientes.Columns.Add("Email", "E-mail");

                dgvClientes.Columns["Id"].DataPropertyName = "Id";
                dgvClientes.Columns["Nome"].DataPropertyName = "Nome";
                dgvClientes.Columns["CpfCnpj"].DataPropertyName = "CpfCnpj";
                dgvClientes.Columns["Email"].DataPropertyName = "Email";

                // Carrega os dados (era 'CarregarDados')
                dgvClientes.DataSource = await _clienteController.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MUDANÇA: Lógica de Limpar (era da base)
        private void LimparFormulario()
        {
            _clienteSelecionado = null;
            txtNome.Clear();
            txtCpfCnpj.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            dgvClientes.ClearSelection();
            txtNome.Focus();
        }

        // MUDANÇA: Evento 'SelectionChanged' (era da base)
        private void dgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                _clienteSelecionado = dgvClientes.SelectedRows[0].DataBoundItem as Cliente;
                if (_clienteSelecionado != null)
                {
                    txtNome.Text = _clienteSelecionado.Nome;
                    txtCpfCnpj.Text = _clienteSelecionado.CpfCnpj;
                    txtEmail.Text = _clienteSelecionado.Email;
                    txtTelefone.Text = _clienteSelecionado.Telefone;
                }
            }
            else
            {
                // Se nada estiver selecionado (ex: após Limpar), garante que o objeto seja nulo
                _clienteSelecionado = null;
            }
        }

        // MUDANÇA: Evento 'btnNovo_Click' (era da base)
        private void btnNovo_Click(object? sender, EventArgs e)
        {
            LimparFormulario();
        }

        // MUDANÇA: Evento 'btnSalvar_Click' (era da base)
        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            try
            {
                var cliente = _clienteSelecionado ?? new Cliente();

                // Lógica de nulidade (Passo 3)
                cliente.Nome = string.IsNullOrWhiteSpace(txtNome.Text) ? null : txtNome.Text;
                cliente.CpfCnpj = string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? null : txtCpfCnpj.Text;
                cliente.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
                cliente.Telefone = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text;

                // Lógica Async (Passo 11)
                if (await _clienteController.SaveAsync(cliente))
                {
                    MessageBox.Show("Cliente salvo com sucesso!");
                    await ConfigurarEListarClientesAsync(); // Recarrega
                    LimparFormulario(); // Limpa
                }
                else
                {
                    MessageBox.Show("Falha ao salvar o cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MUDANÇA: Evento 'btnExcluir_Click' (era da base)
        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_clienteSelecionado != null)
            {
                var result = MessageBox.Show("Tem certeza que deseja excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Lógica Async (Passo 11)
                        if (await _clienteController.DeleteAsync(_clienteSelecionado.Id))
                        {
                            MessageBox.Show("Cliente excluído com sucesso!");
                            await ConfigurarEListarClientesAsync(); // Recarrega
                            LimparFormulario(); // Limpa
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir o cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

