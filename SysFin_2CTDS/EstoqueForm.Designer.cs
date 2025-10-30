namespace SysFin_2CTDS.View
{
    partial class EstoqueForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstoqueForm));
            dgvEstoque = new DataGridView();
            label1 = new Label();
            btnAtualizar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEstoque).BeginInit();
            SuspendLayout();
            // 
            // dgvEstoque
            // 
            dgvEstoque.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstoque.Location = new Point(12, 71);
            dgvEstoque.Name = "dgvEstoque";
            dgvEstoque.Size = new Size(776, 367);
            dgvEstoque.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 27.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(300, 9);
            label1.Name = "label1";
            label1.Size = new Size(189, 50);
            label1.TabIndex = 1;
            label1.Text = "ESTOQUE";
            // 
            // btnAtualizar
            // 
            btnAtualizar.BackColor = Color.Transparent;
            btnAtualizar.BackgroundImage = (Image)resources.GetObject("btnAtualizar.BackgroundImage");
            btnAtualizar.BackgroundImageLayout = ImageLayout.Stretch;
            btnAtualizar.ForeColor = SystemColors.ControlText;
            btnAtualizar.Location = new Point(748, 22);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(40, 37);
            btnAtualizar.TabIndex = 2;
            btnAtualizar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnAtualizar.UseVisualStyleBackColor = false;
            // 
            // EstoqueForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAtualizar);
            Controls.Add(label1);
            Controls.Add(dgvEstoque);
            Name = "EstoqueForm";
            Text = "EstoqueForm";
            Load += EstoqueForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEstoque).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvEstoque;
        private Label label1;
        private Button btnAtualizar;
    }
}