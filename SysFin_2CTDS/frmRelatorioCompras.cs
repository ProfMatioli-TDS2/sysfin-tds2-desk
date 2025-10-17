using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmRelatorioCompras : Form
    {
        
        private readonly CompraController _compraController;

        public frmRelatorioCompras()
        {
            InitializeComponent();
            
            _compraController = new CompraController();
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            
            DateTime dataInicial = dtpDataInicial.Value;
            DateTime dataFinal = dtpDataFinal.Value;

           
            List<Compra> resultados = _compraController.GetComprasPorPeriodo(dataInicial, dataFinal);

            
            ConfigurarGrid();
            dgvResultados.DataSource = resultados;

            
            CalcularEExibirTotais(resultados);
        }

       
        private void ConfigurarGrid()
        {
            dgvResultados.AutoGenerateColumns = false;
            dgvResultados.Columns.Clear();

            dgvResultados.Columns.Add("DataCompra", "Data da Compra");
            dgvResultados.Columns.Add("NomeFornecedor", "Fornecedor");
            dgvResultados.Columns.Add("ValorTotal", "Valor Total");

            
            dgvResultados.Columns["DataCompra"].DataPropertyName = "DataCompra";
            dgvResultados.Columns["NomeFornecedor"].DataPropertyName = "NomeFornecedor";
            dgvResultados.Columns["ValorTotal"].DataPropertyName = "ValorTotal";

            
            dgvResultados.Columns["ValorTotal"].DefaultCellStyle.Format = "C2";

            
            dgvResultados.Columns["NomeFornecedor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

       
        private void CalcularEExibirTotais(List<Compra> compras)
        {
            
            int totalCompras = compras.Count();

            
            decimal valorTotal = compras.Sum(c => c.ValorTotal);

           
            lblTotalCompras.Text = $"Total de Compras: {totalCompras}";
            lblValorTotal.Text = $"Valor Total Comprado: {valorTotal:C2}";
        }
    }
}