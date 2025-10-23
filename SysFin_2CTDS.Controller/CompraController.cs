using Microsoft.Data.SqlClient;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using SysFin_2CTDS.Models;
using System;
using System.Collections.Generic;

namespace SysFin_2CTDS.Controller
{
    public class CompraController
    {


        public void OperacaoCompra()
        {
            var itensCompra = new List<Compra>();
            decimal valorTotal = 0;

            foreach (var item in itensCompra)
            {
                var compraItem = new Compra
                {
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.ProdutoNome,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    Subtotal = item.Quantidade * item.ValorUnitario
                };
                valorTotal += compraItem.Subtotal;
                itensCompra.Add(compraItem);
            }

            int fornecedorId = 1;

            try
            {
                bool sucesso = Compra.RegistrarCompra(itensCompra, fornecedorId, valorTotal);
                MessageBox.Show("Compra registrada com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao registrar a compra: " + ex.Message);
            }

        }


        /// <summary>
        /// Obtém todas as compras realizadas em um determinado período.
        /// </summary>
        /// <param name="dataInicial">A data de início do período.</param>
        /// <param name="dataFinal">A data de fim do período.</param>
        /// <returns>Uma lista de objetos Compra com os dados do relatório.</returns>
        public List<Compra> GetComprasPorPeriodo(DateTime dataInicial, DateTime dataFinal)
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

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        listaCompras.Add(new Compra
                        {
                            DataCompra = reader.GetDateTime(reader.GetOrdinal("data_compra")),
                            NomeFornecedor = reader.GetString(reader.GetOrdinal("nome_fornecedor")),
                            ValorTotal = reader.GetDecimal(reader.GetOrdinal("valor_total"))
                        });
                    }
                }
            }
            return listaCompras;
        }
    }
}