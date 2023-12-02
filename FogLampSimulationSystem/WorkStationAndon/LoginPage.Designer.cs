
namespace WorkStationAndon
{
    partial class LoginPage
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
            this.LogInButton = new System.Windows.Forms.Button();
            this.WorkStationIDTextBox = new System.Windows.Forms.TextBox();
            this.WorkStationIDLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LogInButton
            // 
            this.LogInButton.Location = new System.Drawing.Point(351, 200);
            this.LogInButton.Name = "LogInButton";
            this.LogInButton.Size = new System.Drawing.Size(125, 23);
            this.LogInButton.TabIndex = 0;
            this.LogInButton.Text = "Log In";
            this.LogInButton.UseVisualStyleBackColor = true;
            this.LogInButton.Click += new System.EventHandler(this.LogInButton_Click);
            // 
            // WorkStationIDTextBox
            // 
            this.WorkStationIDTextBox.Location = new System.Drawing.Point(418, 141);
            this.WorkStationIDTextBox.Name = "WorkStationIDTextBox";
            this.WorkStationIDTextBox.Size = new System.Drawing.Size(86, 20);
            this.WorkStationIDTextBox.TabIndex = 1;
            // 
            // WorkStationIDLabel
            // 
            this.WorkStationIDLabel.AutoSize = true;
            this.WorkStationIDLabel.Location = new System.Drawing.Point(329, 144);
            this.WorkStationIDLabel.Name = "WorkStationIDLabel";
            this.WorkStationIDLabel.Size = new System.Drawing.Size(83, 13);
            this.WorkStationIDLabel.TabIndex = 3;
            this.WorkStationIDLabel.Text = "Work Station ID";
            // 
            // LoginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WorkStationIDLabel);
            this.Controls.Add(this.WorkStationIDTextBox);
            this.Controls.Add(this.LogInButton);
            this.Name = "LoginPage";
            this.Size = new System.Drawing.Size(800, 450);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LogInButton;
        private System.Windows.Forms.TextBox WorkStationIDTextBox;
        private System.Windows.Forms.Label WorkStationIDLabel;
    }
}