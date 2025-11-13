using System;
using System.ComponentModel;
using System.Windows.Forms;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Controller;



namespace SysFin_2CTDS.View
{
    public partial class frmRegistroCompras : Form
    {
        private BindingList<Compra> itensCompra = new BindingList<Compra>();
        public frmRegistroCompras()
        {
            InitializeComponent();
        }

        private void frmRegistroCompras_Load(object sender, EventArgs e)
        {
            CarregarFornecedores();
            CarregarProdutos();
        }

        // Método responsável por buscar e carregar os fornecedores no ComboBox
        private void CarregarFornecedores()
        {
            FornecedorController fornecedorController = new FornecedorController();
            var listaDeFornecedores = fornecedorController.GetAll();

            cboFornecedor.DataSource = listaDeFornecedores;
            cboFornecedor.DisplayMember = "Nome"; 
            cboFornecedor.ValueMember = "Id";     
            cboFornecedor.SelectedIndex = -1;
        }

        private void CarregarProdutos()
        {
            ProdutoController produtoController = new ProdutoController();
            var listaDeProdutos = produtoController.ListarProdutos();

            cboProduto.DataSource = listaDeProdutos;
            cboProduto.DisplayMember = "Nome"; 
            cboProduto.ValueMember = "Id";     
            cboProduto.SelectedIndex = -1;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (cboProduto.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione um produto.");
                return;
            }
            if (numQuantidade.Value <= 0)
            {
                MessageBox.Show("A quantidade deve ser maior que zero.");
                return;
            }
            if (numValorUnitario.Value <= 0)
            {
                MessageBox.Show("O valor unitário deve ser maior que zero.");
                return;
            }

            var item = new Compra
            {
                ProdutoId = (int)cboProduto.SelectedValue,
                ProdutoNome = cboProduto.Text,
                Quantidade = (int)numQuantidade.Value,
                ValorUnitario = numValorUnitario.Value
            };

            itensCompra.Add(item);
            AtualizarValorTotal();

            cboProduto.SelectedIndex = -1;
            numQuantidade.Value = 1;
            numValorUnitario.Value = 0;
            cboProduto.Focus();
        }

        // Método para calcular e exibir o valor total da compra
        private void AtualizarValorTotal()
        {
            decimal total = 0;
            foreach (var item in itensCompra)
            {
                total += item.Subtotal;
            }

            lblValorTotal.Text = total.ToString("C"); 

        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            if (cboFornecedor.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione um fornecedor para finalizar a compra.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (itensCompra.Count == 0)
            {
                MessageBox.Show("A compra não pode ser finalizada porque não há itens na lista.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int fornecedorId = (int)cboFornecedor.SelectedValue;
            DateTime dataDaCompra = DateTime.Now;
            var listaDeItens = this.itensCompra;

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("--- Compra Pronta para ser Salva ---");
            sb.AppendLine();
            sb.AppendLine($"ID do Fornecedor: {fornecedorId}");
            sb.AppendLine($"Data da Compra: {dataDaCompra:dd/yyyy HH:mm:ss}");
            sb.AppendLine($"Total de Itens: {listaDeItens.Count}");
            sb.AppendLine($"Valor Total: {lblValorTotal.Text}");
            sb.AppendLine();
            sb.AppendLine("Itens:");
            foreach (var item in listaDeItens)
            {
                sb.AppendLine($"- {item.ProdutoNome} | Qtd: {item.Quantidade} | Vlr. Un.: {item.ValorUnitario:C} | Subtotal: {item.Subtotal:C}");
            }

            MessageBox.Show(sb.ToString(), "Dados da Compra Coletados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


