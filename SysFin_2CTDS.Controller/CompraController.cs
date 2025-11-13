using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Controller
{
    public class CompraController
    {
        // (Do Passo 28)
        public async Task RegistrarCompraAsync(int fornecedorId, BindingList<Compra> itensCompra, decimal valorTotal)
        {
            if (itensCompra.Count == 0)
                throw new Exception("Não há itens no carrinho.");

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();
                using (var transaction = (SqlTransaction)await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 1. Inserir a Compra (Tabela 'compras')
                        var sqlCompra = "INSERT INTO compras (id_fornecedor, data_compra, valor_total) OUTPUT INSERTED.id VALUES (@id_fornecedor, @data, @total)";
                        var cmdCompra = new SqlCommand(sqlCompra, connection, transaction);
                        cmdCompra.Parameters.AddWithValue("@id_fornecedor", fornecedorId);
                        cmdCompra.Parameters.AddWithValue("@data", DateTime.Now);
                        cmdCompra.Parameters.AddWithValue("@total", valorTotal);

                        int compraId = (int)await cmdCompra.ExecuteScalarAsync();

                        // 2. Inserir os Itens (Tabela 'itens_compra') e Atualizar Estoque (Tabela 'produtos')
                        var sqlItem = "INSERT INTO itens_compra (id_compra, id_produto, quantidade, valor_unitario) VALUES (@id_compra, @id_produto, @qtd, @vlr_unit)";
                        var sqlEstoque = "UPDATE produtos SET estoque_atual = estoque_atual + @qtd_comprada WHERE id = @id_produto";

                        foreach (var item in itensCompra)
                        {
                            // 2a. Inserir item
                            var cmdItem = new SqlCommand(sqlItem, connection, transaction);
                            cmdItem.Parameters.AddWithValue("@id_compra", compraId);
                            cmdItem.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                            cmdItem.Parameters.AddWithValue("@qtd", item.Quantidade);
                            cmdItem.Parameters.AddWithValue("@vlr_unit", item.ValorUnitario);
                            await cmdItem.ExecuteNonQueryAsync();

                            // 2b. Atualizar estoque
                            var cmdEstoque = new SqlCommand(sqlEstoque, connection, transaction);
                            cmdEstoque.Parameters.AddWithValue("@qtd_comprada", item.Quantidade);
                            cmdEstoque.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                            await cmdEstoque.ExecuteNonQueryAsync();
                        }

                        // 3. Lançar no Movimento de Caixa (Tabela 'movimento_caixa')
                        var sqlCaixa = @"INSERT INTO movimento_caixa 
                                     (data_movimento, descricao, id_plano_de_contas, tipo, valor, id_compra) 
                                     VALUES (@data, @desc, (SELECT id FROM plano_de_contas WHERE descricao = 'Compra de Mercadorias'), 'S', @valor, @id_compra)";

                        var cmdCaixa = new SqlCommand(sqlCaixa, connection, transaction);
                        cmdCaixa.Parameters.AddWithValue("@data", DateTime.Now);
                        cmdCaixa.Parameters.AddWithValue("@desc", $"Compra (NF: {compraId})");
                        cmdCaixa.Parameters.AddWithValue("@valor", valorTotal);
                        cmdCaixa.Parameters.AddWithValue("@id_compra", compraId);
                        await cmdCaixa.ExecuteNonQueryAsync();

                        // Se tudo deu certo, commita a transação
                        await transaction.CommitAsync();
                    }
                    catch (SqlException ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("Erro ao registrar a compra no banco de dados: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("Erro inesperado: " + ex.Message);
                    }
                }
            }
        }

        // --- NOVO MÉTODO (Tarefa 14) ---
        /// <summary>
        /// Busca o relatório de Compras (volume) por período.
        /// </summary>
        public async Task<List<Compra>> GetComprasPorPeriodoAsync(DateTime dataInicial, DateTime dataFinal)
        {
            var lista = new List<Compra>();
            using (var connection = Database.GetConnection())
            {
                // Query SQL que junta compras e fornecedores
                string sql = @"
                    SELECT 
                        c.data_compra,
                        f.nome AS nome_fornecedor,
                        c.valor_total
                    FROM compras c
                    JOIN fornecedores f ON c.id_fornecedor = f.id
                    WHERE 
                        c.data_compra BETWEEN @dataInicial AND @dataFinal
                    ORDER BY 
                        c.data_compra DESC";

                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@dataInicial", dataInicial.Date);
                command.Parameters.AddWithValue("@dataFinal", dataFinal.Date.AddDays(1).AddSeconds(-1)); // Pega o dia final inteiro

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Compra
                            {
                                DataCompra = reader.GetDateTime(reader.GetOrdinal("data_compra")),
                                NomeFornecedor = reader.GetString(reader.GetOrdinal("nome_fornecedor")),
                                ValorTotal = reader.GetDecimal(reader.GetOrdinal("valor_total"))
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar relatório de compras: " + ex.Message);
                }
            }
            return lista;
        }
    }
}