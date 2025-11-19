namespace SysFin_2CTDS.View
{
    partial class MdiPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiPrincipal));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssUsuarioLogado = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlSideMenu = new System.Windows.Forms.Panel();

            // Definição dos Botões e Labels
            this.btnSair = new System.Windows.Forms.Button();

            // Grupo Relatórios
            this.btnRelatorioVendas = new System.Windows.Forms.Button();
            this.btnRelatorioCompras = new System.Windows.Forms.Button();
            this.btnContasPagar = new System.Windows.Forms.Button();
            this.lblMenuRelatorios = new System.Windows.Forms.Label();

            // Grupo Movimentações
            this.btnEstoque = new System.Windows.Forms.Button();
            this.btnVendas = new System.Windows.Forms.Button();
            this.btnCompras = new System.Windows.Forms.Button();
            this.btnFluxoCaixa = new System.Windows.Forms.Button();
            this.lblMenuMovimentacoes = new System.Windows.Forms.Label();

            // Grupo Cadastros
            this.btnPlanoDeContas = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnProdutos = new System.Windows.Forms.Button();
            this.btnFornecedores = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.lblMenuCadastros = new System.Windows.Forms.Label();

            // Logo
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();

            this.statusStrip1.SuspendLayout();
            this.pnlSideMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssUsuarioLogado});
            this.statusStrip1.Location = new System.Drawing.Point(250, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(683, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssUsuarioLogado
            // 
            this.tssUsuarioLogado.Name = "tssUsuarioLogado";
            this.tssUsuarioLogado.Size = new System.Drawing.Size(121, 17);
            this.tssUsuarioLogado.Text = "Usuário: (não logado)";

            // 
            // pnlSideMenu
            // 
            this.pnlSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.pnlSideMenu.AutoScroll = true;
            this.pnlSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSideMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlSideMenu.Name = "pnlSideMenu";
            this.pnlSideMenu.Size = new System.Drawing.Size(250, 561);
            this.pnlSideMenu.TabIndex = 4;

            // --- ORDEM DE ADIÇÃO DOS CONTROLES ---
            // IMPORTANTE: Para Dock=Top, o ÚLTIMO adicionado fica no TOPO VISUAL.
            // Estamos adicionando de BAIXO para CIMA.

            // 1. Botão Sair (Dock Bottom)
            this.pnlSideMenu.Controls.Add(this.btnSair);

            // 2. Grupo Relatórios (Ficam no final da lista visual)
            this.pnlSideMenu.Controls.Add(this.btnRelatorioVendas);
            this.pnlSideMenu.Controls.Add(this.btnRelatorioCompras);
            this.pnlSideMenu.Controls.Add(this.btnContasPagar);
            this.pnlSideMenu.Controls.Add(this.lblMenuRelatorios);

            // 3. Grupo Movimentações (Ficam no meio)
            this.pnlSideMenu.Controls.Add(this.btnEstoque);
            this.pnlSideMenu.Controls.Add(this.btnVendas);
            this.pnlSideMenu.Controls.Add(this.btnCompras);
            this.pnlSideMenu.Controls.Add(this.btnFluxoCaixa);
            this.pnlSideMenu.Controls.Add(this.lblMenuMovimentacoes);

            // 4. Grupo Cadastros (Ficam no topo, abaixo do logo)
            this.pnlSideMenu.Controls.Add(this.btnPlanoDeContas);
            this.pnlSideMenu.Controls.Add(this.btnUsuarios);
            this.pnlSideMenu.Controls.Add(this.btnProdutos);
            this.pnlSideMenu.Controls.Add(this.btnFornecedores);
            this.pnlSideMenu.Controls.Add(this.btnClientes);
            this.pnlSideMenu.Controls.Add(this.lblMenuCadastros);

            // 5. Logo (Fica no topo absoluto)
            this.pnlSideMenu.Controls.Add(this.pnlLogo);

            // --- Configuração dos Controles ---

            // Sair
            this.ConfigurarBotaoMenu(this.btnSair, "  Sair (Logout)", "tsbFechar.Image");
            this.btnSair.Dock = System.Windows.Forms.DockStyle.Bottom; // Exceção: Dock Bottom
            this.btnSair.ForeColor = System.Drawing.Color.Salmon;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);

            // Relatórios
            this.ConfigurarBotaoMenu(this.btnRelatorioVendas, "  Relatório de Vendas", "tsbSalvar.Image");
            this.btnRelatorioVendas.Click += new System.EventHandler(this.btnRelatorioVendas_Click);

            this.ConfigurarBotaoMenu(this.btnRelatorioCompras, "  Relatório de Compras", "tsbSalvar.Image");
            this.btnRelatorioCompras.Click += new System.EventHandler(this.btnRelatorioCompras_Click);

            this.ConfigurarBotaoMenu(this.btnContasPagar, "  Contas a Pagar", "tsbSalvar.Image");
            this.btnContasPagar.Click += new System.EventHandler(this.btnContasPagar_Click);

            this.ConfigurarSeparador(this.lblMenuRelatorios, "RELATÓRIOS");

            // Movimentações
            this.ConfigurarBotaoMenu(this.btnEstoque, "  Estoque", "tsbEstoque.Image");
            this.btnEstoque.Click += new System.EventHandler(this.btnEstoque_Click);

            this.ConfigurarBotaoMenu(this.btnVendas, "  Registrar Vendas", "tsbVendas.Image");
            this.btnVendas.Click += new System.EventHandler(this.btnVendas_Click);

            this.ConfigurarBotaoMenu(this.btnCompras, "  Registrar Compras", "tsbCompras.Image");
            this.btnCompras.Click += new System.EventHandler(this.btnCompras_Click);

            this.ConfigurarBotaoMenu(this.btnFluxoCaixa, "  Fluxo de Caixa", "tsbSalvar.Image");
            this.btnFluxoCaixa.Click += new System.EventHandler(this.btnFluxoCaixa_Click);

            this.ConfigurarSeparador(this.lblMenuMovimentacoes, "FINANCEIRO");

            // Cadastros
            this.ConfigurarBotaoMenu(this.btnPlanoDeContas, "  Plano de Contas", "tsbNovo.Image");
            this.btnPlanoDeContas.Click += new System.EventHandler(this.btnPlanoDeContas_Click);

            this.ConfigurarBotaoMenu(this.btnUsuarios, "  Usuários", "tsbNovo.Image");
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);

            this.ConfigurarBotaoMenu(this.btnProdutos, "  Produtos", "tsbNovo.Image");
            this.btnProdutos.Click += new System.EventHandler(this.btnProdutos_Click);

            this.ConfigurarBotaoMenu(this.btnFornecedores, "  Fornecedores", "tsbNovo.Image");
            this.btnFornecedores.Click += new System.EventHandler(this.btnFornecedores_Click);

            this.ConfigurarBotaoMenu(this.btnClientes, "  Clientes", "tsbNovo.Image");
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);

            this.ConfigurarSeparador(this.lblMenuCadastros, "CADASTROS");

            // Logo
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.pnlLogo.Controls.Add(this.label1);
            this.pnlLogo.Controls.Add(this.pictureBox1);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(250, 100);
            this.pnlLogo.TabIndex = 0;
            this.pnlLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlLogo.Click += new System.EventHandler(this.pnlLogo_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(90, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "SysFin";
            this.label1.Click += new System.EventHandler(this.pnlLogo_Click);

            // pictureBox1
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(20, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pnlLogo_Click);

            // MdiPrincipal
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1084, 611);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlSideMenu);
            this.IsMdiContainer = true;
            this.Name = "MdiPrincipal";
            this.Text = "SysFin - Sistema Financeiro 2.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MdiPrincipal_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlSideMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigurarBotaoMenu(System.Windows.Forms.Button btn, string texto, string resourceName)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiPrincipal));
            btn.Dock = System.Windows.Forms.DockStyle.Top;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(67)))), ((int)(((byte)(70)))));
            btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btn.ForeColor = System.Drawing.Color.Gainsboro;
            btn.Image = ((System.Drawing.Image)(resources.GetObject(resourceName)));
            btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Name = "btn" + texto.Replace(" ", "").Trim();
            btn.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            btn.Size = new System.Drawing.Size(250, 45);
            btn.TabIndex = 0;
            btn.Text = "  " + texto.Trim();
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btn.UseVisualStyleBackColor = true;
        }

        private void ConfigurarSeparador(System.Windows.Forms.Label lbl, string texto)
        {
            lbl.Dock = System.Windows.Forms.DockStyle.Top;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl.ForeColor = System.Drawing.Color.Gray;
            lbl.Name = "lbl" + texto.Replace(" ", "");
            lbl.Padding = new System.Windows.Forms.Padding(10, 15, 0, 5);
            lbl.Size = new System.Drawing.Size(250, 40);
            lbl.TabIndex = 0;
            lbl.Text = texto;
        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssUsuarioLogado;
        private System.Windows.Forms.Panel pnlSideMenu;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSair;

        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnFornecedores;
        private System.Windows.Forms.Button btnProdutos;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnPlanoDeContas;

        private System.Windows.Forms.Button btnFluxoCaixa;
        private System.Windows.Forms.Button btnCompras;
        private System.Windows.Forms.Button btnVendas;
        private System.Windows.Forms.Button btnEstoque;

        private System.Windows.Forms.Button btnContasPagar;
        private System.Windows.Forms.Button btnRelatorioCompras;
        private System.Windows.Forms.Button btnRelatorioVendas;

        private System.Windows.Forms.Label lblMenuCadastros;
        private System.Windows.Forms.Label lblMenuMovimentacoes;
        private System.Windows.Forms.Label lblMenuRelatorios;
    }
}