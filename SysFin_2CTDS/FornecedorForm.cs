using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks; // Adicionado
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    // MUDANÇA: Herda de 'Form', não de 'FrmCadastroBase'
    public partial class FornecedorForm : Form
    {
        private readonly FornecedorController _fornecedorController;
        private readonly RelatorioController _relatorioController; // Mantido
        private Fornecedor? _fornecedorSelecionado;
        private string _currentOrderBy = "nome";
        private string _currentDirection = "ASC";

        private PrintDocument printDocument = new PrintDocument();
        private List<Fornecedor> _fornecedoresParaRelatorio = new List<Fornecedor>();
        private int _indiceFornecedorAtual = 0;

        public FornecedorForm()
        {
            InitializeComponent();
            _fornecedorController = new FornecedorController();
            _relatorioController = new RelatorioController();

            // MUDANÇA: Conectando eventos manualmente
            this.Load += new System.EventHandler(this.FornecedorForm_Load);
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            this.btnGerarRelatorio.Click += new System.EventHandler(this.btnGerarRelatorio_Click);
            this.dgvFornecedores.SelectionChanged += new System.EventHandler(this.dgvFornecedores_SelectionChanged);
            this.dgvFornecedores.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFornecedores_ColumnHeaderMouseClick);
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
        }

        private async void FornecedorForm_Load(object? sender, EventArgs e)
        {
            await ConfigurarEListarFornecedoresAsync();
        }

        private async Task ConfigurarEListarFornecedoresAsync()
        {
            try
            {
                // Configuração da Grade
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

                // Estilização (como era antes)
                dgvFornecedores.ReadOnly = true;
                dgvFornecedores.AllowUserToAddRows = false;
                dgvFornecedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvFornecedores.MultiSelect = false;
                dgvFornecedores.RowHeadersVisible = false;
                dgvFornecedores.Columns["Id"].Width = 110;
                dgvFornecedores.Columns["Nome"].Width = 200;
                // ... (outras estilizações) ...

                mtbCnpj.Mask = "00.000.000/0000-00";
                mtbTelefone.Mask = "(00) 00000-0000";

                // Carregamento de Dados
                dgvFornecedores.DataSource = await _fornecedorController.GetAllAsync(_currentOrderBy, _currentDirection);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar fornecedores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparFormulario()
        {
            _fornecedorSelecionado = null;
            txtNome.Clear();
            mtbCnpj.Clear();
            txtEmail.Clear();
            mtbTelefone.Clear();
            dgvFornecedores.ClearSelection();
            txtNome.Focus();
        }

        private void dgvFornecedores_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvFornecedores.SelectedRows.Count > 0)
            {
                _fornecedorSelecionado = dgvFornecedores.SelectedRows[0].DataBoundItem as Fornecedor;
                if (_fornecedorSelecionado != null)
                {
                    txtNome.Text = _fornecedorSelecionado.Nome;
                    mtbCnpj.Text = _fornecedorSelecionado.Cnpj;
                    txtEmail.Text = _fornecedorSelecionado.Email;
                    mtbTelefone.Text = _fornecedorSelecionado.Telefone;
                }
            }
            else
            {
                _fornecedorSelecionado = null;
            }
        }

        private void btnNovo_Click(object? sender, EventArgs e)
        {
            LimparFormulario();
        }

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            try
            {
                var fornecedor = _fornecedorSelecionado ?? new Fornecedor();

                fornecedor.Nome = string.IsNullOrWhiteSpace(txtNome.Text) ? null : txtNome.Text;
                fornecedor.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
                fornecedor.Cnpj = mtbCnpj.MaskCompleted ? new string(mtbCnpj.Text.Where(char.IsDigit).ToArray()) : null;
                fornecedor.Telefone = mtbTelefone.MaskCompleted ? new string(mtbTelefone.Text.Where(char.IsDigit).ToArray()) : null;

                var errors = await _fornecedorController.SaveAsync(fornecedor);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Erros de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Fornecedor salvo com sucesso!");
                    await ConfigurarEListarFornecedoresAsync(); // Recarrega
                    LimparFormulario(); // Limpa
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_fornecedorSelecionado != null)
            {
                var result = MessageBox.Show("Tem certeza que deseja excluir este fornecedor?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (await _fornecedorController.DeleteAsync(_fornecedorSelecionado.Id))
                        {
                            MessageBox.Show("Fornecedor excluído com sucesso!");
                            await ConfigurarEListarFornecedoresAsync(); // Recarrega
                            LimparFormulario(); // Limpa
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir o fornecedor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Selecione um fornecedor para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- Lógica Específica do Fornecedor (Mantida) ---

        private async void dgvFornecedores_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            // ... (código de ordenação, como no Passo 15) ...
            string clickedColumnName = dgvFornecedores.Columns[e.ColumnIndex].DataPropertyName;
            _currentOrderBy = clickedColumnName.ToLower(); // Simplificado
            _currentDirection = (_currentDirection == "ASC") ? "DESC" : "ASC";
            await ConfigurarEListarFornecedoresAsync();
        }

        private async void btnGerarRelatorio_Click(object? sender, EventArgs e)
        {
            SaveFileDialog salvar = new SaveFileDialog();
            salvar.Filter = "Arquivo PDF (*.pdf)|*.pdf";
            salvar.FileName = "Relatorio_Fornecedores.pdf";

            if (salvar.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _relatorioController.GerarRelatorioFornecedoresAsync(salvar.FileName);
                    MessageBox.Show("Relatório gerado com sucesso!", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao Gerar PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ... (printDocument_PrintPage, etc., como no Passo 15) ...
        private void printDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            if (e == null || e.Graphics == null) return;
            // ... (Lógica de impressão do Passo 15) ...
        }
    }
}

