using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Models;
using System;
using System.Windows.Forms;
using System.Linq; // Adicionado para usar LINQ

namespace SysFin_2CTDS.View {

    public partial class FornecedorForm : Form {
        private readonly FornecedorController _fornecedorController;
        private Fornecedor _fornecedorSelecionado;

        public FornecedorForm() {
            InitializeComponent();
            _fornecedorController = new FornecedorController();
        }

        private void FornecedorForm_Load(object sender, EventArgs e) {
            CarregarFornecedores();
        }

        private void CarregarFornecedores() {
            dgvFornecedores.AutoGenerateColumns = false;
            dgvFornecedores.Columns.Clear();

            dgvFornecedores.Columns.Add("Id", "ID");
            dgvFornecedores.Columns.Add("Nome", "Nome");
            dgvFornecedores.Columns.Add("Cnpj", "CNPJ");
            dgvFornecedores.Columns.Add("Email", "E-mail");
            dgvFornecedores.Columns.Add("Telefone", "Telefone"); // Adicionada a coluna Telefone

            dgvFornecedores.Columns["Id"].DataPropertyName = "Id";
            dgvFornecedores.Columns["Nome"].DataPropertyName = "Nome";
            dgvFornecedores.Columns["Cnpj"].DataPropertyName = "Cnpj";
            dgvFornecedores.Columns["Email"].DataPropertyName = "Email";
            dgvFornecedores.Columns["Telefone"].DataPropertyName = "Telefone"; // Mapeamento da propriedade Telefone

            dgvFornecedores.DataSource = _fornecedorController.GetAll();
        }

        private void LimparFormulario() {
            _fornecedorSelecionado = null;
            txtNome.Clear();
            txtCnpj.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            dgvFornecedores.ClearSelection();
            txtNome.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e) {
            LimparFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e) {
            var fornecedor = _fornecedorSelecionado ?? new Fornecedor();
            fornecedor.Nome = txtNome.Text;
            fornecedor.Cnpj = txtCnpj.Text;
            fornecedor.Email = txtEmail.Text;
            fornecedor.Telefone = txtTelefone.Text;

            var errors = _fornecedorController.Save(fornecedor);

            if(errors.Any()) {
                MessageBox.Show(string.Join("\n", errors), "Erros de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);

            } else {
                MessageBox.Show("Fornecedor salvo com sucesso!");
                LimparFormulario();
                CarregarFornecedores();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e) {
            if(_fornecedorSelecionado != null) {
                var result = MessageBox.Show("Tem certeza que deseja excluir este fornecedor?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes) {
                    if(_fornecedorController.Delete(_fornecedorSelecionado.Id)) {
                        MessageBox.Show("Fornecedor excluído com sucesso!");
                        LimparFormulario();
                        CarregarFornecedores();
                    } else {
                        MessageBox.Show("Falha ao excluir o fornecedor. Verifique se há dependências ou tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else {
                MessageBox.Show("Selecione um fornecedor para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvFornecedores_SelectionChanged(object sender, EventArgs e) {
            if(dgvFornecedores.SelectedRows.Count > 0) {
                _fornecedorSelecionado = dgvFornecedores.SelectedRows[0].DataBoundItem as Fornecedor;

                if(_fornecedorSelecionado != null) {
                    txtNome.Text = _fornecedorSelecionado.Nome;
                    txtCnpj.Text = _fornecedorSelecionado.Cnpj;
                    txtEmail.Text = _fornecedorSelecionado.Email;
                    txtTelefone.Text = _fornecedorSelecionado.Telefone;
                }
            }
        }
    }
}
