using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace SysFin_2CTDS.Controller
{
    public class ProdutoController
    {
        public string CadastrarProduto(string nome, string descricao, decimal precoVenda, int estoqueInicial)
        {
            using (var connection = Database.GetConnection())
            {
                var sql = "INSERT INTO produtos (nome, descricao, preco_venda, estoque_atual) VALUES (@nome, @descricao, @preco_venda, @estoque_atual)";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@descricao", descricao);
                    command.Parameters.AddWithValue("@preco_venda", precoVenda);
                    command.Parameters.AddWithValue("@estoque_atual", estoqueInicial);

                    try
                    {
                        connection.Open();
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            return $"Produto '{nome}' cadastrado com sucesso!";
                        }
                        else
                        {
                            return "Nenhuma linha foi afetada. O produto não foi cadastrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Erro ao cadastrar produto: " + ex.Message;
                    }
                }
            }
        }

        // 3. MÉTODO PARA LISTAR OS PRODUTOS DO BANCO
        public List<Produto> ListarProdutos()
        {
            var produtos = new List<Produto>();

            using (var connection = Database.GetConnection())
            {
                var sql = "SELECT * FROM produtos ORDER BY nome";
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                produtos.Add(MapearProduto(reader));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao listar produtos: " + ex.Message);
                    }
                }
            }
            return produtos;
        }

        // 4. MÉTODO PARA LISTAR PRODUTOS POR NOME (BUSCA)
        public List<Produto> ListarProdutosPorNome(string termoBusca)
        {
            if (string.IsNullOrWhiteSpace(termoBusca))
            {
                return ListarProdutos();
            }

            var produtos = new List<Produto>();
            using (var connection = Database.GetConnection())
            {
                var sql = "SELECT * FROM produtos WHERE nome LIKE @termoBusca ORDER BY nome";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@termoBusca", "%" + termoBusca + "%");

                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                produtos.Add(MapearProduto(reader));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao buscar produtos: " + ex.Message);
                    }
                }
            }
            return produtos;
        }

        // 5. MÉTODO PARA EXCLUIR PRODUTO
        public bool ExcluirProduto(int id)
        {
            using (var connection = Database.GetConnection())
            {
                var sql = "DELETE FROM produtos WHERE id = @id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao excluir produto: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        // 6. MÉTODO PARA BUSCAR UM PRODUTO ÚNICO POR ID
        public Produto BuscarProdutoPorId(int id)
        {
            using (var connection = Database.GetConnection())
            {
                var sql = "SELECT * FROM produtos WHERE id = @id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearProduto(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao buscar produto por ID: " + ex.Message);
                    }
                }
            }
            return null;
        }

        // 7. MÉTODO PARA ATUALIZAR O PRODUTO
        public string AtualizarProduto(int id, string nome, string descricao, decimal precoVenda)
        {
            using (var connection = Database.GetConnection())
            {
                var sql = "UPDATE produtos SET nome = @nome, descricao = @descricao, preco_venda = @preco_venda WHERE id = @id";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@descricao", descricao);
                    command.Parameters.AddWithValue("@preco_venda", precoVenda);

                    try
                    {
                        connection.Open();
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            return $"Produto '{nome}' atualizado com sucesso!";
                        }
                        else
                        {
                            return "Produto não encontrado. A atualização falhou.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Erro ao atualizar produto: " + ex.Message;
                    }
                }
            }
        }

        // 8. MÉTODO AUXILIAR PARA MAPEAR O PRODUTO
        private Produto MapearProduto(SqlDataReader reader)
        {
            return new Produto
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nome = reader.GetString(reader.GetOrdinal("nome")),
                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? "" : reader.GetString(reader.GetOrdinal("descricao")),
                PrecoVenda = reader.GetDecimal(reader.GetOrdinal("preco_venda")),
                EstoqueAtual = reader.GetInt32(reader.GetOrdinal("estoque_atual"))
            };
        }
    }
}