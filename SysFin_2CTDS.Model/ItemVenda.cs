namespace SysFin_2CTDS.Models // Colocado no namespace Models
{
    /// <summary>
    /// Model para os itens no carrinho de Venda.
    /// </summary>
    public class ItemVenda
    {
        public int ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Subtotal => Quantidade * ValorUnitario;
    }
}