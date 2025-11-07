namespace SysFin_2CTDS.View
{
    partial class frmListagemProdutos
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
            dgvProdutos = new DataGridView();
            btnNovo = new Button();
            label1 = new Label();
            txtBusca = new TextBox();
            btnBuscar = new Button();
            btnExcluir = new Button();
            btnEditar = new Button();
            btnRelatorio = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).BeginInit();
            SuspendLayout();
            // 
            // dgvProdutos
            // 
            dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProdutos.Location = new Point(12, 66);
            dgvProdutos.Name = "dgvProdutos";
            dgvProdutos.Size = new Size(921, 450);
            dgvProdutos.TabIndex = 0;
            // 
            // btnNovo
            // 
            btnNovo.Font = new Font("Segoe UI", 9F);
            btnNovo.Location = new Point(12, 522);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(150, 34);
            btnNovo.TabIndex = 2;
            btnNovo.Text = "Novo Produto";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(134, 21);
            label1.TabIndex = 3;
            label1.Text = "Buscar por Nome:";
            // 
            // txtBusca
            // 
            txtBusca.Location = new Point(152, 23);
            txtBusca.Name = "txtBusca";
            txtBusca.Size = new Size(625, 23);
            txtBusca.TabIndex = 0;
            txtBusca.TextChanged += txtBusca_TextChanged;
            txtBusca.KeyDown += txtBusca_KeyDown;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(783, 18);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(150, 34);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(524, 522);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(150, 34);
            btnExcluir.TabIndex = 4;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(258, 522);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(150, 34);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnRelatorio
            // 
            btnRelatorio.Location = new Point(783, 522);
            btnRelatorio.Name = "btnRelatorio";
            btnRelatorio.Size = new Size(150, 34);
            btnRelatorio.TabIndex = 5;
            btnRelatorio.Text = "Gerar PDF";
            btnRelatorio.UseVisualStyleBackColor = true;
            btnRelatorio.Click += btnRelatorio_Click;
            // 
            // frmListagemProdutos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(945, 580);
            Controls.Add(btnRelatorio);
            Controls.Add(btnEditar);
            Controls.Add(btnExcluir);
            Controls.Add(btnBuscar);
            Controls.Add(txtBusca);
            Controls.Add(label1);
            Controls.Add(btnNovo);
            Controls.Add(dgvProdutos);
            Name = "frmListagemProdutos";
            Text = "Listagem de Produtos";
            Load += frmListagemProdutos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProdutos;
        private Button btnNovo;
        private Label label1;
        private TextBox txtBusca;
        private Button btnBuscar;
        private Button btnExcluir;
        private Button btnEditar;
        private Button btnRelatorio;
    }
}