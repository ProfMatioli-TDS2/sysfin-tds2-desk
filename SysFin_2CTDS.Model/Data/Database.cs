using Microsoft.Data.SqlClient;
using System.Configuration; // Necessário para ler o App.config

namespace SysFin_2CTDS.Model.Data {
    public class Database {
        // Este método agora lê diretamente a string de conexão do App.config do projeto em execução.
        private static string GetConnectionString() {
            // O ConfigurationManager encontra o App.config automaticamente.
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static SqlConnection GetConnection() {
            return new SqlConnection(GetConnectionString());
        }
    }
}

