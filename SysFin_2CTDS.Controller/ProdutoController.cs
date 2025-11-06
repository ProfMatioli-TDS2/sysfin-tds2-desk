using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // MUDANÇA: Adicionado

namespace SysFin_2CTDS.Controller
{
    public class ProdutoController
    {
        // MUDANÇA: Assinatura agora é async Task<string>
        public async Task<string> CadastrarProduto(string? nome, string? descricao, decimal precoVenda, int estoqueInicial)
        {
            using (var connection = Database.GetConnection())
            {
                var sql = "INSERT INTO produtos (nome, descricao, preco_venda, estoque_atual) VALUES (@nome, @descricao, @preco_venda, @estoque_atual)";

                using (var command = new SqlCommand(sql, connection))
                {
                    // MUDANÇA: Tratamento de nulos
                    command.Parameters.AddWithValue("@nome", (object)nome ?? DBNull.Value);
                    command.Parameters.AddWithValue("@descricao", (object)descricao ?? DBNull.Value);
                    command.Parameters.AddWithValue("@preco_venda", precoVenda);
                    command.Parameters.AddWithValue("@estoque_atual", estoqueInicial);

                    try
                    {
                        await connection.OpenAsync(); // MUDANÇA: Async
                        int linhasAfetadas = await command.ExecuteNonQueryAsync(); // MUDANÇA: Async

                        if (linhasAfetadas > 0)
                        {
                            return $"Produto '{nome}' cadastrado com sucesso!";
                        }
                        else
                        {
                            return "Nenhuma linha foi afetada. O produto não foi cadastrado.";
                        }
                    }
                    catch (SqlException ex) // MUDANÇA: Captura SqlException
                    {
                        throw new Exception($"Erro de banco de dados ao cadastrar: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        return "Erro ao cadastrar produto: " + ex.Message;
                    }
                }
            }
        }

        // MUDANÇA: Assinatura agora é async Task<List<Produto>>
        public async Task<List<Produto>> ListarProdutosAsync()
        {
            var produtos = new List<Produto>();

            using (var connection = Database.GetConnection())
            {
                // MUDANÇA: SELECT * removido
                var sql = "SELECT id, nome, descricao, preco_venda, estoque_atual FROM produtos ORDER BY nome";
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        await connection.OpenAsync(); // MUDANÇA: Async
                        using (var reader = await command.ExecuteReaderAsync()) // MUDANÇA: Async
                        {
                            while (await reader.ReadAsync()) // MUDANÇA: Async
                            {
                                produtos.Add(MapearProduto(reader));
                            }
                        }
                    }
                    catch (SqlException ex) // MUDANÇA: Captura SqlException
                    {
                        throw new Exception($"Erro de banco de dados ao listar produtos: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao listar produtos: " + ex.Message);
                    }
                }
            }
            return produtos;
        }

        // MUDANÇA: Assinatura agora é async Task<List<Produto>>
        public async Task<List<Produto>> ListarProdutosPorNomeAsync(string termoBusca)
        {
            if (string.IsNullOrWhiteSpace(termoBusca))
            {
                return await ListarProdutosAsync(); // MUDANÇA: Async
            }

            var produtos = new List<Produto>();
            using (var connection = Database.GetConnection())
            {
                // MUDANÇA: SELECT * removido
                var sql = "SELECT id, nome, descricao, preco_venda, estoque_atual FROM produtos WHERE nome LIKE @termoBusca ORDER BY nome";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@termoBusca", "%" + termoBusca + "%");

                    try
                    {
                        await connection.OpenAsync(); // MUDANÇA: Async
                        using (var reader = await command.ExecuteReaderAsync()) // MUDANÇA: Async
                        {
                            while (await reader.ReadAsync()) // MUDANÇA: Async
                            {
                                produtos.Add(MapearProduto(reader));
                            }
                        }
                    }
                    catch (SqlException ex) // MUDANÇA: Captura SqlException
                    {
                        throw new Exception($"Erro de banco de dados ao buscar produtos: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao buscar produtos: " + ex.Message);
                    }
                }
            }
            return produtos;
        }

        // MUDANÇA: Assinatura agora é async Task<bool>
        public async Task<bool> ExcluirProdutoAsync(int id)
        {
            using (var connection = Database.GetConnection())
            {
                var sql = "DELETE FROM produtos WHERE id = @id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        await connection.OpenAsync(); // MUDANÇA: Async
                        int linhasAfetadas = await command.ExecuteNonQueryAsync(); // MUDANÇA: Async
                        return linhasAfetadas > 0;
                    }
                    catch (SqlException ex) // MUDANÇA: Captura SqlException
                    {
                        throw new Exception($"Erro de banco de dados ao excluir: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao excluir produto: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        // MUDANÇA: Assinatura agora é async Task<Produto?>
        public async Task<Produto?> BuscarProdutoPorIdAsync(int id)
        {
            using (var connection = Database.GetConnection())
            {
                // MUDANÇA: SELECT * removido
                var sql = "SELECT id, nome, descricao, preco_venda, estoque_atual FROM produtos WHERE id = @id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        await connection.OpenAsync(); // MUDANÇA: Async
                        using (var reader = await command.ExecuteReaderAsync()) // MUDANÇA: Async
                        {
                            if (await reader.ReadAsync()) // MUDANÇA: Async
                            {
                                return MapearProduto(reader);
                            }
                        }
                    }
                    catch (SqlException ex) // MUDANÇA: Captura SqlException
                    {
                        throw new Exception($"Erro de banco de dados ao buscar produto: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao buscar produto por ID: " + ex.Message);
                    }
                }
            }
            return null;
        }

        // MUDANÇA: Assinatura agora é async Task<string>
        public async Task<string> AtualizarProdutoAsync(int id, string? nome, string? descricao, decimal precoVenda)
        {
            using (var connection = Database.GetConnection())
            {
                var sql = "UPDATE produtos SET nome = @nome, descricao = @descricao, preco_venda = @preco_venda WHERE id = @id";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    // MUDANÇA: Tratamento de nulos
                    command.Parameters.AddWithValue("@nome", (object)nome ?? DBNull.Value);
                    command.Parameters.AddWithValue("@descricao", (object)descricao ?? DBNull.Value);
                    command.Parameters.AddWithValue("@preco_venda", precoVenda);

                    try
                    {
                        await connection.OpenAsync(); // MUDANÇA: Async
                        int linhasAfetadas = await command.ExecuteNonQueryAsync(); // MUDANÇA: Async

                        if (linhasAfetadas > 0)
                        {
                            return $"Produto '{nome}' atualizado com sucesso!";
                        }
                        else
                        {
                            return "Produto não encontrado. A atualização falhou.";
                        }
                    }
                    catch (SqlException ex) // MUDANÇA: Captura SqlException
                    {
                        throw new Exception($"Erro de banco de dados ao atualizar: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        return "Erro ao atualizar produto: " + ex.Message;
                    }
                }
            }
        }

        private Produto MapearProduto(SqlDataReader reader)
        {
            return new Produto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nome = reader.IsDBNull(reader.GetOrdinal("nome")) ? null : reader.GetString(reader.GetOrdinal("nome")),
                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString(reader.GetOrdinal("descricao")),
                PrecoVenda = reader.GetDecimal(reader.GetOrdinal("preco_venda")),
                EstoqueAtual = reader.GetInt32(reader.GetOrdinal("estoque_atual"))
            };
        }
    }
}

