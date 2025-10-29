using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class EstoqueForm : Form
    {
        private ProdutoController produtoController = new ProdutoController();
        private const int ESTOQUE_MINIMO = 100;

        public EstoqueForm()
        {
            InitializeComponent();
        }

        private void EstoqueForm_Load(object sender, EventArgs e)
        {
            CarregarEstoque();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarEstoque();
        }

        private void CarregarEstoque()
        {
            try
            {
                List<Produto> produtos = produtoController.ListarProdutos();

                dgvEstoque.DataSource = null;
                dgvEstoque.DataSource = produtos;

                // Exibe apenas colunas relevantes
                dgvEstoque.Columns["Id"].Visible = false;
                dgvEstoque.Columns["Descricao"].Visible = false;
                dgvEstoque.Columns["PrecoVenda"].Visible = false;

                dgvEstoque.Columns["Nome"].HeaderText = "Nome do Produto";
                dgvEstoque.Columns["EstoqueAtual"].HeaderText = "Estoque Atual";

                // Formatação visual
                dgvEstoque.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvEstoque.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEstoque.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvEstoque.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
                dgvEstoque.EnableHeadersVisualStyles = false;

                DestacarEstoqueBaixo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar o estoque: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DestacarEstoqueBaixo()
        {
            foreach (DataGridViewRow row in dgvEstoque.Rows)
            {
                if (row.Cells["EstoqueAtual"].Value != null &&
                    int.TryParse(row.Cells["EstoqueAtual"].Value.ToString(), out int estoqueAtual))
                {
                    if (estoqueAtual < ESTOQUE_MINIMO)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.Font = new Font(dgvEstoque.Font, FontStyle.Bold);
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.Font = new Font(dgvEstoque.Font, FontStyle.Regular);
                    }
                }
            }
        }
    }
}
