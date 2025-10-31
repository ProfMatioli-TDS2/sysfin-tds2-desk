using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel; // Adicionado para BindingList
using System.Threading.Tasks; // Adicionado para Async

namespace SysFin_2CTDS.Controller
{
    public class CompraController
    {
        public async Task<List<Compra>> GetComprasPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var listaCompras = new List<Compra>();
            using (var connection = Database.GetConnection())
            {
                string sql = @"
                    SELECT 
                        c.data_compra,
                        f.nome AS nome_fornecedor,
                        c.valor_total
                    FROM compras c
                    JOIN fornecedores f ON c.id_fornecedor = f.id
                    WHERE c.data_compra BETWEEN @dataInicial AND @dataFinal
                    ORDER BY c.data_compra ASC";

                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@dataInicial", dataInicial);
                command.Parameters.AddWithValue("@dataFinal", dataFinal);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listaCompras.Add(new Compra
                            {
                                DataCompra = reader.GetDateTime(reader.GetOrdinal("data_compra")),
                                NomeFornecedor = reader.IsDBNull(reader.GetOrdinal("nome_fornecedor")) ? null : reader.GetString(reader.GetOrdinal("nome_fornecedor")),
                                ValorTotal = reader.GetDecimal(reader.GetOrdinal("valor_total"))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Lança o erro para a View (Formulário) tratar
                    throw new Exception("Erro ao buscar compras por período: " + ex.Message, ex);
                }
            }
            return listaCompras;
        }

        // MUDANÇA: O método agora é 'async Task' e aceita BindingList
        public async Task RegistrarCompra(int fornecedorId, DateTime dataDaCompra, decimal valorTotal, BindingList<Compra> itens)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();
                // Inicia a transação
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Inserir o registro principal na tabela 'compras'
                        var sqlCompra = "INSERT INTO compras (id_fornecedor, data_compra, valor_total) VALUES (@id_fornecedor, @data_compra, @valor_total); SELECT SCOPE_IDENTITY();";
                        int compraId;
                        using (var cmdCompra = new SqlCommand(sqlCompra, connection, transaction))
                        {
                            cmdCompra.Parameters.AddWithValue("@id_fornecedor", fornecedorId);
                            cmdCompra.Parameters.AddWithValue("@data_compra", dataDaCompra);
                            cmdCompra.Parameters.AddWithValue("@valor_total", valorTotal);

                            // Executa e pega o ID da compra recém-criada
                            var result = await cmdCompra.ExecuteScalarAsync();
                            compraId = Convert.ToInt32(result);
                        }

                        // 2. Inserir cada item na tabela 'itens_compra' E atualizar o estoque
                        foreach (var item in itens)
                        {
                            // 2a. Inserir o item da compra
                            var sqlItem = "INSERT INTO itens_compra (id_compra, id_produto, quantidade, valor_unitario) VALUES (@id_compra, @id_produto, @quantidade, @valor_unitario)";
                            using (var cmdItem = new SqlCommand(sqlItem, connection, transaction))
                            {
                                cmdItem.Parameters.AddWithValue("@id_compra", compraId);
                                cmdItem.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                                cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                                cmdItem.Parameters.AddWithValue("@valor_unitario", item.ValorUnitario);
                                await cmdItem.ExecuteNonQueryAsync();
                            }

                            // 2b. Atualizar o estoque do produto
                            var sqlEstoque = "UPDATE produtos SET estoque_atual = estoque_atual + @quantidade WHERE id = @id_produto";
                            using (var cmdEstoque = new SqlCommand(sqlEstoque, connection, transaction))
                            {
                                cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                                cmdEstoque.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                                await cmdEstoque.ExecuteNonQueryAsync();
                            }
                        }

                        // Se tudo deu certo, 'commita' a transação
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Se algo deu errado, 'rollback' (desfaz) tudo
                        transaction.Rollback();
                        throw; // Lança o erro para o formulário (View)
                    }
                }
            }
        }
    }
}

