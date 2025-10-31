namespace SysFin_2CTDS.View {
    partial class frmRegistroVenda {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            label1 = new Label();
            cmbCliente = new ComboBox();
            groupBox1 = new GroupBox();
            dgvItens = new DataGridView();
            btnFinalizarVenda = new Button();
            lblValorTotal = new Label();
            btnAdicionar = new Button();
            txtQuantidade = new TextBox();
            txtValorUnitario = new TextBox();
            cmbProduto = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            lblEstoqueDisponivel = new Label();
            label2 = new Label();
            ProdutoID = new DataGridViewTextBoxColumn();
            NomeProduto = new DataGridViewTextBoxColumn();
            Quantidade = new DataGridViewTextBoxColumn();
            ValorUnitario = new DataGridViewTextBoxColumn();
            Subtotal = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItens).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 96);
            label1.Name = "label1";
            label1.Size = new Size(54, 17);
            label1.TabIndex = 0;
            label1.Text = "Cliente: ";
            // 
            // cmbCliente
            // 
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(81, 93);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(121, 25);
            cmbCliente.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvItens);
            groupBox1.Controls.Add(btnFinalizarVenda);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(lblValorTotal);
            groupBox1.Controls.Add(cmbCliente);
            groupBox1.Controls.Add(btnAdicionar);
            groupBox1.Controls.Add(txtQuantidade);
            groupBox1.Controls.Add(txtValorUnitario);
            groupBox1.Controls.Add(cmbProduto);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(lblEstoqueDisponivel);
            groupBox1.Controls.Add(label2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(800, 450);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Adicionar Item";
            // 
            // dgvItens
            // 
            dgvItens.AllowUserToAddRows = false;
            dgvItens.AllowUserToDeleteRows = false;
            dgvItens.AllowUserToResizeColumns = false;
            dgvItens.AllowUserToResizeRows = false;
            dgvItens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItens.Columns.AddRange(new DataGridViewColumn[] { ProdutoID, NomeProduto, Quantidade, ValorUnitario, Subtotal });
            dgvItens.Location = new Point(260, 35);
            dgvItens.Name = "dgvItens";
            dgvItens.Size = new Size(513, 263);
            dgvItens.TabIndex = 12;
            // 
            // btnFinalizarVenda
            // 
            btnFinalizarVenda.Location = new Point(520, 334);
            btnFinalizarVenda.Name = "btnFinalizarVenda";
            btnFinalizarVenda.Size = new Size(119, 23);
            btnFinalizarVenda.TabIndex = 11;
            btnFinalizarVenda.Text = "Finalizar Venda";
            btnFinalizarVenda.UseVisualStyleBackColor = true;
            btnFinalizarVenda.Click += btnFinalizarVenda_Click;
            // 
            // lblValorTotal
            // 
            lblValorTotal.AutoSize = true;
            lblValorTotal.Location = new Point(421, 340);
            lblValorTotal.Name = "lblValorTotal";
            lblValorTotal.Size = new Size(93, 17);
            lblValorTotal.TabIndex = 10;
            lblValorTotal.Text = "Valor total: R$:";
            // 
            // btnAdicionar
            // 
            btnAdicionar.Location = new Point(519, 363);
            btnAdicionar.Name = "btnAdicionar";
            btnAdicionar.Size = new Size(120, 23);
            btnAdicionar.TabIndex = 8;
            btnAdicionar.Text = "Adicionar";
            btnAdicionar.UseVisualStyleBackColor = true;
            btnAdicionar.Click += btnAdicionar_Click;
            // 
            // txtQuantidade
            // 
            txtQuantidade.Location = new Point(106, 171);
            txtQuantidade.Name = "txtQuantidade";
            txtQuantidade.Size = new Size(100, 25);
            txtQuantidade.TabIndex = 7;
            // 
            // txtValorUnitario
            // 
            txtValorUnitario.Location = new Point(118, 124);
            txtValorUnitario.Name = "txtValorUnitario";
            txtValorUnitario.Size = new Size(100, 25);
            txtValorUnitario.TabIndex = 6;
            // 
            // cmbProduto
            // 
            cmbProduto.FormattingEnabled = true;
            cmbProduto.Location = new Point(75, 35);
            cmbProduto.Name = "cmbProduto";
            cmbProduto.Size = new Size(121, 25);
            cmbProduto.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(21, 124);
            label6.Name = "label6";
            label6.Size = new Size(91, 17);
            label6.TabIndex = 4;
            label6.Text = "Valor Unitário:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 171);
            label5.Name = "label5";
            label5.Size = new Size(79, 17);
            label5.TabIndex = 3;
            label5.Text = "Quantidade:";
            // 
            // lblEstoqueDisponivel
            // 
            lblEstoqueDisponivel.AutoSize = true;
            lblEstoqueDisponivel.Location = new Point(21, 66);
            lblEstoqueDisponivel.Name = "lblEstoqueDisponivel";
            lblEstoqueDisponivel.Size = new Size(62, 17);
            lblEstoqueDisponivel.TabIndex = 2;
            lblEstoqueDisponivel.Text = "Estoque: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 38);
            label2.Name = "label2";
            label2.Size = new Size(58, 17);
            label2.TabIndex = 0;
            label2.Text = "Produto:";
            // 
            // ProdutoID
            // 
            ProdutoID.HeaderText = "ID:";
            ProdutoID.Name = "ProdutoID";
            ProdutoID.Visible = false;
            // 
            // NomeProduto
            // 
            NomeProduto.HeaderText = "Produto:";
            NomeProduto.Name = "NomeProduto";
            NomeProduto.ReadOnly = true;
            // 
            // Quantidade
            // 
            Quantidade.HeaderText = "Qtde:";
            Quantidade.Name = "Quantidade";
            Quantidade.ReadOnly = true;
            Quantidade.Width = 60;
            // 
            // ValorUnitario
            // 
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            ValorUnitario.DefaultCellStyle = dataGridViewCellStyle1;
            ValorUnitario.HeaderText = "Vlr. Unit.";
            ValorUnitario.Name = "ValorUnitario";
            ValorUnitario.ReadOnly = true;
            // 
            // Subtotal
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            Subtotal.DefaultCellStyle = dataGridViewCellStyle2;
            Subtotal.HeaderText = "Subtotal";
            Subtotal.Name = "Subtotal";
            Subtotal.ReadOnly = true;
            // 
            // frmRegistroVenda
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Name = "frmRegistroVenda";
            Text = "frmRegistroVenda";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItens).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private ComboBox cmbCliente;
        private GroupBox groupBox1;
        private Button button2;
        private Button btnAdicionar;
        private TextBox txtQuantidade;
        private TextBox txtValorUnitario;
        private ComboBox cmbProduto;
        private Label label6;
        private Label label5;
        private Label lblEstoqueDisponivel;
        private Label label2;
        private Label lblValorTotal;
        private Button btnFinalizarVenda;
        private DataGridView dgvItens;
        private DataGridViewTextBoxColumn ProdutoID;
        private DataGridViewTextBoxColumn NomeProduto;
        private DataGridViewTextBoxColumn Quantidade;
        private DataGridViewTextBoxColumn ValorUnitario;
        private DataGridViewTextBoxColumn Subtotal;
    }
}