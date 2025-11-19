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
        // Lista de itens no "carrinho" (Usa o Model 'Compra' como DTO)
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

            // Configura a Grid
            ConfigurarGrid();
            dgvItensCompra.DataSource = itensCompra;
        }

        private async void frmRegistroCompras_Load(object? sender, EventArgs e)
        {
            // Carrega dados iniciais
            await Task.WhenAll(CarregarFornecedores(), CarregarProdutos());
            LimparFormulario(false);
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
                HeaderText = "Vl. Unit.",
                Width = 100,
                DefaultCellStyle = { Format = "C2" }
            });
            dgvItensCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                Width = 100,
                DefaultCellStyle = { Format = "C2" }
            });
        }

        private async Task CarregarFornecedores()
        {
            try
            {
                var listaDeFornecedores = await _fornecedorController.GetAllAsync("nome", "ASC"); // Adicionado parametros padrão
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

        // Auto-preenche o valor ao selecionar produto
        private void cboProduto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboProduto.SelectedItem is Produto produtoSelecionado)
            {
                // Em compras, geralmente o valor unitário é zero ou o custo anterior, 
                // mas podemos sugerir o preço de venda ou 0. Vamos deixar 0 para o usuário digitar.
                numValorUnitario.Value = 0;
            }
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

            LimparCamposDoItem();
        }

        // --- MÉTODOS QUE FALTAVAM (Correção dos Erros) ---

        private void btnRemover_Click(object? sender, EventArgs e)
        {
            if (dgvItensCompra.SelectedRows.Count > 0)
            {
                var itemSelecionado = (Compra)dgvItensCompra.SelectedRows[0].DataBoundItem;
                itensCompra.Remove(itemSelecionado);
                AtualizarValorTotal();
            }
            else
            {
                MessageBox.Show("Selecione um item na lista para remover.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimparTudo_Click(object? sender, EventArgs e)
        {
            LimparFormulario(true);
        }

        // -------------------------------------------------

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

            var confirmacao = MessageBox.Show($"Deseja finalizar a compra no valor de {lblValorTotal.Text}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacao == DialogResult.No) return;

            try
            {
                if (!(cboFornecedor.SelectedItem is Model.Fornecedor fornecedorSelecionado))
                {
                    MessageBox.Show("Fornecedor selecionado inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int fornecedorId = fornecedorSelecionado.Id;
                decimal valorTotal = itensCompra.Sum(item => item.Subtotal);

                await _compraController.RegistrarCompraAsync(fornecedorId, itensCompra, valorTotal);

                MessageBox.Show("Compra registrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparFormulario(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao finalizar a compra: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarValorTotal()
        {
            decimal total = itensCompra.Sum(item => item.Subtotal);
            lblValorTotal.Text = total.ToString("C");
        }

        private void LimparCamposDoItem()
        {
            cboProduto.SelectedIndex = -1;
            numQuantidade.Value = 1;
            numValorUnitario.Value = 0;
            cboProduto.Focus();
        }

        private void LimparFormulario(bool perguntar)
        {
            if (perguntar)
            {
                if (MessageBox.Show("Tem certeza que deseja limpar todos os campos?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            itensCompra.Clear();
            cboFornecedor.SelectedIndex = -1;
            LimparCamposDoItem();
            AtualizarValorTotal();
        }
    }
}