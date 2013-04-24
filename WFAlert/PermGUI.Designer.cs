namespace WFAlert
{
    partial class PermGUI
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
            this.bt_getperm = new System.Windows.Forms.Button();
            this.bt_haveperm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_getperm
            // 
            this.bt_getperm.Location = new System.Drawing.Point(13, 11);
            this.bt_getperm.Name = "bt_getperm";
            this.bt_getperm.Size = new System.Drawing.Size(126, 56);
            this.bt_getperm.TabIndex = 0;
            this.bt_getperm.Text = "Get Permission";
            this.bt_getperm.UseVisualStyleBackColor = true;
            this.bt_getperm.Click += new System.EventHandler(this.bt_newcred_Click);
            // 
            // bt_haveperm
            // 
            this.bt_haveperm.Location = new System.Drawing.Point(145, 11);
            this.bt_haveperm.Name = "bt_haveperm";
            this.bt_haveperm.Size = new System.Drawing.Size(126, 56);
            this.bt_haveperm.TabIndex = 1;
            this.bt_haveperm.Text = "Have Permission";
            this.bt_haveperm.UseVisualStyleBackColor = true;
            this.bt_haveperm.Click += new System.EventHandler(this.bt_prevcred_Click);
            // 
            // PermGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 81);
            this.Controls.Add(this.bt_haveperm);
            this.Controls.Add(this.bt_getperm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PermGUI";
            this.Text = "WFAlert - Permissions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_getperm;
        private System.Windows.Forms.Button bt_haveperm;
    }
}