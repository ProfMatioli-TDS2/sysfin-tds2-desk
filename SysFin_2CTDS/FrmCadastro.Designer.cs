namespace SysFin_2CTDS.Views {
    partial class FrmCadastroBase {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            toolStrip1 = new ToolStrip();
            tsbNovo = new ToolStripButton();
            tsbSalvar = new ToolStripButton();
            tsbExcluir = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsbFechar = new ToolStripButton();
            dgvDados = new DataGridView();
            pnlCampos = new Panel();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDados).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbNovo, tsbSalvar, tsbExcluir, toolStripSeparator1, tsbFechar });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(915, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // tsbNovo
            // 
            tsbNovo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbNovo.ImageTransparentColor = Color.Magenta;
            tsbNovo.Name = "tsbNovo";
            tsbNovo.Size = new Size(23, 22);
            tsbNovo.Text = "Novo";
            // 
            // tsbSalvar
            // 
            tsbSalvar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbSalvar.ImageTransparentColor = Color.Magenta;
            tsbSalvar.Name = "tsbSalvar";
            tsbSalvar.Size = new Size(23, 22);
            tsbSalvar.Text = "Salvar";
            // 
            // tsbExcluir
            // 
            tsbExcluir.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbExcluir.ImageTransparentColor = Color.Magenta;
            tsbExcluir.Name = "tsbExcluir";
            tsbExcluir.Size = new Size(23, 22);
            tsbExcluir.Text = "Excluir";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // tsbFechar
            // 
            tsbFechar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbFechar.ImageTransparentColor = Color.Magenta;
            tsbFechar.Name = "tsbFechar";
            tsbFechar.Size = new Size(23, 22);
            tsbFechar.Text = "Fechar";
            tsbFechar.Click += tsbFechar_Click;
            // 
            // dgvDados
            // 
            dgvDados.AllowUserToAddRows = false;
            dgvDados.AllowUserToDeleteRows = false;
            dgvDados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDados.Dock = DockStyle.Left;
            dgvDados.Location = new Point(0, 25);
            dgvDados.Margin = new Padding(4, 4, 4, 4);
            dgvDados.MultiSelect = false;
            dgvDados.Name = "dgvDados";
            dgvDados.ReadOnly = true;
            dgvDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDados.Size = new Size(513, 563);
            dgvDados.TabIndex = 1;
            dgvDados.CellContentClick += dgvDados_CellContentClick;
            // 
            // pnlCampos
            // 
            pnlCampos.Dock = DockStyle.Fill;
            pnlCampos.Location = new Point(513, 25);
            pnlCampos.Margin = new Padding(4, 4, 4, 4);
            pnlCampos.Name = "pnlCampos";
            pnlCampos.Size = new Size(402, 563);
            pnlCampos.TabIndex = 2;
            // 
            // FrmCadastroBase
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 588);
            Controls.Add(pnlCampos);
            Controls.Add(dgvDados);
            Controls.Add(toolStrip1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "FrmCadastroBase";
            Text = "FrmCadastroBase";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton tsbNovo;
        protected System.Windows.Forms.ToolStripButton tsbSalvar;
        protected System.Windows.Forms.ToolStripButton tsbExcluir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbFechar;
        protected System.Windows.Forms.DataGridView dgvDados;
        protected System.Windows.Forms.Panel pnlCampos;
    }
}
