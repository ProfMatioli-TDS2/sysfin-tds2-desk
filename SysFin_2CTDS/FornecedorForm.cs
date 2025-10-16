using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class FornecedorForm : Form
    {
        private readonly FornecedorController _fornecedorController;
        private Fornecedor _fornecedorSelecionado;
        private string _currentOrderBy = "nome"; // Padrão de ordenação inicial
        private string _currentDirection = "ASC"; // Padrão de direção inicial

        public FornecedorForm()
        {
            InitializeComponent();
            _fornecedorController = new FornecedorController();
            // Configura o DataGridView para permitir ordenação por clique no cabeçalho
            dgvFornecedores.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvFornecedores_ColumnHeaderMouseClick);
        }

        private void FornecedorForm_Load(object sender, EventArgs e)
        {
            CarregarFornecedores();
        }

        private void CarregarFornecedores()
        {
            dgvFornecedores.AutoGenerateColumns = false;
            if (dgvFornecedores.Columns.Count == 0)
            {
                dgvFornecedores.Columns.Add("Id", "ID");
                dgvFornecedores.Columns.Add("Nome", "Nome");
                dgvFornecedores.Columns.Add("Cnpj", "CNPJ");
                dgvFornecedores.Columns.Add("Email", "E-mail");
                dgvFornecedores.Columns.Add("Telefone", "Telefone");

                dgvFornecedores.Columns["Id"].DataPropertyName = "Id";
                dgvFornecedores.Columns["Nome"].DataPropertyName = "Nome";
                dgvFornecedores.Columns["Cnpj"].DataPropertyName = "Cnpj";
                dgvFornecedores.Columns["Email"].DataPropertyName = "Email";
                dgvFornecedores.Columns["Telefone"].DataPropertyName = "Telefone";
            }

            // Chama o controller com os parâmetros de ordenação atuais
            dgvFornecedores.DataSource = _fornecedorController.GetAll(_currentOrderBy, _currentDirection);
        }

        private void LimparFormulario()
        {
            _fornecedorSelecionado = null;
            txtNome.Clear();
            txtCnpj.Clear();
            txtEmail.Clear();
            mtbTelefone.Clear(); // MODIFICADO
            dgvFornecedores.ClearSelection();
            txtNome.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var fornecedor = _fornecedorSelecionado ?? new Fornecedor();
            fornecedor.Nome = txtNome.Text;
            fornecedor.Cnpj = txtCnpj.Text;
            fornecedor.Email = txtEmail.Text;
            fornecedor.Telefone = mtbTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""); // MODIFICADO para remover máscara antes de salvar

            var errors = _fornecedorController.Save(fornecedor);

            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Erros de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Fornecedor salvo com sucesso!");
                LimparFormulario();
                CarregarFornecedores();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_fornecedorSelecionado != null)
            {
                var result = MessageBox.Show("Tem certeza que deseja excluir este fornecedor?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (_fornecedorController.Delete(_fornecedorSelecionado.Id))
                    {
                        MessageBox.Show("Fornecedor excluído com sucesso!");
                        LimparFormulario();
                        CarregarFornecedores();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao excluir o fornecedor. Verifique se há dependências ou tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um fornecedor para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvFornecedores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFornecedores.SelectedRows.Count > 0)
            {
                _fornecedorSelecionado = dgvFornecedores.SelectedRows[0].DataBoundItem as Fornecedor;

                if (_fornecedorSelecionado != null)
                {
                    txtNome.Text = _fornecedorSelecionado.Nome;
                    txtCnpj.Text = _fornecedorSelecionado.Cnpj;
                    txtEmail.Text = _fornecedorSelecionado.Email;
                    mtbTelefone.Text = _fornecedorSelecionado.Telefone; // MODIFICADO
                }
            }
        }

        private void dgvFornecedores_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string clickedColumnName = dgvFornecedores.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(clickedColumnName))
            {
                clickedColumnName = dgvFornecedores.Columns[e.ColumnIndex].Name;
            }

            string columnToOrderBy = clickedColumnName.ToLower() switch
            {
                "id" => "id",
                "nome" => "nome",
                "cnpj" => "cnpj",
                "email" => "email",
                "telefone" => "telefone",
                _ => "nome",
            };

            if (_currentOrderBy == columnToOrderBy)
            {
                _currentDirection = (_currentDirection == "ASC") ? "DESC" : "ASC";
            }
            else
            {
                _currentOrderBy = columnToOrderBy;
                _currentDirection = "ASC";
            }

            CarregarFornecedores();
        }
    }
}
