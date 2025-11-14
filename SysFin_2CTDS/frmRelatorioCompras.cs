using SysFin_2CTDS.Controller;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmRelatorioCompras : Form
    {
        private readonly CompraController _controller;

        public frmRelatorioCompras()
        {
            InitializeComponent();
            _controller = new CompraController();
        }

        private async void frmRelatorioCompras_Load(object? sender, EventArgs e)
        {
            ConfigurarGrid();
            dtpInicial.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFinal.Value = DateTime.Now;

            await CarregarRelatorioAsync();
        }

        private void ConfigurarGrid()
        {
            dgvRelatorio.AutoGenerateColumns = false;
            dgvRelatorio.Columns.Clear();

            dgvRelatorio.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Data",
                DataPropertyName = "DataCompra",
                HeaderText = "Data da Compra",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });
            dgvRelatorio.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fornecedor",
                DataPropertyName = "NomeFornecedor",
                HeaderText = "Fornecedor",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvRelatorio.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Valor",
                DataPropertyName = "ValorTotal",
                HeaderText = "Valor Total (R$)",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private async Task CarregarRelatorioAsync()
        {
            try
            {
                var relatorio = await _controller.GetComprasPorPeriodoAsync(dtpInicial.Value, dtpFinal.Value);
                dgvRelatorio.DataSource = relatorio;

                decimal totalPeriodo = relatorio.Sum(item => item.ValorTotal);

                lblTotalPeriodo.Text = totalPeriodo.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
                lblTotalPeriodo.ForeColor = Color.Firebrick; // Compras são sempre despesas
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Carregar Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnBuscar_Click(object? sender, EventArgs e)
        {
            await CarregarRelatorioAsync();
        }
    }
}