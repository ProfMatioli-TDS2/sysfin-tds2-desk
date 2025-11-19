using System;
using System.Collections.Generic;
using System.Linq;

namespace SysFin_2CTDS.Model
{
    /// <summary>
    /// Representa o usuário logado na sessão (como no PDF).
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Login { get; set; }
        public bool Ativo { get; set; }

        /// <summary>
        /// Lista dos nomes dos perfis (ex: "Administrador", "Vendedor").
        /// </summary>
        public List<string> Perfis { get; set; } = new List<string>();

        /// <summary>
        /// Método auxiliar (como no PDF) para verificar se o usuário tem um perfil.
        /// </summary>
        public bool HasRole(string perfil) => Perfis.Contains(perfil);
    }
}