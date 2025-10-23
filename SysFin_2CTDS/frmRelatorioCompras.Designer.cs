namespace SysFin_2CTDS.View
{
    partial class frmRelatorioCompras
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
            groupBox1 = new GroupBox();
            btnGerarRelatorio = new Button();
            dtpDataFinal = new DateTimePicker();
            dtpDataInicial = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            dgvResultados = new DataGridView();
            lblTotalCompras = new Label();
            lblValorTotal = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnGerarRelatorio);
            groupBox1.Controls.Add(dtpDataFinal);
            groupBox1.Controls.Add(dtpDataInicial);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(18, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(513, 164);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtros do Relatório";
            // 
            // btnGerarRelatorio
            // 
            btnGerarRelatorio.Location = new Point(6, 120);
            btnGerarRelatorio.Name = "btnGerarRelatorio";
            btnGerarRelatorio.Size = new Size(102, 23);
            btnGerarRelatorio.TabIndex = 4;
            btnGerarRelatorio.Text = "Gerar Relatório";
            btnGerarRelatorio.UseVisualStyleBackColor = true;
            btnGerarRelatorio.Click += btnGerarRelatorio_Click;
            // 
            // dtpDataFinal
            // 
            dtpDataFinal.Format = DateTimePickerFormat.Short;
            dtpDataFinal.Location = new Point(80, 78);
            dtpDataFinal.Name = "dtpDataFinal";
            dtpDataFinal.Size = new Size(200, 23);
            dtpDataFinal.TabIndex = 3;
            // 
            // dtpDataInicial
            // 
            dtpDataInicial.Format = DateTimePickerFormat.Short;
            dtpDataInicial.Location = new Point(80, 46);
            dtpDataInicial.Name = "dtpDataInicial";
            dtpDataInicial.Size = new Size(200, 23);
            dtpDataInicial.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 84);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 1;
            label2.Text = "Data Final:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 52);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 0;
            label1.Text = "Data Inicial:";
            // 
            // dgvResultados
            // 
            dgvResultados.AllowUserToAddRows = false;
            dgvResultados.AllowUserToDeleteRows = false;
            dgvResultados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultados.Location = new Point(18, 182);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.ReadOnly = true;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.Size = new Size(513, 185);
            dgvResultados.TabIndex = 1;
            // 
            // lblTotalCompras
            // 
            lblTotalCompras.AutoSize = true;
            lblTotalCompras.Location = new Point(18, 380);
            lblTotalCompras.Name = "lblTotalCompras";
            lblTotalCompras.Size = new Size(112, 15);
            lblTotalCompras.TabIndex = 2;
            lblTotalCompras.Text = "Total de Compras: 0";
            // 
            // lblValorTotal
            // 
            lblValorTotal.AutoSize = true;
            lblValorTotal.Location = new Point(18, 409);
            lblValorTotal.Name = "lblValorTotal";
            lblValorTotal.Size = new Size(165, 15);
            lblValorTotal.TabIndex = 3;
            lblValorTotal.Text = "Valor Total Comprado: R$ 0,00";
            // 
            // frmRelatorioCompras
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblValorTotal);
            Controls.Add(lblTotalCompras);
            Controls.Add(dgvResultados);
            Controls.Add(groupBox1);
            Name = "frmRelatorioCompras";
            Text = "frmRelatorioCompras";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private DateTimePicker dtpDataInicial;
        private Label label2;
        private Label label1;
        private Button btnGerarRelatorio;
        private DateTimePicker dtpDataFinal;
        private DataGridView dgvResultados;
        private Label lblTotalCompras;
        private Label lblValorTotal;
    }
}