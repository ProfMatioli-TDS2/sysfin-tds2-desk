namespace SysFin_2CTDS.View
{
    partial class frmCadastroProduto
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtNome = new TextBox();
            txtDescricao = new TextBox();
            numPrecoVenda = new NumericUpDown();
            numEstoque = new NumericUpDown();
            btnSalvar = new Button();
            ((System.ComponentModel.ISupportInitialize)numPrecoVenda).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEstoque).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(32, 85);
            label1.Name = "label1";
            label1.Size = new Size(84, 21);
            label1.TabIndex = 0;
            label1.Text = "Descrição: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(32, 40);
            label2.Name = "label2";
            label2.Size = new Size(56, 21);
            label2.TabIndex = 1;
            label2.Text = "Nome:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(32, 184);
            label3.Name = "label3";
            label3.Size = new Size(112, 21);
            label3.TabIndex = 2;
            label3.Text = "Estoque Inicial:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(32, 135);
            label4.Name = "label4";
            label4.Size = new Size(120, 21);
            label4.TabIndex = 3;
            label4.Text = "Preço de Venda:";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(94, 38);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(130, 23);
            txtNome.TabIndex = 4;
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(122, 83);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(271, 23);
            txtDescricao.TabIndex = 5;
            // 
            // numPrecoVenda
            // 
            numPrecoVenda.DecimalPlaces = 2;
            numPrecoVenda.Location = new Point(158, 133);
            numPrecoVenda.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPrecoVenda.Name = "numPrecoVenda";
            numPrecoVenda.Size = new Size(120, 23);
            numPrecoVenda.TabIndex = 6;
            // 
            // numEstoque
            // 
            numEstoque.Location = new Point(150, 184);
            numEstoque.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numEstoque.Name = "numEstoque";
            numEstoque.Size = new Size(120, 23);
            numEstoque.TabIndex = 7;
            // 
            // btnSalvar
            // 
            btnSalvar.Font = new Font("Segoe UI", 12F);
            btnSalvar.Location = new Point(142, 242);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(119, 45);
            btnSalvar.TabIndex = 8;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // frmCadastroProduto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 310);
            Controls.Add(btnSalvar);
            Controls.Add(numEstoque);
            Controls.Add(numPrecoVenda);
            Controls.Add(txtDescricao);
            Controls.Add(txtNome);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmCadastroProduto";
            Text = "frmCadastroProduto";
            ((System.ComponentModel.ISupportInitialize)numPrecoVenda).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEstoque).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtNome;
        private TextBox txtDescricao;
        private NumericUpDown numPrecoVenda;
        private NumericUpDown numEstoque;
        private Button btnSalvar;
    }
}