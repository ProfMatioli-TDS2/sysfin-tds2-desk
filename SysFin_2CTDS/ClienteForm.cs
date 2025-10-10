using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Models;
using System;
using System.Windows.Forms;

namespace SysFin_2CTDS.View {
    public partial class ClienteForm : Form {
        // Instância do controller que contém a lógica de negócio
        private readonly ClienteController _clienteController;
        // Armazena o cliente atualmente selecionado na grade
        private Cliente _clienteSelecionado;

        public ClienteForm() {
            InitializeComponent();
            // Inicializa o controller
            _clienteController = new ClienteController();
        }

        private void ClienteForm_Load(object sender, EventArgs e) {
            CarregarClientes();
        }

        private void CarregarClientes() {
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
            dgvClientes.Columns["CpfCnpj"].DataPropertyName = "CpfCnpj";
            dgvClientes.Columns["Email"].DataPropertyName = "Email";

            // Busca os dados do banco através do controller e preenche a grade
            dgvClientes.DataSource = _clienteController.GetAll();
        }

        private void LimparFormulario() {
            _clienteSelecionado = null;
            txtNome.Clear();
            txtCpfCnpj.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            dgvClientes.ClearSelection();
            txtNome.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e) {
            LimparFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e) {
            // Se _clienteSelecionado for nulo, cria um novo objeto. Senão, usa o existente.
            var cliente = _clienteSelecionado ?? new Cliente();
            cliente.Nome = txtNome.Text;
            cliente.CpfCnpj = txtCpfCnpj.Text;
            cliente.Email = txtEmail.Text;
            cliente.Telefone = txtTelefone.Text;

            // Chama o método Save do controller
            if(_clienteController.Save(cliente)) {
                MessageBox.Show("Cliente salvo com sucesso!");
                LimparFormulario();
                CarregarClientes();
            } else {
                MessageBox.Show("Falha ao salvar o cliente. Verifique os dados e tente novamente.");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e) {
            if(_clienteSelecionado != null) {
                var result = MessageBox.Show("Tem certeza que deseja excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo);
                if(result == DialogResult.Yes) {
                    if(_clienteController.Delete(_clienteSelecionado.Id)) {
                        MessageBox.Show("Cliente excluído com sucesso!");
                        LimparFormulario();
                        CarregarClientes();
                    } else {
                        MessageBox.Show("Falha ao excluir o cliente.");
                    }
                }
            } else {
                MessageBox.Show("Selecione um cliente para excluir.");
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e) {
            // Verifica se há alguma linha selecionada
            if(dgvClientes.SelectedRows.Count > 0) {
                // Pega o objeto Cliente associado à linha selecionada
                _clienteSelecionado = dgvClientes.SelectedRows[0].DataBoundItem as Cliente;

                if(_clienteSelecionado != null) {
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
