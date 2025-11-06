using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Controller
{
    public class UsuarioController
    {
        /// <summary>
        /// Tenta autenticar um usuário e carregar seus perfis.
        /// </summary>
        /// <returns>Um objeto Usuario se for bem-sucedido, ou null se falhar.</returns>
        public async Task<Usuario?> LoginAsync(string login, string senha)
        {
            // NOTA: Verificação de Senha
            // O banco de dados espera um 'senha_hash'. Em um projeto real,
            // NUNCA guardaríamos a senha em texto puro.
            // Para este guia, vamos assumir que a senha vinda do form
            // seria "hasheada" e comparada com o banco.
            // Aqui, vamos apenas simular a busca pelo login e senha (texto puro)
            // para focar no carregamento dos PERFIS, que é o objetivo do guia.

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                // 1. Encontrar o usuário pelo login e "senha" (simulando hash)
                // Em um app real: SELECT ... WHERE login = @login
                // E a verificação da senha seria feita em C# (ex: BCrypt.Verify)

                // Para este guia, vamos simplificar (INSEGURO, APENAS DIDÁTICO):
                var sqlLogin = "SELECT id, nome, ativo FROM usuarios WHERE login = @login AND senha_hash = @senha";

                Usuario? usuario = null;

                using (var cmdLogin = new SqlCommand(sqlLogin, connection))
                {
                    cmdLogin.Parameters.AddWithValue("@login", login);
                    cmdLogin.Parameters.AddWithValue("@senha", senha); // Simulação. Deveria ser um hash.

                    using (var reader = await cmdLogin.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            if (!reader.GetBoolean(reader.GetOrdinal("ativo")))
                            {
                                throw new Exception("Usuário está inativo.");
                            }

                            usuario = new Usuario
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Login = login,
                                Ativo = true
                            };
                        }
                        else
                        {
                            // Usuário ou senha incorretos
                            return null;
                        }
                    }
                }

                // 2. Se encontrou o usuário, carregar seus perfis (a parte importante)
                var sqlPerfis = @"
                    SELECT p.nome 
                    FROM perfis p
                    JOIN usuario_perfis up ON p.id = up.id_perfil
                    WHERE up.id_usuario = @id_usuario";

                using (var cmdPerfis = new SqlCommand(sqlPerfis, connection))
                {
                    cmdPerfis.Parameters.AddWithValue("@id_usuario", usuario.Id);
                    using (var readerPerfis = await cmdPerfis.ExecuteReaderAsync())
                    {
                        while (await readerPerfis.ReadAsync())
                        {
                            usuario.Perfis.Add(readerPerfis.GetString(0));
                        }
                    }
                }

                return usuario;
            }
        }
    }
}