using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmFluxoCaixa : Form
    {
        private readonly MovimentoCaixaController _controller;

        public frmFluxoCaixa()
        {
            InitializeComponent();
            _controller = new MovimentoCaixaController();
        }

        private async void frmFluxoCaixa_Load(object? sender, EventArgs e)
        {
            ConfigurarGrid();
            dtpInicial.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFinal.Value = DateTime.Now;

            await AtualizarTudoAsync();
        }

        private void ConfigurarGrid()
        {
            dgvMovimentos.AutoGenerateColumns = false;
            dgvMovimentos.Columns.Clear();

            dgvMovimentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Data",
                DataPropertyName = "DataMovimento",
                HeaderText = "Data",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });
            dgvMovimentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descricao",
                DataPropertyName = "Descricao",
                HeaderText = "Descrição",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvMovimentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanoConta",
                DataPropertyName = "PlanoContaDescricao",
                HeaderText = "Plano de Conta",
                Width = 150
            });
            dgvMovimentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                DataPropertyName = "TipoDisplay",
                HeaderText = "Tipo",
                Width = 80
            });
            dgvMovimentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Valor",
                DataPropertyName = "ValorFormatado",
                HeaderText = "Valor (R$)",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private async Task AtualizarTudoAsync()
        {
            await CarregarMovimentosAsync();
            await CarregarSaldoTotalAsync();
        }

        private async Task CarregarMovimentosAsync()
        {
            try
            {
                var movimentos = await _controller.GetMovimentosPorPeriodoAsync(dtpInicial.Value, dtpFinal.Value);
                dgvMovimentos.DataSource = movimentos;

                decimal saldoPeriodo = movimentos.Sum(m => m.ValorFormatado);

                lblSaldoPeriodo.Text = saldoPeriodo.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
                lblSaldoPeriodo.ForeColor = (saldoPeriodo >= 0) ? Color.DarkGreen : Color.Firebrick;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Carregar Movimentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CarregarSaldoTotalAsync()
        {
            try
            {
                decimal saldoTotal = await _controller.GetSaldoTotalAsync();
                lblSaldoTotal.Text = saldoTotal.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
                lblSaldoTotal.ForeColor = (saldoTotal >= 0) ? Color.DarkBlue : Color.Firebrick;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Carregar Saldo Total", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMovimentos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvMovimentos.Columns[e.ColumnIndex].Name == "Valor")
            {
                if (e.Value is decimal valor)
                {
                    e.Value = valor.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
                    e.CellStyle.ForeColor = (valor >= 0) ? Color.DarkGreen : Color.Firebrick;
                }
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await AtualizarTudoAsync();
        }

        private async void btnNovoLancamento_Click(object? sender, EventArgs e)
        {
            using (var form = new frmLancamentoManual())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await AtualizarTudoAsync();
                }
            }
        }
    }
}