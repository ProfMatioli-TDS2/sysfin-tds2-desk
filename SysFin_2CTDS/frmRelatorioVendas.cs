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
    public partial class frmRelatorioVendas : Form
    {
        private readonly VendaController _controller;

        public frmRelatorioVendas()
        {
            InitializeComponent();
            _controller = new VendaController();
        }

        private async void frmRelatorioVendas_Load(object? sender, EventArgs e)
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
                DataPropertyName = "DataVenda",
                HeaderText = "Data da Venda",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });
            dgvRelatorio.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                DataPropertyName = "NomeCliente",
                HeaderText = "Cliente",
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
                var relatorio = await _controller.GetVendasPorPeriodoAsync(dtpInicial.Value, dtpFinal.Value);
                dgvRelatorio.DataSource = relatorio;

                decimal totalPeriodo = relatorio.Sum(item => item.ValorTotal);

                lblTotalPeriodo.Text = totalPeriodo.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
                lblTotalPeriodo.ForeColor = Color.DarkGreen;
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