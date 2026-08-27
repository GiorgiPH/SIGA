namespace PV
{
    partial class FavoritosMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlFavoritos = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.flpFavoritos = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pnlFavoritos
            // 
            this.pnlFavoritos.AutoSize = true;
            this.pnlFavoritos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFavoritos.Location = new System.Drawing.Point(0, 0);
            this.pnlFavoritos.Name = "pnlFavoritos";
            this.pnlFavoritos.Size = new System.Drawing.Size(150, 0);
            this.pnlFavoritos.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(68, 13);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "FAVORITOS";
            // 
            // flpFavoritos
            // 
            this.flpFavoritos.AutoSize = true;
            this.flpFavoritos.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpFavoritos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFavoritos.Location = new System.Drawing.Point(0, 13);
            this.flpFavoritos.Name = "flpFavoritos";
            this.flpFavoritos.Size = new System.Drawing.Size(150, 0);
            this.flpFavoritos.TabIndex = 2;
            this.flpFavoritos.WrapContents = false;
            // 
            // FavoritosMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpFavoritos);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pnlFavoritos);
            this.Name = "FavoritosMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlFavoritos;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.FlowLayoutPanel flpFavoritos;
    }
}
