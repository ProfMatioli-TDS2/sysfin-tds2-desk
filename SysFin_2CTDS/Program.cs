using SysFin_2CTDS.View;
using System;
using System.Windows.Forms;


namespace SysFin_2CTDS.View {
    internal static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new MdiPrincipal());            
        }
    }
}

