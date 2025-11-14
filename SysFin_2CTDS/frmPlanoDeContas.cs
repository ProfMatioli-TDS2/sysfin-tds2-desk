using iTextSharp.text.pdf;
using iTextSharp.text;
using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmPlanoDeContas : Form
    {
        private readonly PlanoDeContasController _controller;
        private PlanoDeContas? _contaSelecionada;
        private List<TipoConta> _tiposDeConta; // Lista para o ComboBox

        public frmPlanoDeContas()
        {
            InitializeComponent();
            _controller = new PlanoDeContasController();

            // 1. Define a lista de Tipos (R/D)
            _tiposDeConta = new List<TipoConta>
            {
                new TipoConta { Nome = "Receita", Valor = 'R' },
                new TipoConta { Nome = "Despesa", Valor = 'D' }
            };
        }

        private async void frmPlanoDeContas_Load(object? sender, EventArgs e)
        {
            ConfigurarGrid();
            ConfigurarComboBox();
            await CarregarContasAsync();
        }

        private void ConfigurarGrid()
        {
            dgvContas.AutoGenerateColumns = false;
            dgvContas.Columns.Clear();
            dgvContas.Columns.Add("Id", "ID");
            dgvContas.Columns.Add("Descricao", "Descrição");
            dgvContas.Columns.Add("TipoDisplay", "Tipo"); // Usa a propriedade "TipoDisplay"

            dgvContas.Columns["Id"].DataPropertyName = "Id";
            dgvContas.Columns["Descricao"].DataPropertyName = "Descricao";
            dgvContas.Columns["Descricao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvContas.Columns["TipoDisplay"].DataPropertyName = "TipoDisplay";
        }

        private void ConfigurarComboBox()
        {
            cboTipo.DataSource = _tiposDeConta;
            cboTipo.DisplayMember = "Nome"; // Mostra "Receita" ou "Despesa"
            cboTipo.ValueMember = "Valor"; // Salva 'R' ou 'D'
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList; // Impede digitação
        }

        private async Task CarregarContasAsync()
        {
            try
            {
                dgvContas.DataSource = null;
                dgvContas.DataSource = await _controller.GetAllAsync();
                LimparFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparFormulario()
        {
            _contaSelecionada = null;
            txtDescricao.Clear();
            cboTipo.SelectedIndex = -1;
            dgvContas.ClearSelection();
            txtDescricao.Focus();
        }

        private void dgvContas_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvContas.SelectedRows.Count > 0)
            {
                _contaSelecionada = dgvContas.SelectedRows[0].DataBoundItem as PlanoDeContas;
                if (_contaSelecionada != null)
                {
                    txtDescricao.Text = _contaSelecionada.Descricao;
                    cboTipo.SelectedValue = _contaSelecionada.Tipo;
                }
            }
            else
            {
                _contaSelecionada = null;
            }
        }

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            try
            {
                var conta = _contaSelecionada ?? new PlanoDeContas();
                conta.Descricao = txtDescricao.Text;

                if (cboTipo.SelectedValue != null)
                {
                    conta.Tipo = (char)cboTipo.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Por favor, selecione um Tipo (Receita ou Despesa).", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (await _controller.SaveAsync(conta))
                {
                    MessageBox.Show("Conta salva com sucesso!");
                    await CarregarContasAsync();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Falha ao salvar a conta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_contaSelecionada == null)
            {
                MessageBox.Show("Selecione uma conta para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Tem certeza que deseja excluir esta conta?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (await _controller.DeleteAsync(_contaSelecionada.Id))
                    {
                        MessageBox.Show("Conta excluída com sucesso!");
                        await CarregarContasAsync();
                        LimparFormulario();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao excluir a conta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // Caixa para escolher onde salvar
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "Salvar Relatório";
                save.Filter = "Arquivo PDF (*.pdf)|*.pdf";
                save.FileName = "PlanoDeContas.pdf";

                if (save.ShowDialog() != DialogResult.OK)
                    return;

                // Criando o PDF
                Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));
                doc.Open();

                // Título
                Paragraph titulo = new Paragraph("Relatório - Plano de Contas\n\n",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                // Tabela do PDF – 3 colunas
                PdfPTable tabela = new PdfPTable(3);
                tabela.WidthPercentage = 100;
                tabela.SetWidths(new float[] { 15f, 55f, 30f });

                // Cabeçalhos
                tabela.AddCell(new PdfPCell(new Phrase("ID")) { BackgroundColor = BaseColor.LIGHT_GRAY });
                tabela.AddCell(new PdfPCell(new Phrase("Descrição")) { BackgroundColor = BaseColor.LIGHT_GRAY });
                tabela.AddCell(new PdfPCell(new Phrase("Tipo")) { BackgroundColor = BaseColor.LIGHT_GRAY });

                // Preenchendo a tabela com os dados da grid
                foreach (DataGridViewRow row in dgvContas.Rows)
                {
                    if (row.DataBoundItem is PlanoDeContas conta)
                    {
                        string tipoTexto = conta.Tipo == 'R' ? "Receita" : "Despesa";

                        tabela.AddCell(conta.Id.ToString());
                        tabela.AddCell(conta.Descricao);
                        tabela.AddCell(tipoTexto);
                    }
                }

                doc.Add(tabela);
                doc.Close();

                MessageBox.Show("PDF gerado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar PDF: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}