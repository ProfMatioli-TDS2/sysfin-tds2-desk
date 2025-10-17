using SysFin_2CTDS.Controller; // Não se esqueça de adicionar
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SysFin_2CTDS.View
{
    public partial class frmListagemProdutos : Form
    {
        public frmListagemProdutos()
        {
            InitializeComponent();
        }

        // Método que carrega os dados no DataGridView
        private void CarregarProdutos()
        {
            ProdutoController controller = new ProdutoController();
            dgvProdutos.DataSource = null; // Limpa a grid para evitar duplicação
            dgvProdutos.DataSource = controller.ListarProdutos();
        }

        private void frmListagemProdutos_Load(object sender, EventArgs e)
        {
            // Quando o formulário carregar, chama o método para preencher a lista
            CarregarProdutos();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            // O botão atualizar também chama o método
            CarregarProdutos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // Cria e exibe o formulário de cadastro
            frmCadastroProduto telaCadastro = new frmCadastroProduto();
            telaCadastro.ShowDialog(); // ShowDialog trava esta tela até que a de cadastro seja fechada

            // Após fechar a tela de cadastro, atualizamos a lista para ver o novo item
            CarregarProdutos();
        }

        // Adicione este método de evento no arquivo frmListagemProdutos.cs

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // 1. Pega o texto digitado pelo usuário
            string termoBusca = txtBusca.Text;

            // 2. Instancia o controller
            ProdutoController controller = new ProdutoController();

            // 3. Chama o novo método de busca do controller
            List<Model.Produto> resultados = controller.ListarProdutosPorNome(termoBusca);

            // 4. Atualiza o DataGridView com os resultados da busca
            dgvProdutos.DataSource = null;
            dgvProdutos.DataSource = resultados;
        }

        // Adicione este método de evento no arquivo frmListagemProdutos.cs

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // 1. Verifica se há alguma linha selecionada no DataGridView
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Para a execução do método aqui
            }

            // 2. Pede confirmação ao usuário
            DialogResult resultadoConfirmacao = MessageBox.Show("Tem certeza que deseja excluir o produto selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultadoConfirmacao == DialogResult.Yes)
            {
                // 3. Obtém o ID do produto da linha selecionada
                // A célula "Id" é acessada pelo nome da propriedade na classe Produto
                int idSelecionado = (int)dgvProdutos.SelectedRows[0].Cells["Id"].Value;

                // 4. Instancia o controller
                ProdutoController controller = new ProdutoController();

                // 5. Chama o método de exclusão
                bool sucesso = controller.ExcluirProduto(idSelecionado);

                // 6. Verifica o resultado e atualiza a tela
                if (sucesso)
                {
                    MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarProdutos(); // Atualiza a grid para remover a linha
                }
                else
                {
                    MessageBox.Show("Não foi possível encontrar o produto para excluir.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Adicione este método de evento em frmListagemProdutos.cs

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um produto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pega o ID da linha selecionada
            int idSelecionado = (int)dgvProdutos.SelectedRows[0].Cells["Id"].Value;

            // Cria a tela de cadastro usando o NOVO CONSTRUTOR, passando o ID
            frmCadastroProduto telaEdicao = new frmCadastroProduto(idSelecionado);
            telaEdicao.ShowDialog();

            // Após fechar a tela de edição, atualiza a lista
            CarregarProdutos();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            RelatorioController relatorioController = new RelatorioController();

            // Chama o método que gera o PDF e retorna o caminho do arquivo
            string resultado = relatorioController.GerarRelatorioProdutos();

            // Verifica se o resultado é um caminho (sucesso) ou uma mensagem de erro
            if (resultado.StartsWith("ERRO:"))
            {
                MessageBox.Show(resultado, "Erro ao Gerar Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Relatório gerado com sucesso!\nSalvo em: " + resultado, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: Tenta abrir o arquivo PDF gerado
                try
                {
                    System.Diagnostics.Process.Start(resultado);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível abrir o arquivo PDF automaticamente.\nErro: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}