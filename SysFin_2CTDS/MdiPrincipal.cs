using SysFin_2CTDS.Models; // Para Cliente
using System;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class MdiPrincipal : Form
    {
        public MdiPrincipal()
        {
            InitializeComponent();
        }

        // Evento de clique para o item de menu "Clientes"
        private void clientesToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // MUDANÇA: Bloco 'using' removido
            var clienteForm = new ClienteForm();
            clienteForm.MdiParent = this;
            clienteForm.Show();
        }

        // Evento de clique para o item de menu "Fornecedores"
        private void fornecedoresToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // MUDANÇA: Bloco 'using' removido
            var fornecedorForm = new FornecedorForm();
            fornecedorForm.MdiParent = this;
            fornecedorForm.Show();
        }

        // Evento de clique para o item de menu "Produtos"
        private void produtosToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // MUDANÇA: Bloco 'using' removido
            var produtosForm = new frmListagemProdutos();
            produtosForm.MdiParent = this;
            produtosForm.Show();
        }

        // Evento de clique para o item de menu "Estoque"
        private void estoqueToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // MUDANÇA: Bloco 'using' removido
            var estoqueForm = new EstoqueForm();
            estoqueForm.MdiParent = this;
            estoqueForm.Show();
        }

        // Evento de clique para o item de menu "Compras"
        private void comprasToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // MUDANÇA: Bloco 'using' removido
            var comprasForm = new frmRegistroCompras();
            comprasForm.MdiParent = this;
            comprasForm.Show();
        }

        // Evento de clique para o item de menu "Vendas"
        private void vendasToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade de Vendas ainda não implementada.", "Em Desenvolvimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

