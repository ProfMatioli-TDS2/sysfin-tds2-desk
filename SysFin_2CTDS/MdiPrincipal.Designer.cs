namespace SysFin_2CTDS.View
{
    partial class MdiPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            cadastrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fornecedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            // MUDANÇA: Alterado o nome da variável e o texto
            produtosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            comprasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            vendasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            estoqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { cadastrosToolStripMenuItem, comprasToolStripMenuItem, vendasToolStripMenuItem, estoqueToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(933, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // cadastrosToolStripMenuItem
            // 
            // MUDANÇA: Adicionado 'produtosToolStripMenuItem'
            cadastrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { clientesToolStripMenuItem, fornecedoresToolStripMenuItem, produtosToolStripMenuItem });
            cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            cadastrosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            cadastrosToolStripMenuItem.Text = "Cadastros";
            // 
            // clientesToolStripMenuItem
            // 
            clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            clientesToolStripMenuItem.Size = new System.Drawing.Size(180, 22); // MUDANÇA: Size
            clientesToolStripMenuItem.Text = "Clientes";
            clientesToolStripMenuItem.Click += clientesToolStripMenuItem_Click;
            // 
            // fornecedoresToolStripMenuItem
            // 
            fornecedoresToolStripMenuItem.Name = "fornecedoresToolStripMenuItem";
            fornecedoresToolStripMenuItem.Size = new System.Drawing.Size(180, 22); // MUDANÇA: Size
            fornecedoresToolStripMenuItem.Text = "Fornecedores";
            fornecedoresToolStripMenuItem.Click += fornecedoresToolStripMenuItem_Click;
            // 
            // produtosToolStripMenuItem
            // 
            // MUDANÇA: Este era o 'visualizarCadastrosToolStripMenuItem'
            produtosToolStripMenuItem.Name = "produtosToolStripMenuItem";
            produtosToolStripMenuItem.Size = new System.Drawing.Size(180, 22); // MUDANÇA: Size
            produtosToolStripMenuItem.Text = "Produtos"; // MUDANÇA: Texto
            produtosToolStripMenuItem.Click += produtosToolStripMenuItem_Click; // MUDANÇA: Evento
            // 
            // comprasToolStripMenuItem
            // 
            comprasToolStripMenuItem.Name = "comprasToolStripMenuItem";
            comprasToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            comprasToolStripMenuItem.Text = "Compras";
            // MUDANÇA: Adicionado evento de clique
            comprasToolStripMenuItem.Click += comprasToolStripMenuItem_Click;
            // 
            // vendasToolStripMenuItem
            // 
            vendasToolStripMenuItem.Name = "vendasToolStripMenuItem";
            vendasToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            vendasToolStripMenuItem.Text = "Vendas";
            // MUDANÇA: Adicionado evento de clique
            vendasToolStripMenuItem.Click += vendasToolStripMenuItem_Click;
            // 
            // estoqueToolStripMenuItem
            // 
            estoqueToolStripMenuItem.Name = "estoqueToolStripMenuItem";
            estoqueToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            estoqueToolStripMenuItem.Text = "Estoque";
            estoqueToolStripMenuItem.Click += estoqueToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new System.Drawing.Point(0, 497);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(933, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // MdiPrincipal
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MdiPrincipal";
            Text = "SysFin - Controle Financeiro";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cadastrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comprasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendasToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fornecedoresToolStripMenuItem;
        // MUDANÇA: Renomeado
        private System.Windows.Forms.ToolStripMenuItem produtosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estoqueToolStripMenuItem;
    }
}
