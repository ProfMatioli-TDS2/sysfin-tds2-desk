using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Models; // Para Cliente
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks; // Adicionado

// MUDANÇA: Resolvendo a ambiguidade explicitamente
using ItemVenda = SysFin_2CTDS.Models.ItemVenda;

namespace SysFin_2CTDS.View
{
    public partial class frmRegistroVendas : Form
    {
        // Controllers
        private readonly ClienteController _clienteController;
        private readonly ProdutoController _produtoController;
        private readonly VendaController _vendaController;

        // Lista de itens no "carrinho"
        // Agora usa o 'ItemVenda' definido acima (do namespace Models)
        private BindingList<ItemVenda> itensVenda = new BindingList<ItemVenda>();

        public frmRegistroVendas()
        {
            InitializeComponent();
            _clienteController = new ClienteController();
            _produtoController = new ProdutoController();
            _vendaController = new VendaController();

            // Configura o DataGridView
            ConfigurarGrid();
        }

        private async void frmRegistroVendas_Load(object? sender, EventArgs e)
        {
            // Carrega os dropdowns
            await CarregarClientes();
            await CarregarProdutos();

            // Limpa os campos
            LimparFormulario(false); // Limpa sem perguntar
        }

        private void ConfigurarGrid()
        {
            dgvItensVenda.AutoGenerateColumns = false;
            dgvItensVenda.DataSource = itensVenda; // Liga a lista à grid

            // Adiciona colunas manualmente
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
                DefaultCellStyle = { Format = "C2" } // Formato de moeda
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
                var listaDeClientes = await _clienteController.GetAllAsync();
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
            // Auto-preenche o valor do produto quando selecionado
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
            // Validações
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

            // Pega o produto selecionado
            var produtoSelecionado = (Produto)cboProduto.SelectedItem;

            // Verifica se o item já está na lista
            var itemExistente = itensVenda.FirstOrDefault(item => item.ProdutoId == produtoSelecionado.Id);

            if (itemExistente != null)
            {
                // Se já existe, apenas soma a quantidade
                itemExistente.Quantidade += (int)numQuantidade.Value;
                // Força a grid a atualizar (BindingList não atualiza automático em propriedades filhas)
                itensVenda.ResetBindings();
            }
            else
            {
                // Se é novo, cria o item e adiciona
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
                // Pega o item vinculado à linha selecionada
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
            // Validações
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

            // Confirmação
            var confirmResult = MessageBox.Show("Deseja realmente finalizar esta venda?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            try
            {
                int clienteId = (int)cboCliente.SelectedValue;
                decimal valorTotal = itensVenda.Sum(item => item.Subtotal);

                // Chama o Controller
                await _vendaController.RegistrarVendaAsync(clienteId, itensVenda, valorTotal);

                MessageBox.Show("Venda registrada com sucesso! Estoque atualizado e caixa lançado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparFormulario(false);
            }
            catch (Exception ex)
            {
                // Captura erros do Controller (ex: estoque insuficiente)
                MessageBox.Show(ex.Message, "Erro ao Finalizar Venda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimparTudo_Click(object? sender, EventArgs e)
        {
            LimparFormulario(true); // Limpa perguntando
        }

        // --- Métodos Auxiliares ---

        private void AtualizarValorTotal()
        {
            decimal total = itensVenda.Sum(item => item.Subtotal);
            lblValorTotal.Text = total.ToString("C"); // Formato de Moeda
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