using System;

namespace SysFin_2CTDS.Model
{
    /// <summary>
    /// Model para o Relatório de Vendas (Tarefa 13).
    /// RENOMEADO para RelatorioVenda para evitar conflito.
    /// </summary>
    public class RelatorioVenda
    {
        public DateTime DataVenda { get; set; }
        public string? NomeCliente { get; set; }
        public decimal ValorTotal { get; set; }
    }
}