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
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var clienteForm = new ClienteForm();
            clienteForm.MdiParent = this;
            clienteForm.Show();
        }

        // Evento de clique para o item de menu "Fornecedores"
        private void fornecedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is FornecedorForm)
                {
                    form.Focus(); // Traz o formulário já aberto para frente
                    return;       // Cancela a abertura de uma nova instância
                }
            }

            var fornecedorForm = new FornecedorForm();
            fornecedorForm.MdiParent = this;
            fornecedorForm.Show();
        }

        // Evento de clique para o novo item de menu "Visualizar Cadastros"
        private void visualizarCadastrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: No futuro, este botão abrirá o formulário de visualização.
            MessageBox.Show("Funcionalidade para visualizar cadastros ainda não implementada.", "Em Desenvolvimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


