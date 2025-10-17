using System;


namespace SysFin_2CTDS.Models
{
   
    public class Compra
    {
        public DateTime DataCompra
        {
            get; set;
        }
        public string NomeFornecedor
        {
            get; set;
        }
        public decimal ValorTotal
        {
            get; set;
        }
    }
}