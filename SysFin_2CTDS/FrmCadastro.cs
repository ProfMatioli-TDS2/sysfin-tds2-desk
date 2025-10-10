using System;
using System.Windows.Forms;

namespace SysFin_2CTDS.Views {
    // Este é um formulário base. Ele não será exibido diretamente,
    // mas outros formulários irão herdar sua aparência e comportamento.
    public partial class FrmCadastroBase : Form {
        public FrmCadastroBase() {
            InitializeComponent();
        }

        // O botão fechar tem um comportamento padrão que serve para todos.
        private void tsbFechar_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
