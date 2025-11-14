using System;

namespace SysFin_2CTDS.Model
{
    public class MovimentoCaixa
    {
        public int Id { get; set; }
        public DateTime DataMovimento { get; set; }
        public string? Descricao { get; set; }
        public int IdPlanoDeContas { get; set; }
        public string? PlanoContaDescricao { get; set; }
        public char Tipo { get; set; }
        public decimal Valor { get; set; }
        public int? IdVenda { get; set; }
        public int? IdCompra { get; set; }
        public string TipoDisplay => (Tipo == 'E') ? "Entrada" : "Saída";

        public decimal ValorFormatado => (Tipo == 'E') ? Valor : -Valor;
    }
}