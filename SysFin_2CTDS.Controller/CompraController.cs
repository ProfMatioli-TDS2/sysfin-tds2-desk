using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SysFin_2CTDS.Controller
{
    public class CompraController
    {
        // ID '2' é 'Compra de Mercadorias' conforme o script SQL padrão.
        private const int ID_PLANO_CONTAS_DESPESA_COMPRA = 2;

        /// <summary>
        /// Obtém todas as compras (apenas o cabeçalho) por período.
        /// </summary>
        public async Task<List<Compra>> GetComprasPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var listaCompras = new List<Compra>();

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
                            // Adiciona o item à lista
                            listaCompras.Add(new Compra
                            {
                                DataCompra = reader.GetDateTime(reader.GetOrdinal("data_compra")),
                                // Verifica se o nome do fornecedor é nulo
                                NomeFornecedor = reader.IsDBNull(reader.GetOrdinal("nome_fornecedor")) ? null : reader.GetString(reader.GetOrdinal("nome_fornecedor")),
                                ValorTotal = reader.GetDecimal(reader.GetOrdinal("valor_total"))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Lança a exceção para a View (Form) tratar
                    throw new Exception("Erro ao buscar compras por período: " + ex.Message);
                }
            }
            return listaCompras;
        }

        /// <summary>
        /// Registra uma nova compra, seus itens, atualiza o estoque e lança no caixa.
        /// (Usa Transação)
        /// </summary>
        public async Task RegistrarCompra(int fornecedorId, DateTime dataDaCompra, decimal valorTotal, BindingList<Compra> itens)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();
                // Inicia a transação
                using (var transaction = (SqlTransaction)await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 1. Inserir o cabeçalho na tabela 'compras' e obter o ID
                        var sqlCompra = "INSERT INTO compras (id_fornecedor, data_compra, valor_total) OUTPUT INSERTED.id VALUES (@id_fornecedor, @data_compra, @valor_total)";

                        int compraId;
                        using (var cmdCompra = new SqlCommand(sqlCompra, connection, transaction))
                        {
                            cmdCompra.Parameters.AddWithValue("@id_fornecedor", fornecedorId);
                            cmdCompra.Parameters.AddWithValue("@data_compra", dataDaCompra);
                            cmdCompra.Parameters.AddWithValue("@valor_total", valorTotal);

                            // Captura o ID da compra recém-criada
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

                            // 2b. Atualizar (somar) o estoque do produto
                            var sqlEstoque = "UPDATE produtos SET estoque_atual = estoque_atual + @quantidade WHERE id = @id_produto";
                            using (var cmdEstoque = new SqlCommand(sqlEstoque, connection, transaction))
                            {
                                cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                                cmdEstoque.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                                await cmdEstoque.ExecuteNonQueryAsync();
                            }
                        }

                        // --- ESTA É A PARTE NOVA (do Passo 28) ---
                        // 3. Lançar no Movimento de Caixa (Despesa)
                        var sqlCaixa = @"
                            INSERT INTO movimento_caixa
                            (data_movimento, descricao, id_plano_de_contas, tipo, valor, id_compra)
                            VALUES (@data, @descricao, @id_plano, 'S', @valor, @id_compra)"; // 'S' de Saída

                        using (var cmdCaixa = new SqlCommand(sqlCaixa, connection, transaction))
                        {
                            cmdCaixa.Parameters.AddWithValue("@data", dataDaCompra);
                            cmdCaixa.Parameters.AddWithValue("@descricao", $"Despesa referente à Compra ID {compraId}");
                            cmdCaixa.Parameters.AddWithValue("@id_plano", ID_PLANO_CONTAS_DESPESA_COMPRA);
                            cmdCaixa.Parameters.AddWithValue("@valor", valorTotal);
                            cmdCaixa.Parameters.AddWithValue("@id_compra", compraId);
                            await cmdCaixa.ExecuteNonQueryAsync();
                        }
                        // --- FIM DA PARTE NOVA ---

                        // Se tudo deu certo, 'commita' a transação
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        // Se algo deu errado, desfaz tudo
                        await transaction.RollbackAsync();
                        // Relança o erro para o formulário (View) exibi-lo
                        throw new Exception("Erro ao registrar a compra (operação revertida): " + ex.Message);
                    }
                }
            }
        }
    }
}