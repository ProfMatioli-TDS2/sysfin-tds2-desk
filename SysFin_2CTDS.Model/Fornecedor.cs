// O namespace deve corresponder à sua estrutura de pastas
using System.ComponentModel.DataAnnotations;

<<<<<<< Updated upstream
namespace SysFin_2CTDS.Models {
=======
namespace SysFin_2CTDS.Models
{
>>>>>>> Stashed changes
    // Esta classe representa a tabela 'fornecedores' do banco de dados.
    public class Fornecedor
    {
        public int Id
        {
            get; set;
        }
        [Required(ErrorMessage = "O nome do fornecedor é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do fornecedor deve ter no máximo 100 caracteres.")]
<<<<<<< Updated upstream
        public string Nome {
=======
        public string Nome
        {
>>>>>>> Stashed changes
            get; set;
        }
        [Required(ErrorMessage = "O CNPJ do fornecedor é obrigatório.")]
        [StringLength(18, ErrorMessage = "O CNPJ do fornecedor deve ter no máximo 18 caracteres.")]
<<<<<<< Updated upstream
        public string Cnpj {
            get; set;
        }
        [StringLength(100, ErrorMessage = "O e-mail do fornecedor deve ter no máximo 100 caracteres.")]
        public string Email {
            get; set;
        }
        [StringLength(20, ErrorMessage = "O telefone do fornecedor deve ter no máximo 20 caracteres.")]
        public string Telefone {
=======
        public string Cnpj
        {
            get; set;
        }
        [StringLength(100, ErrorMessage = "O e-mail do fornecedor deve ter no máximo 100 caracteres.")]
        public string Email
        {
            get; set;
        }
        [StringLength(20, ErrorMessage = "O telefone do fornecedor deve ter no máximo 20 caracteres.")]
        public string Telefone
        {
>>>>>>> Stashed changes
            get; set;
        }
    }
}