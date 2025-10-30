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
            btnAtualizar = new Button();
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
            dgvProdutos.Size = new Size(522, 183);
            dgvProdutos.TabIndex = 0;
            // 
            // btnNovo
            // 
            btnNovo.Font = new Font("Segoe UI", 9F);
            btnNovo.Location = new Point(12, 255);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(91, 33);
            btnNovo.TabIndex = 1;
            btnNovo.Text = "Novo Produto";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // btnAtualizar
            // 
            btnAtualizar.Location = new Point(109, 255);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(88, 33);
            btnAtualizar.TabIndex = 2;
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.UseVisualStyleBackColor = true;
            btnAtualizar.Click += btnAtualizar_Click;
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
            txtBusca.Size = new Size(242, 23);
            txtBusca.TabIndex = 4;
            txtBusca.TextChanged += txtBusca_TextChanged;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(203, 255);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(90, 34);
            btnBuscar.TabIndex = 5;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(378, 255);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(75, 34);
            btnExcluir.TabIndex = 6;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(299, 255);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(73, 34);
            btnEditar.TabIndex = 7;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnRelatorio
            // 
            btnRelatorio.Location = new Point(459, 254);
            btnRelatorio.Name = "btnRelatorio";
            btnRelatorio.Size = new Size(75, 35);
            btnRelatorio.TabIndex = 8;
            btnRelatorio.Text = "Gerar PDF";
            btnRelatorio.UseVisualStyleBackColor = true;
            btnRelatorio.Click += btnRelatorio_Click;
            // 
            // frmListagemProdutos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(546, 299);
            Controls.Add(btnRelatorio);
            Controls.Add(btnEditar);
            Controls.Add(btnExcluir);
            Controls.Add(btnBuscar);
            Controls.Add(txtBusca);
            Controls.Add(label1);
            Controls.Add(btnAtualizar);
            Controls.Add(btnNovo);
            Controls.Add(dgvProdutos);
            Name = "frmListagemProdutos";
            Text = "frmListagemProdutos";
            Load += frmListagemProdutos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProdutos;
        private Button btnNovo;
        private Button btnAtualizar;
        private Label label1;
        private TextBox txtBusca;
        private Button btnBuscar;
        private Button btnExcluir;
        private Button btnEditar;
        private Button btnRelatorio;
    }
}