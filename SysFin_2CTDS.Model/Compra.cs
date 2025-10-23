using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SysFin_2CTDS.Model
{
    public class Compra
    {
        public int ProdutoId
        {
            get; set;
        }
        public string ProdutoNome
        {
            get; set;
        }
        public int Quantidade
        {
            get; set;
        }
        public decimal ValorUnitario
        {
            get; set;
        }

        public decimal Subtotal
        {
            get
            {
                return Quantidade * ValorUnitario;
            }
        }

        public DateTime DataCompra
        {
            get; set;
        }
        public string NomeFornecedor
        {
            get; set;
        }
        public decimal ValorTotal
        {
            get; set;
        }

        private static string connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|\Database\BancoDados.mdf;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool RegistrarCompra(List<Compra> itensCompra, int fornecedorId, decimal valorTotal)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {

                    var cmdCompra = new SqlCommand("INSERT INTO compras (id_fornecedor, data_compra, valor_total) VALUES (@id_fornecedor, @data_compra, @valor_total); SELECT SCOPE_IDENTITY();", connection, transaction);
                    cmdCompra.Parameters.AddWithValue("@id_fornecedor", fornecedorId);
                    cmdCompra.Parameters.AddWithValue("@data_compra", DateTime.Now);
                    cmdCompra.Parameters.AddWithValue("@valor_total", valorTotal);

                    int idCompra = Convert.ToInt32(cmdCompra.ExecuteScalar());

                    foreach (var item in itensCompra)
                    {
                        var cmdItem = new SqlCommand("INSERT INTO itens_compra (id_compra, id_produto, quantidade, valor_unitario) VALUES (@id_compra, @id_produto, @quantidade, @valor_unitario)", connection, transaction);
                        cmdItem.Parameters.AddWithValue("@id_compra", idCompra);
                        cmdItem.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                        cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                        cmdItem.Parameters.AddWithValue("@valor_unitario", item.ValorUnitario);
                        cmdItem.ExecuteNonQuery();

                        var cmdEstoque = new SqlCommand("UPDATE produtos SET estoque_atual = estoque_atual + @quantidade WHERE id = @id_produto", connection, transaction);
                        cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                        cmdEstoque.Parameters.AddWithValue("@id_produto", item.ProdutoId);
                        cmdEstoque.ExecuteNonQuery();
                    }

                    var cmdCaixa = new SqlCommand("INSERT INTO movimento_caixa (data_movimento, descricao, id_plano_de_contas, tipo, valor, id_compra) VALUES (@data_movimento, @descricao, @id_plano_de_contas, @tipo, @valor, @id_compra)", connection, transaction);
                    cmdCaixa.Parameters.AddWithValue("@data_movimento", DateTime.Now);
                    cmdCaixa.Parameters.AddWithValue("@descricao", "Compra de Mercadorias");
                    cmdCaixa.Parameters.AddWithValue("@id_plano_de_contas", 2);  
                    cmdCaixa.Parameters.AddWithValue("@tipo", "S"); 
                    cmdCaixa.Parameters.AddWithValue("@valor", valorTotal);
                    cmdCaixa.Parameters.AddWithValue("@id_compra", idCompra);
                    cmdCaixa.ExecuteNonQuery();


                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Erro ao registrar a compra: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
