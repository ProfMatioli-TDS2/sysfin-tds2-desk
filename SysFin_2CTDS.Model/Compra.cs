using System;
using System.ComponentModel;

namespace SysFin_2CTDS.Model
{
    public class Compra
    {
        // Usado no Relatório (Preenchido pelo Controller)
        public DateTime DataCompra { get; set; }
        public string? NomeFornecedor { get; set; }
        public decimal ValorTotal { get; set; }

        // Usado no 'Carrinho' (frmRegistroCompras)
        [Browsable(false)] // Esconde do DataGridView
        public int ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Subtotal
        {
            get { return Quantidade * ValorUnitario; }
        }
    }
}