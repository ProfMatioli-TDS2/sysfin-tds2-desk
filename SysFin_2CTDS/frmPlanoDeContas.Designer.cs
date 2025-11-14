namespace SysFin_2CTDS.View
{
    partial class frmPlanoDeContas
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
            dgvContas = new DataGridView();
            groupBox1 = new GroupBox();
            cboTipo = new ComboBox();
            label2 = new Label();
            txtDescricao = new TextBox();
            label1 = new Label();
            btnSalvar = new Button();
            btnExcluir = new Button();
            label = new Label();
            btnPDF = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvContas).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvContas
            // 
            dgvContas.AllowUserToAddRows = false;
            dgvContas.AllowUserToDeleteRows = false;
            dgvContas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContas.Location = new Point(14, 178);
            dgvContas.Margin = new Padding(4, 3, 4, 3);
            dgvContas.MultiSelect = false;
            dgvContas.Name = "dgvContas";
            dgvContas.ReadOnly = true;
            dgvContas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContas.Size = new Size(906, 327);
            dgvContas.TabIndex = 4;
            dgvContas.SelectionChanged += dgvContas_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ActiveCaption;
            groupBox1.Controls.Add(cboTipo);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtDescricao);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(15, 74);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(905, 98);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Plano de Contas";
            // 
            // cboTipo
            // 
            cboTipo.FormattingEnabled = true;
            cboTipo.Location = new Point(679, 47);
            cboTipo.Margin = new Padding(4, 3, 4, 3);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new Size(208, 23);
            cboTipo.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(676, 29);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 2;
            label2.Text = "Tipo";
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(14, 47);
            txtDescricao.Margin = new Padding(4, 3, 4, 3);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(636, 23);
            txtDescricao.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 29);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Descrição";
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.DarkSeaGreen;
            btnSalvar.FlatAppearance.BorderColor = Color.Black;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Location = new Point(14, 525);
            btnSalvar.Margin = new Padding(4, 3, 4, 3);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(88, 27);
            btnSalvar.TabIndex = 2;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.BackColor = Color.IndianRed;
            btnExcluir.FlatAppearance.BorderColor = Color.Black;
            btnExcluir.FlatStyle = FlatStyle.Flat;
            btnExcluir.ForeColor = SystemColors.ButtonHighlight;
            btnExcluir.Location = new Point(131, 525);
            btnExcluir.Margin = new Padding(4, 3, 4, 3);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(88, 27);
            btnExcluir.TabIndex = 3;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = false;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Segoe UI Semibold", 30F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label.Location = new Point(277, 9);
            label.Name = "label";
            label.Size = new Size(374, 54);
            label.TabIndex = 5;
            label.Text = "Cadastro de Contas";
            // 
            // btnPDF
            // 
            btnPDF.FlatStyle = FlatStyle.Flat;
            btnPDF.Location = new Point(252, 525);
            btnPDF.Margin = new Padding(4, 3, 4, 3);
            btnPDF.Name = "btnPDF";
            btnPDF.Size = new Size(88, 27);
            btnPDF.TabIndex = 6;
            btnPDF.Text = "PDF";
            btnPDF.UseVisualStyleBackColor = true;
            btnPDF.Click += btnPDF_Click;
            // 
            // frmPlanoDeContas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1520, 655);
            Controls.Add(btnPDF);
            Controls.Add(label);
            Controls.Add(btnExcluir);
            Controls.Add(btnSalvar);
            Controls.Add(groupBox1);
            Controls.Add(dgvContas);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmPlanoDeContas";
            Text = "Gestão de Plano de Contas";
            Load += frmPlanoDeContas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvContas).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvContas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnExcluir;
        private Label label;
        private Button btnPDF;
    }
}