using System;
using System.Collections.Generic;

namespace SysFin_2CTDS.Model
{
    /// <summary>
    /// Classe estática para armazenar a sessão global (conforme guia PDF).
    /// </summary>
    public static class SessionManager
    {
        public static Usuario? CurrentUser { get; private set; }

        public static void Login(Usuario usuario)
        {
            CurrentUser = usuario;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        /// <summary>
        /// Verifica se o usuário logado tem um perfil específico.
        /// </summary>
        public static bool HasRole(string perfil)
        {
            // O operador '?? false' garante que se CurrentUser for nulo, o resultado é falso.
            return CurrentUser?.HasRole(perfil) ?? false;
        }
    }
}