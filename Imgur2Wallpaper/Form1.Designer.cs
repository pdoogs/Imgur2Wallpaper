namespace Imgur2Wallpaper
{
    partial class Form1
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
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.urlLabel = new System.Windows.Forms.Label();
            this.setWallpaperButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(47, 6);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(505, 20);
            this.urlTextBox.TabIndex = 0;
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Location = new System.Drawing.Point(12, 9);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(29, 13);
            this.urlLabel.TabIndex = 1;
            this.urlLabel.Text = "URL";
            // 
            // setWallpaperButton
            // 
            this.setWallpaperButton.Location = new System.Drawing.Point(559, 4);
            this.setWallpaperButton.Name = "setWallpaperButton";
            this.setWallpaperButton.Size = new System.Drawing.Size(109, 23);
            this.setWallpaperButton.TabIndex = 2;
            this.setWallpaperButton.Text = "Set Wallpaper";
            this.setWallpaperButton.UseVisualStyleBackColor = true;
            this.setWallpaperButton.Click += new System.EventHandler(this.setWallpaperButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 38);
            this.Controls.Add(this.setWallpaperButton);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.urlTextBox);
            this.Name = "Form1";
            this.Text = "Set Imgur Wallpaper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.Button setWallpaperButton;
    }
}

