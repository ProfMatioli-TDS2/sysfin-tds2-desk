using System;
using System.ComponentModel;
using System.Windows.Forms;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Controller;
using System.Collections.Generic; // MUDANÇA: Adicionado
using System.Threading.Tasks; // MUDANÇA: Adicionado
using System.Linq; // MUDANÇA: Adicionado

namespace SysFin_2CTDS.View
{
    public partial class frmRegistroCompras : Form
    {
        private BindingList<Compra> itensCompra = new BindingList<Compra>();

        // MUDANÇA: Controllers como campos
        private readonly FornecedorController _fornecedorController;
        private readonly ProdutoController _produtoController;
        private readonly CompraController _compraController;

        public frmRegistroCompras()
        {
            InitializeComponent();
            _fornecedorController = new FornecedorController();
            _produtoController = new ProdutoController();
            _compraController = new CompraController();

            // MUDANÇA: Configurar o DataGridView
            dgvItensCompra.DataSource = itensCompra;
            ConfigurarGrid();
        }

        // MUDANÇA: Evento de Load agora é 'async'
        private async void frmRegistroCompras_Load(object? sender, EventArgs e) // MUDANÇA: object?
        {
            // MUDANÇA: Usando Task.WhenAll para carregar em paralelo
            await Task.WhenAll(CarregarFornecedores(), CarregarProdutos());
        }

        // MUDANÇA: Método agora é 'async Task'
        private async Task CarregarFornecedores()
        {
            try
            {
                // MUDANÇA: Chamando método Async
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

        // MUDANÇA: Método agora é 'async Task'
        private async Task CarregarProdutos()
        {
            try
            {
                // MUDANÇA: Chamando método Async
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

        // MUDANÇA: Nova função para configurar a grid
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

        private void btnAdicionar_Click(object? sender, EventArgs e) // MUDANÇA: object?
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

            // MUDANÇA: Tratamento de conversão segura
            if (!(cboProduto.SelectedItem is Model.Produto produtoSelecionado))
            {
                MessageBox.Show("Produto selecionado inválido.");
                return;
            }

            var item = new Compra
            {
                ProdutoId = produtoSelecionado.Id, // MUDANÇA: Usando o ID do objeto
                ProdutoNome = produtoSelecionado.Nome, // MUDANÇA: Usando o Nome do objeto
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

        // MUDANÇA: Método agora é 'async void'
        private async void btnFinalizarCompra_Click(object? sender, EventArgs e) // MUDANÇA: object?
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
                // MUDANÇA: Tratamento de conversão segura
                if (!(cboFornecedor.SelectedItem is Model.Fornecedor fornecedorSelecionado))
                {
                    MessageBox.Show("Fornecedor selecionado inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int fornecedorId = fornecedorSelecionado.Id; // MUDANÇA: Usando o ID do objeto
                DateTime dataDaCompra = DateTime.Now;
                decimal valorTotal = itensCompra.Sum(item => item.Subtotal);

                // MUDANÇA: Removido o .ToList() para passar o BindingList diretamente
                await _compraController.RegistrarCompra(fornecedorId, dataDaCompra, valorTotal, itensCompra);

                MessageBox.Show("Compra registrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao finalizar a compra: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MUDANÇA: Novo método
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

