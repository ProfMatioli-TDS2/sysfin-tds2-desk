using SysFin_2CTDS.View;
using System;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- MUDANÇA (Passo 45) ---
            // Vamos criar um loop para manter a aplicação viva
            // e permitir o "Logout" (voltar ao Login).

            bool querSairDoApp = false;

            while (!querSairDoApp)
            {
                // 1. Mostra o Login
                using (var loginForm = new frmLogin())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // 2. Se o Login foi OK, mostra o MDI Principal
                        using (var mdiForm = new MdiPrincipal())
                        {
                            // ATENÇÃO: Mudamos de Application.Run() para ShowDialog()
                            // Isso pausa o código aqui até o MDI fechar.
                            DialogResult mdiResult = mdiForm.ShowDialog();

                            // 3. Verifica o resultado do MDI
                            if (mdiResult == DialogResult.Retry)
                            {
                                // O usuário clicou em "Sair" (Logout)
                                // 'querSairDoApp' continua 'false',
                                // então o 'while' loop vai rodar de novo (voltando ao Login).
                            }
                            else
                            {
                                // O usuário fechou o MDI (clicou no 'X')
                                querSairDoApp = true; // Quebra o loop e sai da aplicação.
                            }
                        }
                    }
                    else
                    {
                        // O usuário clicou em "Sair" na tela de Login
                        querSairDoApp = true; // Quebra o loop e sai da aplicação.
                    }
                }
            }
            // --- FIM DA MUDANÇA ---
        }
    }
}