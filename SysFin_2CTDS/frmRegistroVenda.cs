using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using SysFin_2CTDS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysFin_2CTDS.View {
    public partial class frmRegistroVenda : Form {
        // Listas para guardar os dados que vêm dos Controllers
        private List<Cliente> listaClientes = new List<Cliente>();
        private List<Produto> listaProdutos = new List<Produto>();

        public frmRegistroVenda() {
            InitializeComponent();

        }

        // --- EVENTOS PRINCIPAIS ---

        // Evento 1: O que acontece quando o formulário é carregado
        private void FormRegistroVenda_Load(object sender, EventArgs e) {
            // Tenta carregar os dados dos Clientes (Tarefa #2) e Produtos (Tarefa #4)
            try {
                // Busca Clientes
                ClienteController clienteCtrl = new ClienteController();
                // CORREÇÃO: O método no seu Controller chama-se 'GetAll()'
                listaClientes = clienteCtrl.GetAll();

                // Busca Produtos
                ProdutoController produtoCtrl = new ProdutoController();
                // CORREÇÃO: O método no seu Controller chama-se 'ListarProdutos()'
                listaProdutos = produtoCtrl.ListarProdutos();

            } catch(Exception ex) {
                MessageBox.Show("Falha ao carregar Clientes/Produtos.\n" + ex.Message,
                                "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Fecha o form se não puder carregar os dados
                return;
            }

            // Preenche a lista de Clientes
            cmbCliente.DataSource = listaClientes;
            cmbCliente.DisplayMember = "Nome"; // Confirmado pelo seu Model 'Cliente.cs'
            cmbCliente.ValueMember = "Id";     // Confirmado pelo seu Model 'Cliente.cs'

            // Preenche a lista de Produtos
            cmbProduto.DataSource = listaProdutos;
            cmbProduto.DisplayMember = "Nome"; // Confirmado pelo seu Model 'Produto.cs'
            cmbProduto.ValueMember = "Id";     // Confirmado pelo seu Model 'Produto.cs'

            // Limpa os campos
            LimparTudo();
        }

        // Evento 2: (Requisito) O que acontece ao selecionar um Produto
        private void cmbProduto_SelectedIndexChanged(object sender, EventArgs e) {
            if(cmbProduto.SelectedItem is Produto produtoSelecionado) {
                // Preenche o valor unitário (editável, como pedido)
                // Confirmado pelo seu Model 'Produto.cs'
                txtValorUnitario.Text = produtoSelecionado.PrecoVenda.ToString("F2");

                // Atualiza o label de estoque
                // CORREÇÃO: A propriedade no seu Model 'Produto.cs' é 'Estoque'
                lblEstoqueDisponivel.Text = $"Estoque: {produtoSelecionado.Estoque}";
            } else {
                txtValorUnitario.Clear();
                lblEstoqueDisponivel.Text = "Estoque: -";
            }
        }

        // Evento 3: (Requisito) O que acontece ao clicar em "Adicionar"
        private void btnAdicionar_Click(object sender, EventArgs e) {
            // 1. Validar Produto
            if(!(cmbProduto.SelectedItem is Produto produto)) {
                MessageBox.Show("Selecione um produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar Quantidade
            if(!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0) {
                MessageBox.Show("Quantidade inválida. Deve ser um número maior que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Validar Valor Unitário (que podia ser editado)
            if(!decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario) || valorUnitario < 0) {
                MessageBox.Show("Valor unitário inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. (Requisito) Validação de Estoque
            // A regra padrão de mercado é (quantidade <= estoque).
            // A sua regra ("não seja maior QUE") é a mesma coisa.
            // CORREÇÃO: A propriedade no seu Model 'Produto.cs' é 'Estoque'
            if(quantidade > produto.Estoque) {
                MessageBox.Show($"Erro: Quantidade solicitada ({quantidade}) é maior que o estoque disponível ({produto.Estoque}).",
                                "Estoque Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 5. Adicionar ao Grid
            decimal subtotal = quantidade * valorUnitario;
            dgvItens.Rows.Add(produto.Id, produto.Nome, quantidade, valorUnitario, subtotal);

            // 6. (Requisito) Calcular Total em tempo real
            AtualizarValorTotal();

            // 7. Limpar campos para novo item
            LimparCamposItem();
            cmbProduto.Focus();
        }

        // Evento 4: (Requisito) O que acontece ao clicar em "Finalizar Venda"
        private void btnFinalizarVenda_Click(object sender, EventArgs e) {
            // 1. Validar Cliente
            if(!(cmbCliente.SelectedItem is Cliente clienteSelecionado)) {
                MessageBox.Show("Selecione um cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar se há itens
            if(dgvItens.Rows.Count == 0) {
                MessageBox.Show("Adicione pelo menos um item à venda.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Coletar dados para a Tarefa #9
            // A Tarefa #9 (lógica de venda) provavelmente espera objetos
            // 'Venda' e 'ItemVenda' do seu projeto Model.

            // Simulação de coleta de dados:
            var dadosParaTarefa9 = new {
                ClienteId = clienteSelecionado.Id,
                Itens = new List<object>(),
                ValorTotal = dgvItens.Rows.Cast<DataGridViewRow>()
                                      .Sum(t => (decimal)t.Cells["Subtotal"].Value)
            };

            foreach(DataGridViewRow row in dgvItens.Rows) {
                dadosParaTarefa9.Itens.Add(new {
                    ProdutoId = (int)row.Cells["ProdutoId"].Value,
                    Quantidade = (int)row.Cells["Quantidade"].Value,
                    ValorUnitario = (decimal)row.Cells["ValorUnitario"].Value
                });
            }

            // 4. Enviar para a Tarefa #9 (Simulação)
            // Quando a Tarefa #9 estiver pronta, você chamará algo como:
            // VendaController vendaCtrl = new VendaController();
            // vendaCtrl.Registrar(dadosParaTarefa9);

            MessageBox.Show($"Venda finalizada para: {clienteSelecionado.Nome}\nTotal: {dadosParaTarefa9.ValorTotal:C2}\n\n(Dados prontos para enviar para a Tarefa #9)",
                            "Venda Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimparTudo();
        }


        private void AtualizarValorTotal() {
            decimal total = 0;
            // Percorre cada linha da grade e soma o subtotal
            foreach(DataGridViewRow row in dgvItens.Rows) {
                if(row.Cells["Subtotal"].Value != null) {
                    total += (decimal)row.Cells["Subtotal"].Value;
                }
            }
            lblValorTotal.Text = $"Valor Total: {total:C2}"; // "C2" formata para R$
        }

        private void LimparCamposItem() {
            cmbProduto.SelectedIndex = -1;
            txtValorUnitario.Clear();
            txtQuantidade.Clear();
            lblEstoqueDisponivel.Text = "Estoque: -";
        }

        private void LimparTudo() {
            cmbCliente.SelectedIndex = -1;
            dgvItens.Rows.Clear();
            LimparCamposItem();
            AtualizarValorTotal(); // Reseta o total para R$ 0,00
        }
    }
}