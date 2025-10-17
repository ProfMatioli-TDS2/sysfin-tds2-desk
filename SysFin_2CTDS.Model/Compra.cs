using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Model
{
        public class Compra
        {
            public int ProdutoId
            {
                get; set;
            }
            public string ProdutoNome
            {
                get; set;
            }
            public int Quantidade
            {
                get; set;
            }
            public decimal ValorUnitario
            {
                get; set;
            }
            public decimal Subtotal
            {
                get
                {
                    return Quantidade * ValorUnitario;
                }
            }
        }
    }
