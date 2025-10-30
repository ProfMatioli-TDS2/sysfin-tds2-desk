namespace SysFin_2CTDS.View
{
    partial class FornecedorForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvFornecedores = new DataGridView();
            groupBox1 = new GroupBox();
            mtbTelefone = new MaskedTextBox();
            label4 = new Label();
            txtEmail = new TextBox();
            label3 = new Label();
            mtbCnpj = new MaskedTextBox();
            label2 = new Label();
            txtNome = new TextBox();
            label1 = new Label();
            btnNovo = new Button();
            btnSalvar = new Button();
            btnExcluir = new Button();
            btnGerarRelatorio = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvFornecedores).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvFornecedores
            // 
            dgvFornecedores.AllowUserToAddRows = false;
            dgvFornecedores.AllowUserToDeleteRows = false;
            dgvFornecedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFornecedores.Location = new Point(16, 289);
            dgvFornecedores.Margin = new Padding(4, 5, 4, 5);
            dgvFornecedores.MultiSelect = false;
            dgvFornecedores.Name = "dgvFornecedores";
            dgvFornecedores.ReadOnly = true;
            dgvFornecedores.RowHeadersWidth = 51;
            dgvFornecedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFornecedores.Size = new Size(1035, 385);
            dgvFornecedores.TabIndex = 0;
            dgvFornecedores.SelectionChanged += dgvFornecedores_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(mtbTelefone);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(mtbCnpj);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtNome);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 18);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(1035, 197);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Dados do Fornecedor";
            // 
            // mtbTelefone
            // 
            mtbTelefone.Location = new Point(607, 134);
            mtbTelefone.Margin = new Padding(4, 5, 4, 5);
            mtbTelefone.Mask = "(99) 00000-0000";
            mtbTelefone.Name = "mtbTelefone";
            mtbTelefone.Size = new Size(407, 27);
            mtbTelefone.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(603, 109);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(66, 20);
            label4.TabIndex = 6;
            label4.Text = "Telefone";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(20, 134);
            txtEmail.Margin = new Padding(4, 5, 4, 5);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(556, 27);
            txtEmail.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 109);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 4;
            label3.Text = "E-mail";
            // 
            // mtbCnpj
            // 
            mtbCnpj.Location = new Point(607, 63);
            mtbCnpj.Margin = new Padding(4, 5, 4, 5);
            mtbCnpj.Mask = "99.999.999/9999-99";
            mtbCnpj.Name = "mtbCnpj";
            mtbCnpj.Size = new Size(407, 27);
            mtbCnpj.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(603, 38);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 2;
            label2.Text = "CNPJ";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(20, 63);
            txtNome.Margin = new Padding(4, 5, 4, 5);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(556, 27);
            txtNome.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 38);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "Nome";
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(732, 225);
            btnNovo.Margin = new Padding(4, 5, 4, 5);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(100, 35);
            btnNovo.TabIndex = 2;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(840, 225);
            btnSalvar.Margin = new Padding(4, 5, 4, 5);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(100, 35);
            btnSalvar.TabIndex = 3;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(948, 225);
            btnExcluir.Margin = new Padding(4, 5, 4, 5);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(100, 35);
            btnExcluir.TabIndex = 4;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // btnGerarRelatorio
            // 
            btnGerarRelatorio.Location = new Point(623, 225);
            btnGerarRelatorio.Name = "btnGerarRelatorio";
            btnGerarRelatorio.Size = new Size(102, 35);
            btnGerarRelatorio.TabIndex = 5;
            btnGerarRelatorio.Text = "Relatório";
            btnGerarRelatorio.UseVisualStyleBackColor = true;
            btnGerarRelatorio.Click += btnGerarRelatorio_Click_1;
            // 
            // FornecedorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 692);
            Controls.Add(btnGerarRelatorio);
            Controls.Add(btnExcluir);
            Controls.Add(btnSalvar);
            Controls.Add(btnNovo);
            Controls.Add(groupBox1);
            Controls.Add(dgvFornecedores);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FornecedorForm";
            Text = "Gestão de Fornecedores";
            Load += FornecedorForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFornecedores).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFornecedores;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox mtbTelefone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mtbCnpj; // Alterado para MaskedTextBox
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnExcluir;
        private Button btnGerarRelatorio;
    }
}
