using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class FornecedorForm : Form
    {
        private readonly FornecedorController _fornecedorController;
        private Fornecedor? _fornecedorSelecionado;
        private string _currentOrderBy = "nome";
        private string _currentDirection = "ASC";

        // Variáveis para o relatório
        private PrintDocument printDocument = new PrintDocument();
        private List<Fornecedor> _fornecedoresParaRelatorio = new List<Fornecedor>();
        private int _indiceFornecedorAtual = 0;

        public FornecedorForm()
        {
            InitializeComponent();
            _fornecedorController = new FornecedorController();
            dgvFornecedores.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvFornecedores_ColumnHeaderMouseClick);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
        }

        private void FornecedorForm_Load(object sender, EventArgs e)
        {
            CarregarFornecedores();

            //Mascaras CNPJ e Telefone
            mtbCnpj.Mask = "00.000.000/0000-00";
            mtbTelefone.Mask = "(00) 00000-0000";

            //Melhorias no dgvFornecedores
            dgvFornecedores.ReadOnly = true; // Impede edição direta
            dgvFornecedores.AllowUserToAddRows = false; // Impede adicionar linhas
            dgvFornecedores.AllowUserToDeleteRows = false; // Impede excluir linhas
            dgvFornecedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleção por linha
            dgvFornecedores.MultiSelect = false; // Só permite selecionar uma linha
            dgvFornecedores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvFornecedores.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvFornecedores.DefaultCellStyle.BackColor = Color.White;
            dgvFornecedores.DefaultCellStyle.ForeColor = Color.Black;
            dgvFornecedores.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgvFornecedores.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvFornecedores.RowHeadersVisible = false;
            dgvFornecedores.Columns["Id"].Width = 110;
            dgvFornecedores.Columns["Nome"].Width = 200;
            dgvFornecedores.Columns["Cnpj"].Width = 200;
            dgvFornecedores.Columns["Email"].Width = 220;
            dgvFornecedores.Columns["Telefone"].Width = 173;
            dgvFornecedores.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFornecedores.Columns["Telefone"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



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
            dgvFornecedores.DataSource = _fornecedorController.GetAll(_currentOrderBy, _currentDirection);
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var fornecedor = _fornecedorSelecionado ?? new Fornecedor();
            fornecedor.Nome = txtNome.Text;
            fornecedor.Email = txtEmail.Text;
            fornecedor.Cnpj = new string(mtbCnpj.Text.Where(char.IsDigit).ToArray());
            fornecedor.Telefone = new string(mtbTelefone.Text.Where(char.IsDigit).ToArray());



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
                    mtbCnpj.Text = _fornecedorSelecionado.Cnpj;
                    txtEmail.Text = _fornecedorSelecionado.Email;
                    mtbTelefone.Text = _fornecedorSelecionado.Telefone;
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

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            _fornecedoresParaRelatorio = _fornecedorController.GetAll("nome", "ASC");

            if (_fornecedoresParaRelatorio == null || !_fornecedoresParaRelatorio.Any())
            {
                MessageBox.Show("Não há fornecedores para gerar o relatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                _indiceFornecedorAtual = 0;
                printDocument.Print();
            }
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font fonteTitulo = new Font("Arial", 16, FontStyle.Bold);
            Font fonteSubtitulo = new Font("Arial", 12, FontStyle.Regular);
            Font fonteCorpo = new Font("Arial", 10, FontStyle.Regular);
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;

            // Título
            yPos = topMargin + (count * fonteTitulo.GetHeight(e.Graphics));
            e.Graphics.DrawString("Relatório de Fornecedores", fonteTitulo, Brushes.Black, leftMargin, yPos, new StringFormat());
            count++;

            // Data de Geração
            yPos = topMargin + (count * fonteTitulo.GetHeight(e.Graphics));
            e.Graphics.DrawString($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fonteSubtitulo, Brushes.Black, leftMargin, yPos, new StringFormat());
            count += 2;

            // Cabeçalho da tabela
            yPos = topMargin + (count * fonteCorpo.GetHeight(e.Graphics));
            e.Graphics.DrawString("ID".PadRight(5) + "Nome".PadRight(40) + "CNPJ".PadRight(20) + "E-mail".PadRight(30) + "Telefone".PadRight(20), fonteCorpo, Brushes.Black, leftMargin, yPos, new StringFormat());
            count++;
            e.Graphics.DrawLine(Pens.Black, leftMargin, yPos + fonteCorpo.GetHeight(e.Graphics), e.MarginBounds.Right, yPos + fonteCorpo.GetHeight(e.Graphics));
            count++;

            // Corpo da tabela
            while (_indiceFornecedorAtual < _fornecedoresParaRelatorio.Count)
            {
                Fornecedor fornecedor = _fornecedoresParaRelatorio[_indiceFornecedorAtual];
                line = fornecedor.Id.ToString().PadRight(5) +
                       (fornecedor.Nome ?? "").PadRight(40) +
                       (fornecedor.Cnpj ?? "").PadRight(20) +
                       (fornecedor.Email ?? "").PadRight(30) +
                       (fornecedor.Telefone ?? "").PadRight(20);

                yPos = topMargin + (count * fonteCorpo.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, fonteCorpo, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
                _indiceFornecedorAtual++;

                if (yPos + fonteCorpo.GetHeight(e.Graphics) > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            // Rodapé
            yPos = e.MarginBounds.Bottom - fonteCorpo.GetHeight(e.Graphics);
            e.Graphics.DrawString($"Total de Fornecedores: {_fornecedoresParaRelatorio.Count}", fonteCorpo, Brushes.Black, leftMargin, yPos, new StringFormat());

            e.HasMorePages = false;
        }

        private void btnGerarRelatorio_Click_1(object sender, EventArgs e)
        {

        }
    }
}
