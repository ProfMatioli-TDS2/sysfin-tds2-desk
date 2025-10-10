// O namespace deve corresponder à sua estrutura de pastas
namespace SysFin_2CTDS.Models {
    // Esta classe representa a tabela 'fornecedores' do banco de dados.
    public class Fornecedor {
        public int Id {
            get; set;
        }
        public string Nome {
            get; set;
        }
        public string Cnpj {
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
