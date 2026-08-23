namespace PV
{
    partial class SidebarMenuItem
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
            this.btnHeader = new Guna.UI2.WinForms.Guna2Button();
            this.pnlSubMenu = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnHeader
            // 
            this.btnHeader.BorderRadius = 6;
            this.btnHeader.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHeader.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHeader.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHeader.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHeader.FillColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnHeader.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeader.ForeColor = System.Drawing.Color.White;
            this.btnHeader.Location = new System.Drawing.Point(0, 0);
            this.btnHeader.Name = "btnHeader";
            this.btnHeader.Size = new System.Drawing.Size(150, 45);
            this.btnHeader.TabIndex = 1;
            this.btnHeader.Text = "guna2Button1";
            // 
            // pnlSubMenu
            // 
            this.pnlSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.pnlSubMenu.Location = new System.Drawing.Point(3, 51);
            this.pnlSubMenu.Name = "pnlSubMenu";
            this.pnlSubMenu.Size = new System.Drawing.Size(144, 96);
            this.pnlSubMenu.TabIndex = 2;
            this.pnlSubMenu.Visible = false;
            // 
            // SidebarMenuItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSubMenu);
            this.Controls.Add(this.btnHeader);
            this.Name = "SidebarMenuItems";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnHeader;
        private System.Windows.Forms.Panel pnlSubMenu;
    }
}
