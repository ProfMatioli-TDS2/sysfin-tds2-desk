using System.Collections.Generic;
using SysFin_2CTDS.Model;

namespace SysFin_2CTDS.Model
{
    public class DashboardMetrics
    {
        public decimal SaldoTotalCaixa { get; set; }
        public decimal VendasDoMes { get; set; }
        public decimal ComprasDoMes { get; set; }
        public int ProdutosEstoqueBaixo { get; set; }
        public List<Produto>? TopProdutosVendidos { get; set; }
        public List<MovimentoCaixa>? UltimosLancamentos { get; set; }

        public DashboardMetrics()
        {
            TopProdutosVendidos = new List<Produto>();
            UltimosLancamentos = new List<MovimentoCaixa>();
        }
    }
}