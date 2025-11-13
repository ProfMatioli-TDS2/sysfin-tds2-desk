using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Controller
{
    public class UsuarioController
    {
        /// <summary>
        /// (JÁ EXISTE) Tenta autenticar um usuário e carregar seus perfis.
        /// </summary>
        public async Task<Usuario?> LoginAsync(string login, string senha)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                // 1. Encontrar o usuário
                // (Simulando senha em texto puro, como no Passo 42)
                var sqlLogin = "SELECT id, nome, ativo FROM usuarios WHERE login = @login AND senha_hash = @senha";

                Usuario? usuario = null;

                using (var cmdLogin = new SqlCommand(sqlLogin, connection))
                {
                    cmdLogin.Parameters.AddWithValue("@login", login);
                    cmdLogin.Parameters.AddWithValue("@senha", senha);

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
                            return null;
                        }
                    }
                }

                // 2. Carregar os perfis
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

        // --- MÉTODOS NOVOS (Tarefa 16) ---

        /// <summary>
        /// (NOVO) Busca todos os usuários (exceto o logado) para o grid.
        /// </summary>
        public async Task<List<Usuario>> GetAllUsuariosAsync(int idUsuarioLogado)
        {
            var lista = new List<Usuario>();
            using (var connection = Database.GetConnection())
            {
                // Busca todos, exceto o usuário que está logado (para não se auto-excluir)
                var sql = "SELECT id, nome, login, ativo FROM usuarios WHERE id != @idUsuarioLogado ORDER BY nome";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@idUsuarioLogado", idUsuarioLogado);
                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Usuario
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Login = reader.GetString(reader.GetOrdinal("login")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo"))
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar usuários: " + ex.Message);
                }
            }
            return lista;
        }

        /// <summary>
        /// (NOVO) Busca todos os perfis (Admin, Vendedor, etc.) do banco.
        /// </summary>
        public async Task<List<Perfil>> GetAllPerfisAsync()
        {
            var lista = new List<Perfil>();
            using (var connection = Database.GetConnection())
            {
                var sql = "SELECT id, nome FROM perfis ORDER BY nome";
                var command = new SqlCommand(sql, connection);
                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Perfil
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar perfis: " + ex.Message);
                }
            }
            return lista;
        }

        /// <summary>
        /// (NOVO) Busca os IDs dos perfis de UM usuário específico.
        /// </summary>
        public async Task<List<int>> GetPerfisDoUsuarioAsync(int idUsuario)
        {
            var listaIds = new List<int>();
            using (var connection = Database.GetConnection())
            {
                var sql = "SELECT id_perfil FROM usuario_perfis WHERE id_usuario = @idUsuario";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listaIds.Add(reader.GetInt32(0));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar perfis do usuário: " + ex.Message);
                }
            }
            return listaIds;
        }

        /// <summary>
        /// (NOVO) Salva (Insere ou Atualiza) um usuário e seus perfis.
        /// </summary>
        public async Task<bool> SaveUsuarioAsync(Usuario usuario, string novaSenha, List<int> perfisIds)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new Exception("O Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(usuario.Login))
                throw new Exception("O Login é obrigatório.");
            if (usuario.Id == 0 && string.IsNullOrWhiteSpace(novaSenha)) // Se é NOVO usuário
                throw new Exception("A Senha é obrigatória para novos usuários.");
            if (perfisIds.Count == 0)
                throw new Exception("O usuário deve ter pelo menos um perfil.");

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();
                using (var transaction = (SqlTransaction)await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 1. Salvar o Usuário (INSERT ou UPDATE)
                        int usuarioId = usuario.Id;

                        if (usuario.Id > 0) // UPDATE
                        {
                            string sqlUpdate = "UPDATE usuarios SET nome = @nome, login = @login, ativo = @ativo";
                            // Se o campo senha foi preenchido, atualiza a senha
                            if (!string.IsNullOrWhiteSpace(novaSenha))
                            {
                                sqlUpdate += ", senha_hash = @senha";
                            }
                            sqlUpdate += " WHERE id = @id";

                            var cmdUpdate = new SqlCommand(sqlUpdate, connection, transaction);
                            cmdUpdate.Parameters.AddWithValue("@nome", usuario.Nome);
                            cmdUpdate.Parameters.AddWithValue("@login", usuario.Login);
                            cmdUpdate.Parameters.AddWithValue("@ativo", usuario.Ativo);
                            if (!string.IsNullOrWhiteSpace(novaSenha))
                            {
                                cmdUpdate.Parameters.AddWithValue("@senha", novaSenha); // Simulando hash
                            }
                            cmdUpdate.Parameters.AddWithValue("@id", usuario.Id);
                            await cmdUpdate.ExecuteNonQueryAsync();
                        }
                        else // INSERT
                        {
                            var sqlInsert = "INSERT INTO usuarios (nome, login, senha_hash, ativo) OUTPUT INSERTED.id VALUES (@nome, @login, @senha, @ativo)";

                            var cmdInsert = new SqlCommand(sqlInsert, connection, transaction);
                            cmdInsert.Parameters.AddWithValue("@nome", usuario.Nome);
                            cmdInsert.Parameters.AddWithValue("@login", usuario.Login);
                            cmdInsert.Parameters.AddWithValue("@senha", novaSenha); // Simulando hash
                            cmdInsert.Parameters.AddWithValue("@ativo", usuario.Ativo);

                            usuarioId = (int)await cmdInsert.ExecuteScalarAsync();
                        }

                        // 2. Apagar perfis antigos (só para este usuário)
                        var sqlDeletePerfis = "DELETE FROM usuario_perfis WHERE id_usuario = @idUsuario";
                        var cmdDeletePerfis = new SqlCommand(sqlDeletePerfis, connection, transaction);
                        cmdDeletePerfis.Parameters.AddWithValue("@idUsuario", usuarioId);
                        await cmdDeletePerfis.ExecuteNonQueryAsync();

                        // 3. Inserir os novos perfis
                        var sqlInsertPerfil = "INSERT INTO usuario_perfis (id_usuario, id_perfil) VALUES (@idUsuario, @idPerfil)";
                        foreach (int perfilId in perfisIds)
                        {
                            var cmdInsertPerfil = new SqlCommand(sqlInsertPerfil, connection, transaction);
                            cmdInsertPerfil.Parameters.AddWithValue("@idUsuario", usuarioId);
                            cmdInsertPerfil.Parameters.AddWithValue("@idPerfil", perfilId);
                            await cmdInsertPerfil.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        await transaction.RollbackAsync();
                        if (ex.Number == 2627 || ex.Number == 2601) // Violação de UNIQUE (Login duplicado)
                        {
                            throw new Exception("Erro: O Login informado já está em uso por outro usuário.");
                        }
                        throw new Exception("Erro de banco de dados ao salvar usuário: " + ex.Message);
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// (NOVO) Exclui um usuário (Transacional).
        /// </summary>
        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();
                using (var transaction = (SqlTransaction)await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 1. Apagar os vínculos de perfil
                        var sqlDeletePerfis = "DELETE FROM usuario_perfis WHERE id_usuario = @idUsuario";
                        var cmdDeletePerfis = new SqlCommand(sqlDeletePerfis, connection, transaction);
                        cmdDeletePerfis.Parameters.AddWithValue("@idUsuario", id);
                        await cmdDeletePerfis.ExecuteNonQueryAsync();

                        // 2. Apagar o usuário
                        var sqlDeleteUsuario = "DELETE FROM usuarios WHERE id = @idUsuario";
                        var cmdDeleteUsuario = new SqlCommand(sqlDeleteUsuario, connection, transaction);
                        cmdDeleteUsuario.Parameters.AddWithValue("@idUsuario", id);
                        int linhas = await cmdDeleteUsuario.ExecuteNonQueryAsync();

                        await transaction.CommitAsync();
                        return linhas > 0;
                    }
                    catch (SqlException ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("Erro de banco de dados ao excluir usuário: " + ex.Message);
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
    }
}