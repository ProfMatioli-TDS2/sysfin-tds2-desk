using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Models; // Para Cliente
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

// Resolvendo a ambiguidade explicitamente
using ItemVenda = SysFin_2CTDS.Models.ItemVenda;

namespace SysFin_2CTDS.View
{
    public partial class frmRegistroVendas : Form
    {
        // Controllers
        private readonly ClienteController _clienteController;
        private readonly ProdutoController _produtoController;
        private readonly VendaController _vendaController;

        private BindingList<ItemVenda> itensVenda = new BindingList<ItemVenda>();

        public frmRegistroVendas()
        {
            InitializeComponent();
            _clienteController = new ClienteController();
            _produtoController = new ProdutoController();
            _vendaController = new VendaController();

            ConfigurarGrid();
        }

        private async void frmRegistroVendas_Load(object? sender, EventArgs e)
        {
            await CarregarClientes();
            await CarregarProdutos();
            LimparFormulario(false);
        }

        private void ConfigurarGrid()
        {
            dgvItensVenda.AutoGenerateColumns = false;
            dgvItensVenda.DataSource = itensVenda;

            dgvItensVenda.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProdutoNome",
                HeaderText = "Produto",
                Width = 200
            });
            dgvItensVenda.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantidade",
                HeaderText = "Qtd",
                Width = 60
            });
            dgvItensVenda.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ValorUnitario",
                HeaderText = "Vlr. Unit.",
                Width = 90,
                DefaultCellStyle = { Format = "C2" }
            });
            dgvItensVenda.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                Width = 100,
                DefaultCellStyle = { Format = "C2" }
            });
        }

        private async Task CarregarClientes()
        {
            try
            {
                // --- INÍCIO DA CORREÇÃO ---
                // O método agora espera um filtro. Passamos "" (vazio) para trazer todos.
                var listaDeClientes = await _clienteController.GetAllAsync("");
                // --- FIM DA CORREÇÃO ---

                cboCliente.DataSource = listaDeClientes;
                cboCliente.DisplayMember = "Nome";
                cboCliente.ValueMember = "Id";
                cboCliente.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message);
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
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
        }

        private void cboProduto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboProduto.SelectedItem is Produto produtoSelecionado)
            {
                numValorUnitario.Value = produtoSelecionado.PrecoVenda;
            }
            else
            {
                numValorUnitario.Value = 0;
            }
        }

        private void btnAdicionar_Click(object? sender, EventArgs e)
        {
            if (cboProduto.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione um produto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numQuantidade.Value <= 0)
            {
                MessageBox.Show("A quantidade deve ser maior que zero.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var produtoSelecionado = (Produto)cboProduto.SelectedItem;

            var itemExistente = itensVenda.FirstOrDefault(item => item.ProdutoId == produtoSelecionado.Id);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += (int)numQuantidade.Value;
                itensVenda.ResetBindings();
            }
            else
            {
                var item = new ItemVenda
                {
                    ProdutoId = produtoSelecionado.Id,
                    ProdutoNome = produtoSelecionado.Nome,
                    Quantidade = (int)numQuantidade.Value,
                    ValorUnitario = numValorUnitario.Value
                };
                itensVenda.Add(item);
            }

            AtualizarValorTotal();
            LimparCamposDoItem();
        }

        private void btnRemover_Click(object? sender, EventArgs e)
        {
            if (dgvItensVenda.SelectedRows.Count > 0)
            {
                var itemSelecionado = (ItemVenda)dgvItensVenda.SelectedRows[0].DataBoundItem;
                itensVenda.Remove(itemSelecionado);
                AtualizarValorTotal();
            }
            else
            {
                MessageBox.Show("Selecione um item na lista para remover.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnFinalizarVenda_Click(object? sender, EventArgs e)
        {
            if (cboCliente.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione um cliente.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (itensVenda.Count == 0)
            {
                MessageBox.Show("A venda não pode ser finalizada porque não há itens.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Deseja realmente finalizar esta venda?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            try
            {
                int clienteId = (int)cboCliente.SelectedValue;
                decimal valorTotal = itensVenda.Sum(item => item.Subtotal);

                await _vendaController.RegistrarVendaAsync(clienteId, itensVenda, valorTotal);

                MessageBox.Show("Venda registrada com sucesso! Estoque atualizado e caixa lançado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparFormulario(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Finalizar Venda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimparTudo_Click(object? sender, EventArgs e)
        {
            LimparFormulario(true);
        }

        private void AtualizarValorTotal()
        {
            decimal total = itensVenda.Sum(item => item.Subtotal);
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
                var confirmResult = MessageBox.Show("Tem certeza que deseja limpar todos os campos e itens?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.No)
                {
                    return;
                }
            }

            cboCliente.SelectedIndex = -1;
            itensVenda.Clear();
            AtualizarValorTotal();
            LimparCamposDoItem();
            cboCliente.Focus();
        }
    }
}