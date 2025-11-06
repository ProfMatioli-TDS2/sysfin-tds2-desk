using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model.Data;
using SysFin_2CTDS.Models;
using System;
using System.Collections.Generic;

namespace SysFin_2CTDS.Controller
{
    public class VendaController
    {
        public List<string> RealizarVenda(Venda venda)
        {
            var errors = new List<string>();

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    var cmdVenda = new SqlCommand(
                        "INSERT INTO vendas (id_cliente, data_venda, valor_total) VALUES (@id_cliente, @data_venda, @valor_total); SELECT SCOPE_IDENTITY();", 
                        connection, 
                        transaction
                    );

                    cmdVenda.Parameters.AddWithValue("@id_cliente", venda.IdCliente);
                    cmdVenda.Parameters.AddWithValue("@data_venda", venda.DataVenda);
                    cmdVenda.Parameters.AddWithValue("@valor_total", venda.ValorTotal);
                    
                    int idVenda = Convert.ToInt32(cmdVenda.ExecuteScalar());

                    foreach (var item in venda.Itens)
                    {
                        var cmdItem = new SqlCommand(
                            "INSERT INTO itens_venda (id_venda, id_produto, quantidade, valor_unitario) VALUES (@id_venda, @id_produto, @quantidade, @valor_unitario)", 
                            connection, 
                            transaction
                        );
                        cmdItem.Parameters.AddWithValue("@id_venda", idVenda);
                        cmdItem.Parameters.AddWithValue("@id_produto", item.IdProduto);
                        cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                        cmdItem.Parameters.AddWithValue("@valor_unitario", item.ValorUnitario);
                        cmdItem.ExecuteNonQuery();
                        var cmdEstoque = new SqlCommand(
                            "UPDATE produtos SET estoque_atual = estoque_atual - @qtd WHERE id = @id_produto", 
                            connection, 
                            transaction
                        );
                        cmdEstoque.Parameters.AddWithValue("@qtd", item.Quantidade);
                        cmdEstoque.Parameters.AddWithValue("@id_produto", item.IdProduto);
                        cmdEstoque.ExecuteNonQuery();
                    }

                    var cmdCaixa = new SqlCommand(
                        "INSERT INTO movimento_caixa (data_movimento, descricao, id_plano_de_contas, tipo, valor, id_venda) VALUES (@data, @desc, @id_plano, 'E', @valor, @id_venda)", 
                        connection, 
                        transaction
                    );
                    cmdCaixa.Parameters.AddWithValue("@data", venda.DataVenda);
                    cmdCaixa.Parameters.AddWithValue("@desc", "Receita de Venda #" + idVenda);
                    cmdCaixa.Parameters.AddWithValue("@id_plano", 1); // ID 1 = Receita de Venda
                    cmdCaixa.Parameters.AddWithValue("@valor", venda.ValorTotal);
                    cmdCaixa.Parameters.AddWithValue("@id_venda", idVenda);
                    cmdCaixa.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        errors.Add($"Falha ao realizar a venda. A operação foi desfeita. Erro: {ex.Message}");
                    }
                    catch (Exception exRollback)
                    {
                        errors.Add($"Erro crítico. Falha ao realizar a venda e falha ao reverter a transação: {exRollback.Message}");
                    }
                }
            } 

            return errors;
        }
    }
}
