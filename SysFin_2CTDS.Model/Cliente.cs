// O namespace deve corresponder à sua estrutura de pastas
namespace SysFin_2CTDS.Models
{
    // Esta classe representa a tabela 'clientes' do banco de dados.
    // Cada propriedade corresponde a uma coluna da tabela.
    public class Cliente
    {
        public int Id { get; set; }
        // MANTIDO: A versão 'string?' (anulável) está correta
        // para alinhar com o <Nullable>enable</Nullable> do .NET 8.
        public string? Nome { get; set; }
        public string? CpfCnpj { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}