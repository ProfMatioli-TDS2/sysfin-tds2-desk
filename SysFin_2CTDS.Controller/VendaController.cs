using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SysFin_2CTDS.Models; // Adicionado para usar o ItemVenda correto

namespace SysFin_2CTDS.Controller
{
    public class VendaController
    {
        /// <summary>
        /// Registra uma Venda completa, atualiza estoque e lança no caixa.
        /// Usa uma transação para garantir a integridade dos dados.
        /// </summary>
        public async Task RegistrarVendaAsync(int idCliente, BindingList<ItemVenda> itensVenda, decimal valorTotal)
        {
            // ID '1' é 'Receita de Vendas' conforme o script SQL padrão.
            const int ID_PLANO_CONTAS_RECEITA_VENDA = 1;

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();
                // Inicia a transação
                using (var transaction = (SqlTransaction)await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 1. Inserir o cabeçalho na tabela 'vendas' e obter o ID
                        var sqlVenda = "INSERT INTO vendas (id_cliente, data_venda, valor_total) OUTPUT INSERTED.id VALUES (@id_cliente, @data_venda, @valor_total)";

                        int vendaId;
                        using (var cmdVenda = new SqlCommand(sqlVenda, connection, transaction))
                        {
                            cmdVenda.Parameters.AddWithValue("@id_cliente", idCliente);
                            cmdVenda.Parameters.AddWithValue("@data_venda", DateTime.Now);
                            cmdVenda.Parameters.AddWithValue("@valor_total", valorTotal);

                            // Captura o ID da venda recém-criada
                            var result = await cmdVenda.ExecuteScalarAsync();
                            vendaId = Convert.ToInt32(result);
                        }

                        // 2. Inserir cada item na tabela 'itens_venda' E atualizar o estoque
                        foreach (var item in itensVenda)
                        {
                            // 2a. Inserir o item da venda
                            var sqlItem = "INSERT INTO itens_venda (id_venda, id_produto, quantidade, valor_unitario) VALUES (@id_venda, @id_produto, @quantidade, @valor_unitario)";
                            using (var cmdItem = new SqlCommand(sqlItem, connection, transaction))
                            {
                                cmdItem.Parameters.AddWithValue("@id_venda", vendaId);
                                cmdItem.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                                cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                                cmdItem.Parameters.AddWithValue("@valor_unitario", item.ValorUnitario);
                                await cmdItem.ExecuteNonQueryAsync();
                            }

                            // 2b. Atualizar (subtrair) o estoque do produto
                            // Adicionado AND estoque_atual >= @quantidade para garantir que não fique negativo
                            var sqlEstoque = "UPDATE produtos SET estoque_atual = estoque_atual - @quantidade WHERE id = @id_produto AND estoque_atual >= @quantidade";
                            using (var cmdEstoque = new SqlCommand(sqlEstoque, connection, transaction))
                            {
                                cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                                cmdEstoque.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                                int linhasAfetadas = await cmdEstoque.ExecuteNonQueryAsync();

                                // Se 'linhasAfetadas' for 0, o estoque era insuficiente.
                                if (linhasAfetadas == 0)
                                {
                                    // Isso força o 'catch' e o 'Rollback'
                                    throw new Exception($"Estoque insuficiente para o produto '{item.ProdutoNome}'. Venda cancelada.");
                                }
                            }
                        } // Fim do foreach

                        // 3. Lançar no Movimento de Caixa (Receita)
                        var sqlCaixa = @"
                            INSERT INTO movimento_caixa
                            (data_movimento, descricao, id_plano_de_contas, tipo, valor, id_venda)
                            VALUES (@data, @descricao, @id_plano, 'E', @valor, @id_venda)"; // 'E' de Entrada

                        using (var cmdCaixa = new SqlCommand(sqlCaixa, connection, transaction))
                        {
                            cmdCaixa.Parameters.AddWithValue("@data", DateTime.Now);
                            cmdCaixa.Parameters.AddWithValue("@descricao", $"Receita referente à Venda ID {vendaId}");
                            cmdCaixa.Parameters.AddWithValue("@id_plano", ID_PLANO_CONTAS_RECEITA_VENDA);
                            cmdCaixa.Parameters.AddWithValue("@valor", valorTotal);
                            cmdCaixa.Parameters.AddWithValue("@id_venda", vendaId);
                            await cmdCaixa.ExecuteNonQueryAsync();
                        }

                        // Se tudo deu certo, confirma a transação
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        // Se algo deu errado (ex: estoque insuficiente), desfaz tudo
                        await transaction.RollbackAsync();
                        // Relança o erro para o formulário (View) exibi-lo
                        throw new Exception("Erro ao registrar a venda: " + ex.Message);
                    }
                } // Fim do using transaction
            } // Fim do using connection
        } // Fim do RegistrarVendaAsync
    } // Fim da classe VendaController
} // Fim do namespace