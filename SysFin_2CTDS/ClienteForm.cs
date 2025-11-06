using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Models;
using System;
using System.Threading.Tasks; // Adicionado para Async
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    // Herda diretamente de Form, SEM FrmCadastroBase
    public partial class ClienteForm : Form
    {
        // Instância do controller que contém a lógica de negócio
        private readonly ClienteController _clienteController;
        // Armazena o cliente atualmente selecionado na grade
        private Cliente? _clienteSelecionado; // Adicionado '?'

        public ClienteForm()
        {
            InitializeComponent();
            // Inicializa o controller
            _clienteController = new ClienteController();
        }

        // Evento Load agora é async
        private async void ClienteForm_Load(object? sender, EventArgs e)
        {
            ConfigurarColunasGrid();
            await CarregarClientes();
        }

        private void ConfigurarColunasGrid()
        {
            // Configura a grade para não gerar colunas automaticamente
            dgvClientes.AutoGenerateColumns = false;
            // Limpa colunas existentes para evitar duplicação
            dgvClientes.Columns.Clear();

            // Adiciona as colunas manualmente
            dgvClientes.Columns.Add("Id", "ID");
            dgvClientes.Columns.Add("Nome", "Nome");
            dgvClientes.Columns.Add("CpfCnpj", "CPF/CNPJ");
            dgvClientes.Columns.Add("Email", "E-mail");

            // Define qual propriedade do objeto Cliente preencherá cada coluna
            dgvClientes.Columns["Id"].DataPropertyName = "Id";
            dgvClientes.Columns["Nome"].DataPropertyName = "Nome";
            dgvClientes.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvClientes.Columns["CpfCnpj"].DataPropertyName = "CpfCnpj";
            dgvClientes.Columns["Email"].DataPropertyName = "Email";
        }

        private async Task CarregarClientes()
        {
            try
            {
                // Busca os dados do banco através do controller e preenche a grade
                dgvClientes.DataSource = await _clienteController.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Carregar Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void btnNovo_Click(object? sender, EventArgs e)
        {
            LimparFormulario();
        }

        // Evento Salvar agora é async
        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            // Se _clienteSelecionado for nulo, cria um novo objeto. Senão, usa o existente.
            var cliente = _clienteSelecionado ?? new Cliente();

            // Lê dados tratando strings vazias como null
            cliente.Nome = string.IsNullOrWhiteSpace(txtNome.Text) ? null : txtNome.Text;
            cliente.CpfCnpj = string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? null : txtCpfCnpj.Text;
            cliente.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
            cliente.Telefone = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text;

            try
            {
                // Chama o método SaveAsync do controller
                if (await _clienteController.SaveAsync(cliente))
                {
                    MessageBox.Show("Cliente salvo com sucesso!");
                    LimparFormulario();
                    await CarregarClientes(); // Recarrega
                }
                else
                {
                    MessageBox.Show("Falha ao salvar o cliente. Verifique os dados e tente novamente.");
                }
            }
            catch (Exception ex)
            {
                // Captura erros (ex: CPF/CNPJ duplicado)
                MessageBox.Show(ex.Message, "Erro ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento Excluir agora é async
        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_clienteSelecionado != null)
            {
                var result = MessageBox.Show("Tem certeza que deseja excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (await _clienteController.DeleteAsync(_clienteSelecionado.Id))
                        {
                            MessageBox.Show("Cliente excluído com sucesso!");
                            LimparFormulario();
                            await CarregarClientes(); // Recarrega
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir o cliente.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro ao Excluir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para excluir.");
            }
        }

        private void dgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            // Verifica se há alguma linha selecionada
            if (dgvClientes.SelectedRows.Count > 0)
            {
                // Pega o objeto Cliente associado à linha selecionada
                _clienteSelecionado = dgvClientes.SelectedRows[0].DataBoundItem as Cliente;

                if (_clienteSelecionado != null)
                {
                    // Preenche os campos do formulário com os dados do cliente selecionado
                    txtNome.Text = _clienteSelecionado.Nome;
                    txtCpfCnpj.Text = _clienteSelecionado.CpfCnpj;
                    txtEmail.Text = _clienteSelecionado.Email;
                    txtTelefone.Text = _clienteSelecionado.Telefone;
                }
            }
        }
    }
}