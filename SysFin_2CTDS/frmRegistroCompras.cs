using System;
using System.ComponentModel;
using System.Windows.Forms;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Controller;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SysFin_2CTDS.View
{
    public partial class frmRegistroCompras : Form
    {
        private BindingList<Compra> itensCompra = new BindingList<Compra>();

        private readonly FornecedorController _fornecedorController;
        private readonly ProdutoController _produtoController;
        private readonly CompraController _compraController;

        public frmRegistroCompras()
        {
            InitializeComponent();
            _fornecedorController = new FornecedorController();
            _produtoController = new ProdutoController();
            _compraController = new CompraController();

            dgvItensCompra.DataSource = itensCompra;
            ConfigurarGrid();
        }

        private async void frmRegistroCompras_Load(object? sender, EventArgs e)
        {
            await Task.WhenAll(CarregarFornecedores(), CarregarProdutos());
        }

        private async Task CarregarFornecedores()
        {
            try
            {
                var listaDeFornecedores = await _fornecedorController.GetAllAsync();
                cboFornecedor.DataSource = listaDeFornecedores;
                cboFornecedor.DisplayMember = "Nome";
                cboFornecedor.ValueMember = "Id";
                cboFornecedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar fornecedores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CarregarProdutos()
        {
            try
            {
                var listaDeProdutos = await _produtoController.ListarProdutosAsync();
                cboProduto.DataSource = listaDeProdutos;
                cboProduto.DisplayMember = "Nome";
                cboProduto.ValueMember = "Id";
                cboProduto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            dgvItensCompra.AutoGenerateColumns = false;
            dgvItensCompra.Columns.Clear();

            dgvItensCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProdutoNome",
                HeaderText = "Produto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvItensCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantidade",
                HeaderText = "Qtd",
                Width = 60
            });
            dgvItensCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ValorUnitario",
                HeaderText = "Vl. Unitário",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgvItensCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
        }

        private void btnAdicionar_Click(object? sender, EventArgs e)
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

            if (!(cboProduto.SelectedItem is Model.Produto produtoSelecionado))
            {
                MessageBox.Show("Produto selecionado inválido.");
                return;
            }

            var item = new Compra
            {
                ProdutoId = produtoSelecionado.Id,
                ProdutoNome = produtoSelecionado.Nome,
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

        private void AtualizarValorTotal()
        {
            decimal total = itensCompra.Sum(item => item.Subtotal);
            lblValorTotal.Text = total.ToString("C");
        }

        private async void btnFinalizarCompra_Click(object? sender, EventArgs e)
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

            try
            {
                if (!(cboFornecedor.SelectedItem is Model.Fornecedor fornecedorSelecionado))
                {
                    MessageBox.Show("Fornecedor selecionado inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int fornecedorId = fornecedorSelecionado.Id;
                decimal valorTotal = itensCompra.Sum(item => item.Subtotal);

                // --- INÍCIO DA CORREÇÃO ---
                // 1. O nome do método agora é '...Async'.
                // 2. Removemos o argumento 'dataDaCompra', pois o Controller usa DateTime.Now.
                await _compraController.RegistrarCompraAsync(fornecedorId, itensCompra, valorTotal);
                // --- FIM DA CORREÇÃO ---

                MessageBox.Show("Compra registrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao finalizar a compra: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparFormulario()
        {
            itensCompra.Clear();
            cboFornecedor.SelectedIndex = -1;
            cboProduto.SelectedIndex = -1;
            numQuantidade.Value = 1;
            numValorUnitario.Value = 0;
            AtualizarValorTotal();
        }
    }
}