namespace SysFin_2CTDS.View
{
    partial class frmRegistroCompras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            cboFornecedor = new ComboBox();
            grpItem = new GroupBox();
            btnAdicionar = new Button();
            numValorUnitario = new NumericUpDown();
            label4 = new Label();
            numQuantidade = new NumericUpDown();
            label3 = new Label();
            cboProduto = new ComboBox();
            label2 = new Label();
            dgvItensCompra = new DataGridView();
            label5 = new Label();
            lblValorTotal = new Label();
            btnFinalizarCompra = new Button();
            grpItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numValorUnitario).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQuantidade).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvItensCompra).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 0;
            label1.Text = "Fornecedor:";
            // 
            // cboFornecedor
            // 
            cboFornecedor.FormattingEnabled = true;
            cboFornecedor.Location = new Point(88, 6);
            cboFornecedor.Name = "cboFornecedor";
            cboFornecedor.Size = new Size(307, 23);
            cboFornecedor.TabIndex = 1;
            // 
            // grpItem
            // 
            grpItem.Controls.Add(btnAdicionar);
            grpItem.Controls.Add(numValorUnitario);
            grpItem.Controls.Add(label4);
            grpItem.Controls.Add(numQuantidade);
            grpItem.Controls.Add(label3);
            grpItem.Controls.Add(cboProduto);
            grpItem.Controls.Add(label2);
            grpItem.Location = new Point(12, 35);
            grpItem.Name = "grpItem";
            grpItem.Size = new Size(383, 162);
            grpItem.TabIndex = 2;
            grpItem.TabStop = false;
            grpItem.Text = "Adicionar Item à Compra";
            // 
            // btnAdicionar
            // 
            btnAdicionar.Location = new Point(6, 121);
            btnAdicionar.Name = "btnAdicionar";
            btnAdicionar.Size = new Size(310, 23);
            btnAdicionar.TabIndex = 6;
            btnAdicionar.Text = "Adicionar Item";
            btnAdicionar.UseVisualStyleBackColor = true;
            btnAdicionar.Click += btnAdicionar_Click;
            // 
            // numValorUnitario
            // 
            numValorUnitario.DecimalPlaces = 2;
            numValorUnitario.Location = new Point(6, 92);
            numValorUnitario.Name = "numValorUnitario";
            numValorUnitario.Size = new Size(133, 23);
            numValorUnitario.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 74);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 4;
            label4.Text = "Valor Unitário";
            // 
            // numQuantidade
            // 
            numQuantidade.Location = new Point(183, 38);
            numQuantidade.Name = "numQuantidade";
            numQuantidade.Size = new Size(133, 23);
            numQuantidade.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(183, 19);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 2;
            label3.Text = "Quantiade :";
            // 
            // cboProduto
            // 
            cboProduto.FormattingEnabled = true;
            cboProduto.Location = new Point(6, 37);
            cboProduto.Name = "cboProduto";
            cboProduto.Size = new Size(133, 23);
            cboProduto.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 0;
            label2.Text = "Produtos :";
            // 
            // dgvItensCompra
            // 
            dgvItensCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItensCompra.Location = new Point(12, 203);
            dgvItensCompra.Name = "dgvItensCompra";
            dgvItensCompra.Size = new Size(383, 216);
            dgvItensCompra.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 434);
            label5.Name = "label5";
            label5.Size = new Size(166, 21);
            label5.TabIndex = 4;
            label5.Text = "Valor Total da Compra:";
            // 
            // lblValorTotal
            // 
            lblValorTotal.AutoSize = true;
            lblValorTotal.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValorTotal.Location = new Point(184, 434);
            lblValorTotal.Name = "lblValorTotal";
            lblValorTotal.Size = new Size(63, 21);
            lblValorTotal.TabIndex = 5;
            lblValorTotal.Text = "R$ 0,00";
            // 
            // btnFinalizarCompra
            // 
            btnFinalizarCompra.Location = new Point(12, 469);
            btnFinalizarCompra.Name = "btnFinalizarCompra";
            btnFinalizarCompra.Size = new Size(383, 23);
            btnFinalizarCompra.TabIndex = 6;
            btnFinalizarCompra.Text = "Finalizar Compra";
            btnFinalizarCompra.UseVisualStyleBackColor = true;
            btnFinalizarCompra.Click += btnFinalizarCompra_Click;
            // 
            // frmRegistroCompras
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(943, 574);
            Controls.Add(btnFinalizarCompra);
            Controls.Add(lblValorTotal);
            Controls.Add(label5);
            Controls.Add(dgvItensCompra);
            Controls.Add(grpItem);
            Controls.Add(cboFornecedor);
            Controls.Add(label1);
            Name = "frmRegistroCompras";
            Text = "frmRegistroCompras";
            Load += frmRegistroCompras_Load;
            grpItem.ResumeLayout(false);
            grpItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numValorUnitario).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQuantidade).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvItensCompra).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cboFornecedor;
        private GroupBox grpItem;
        private Label label2;
        private NumericUpDown numValorUnitario;
        private Label label4;
        private NumericUpDown numQuantidade;
        private Label label3;
        private ComboBox cboProduto;
        private Button btnAdicionar;
        private DataGridView dgvItensCompra;
        private Label label5;
        private Label lblValorTotal;
        private Button btnFinalizarCompra;
    }
}