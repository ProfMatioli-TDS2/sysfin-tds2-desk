namespace SysFin_2CTDS.View {
    partial class ClienteForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvClientes = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNome = new DataGridViewTextBoxColumn();
            colCpfCnpj = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colTelefone = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            txtTelefone = new TextBox();
            label4 = new Label();
            txtEmail = new TextBox();
            label3 = new Label();
            txtCpfCnpj = new TextBox();
            label2 = new Label();
            txtNome = new TextBox();
            label1 = new Label();
            btnNovo = new Button();
            btnSalvar = new Button();
            btnExcluir = new Button();
            label5 = new Label();
            txtBuscaNome = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Columns.AddRange(new DataGridViewColumn[] { colId, colNome, colCpfCnpj, colEmail, colTelefone });
            dgvClientes.Location = new Point(14, 217);
            dgvClientes.Margin = new Padding(4, 3, 4, 3);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(905, 288);
            dgvClientes.TabIndex = 0;
            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNome
            // 
            colNome.DataPropertyName = "Nome";
            colNome.HeaderText = "Nome";
            colNome.Name = "colNome";
            colNome.ReadOnly = true;
            // 
            // colCpfCnpj
            // 
            colCpfCnpj.DataPropertyName = "CpfCnpj";
            colCpfCnpj.HeaderText = "CPF/CNPJ";
            colCpfCnpj.Name = "colCpfCnpj";
            colCpfCnpj.ReadOnly = true;
            // 
            // colEmail
            // 
            colEmail.DataPropertyName = "Email";
            colEmail.HeaderText = "E-mail";
            colEmail.Name = "colEmail";
            colEmail.ReadOnly = true;
            // 
            // colTelefone
            // 
            colTelefone.DataPropertyName = "Telefone";
            colTelefone.HeaderText = "Telefone";
            colTelefone.Name = "colTelefone";
            colTelefone.ReadOnly = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtBuscaNome);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtTelefone);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtCpfCnpj);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtNome);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(905, 159);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Dados do Cliente";
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(531, 100);
            txtTelefone.Margin = new Padding(4, 3, 4, 3);
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(356, 23);
            txtTelefone.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(527, 82);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 6;
            label4.Text = "Telefone";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(18, 100);
            txtEmail.Margin = new Padding(4, 3, 4, 3);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(487, 23);
            txtEmail.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 82);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 4;
            label3.Text = "E-mail";
            // 
            // txtCpfCnpj
            // 
            txtCpfCnpj.Location = new Point(531, 47);
            txtCpfCnpj.Margin = new Padding(4, 3, 4, 3);
            txtCpfCnpj.Name = "txtCpfCnpj";
            txtCpfCnpj.Size = new Size(356, 23);
            txtCpfCnpj.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(527, 29);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 2;
            label2.Text = "CPF/CNPJ";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(18, 47);
            txtNome.Margin = new Padding(4, 3, 4, 3);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(487, 23);
            txtNome.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 29);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 0;
            label1.Text = "Nome";
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(640, 184);
            btnNovo.Margin = new Padding(4, 3, 4, 3);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(88, 27);
            btnNovo.TabIndex = 2;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(736, 184);
            btnSalvar.Margin = new Padding(4, 3, 4, 3);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(88, 27);
            btnSalvar.TabIndex = 3;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(831, 184);
            btnExcluir.Margin = new Padding(4, 3, 4, 3);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(88, 27);
            btnExcluir.TabIndex = 4;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 132);
            label5.Name = "label5";
            label5.Size = new Size(102, 15);
            label5.TabIndex = 8;
            label5.Text = "Buscar por Nome:";
            // 
            // txtBuscaNome
            // 
            txtBuscaNome.Location = new Point(122, 128);
            txtBuscaNome.Name = "txtBuscaNome";
            txtBuscaNome.Size = new Size(383, 23);
            txtBuscaNome.TabIndex = 0;
            // 
            // ClienteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(btnExcluir);
            Controls.Add(btnSalvar);
            Controls.Add(btnNovo);
            Controls.Add(groupBox1);
            Controls.Add(dgvClientes);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ClienteForm";
            Text = "Gestão de Clientes";
            Load += ClienteForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCpfCnpj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnExcluir;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNome;
        private DataGridViewTextBoxColumn colCpfCnpj;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colTelefone;
        private Label label5;
        private TextBox txtBuscaNome;
    }
}
