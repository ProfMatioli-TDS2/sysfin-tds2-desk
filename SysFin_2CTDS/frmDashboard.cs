using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmDashboard : Form
    {
        private readonly DashboardController _controller;

        public frmDashboard()
        {
            InitializeComponent();
            _controller = new DashboardController();
        }

        private async void frmDashboard_Load(object sender, EventArgs e)
        {
            await CarregarDadosAsync();
        }

        private async Task CarregarDadosAsync()
        {
            try
            {
                DashboardMetrics metricas = await _controller.CarregarMetricasAsync();
                CultureInfo br = new CultureInfo("pt-BR");

                lblSaldoTotal.Text = metricas.SaldoTotalCaixa.ToString("C", br);
                lblSaldoTotal.ForeColor = (metricas.SaldoTotalCaixa >= 0) ? Color.DarkGreen : Color.Firebrick;

                lblVendasMes.Text = metricas.VendasDoMes.ToString("C", br);
                lblComprasMes.Text = metricas.ComprasDoMes.ToString("C", br);

                lblEstoqueBaixo.Text = metricas.ProdutosEstoqueBaixo.ToString();
                if (metricas.ProdutosEstoqueBaixo > 0)
                {
                    lblEstoqueBaixo.ForeColor = Color.DarkOrange;
                }

                lvUltimosLancamentos.Items.Clear();
                if (metricas.UltimosLancamentos != null)
                {
                    foreach (var lancamento in metricas.UltimosLancamentos)
                    {
                        var item = new ListViewItem(lancamento.DataMovimento.ToString("dd/MM HH:mm"));
                        item.SubItems.Add(lancamento.Descricao);
                        string valorFormatado = lancamento.Valor.ToString("C", br);

                        if (lancamento.Tipo == 'E')
                        {
                            item.SubItems.Add(valorFormatado);
                            item.ForeColor = Color.DarkGreen;
                        }
                        else
                        {
                            item.SubItems.Add(valorFormatado);
                            item.ForeColor = Color.Firebrick;
                        }
                        lvUltimosLancamentos.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar o dashboard: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}