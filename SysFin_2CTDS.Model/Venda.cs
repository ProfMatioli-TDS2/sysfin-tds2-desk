using System;
using System.Collections.Generic;

namespace SysFin_2CTDS.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemVenda> Itens { get; set; }

        public Venda()
        {
            Itens = new List<ItemVenda>();
        }
    }
}
