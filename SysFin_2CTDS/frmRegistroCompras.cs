using System;
using System.ComponentModel;
using System.Windows.Forms;
// Adicione aqui os 'usings' para os projetos Model e Controller quando eles existirem
// using SysFin_2CTDS.Model;
// using SysFin_2CTDS.Controller;

namespace SysFin_2CTDS.View
{
    public partial class frmRegistroCompras : Form
    {
        private BindingList<ItemCompraViewModel> itensCompra = new BindingList<ItemCompraViewModel>();
        public frmRegistroCompras()
        {
            InitializeComponent();
        }

        // Evento que é disparado quando o formulário é carregado
        private void frmRegistroCompras_Load(object sender, EventArgs e)
        {
            // Quando o formulário abrir, vamos carregar os dados iniciais
            CarregarFornecedores();
            CarregarProdutos();
        }

        // Método responsável por buscar e carregar os fornecedores no ComboBox
        private void CarregarFornecedores()
        {
            // --- ESTE TRECHO DEPENDE DA TAREFA #3 ---
            // Quando a classe de controller do Fornecedor estiver pronta,
            // vamos descomentar e ajustar este código.

            // 1. Instanciar o controller de fornecedor
            // FornecedorController fornecedorController = new FornecedorController();

            // 2. Chamar o método que lista todos os fornecedores
            // var listaDeFornecedores = fornecedorController.ListarTodos();

            // 3. Configurar o ComboBox
            // cboFornecedor.DataSource = listaDeFornecedores; // A fonte dos dados
            // cboFornecedor.DisplayMember = "Nome"; // A propriedade que será exibida (ex: "Coca-Cola")
            // cboFornecedor.ValueMember = "Id"; // O valor "escondido" de cada item (ex: 1, 2, 3)
        }

        // Método responsável por buscar e carregar os produtos no ComboBox
        private void CarregarProdutos()
        {
            // --- ESTE TRECHO DEPENDE DA TAREFA #4 ---
            // Quando a classe de controller do Produto estiver pronta,
            // vamos descomentar e ajustar este código.

            // 1. Instanciar o controller de produto
            // ProdutoController produtoController = new ProdutoController();

            // 2. Chamar o método que lista todos os produtos
            // var listaDeProdutos = produtoController.ListarTodos();

            // 3. Configurar o ComboBox
            // cboProduto.DataSource = listaDeProdutos;
            // cboProduto.DisplayMember = "Nome";
            // cboProduto.ValueMember = "Id";
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // --- Validações ---
            // (Temporariamente, vamos usar dados fixos para o produto, já que o ComboBox está vazio)
            // if (cboProduto.SelectedItem == null)
            // {
            //     MessageBox.Show("Por favor, selecione um produto.");
            //     return;
            // } teste

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

            // --- Captura dos Dados ---
            var item = new ItemCompraViewModel
            {
                // Quando a Tarefa #4 estiver pronta, usaremos os dados reais do ComboBox
                // ProdutoId = (int)cboProduto.SelectedValue,
                // ProdutoNome = cboProduto.Text,

                // Por enquanto, usamos dados temporários para poder testar
                ProdutoId = 1, // Exemplo
                ProdutoNome = "Produto de Teste " + (itensCompra.Count + 1), // Exemplo
                Quantidade = (int)numQuantidade.Value,
                ValorUnitario = numValorUnitario.Value
            };

            // --- Adicionar à Lista/Grid ---
            itensCompra.Add(item);

            // --- Atualizar o Total ---
            AtualizarValorTotal();

            // --- Limpar Campos (Opcional) ---
            // cboProduto.SelectedIndex = -1;
            numQuantidade.Value = 1;
            numValorUnitario.Value = 0;
        }

        // Método para calcular e exibir o valor total da compra
        private void AtualizarValorTotal()
        {
            decimal total = 0;
            foreach (var item in itensCompra)
            {
                total += item.Subtotal;
            }

            lblValorTotal.Text = total.ToString("C"); // "C" formata como moeda (ex: R$ 150,00)

        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            // --- 1. Validações ---
            // (A validação do fornecedor está comentada, pois o ComboBox ainda não é carregado)
            // if (cboFornecedor.SelectedItem == null)
            // {
            //     MessageBox.Show("Por favor, selecione um fornecedor para finalizar a compra.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     return;
            // }

            if (itensCompra.Count == 0)
            {
                MessageBox.Show("A compra não pode ser finalizada porque não há itens na lista.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. Coleta de Dados da Tela ---
            // Quando a Tarefa #3 estiver pronta, esta linha vai pegar o ID do fornecedor selecionado.
            // int fornecedorId = (int)cboFornecedor.SelectedValue;

            // Por enquanto, usamos um ID de teste.
            int fornecedorId = 1; // Exemplo de ID do fornecedor

            DateTime dataDaCompra = DateTime.Now; // Pega a data e hora atuais
            var listaDeItens = this.itensCompra;  // A nossa lista que já está preenchida

            // --- 3. Simulação do Envio para a Próxima Camada ---
            // O código abaixo serve para MOSTRAR que os dados foram coletados corretamente.
            // A Tarefa #7 irá substituir este MessageBox por uma chamada ao Controller.

            // Monta uma string com os detalhes da compra para exibir
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("--- Compra Pronta para ser Salva ---");
            sb.AppendLine();
            sb.AppendLine($"ID do Fornecedor: {fornecedorId}");
            sb.AppendLine($"Data da Compra: {dataDaCompra:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine($"Total de Itens: {listaDeItens.Count}");
            sb.AppendLine($"Valor Total: {lblValorTotal.Text}");
            sb.AppendLine();
            sb.AppendLine("Itens:");
            foreach (var item in listaDeItens)
            {
                sb.AppendLine($"- {item.ProdutoNome} | Qtd: {item.Quantidade} | Vlr. Un.: {item.ValorUnitario:C} | Subtotal: {item.Subtotal:C}");
            }

            // Exibe a mensagem de sucesso
            MessageBox.Show(sb.ToString(), "Dados da Compra Coletados", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // --- 4. Futura Chamada ao Controller (Tarefa #7) ---
            // Quando a lógica de negócio estiver pronta, o código será algo como:
            //
            // CompraController compraController = new CompraController();
            // bool sucesso = compraController.Salvar(fornecedorId, dataDaCompra, listaDeItens);
            //
            // if (sucesso)
            // {
            //     MessageBox.Show("Compra salva com sucesso!");
            //     this.Close(); // Fecha a tela após salvar
            // }
            // else
            // {
            //     MessageBox.Show("Ocorreu um erro ao salvar a compra.");
            // }
        }
    }
    // Esta é uma classe auxiliar que representa um item na nossa grid da tela.
    public class ItemCompraViewModel
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
    }

}