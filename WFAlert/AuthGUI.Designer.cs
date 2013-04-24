namespace WFAlert
{
    partial class AuthGUI
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
            this.rtb_code = new System.Windows.Forms.RichTextBox();
            this.bt_code = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_code
            // 
            this.rtb_code.Location = new System.Drawing.Point(93, 12);
            this.rtb_code.Name = "rtb_code";
            this.rtb_code.Size = new System.Drawing.Size(179, 36);
            this.rtb_code.TabIndex = 0;
            this.rtb_code.Text = "Enter code from Twitter.";
            // 
            // bt_code
            // 
            this.bt_code.Location = new System.Drawing.Point(12, 12);
            this.bt_code.Name = "bt_code";
            this.bt_code.Size = new System.Drawing.Size(75, 36);
            this.bt_code.TabIndex = 1;
            this.bt_code.Text = "Submit";
            this.bt_code.UseVisualStyleBackColor = true;
            this.bt_code.Click += new System.EventHandler(this.bt_code_Click);
            // 
            // AuthGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 66);
            this.Controls.Add(this.bt_code);
            this.Controls.Add(this.rtb_code);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthGUI";
            this.Text = "WFAlert - Authorize";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_code;
        private System.Windows.Forms.Button bt_code;
    }
}