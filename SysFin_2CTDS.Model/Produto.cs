using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Model
{
    public class Produto
    {
        public int Id { get; set; }
        // MUDANÇA: Adicionado '?' para permitir valores nulos
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal PrecoVenda { get; set; }
        public int EstoqueAtual { get; set; }
        // MUDANÇA: Removida a propriedade duplicada 'Estoque' que causava um aviso
    }
}