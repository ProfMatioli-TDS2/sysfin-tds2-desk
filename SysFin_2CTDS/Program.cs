using SysFin_2CTDS.View;
using System;
using System.Windows.Forms;


namespace SysFin_2CTDS.View {
    internal static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Alteramos a linha abaixo para iniciar nosso MDI Principal
            Application.Run(new MdiPrincipal());
            Application.Run(new frmListagemProdutos());
        }
    }
}

