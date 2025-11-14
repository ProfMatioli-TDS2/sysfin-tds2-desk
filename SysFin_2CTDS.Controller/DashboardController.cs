using SysFin_2CTDS.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Controller
{
    public class DashboardController
    {
        private readonly MovimentoCaixaController _movimentoCaixaController;
        private readonly VendaController _vendaController;
        private readonly CompraController _compraController;
        private readonly ProdutoController _produtoController;

        public DashboardController()
        {
            _movimentoCaixaController = new MovimentoCaixaController();
            _vendaController = new VendaController();
            _compraController = new CompraController();
            _produtoController = new ProdutoController();
        }

        public async Task<DashboardMetrics> CarregarMetricasAsync()
        {
            var metricas = new DashboardMetrics();
            try
            {
                var dataInicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var dataFimMes = DateTime.Now;

                var taskSaldo = _movimentoCaixaController.GetSaldoTotalAsync();
                var taskVendas = _vendaController.GetVendasPorPeriodoAsync(dataInicioMes, dataFimMes);
                var taskCompras = _compraController.GetComprasPorPeriodoAsync(dataInicioMes, dataFimMes);
                var taskEstoque = _produtoController.ListarProdutosAsync();
                var taskLancamentos = _movimentoCaixaController.GetMovimentosPorPeriodoAsync(DateTime.Now.AddDays(-30), DateTime.Now);

                await Task.WhenAll(taskSaldo, taskVendas, taskCompras, taskEstoque, taskLancamentos);

                metricas.SaldoTotalCaixa = taskSaldo.Result;
                metricas.VendasDoMes = taskVendas.Result.Sum(v => v.ValorTotal);
                metricas.ComprasDoMes = taskCompras.Result.Sum(c => c.ValorTotal);
                metricas.ProdutosEstoqueBaixo = taskEstoque.Result.Count(p => p.EstoqueAtual < 50);

                var ultimos = taskLancamentos.Result;
                metricas.UltimosLancamentos = ultimos.OrderByDescending(m => m.DataMovimento).Take(5).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar métricas do dashboard: " + ex.Message);
            }
            return metricas;
        }
    }
}