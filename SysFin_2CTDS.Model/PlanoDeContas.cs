using System.Collections.Generic;

namespace SysFin_2CTDS.Model
{
    public class PlanoDeContas
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }

        // 'R' para Receita, 'D' para Despesa
        public char Tipo { get; set; }

        /// <summary>
        /// Propriedade somente leitura para exibir o tipo de forma amigável.
        /// </summary>
        public string TipoDisplay
        {
            get
            {
                return (Tipo == 'R') ? "Receita" : "Despesa";
            }
        }
    }

    /// <summary>
    /// Classe auxiliar para preencher o ComboBox de Tipo (R/D).
    /// </summary>
    public class TipoConta
    {
        public string Nome { get; set; } = "";
        public char Valor { get; set; }
    }
}