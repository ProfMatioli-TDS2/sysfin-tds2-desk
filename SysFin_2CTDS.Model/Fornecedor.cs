using System.ComponentModel.DataAnnotations;

namespace SysFin_2CTDS.Model
{

    // Esta classe representa a tabela 'fornecedores' do banco de dados.
    public class Fornecedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do fornecedor é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do fornecedor deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O CNPJ do fornecedor é obrigatório.")]
        [StringLength(18, ErrorMessage = "O CNPJ do fornecedor deve ter no máximo 18 caracteres.")]
        public string? Cnpj { get; set; }

        [StringLength(100, ErrorMessage = "O e-mail do fornecedor deve ter no máximo 100 caracteres.")]
        public string? Email { get; set; }

        [StringLength(20, ErrorMessage = "O telefone do fornecedor deve ter no máximo 20 caracteres.")]
        public string? Telefone { get; set; }


        public bool CnpjTem14Digitos()
        {
            if (string.IsNullOrWhiteSpace(Cnpj))
                return false;

            var apenasNumeros = new string(Cnpj.Where(char.IsDigit).ToArray());
            return apenasNumeros.Length == 14;
        }



        public bool CnpjValido()
        {
            if (string.IsNullOrWhiteSpace(Cnpj))
                return false;

            string cnpj = new string(Cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string digitosVerificadores = cnpj.Substring(12, 2);
            return digitosVerificadores == $"{digito1}{digito2}";
        }

    }
}
