// O namespace deve corresponder à sua estrutura de pastas
namespace SysFin_2CTDS.Models {
    // Esta classe representa a tabela 'clientes' do banco de dados.
    // Cada propriedade corresponde a uma coluna da tabela.
    public class Cliente {
        public int Id {
            get; set;
        }
        public string Nome {
            get; set;
        }
        public string CpfCnpj {
            get; set;
        }
        public string Email {
            get; set;
        }
        public string Telefone {
            get; set;
        }
    }
}
