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
            this.pnlMenuLateral = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            // --- NOVOS LABELS (SEPARADORES) ---
            this.lblMenuRelatorios = new System.Windows.Forms.Label();
            this.btnEstoque = new System.Windows.Forms.Button();
            this.btnRelatorioVendas = new System.Windows.Forms.Button();
            this.btnRelatorioCompras = new System.Windows.Forms.Button();
            this.btnContasPagar = new System.Windows.Forms.Button();
            this.lblMenuFinanceiro = new System.Windows.Forms.Label();
            this.btnVendas = new System.Windows.Forms.Button();
            this.btnCompras = new System.Windows.Forms.Button();
            this.btnFluxoCaixa = new System.Windows.Forms.Button();
            this.lblMenuCadastros = new System.Windows.Forms.Label();
            // --- FIM DOS NOVOS LABELS ---
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnPlanoContas = new System.Windows.Forms.Button();
            this.btnProdutos = new System.Windows.Forms.Button();
            this.btnFornecedores = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.pnlMenuLateral.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssUsuarioLogado});
            this.statusStrip1.Location = new System.Drawing.Point(220, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(713, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssUsuarioLogado
            // 
            this.tssUsuarioLogado.Name = "tssUsuarioLogado";
            this.tssUsuarioLogado.Size = new System.Drawing.Size(121, 17);
            this.tssUsuarioLogado.Text = "Usuário: (não logado)";
            // 
            // pnlMenuLateral
            // 
            this.pnlMenuLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.pnlMenuLateral.Controls.Add(this.btnSair);
            // --- ORDEM MUDADA E LABELS ADICIONADOS ---
            this.pnlMenuLateral.Controls.Add(this.lblMenuRelatorios);
            this.pnlMenuLateral.Controls.Add(this.btnEstoque);
            this.pnlMenuLateral.Controls.Add(this.btnRelatorioVendas);
            this.pnlMenuLateral.Controls.Add(this.btnRelatorioCompras);
            this.pnlMenuLateral.Controls.Add(this.btnContasPagar);
            this.pnlMenuLateral.Controls.Add(this.lblMenuFinanceiro);
            this.pnlMenuLateral.Controls.Add(this.btnVendas);
            this.pnlMenuLateral.Controls.Add(this.btnCompras);
            this.pnlMenuLateral.Controls.Add(this.btnFluxoCaixa);
            this.pnlMenuLateral.Controls.Add(this.lblMenuCadastros);
            // --- FIM DA MUDANÇA ---
            this.pnlMenuLateral.Controls.Add(this.btnUsuarios);
            this.pnlMenuLateral.Controls.Add(this.btnPlanoContas);
            this.pnlMenuLateral.Controls.Add(this.btnProdutos);
            this.pnlMenuLateral.Controls.Add(this.btnFornecedores);
            this.pnlMenuLateral.Controls.Add(this.btnClientes);
            this.pnlMenuLateral.Controls.Add(this.pnlLogo);
            this.pnlMenuLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenuLateral.Location = new System.Drawing.Point(0, 0);
            this.pnlMenuLateral.Name = "pnlMenuLateral";
            this.pnlMenuLateral.Size = new System.Drawing.Size(220, 561);
            this.pnlMenuLateral.TabIndex = 4;
            // 
            // btnSair
            // 
            this.btnSair.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("tsbFechar.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(0, 521);
            this.btnSair.Name = "btnSair";
            this.btnSair.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSair.Size = new System.Drawing.Size(220, 40);
            this.btnSair.TabIndex = 12;
            this.btnSair.Text = "  Sair (Logout)";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // lblMenuRelatorios
            // 
            this.lblMenuRelatorios.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuRelatorios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuRelatorios.ForeColor = System.Drawing.Color.Gray;
            this.lblMenuRelatorios.Location = new System.Drawing.Point(0, 500);
            this.lblMenuRelatorios.Name = "lblMenuRelatorios";
            this.lblMenuRelatorios.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblMenuRelatorios.Size = new System.Drawing.Size(220, 28);
            this.lblMenuRelatorios.TabIndex = 22;
            this.lblMenuRelatorios.Text = "RELATÓRIOS";
            // 
            // btnEstoque
            // 
            this.btnEstoque.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEstoque.FlatAppearance.BorderSize = 0;
            this.btnEstoque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstoque.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstoque.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnEstoque.Image = ((System.Drawing.Image)(resources.GetObject("tsbEstoque.Image")));
            this.btnEstoque.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstoque.Location = new System.Drawing.Point(0, 528); // Ajustado
            this.btnEstoque.Name = "btnEstoque";
            this.btnEstoque.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnEstoque.Size = new System.Drawing.Size(220, 40);
            this.btnEstoque.TabIndex = 10;
            this.btnEstoque.Text = "  Estoque";
            this.btnEstoque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstoque.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEstoque.UseVisualStyleBackColor = true;
            this.btnEstoque.Click += new System.EventHandler(this.btnEstoque_Click);
            // 
            // btnRelatorioVendas
            // 
            this.btnRelatorioVendas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRelatorioVendas.FlatAppearance.BorderSize = 0;
            this.btnRelatorioVendas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorioVendas.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelatorioVendas.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRelatorioVendas.Image = ((System.Drawing.Image)(resources.GetObject("tsbSalvar.Image")));
            this.btnRelatorioVendas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelatorioVendas.Location = new System.Drawing.Point(0, 488); // Ajustado
            this.btnRelatorioVendas.Name = "btnRelatorioVendas";
            this.btnRelatorioVendas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRelatorioVendas.Size = new System.Drawing.Size(220, 40);
            this.btnRelatorioVendas.TabIndex = 7;
            this.btnRelatorioVendas.Text = "  Relatório de Vendas";
            this.btnRelatorioVendas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelatorioVendas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRelatorioVendas.UseVisualStyleBackColor = true;
            this.btnRelatorioVendas.Click += new System.EventHandler(this.btnRelatorioVendas_Click);
            // 
            // btnRelatorioCompras
            // 
            this.btnRelatorioCompras.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRelatorioCompras.FlatAppearance.BorderSize = 0;
            this.btnRelatorioCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorioCompras.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelatorioCompras.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRelatorioCompras.Image = ((System.Drawing.Image)(resources.GetObject("tsbSalvar.Image")));
            this.btnRelatorioCompras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelatorioCompras.Location = new System.Drawing.Point(0, 448); // Ajustado
            this.btnRelatorioCompras.Name = "btnRelatorioCompras";
            this.btnRelatorioCompras.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRelatorioCompras.Size = new System.Drawing.Size(220, 40);
            this.btnRelatorioCompras.TabIndex = 6;
            this.btnRelatorioCompras.Text = "  Relatório de Compras";
            this.btnRelatorioCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelatorioCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRelatorioCompras.UseVisualStyleBackColor = true;
            this.btnRelatorioCompras.Click += new System.EventHandler(this.btnRelatorioCompras_Click);
            // 
            // btnContasPagar
            // 
            this.btnContasPagar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnContasPagar.FlatAppearance.BorderSize = 0;
            this.btnContasPagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContasPagar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContasPagar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnContasPagar.Image = ((System.Drawing.Image)(resources.GetObject("tsbSalvar.Image")));
            this.btnContasPagar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnContasPagar.Location = new System.Drawing.Point(0, 408); // Ajustado
            this.btnContasPagar.Name = "btnContasPagar";
            this.btnContasPagar.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnContasPagar.Size = new System.Drawing.Size(220, 40);
            this.btnContasPagar.TabIndex = 5;
            this.btnContasPagar.Text = "  Contas a Pagar";
            this.btnContasPagar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnContasPagar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnContasPagar.UseVisualStyleBackColor = true;
            this.btnContasPagar.Click += new System.EventHandler(this.btnContasPagar_Click);
            // 
            // lblMenuFinanceiro
            // 
            this.lblMenuFinanceiro.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuFinanceiro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuFinanceiro.ForeColor = System.Drawing.Color.Gray;
            this.lblMenuFinanceiro.Location = new System.Drawing.Point(0, 380);
            this.lblMenuFinanceiro.Name = "lblMenuFinanceiro";
            this.lblMenuFinanceiro.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblMenuFinanceiro.Size = new System.Drawing.Size(220, 28);
            this.lblMenuFinanceiro.TabIndex = 21;
            this.lblMenuFinanceiro.Text = "FINANCEIRO";
            // 
            // btnVendas
            // 
            this.btnVendas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVendas.FlatAppearance.BorderSize = 0;
            this.btnVendas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVendas.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVendas.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnVendas.Image = ((System.Drawing.Image)(resources.GetObject("tsbVendas.Image")));
            this.btnVendas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVendas.Location = new System.Drawing.Point(0, 340); // Ajustado
            this.btnVendas.Name = "btnVendas";
            this.btnVendas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnVendas.Size = new System.Drawing.Size(220, 40);
            this.btnVendas.TabIndex = 9;
            this.btnVendas.Text = "  Registrar Vendas";
            this.btnVendas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVendas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVendas.UseVisualStyleBackColor = true;
            this.btnVendas.Click += new System.EventHandler(this.btnVendas_Click);
            // 
            // btnCompras
            // 
            this.btnCompras.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCompras.FlatAppearance.BorderSize = 0;
            this.btnCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompras.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompras.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCompras.Image = ((System.Drawing.Image)(resources.GetObject("tsbCompras.Image")));
            this.btnCompras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCompras.Location = new System.Drawing.Point(0, 300); // Ajustado
            this.btnCompras.Name = "btnCompras";
            this.btnCompras.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnCompras.Size = new System.Drawing.Size(220, 40);
            this.btnCompras.TabIndex = 8;
            this.btnCompras.Text = "  Registrar Compras";
            this.btnCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCompras.UseVisualStyleBackColor = true;
            this.btnCompras.Click += new System.EventHandler(this.btnCompras_Click);
            // 
            // btnFluxoCaixa
            // 
            this.btnFluxoCaixa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFluxoCaixa.FlatAppearance.BorderSize = 0;
            this.btnFluxoCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFluxoCaixa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFluxoCaixa.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnFluxoCaixa.Image = ((System.Drawing.Image)(resources.GetObject("tsbSalvar.Image")));
            this.btnFluxoCaixa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFluxoCaixa.Location = new System.Drawing.Point(0, 260); // Ajustado
            this.btnFluxoCaixa.Name = "btnFluxoCaixa";
            this.btnFluxoCaixa.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnFluxoCaixa.Size = new System.Drawing.Size(220, 40);
            this.btnFluxoCaixa.TabIndex = 4;
            this.btnFluxoCaixa.Text = "  Fluxo de Caixa";
            this.btnFluxoCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFluxoCaixa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFluxoCaixa.UseVisualStyleBackColor = true;
            this.btnFluxoCaixa.Click += new System.EventHandler(this.btnFluxoCaixa_Click);
            // 
            // lblMenuCadastros
            // 
            this.lblMenuCadastros.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuCadastros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuCadastros.ForeColor = System.Drawing.Color.Gray;
            this.lblMenuCadastros.Location = new System.Drawing.Point(0, 100);
            this.lblMenuCadastros.Name = "lblMenuCadastros";
            this.lblMenuCadastros.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblMenuCadastros.Size = new System.Drawing.Size(220, 28);
            this.lblMenuCadastros.TabIndex = 20;
            this.lblMenuCadastros.Text = "CADASTROS";
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarios.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnUsuarios.Image = ((System.Drawing.Image)(resources.GetObject("tsbNovo.Image")));
            this.btnUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 220); // Ajustado
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnUsuarios.Size = new System.Drawing.Size(220, 40);
            this.btnUsuarios.TabIndex = 3;
            this.btnUsuarios.Text = "  Usuários";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnPlanoContas
            // 
            this.btnPlanoContas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPlanoContas.FlatAppearance.BorderSize = 0;
            this.btnPlanoContas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanoContas.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlanoContas.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPlanoContas.Image = ((System.Drawing.Image)(resources.GetObject("tsbNovo.Image")));
            this.btnPlanoContas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlanoContas.Location = new System.Drawing.Point(0, 180); // Ajustado
            this.btnPlanoContas.Name = "btnPlanoContas";
            this.btnPlanoContas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnPlanoContas.Size = new System.Drawing.Size(220, 40);
            this.btnPlanoContas.TabIndex = 2;
            this.btnPlanoContas.Text = "  Plano de Contas";
            this.btnPlanoContas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlanoContas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPlanoContas.UseVisualStyleBackColor = true;
            this.btnPlanoContas.Click += new System.EventHandler(this.btnPlanoContas_Click);
            // 
            // btnProdutos
            // 
            this.btnProdutos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProdutos.FlatAppearance.BorderSize = 0;
            this.btnProdutos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProdutos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProdutos.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnProdutos.Image = ((System.Drawing.Image)(resources.GetObject("tsbNovo.Image")));
            this.btnProdutos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProdutos.Location = new System.Drawing.Point(0, 140); // Ajustado
            this.btnProdutos.Name = "btnProdutos";
            this.btnProdutos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnProdutos.Size = new System.Drawing.Size(220, 40);
            this.btnProdutos.TabIndex = 1;
            this.btnProdutos.Text = "  Produtos";
            this.btnProdutos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProdutos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProdutos.UseVisualStyleBackColor = true;
            this.btnProdutos.Click += new System.EventHandler(this.btnProdutos_Click);
            // 
            // btnFornecedores
            // 
            this.btnFornecedores.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFornecedores.FlatAppearance.BorderSize = 0;
            this.btnFornecedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFornecedores.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFornecedores.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnFornecedores.Image = ((System.Drawing.Image)(resources.GetObject("tsbNovo.Image")));
            this.btnFornecedores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFornecedores.Location = new System.Drawing.Point(0, 128); // Ajustado
            this.btnFornecedores.Name = "btnFornecedores";
            this.btnFornecedores.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnFornecedores.Size = new System.Drawing.Size(220, 40);
            this.btnFornecedores.TabIndex = 0;
            this.btnFornecedores.Text = "  Fornecedores";
            this.btnFornecedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFornecedores.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFornecedores.UseVisualStyleBackColor = true;
            this.btnFornecedores.Click += new System.EventHandler(this.btnFornecedores_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClientes.FlatAppearance.BorderSize = 0;
            this.btnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClientes.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnClientes.Image = ((System.Drawing.Image)(resources.GetObject("tsbNovo.Image")));
            this.btnClientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClientes.Location = new System.Drawing.Point(0, 128); // Ajustado
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnClientes.Size = new System.Drawing.Size(220, 40);
            this.btnClientes.TabIndex = 0;
            this.btnClientes.Text = "  Clientes";
            this.btnClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.Controls.Add(this.label1);
            this.pnlLogo.Controls.Add(this.pictureBox1);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(220, 100);
            this.pnlLogo.TabIndex = 0;
            this.pnlLogo.Click += new System.EventHandler(this.pnlLogo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(82, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "SysFin";
            this.label1.Click += new System.EventHandler(this.pnlLogo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(23, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(53, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pnlLogo_Click);
            // 
            // MdiPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(933, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlMenuLateral);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MdiPrincipal";
            this.Text = "SysFin - Controle Financeiro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MdiPrincipal_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlMenuLateral.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssUsuarioLogado;
        private System.Windows.Forms.Panel pnlMenuLateral;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnEstoque;
        private System.Windows.Forms.Button btnVendas;
        private System.Windows.Forms.Button btnCompras;
        private System.Windows.Forms.Button btnContasPagar;
        private System.Windows.Forms.Button btnFluxoCaixa;
        private System.Windows.Forms.Button btnPlanoContas;
        private System.Windows.Forms.Button btnProdutos;
        private System.Windows.Forms.Button btnFornecedores;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRelatorioCompras;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnRelatorioVendas;
        // --- NOVOS LABELS (SEPARADORES) ---
        private System.Windows.Forms.Label lblMenuCadastros;
        private System.Windows.Forms.Label lblMenuFinanceiro;
        private System.Windows.Forms.Label lblMenuRelatorios;
    }
}