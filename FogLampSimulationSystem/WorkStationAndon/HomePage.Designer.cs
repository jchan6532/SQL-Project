
namespace WorkStationAndon
{
    partial class HomePage
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
            this.LampsCreatedPanel = new System.Windows.Forms.Panel();
            this.LampsCreatedTextBlock = new System.Windows.Forms.Label();
            this.LampsCreatedLabel = new System.Windows.Forms.Label();
            this.NumDefectsTextBlock = new System.Windows.Forms.Label();
            this.NumDefectsLabel = new System.Windows.Forms.Label();
            this.NumDefectsPanel = new System.Windows.Forms.Panel();
            this.LampsCreatedPanel.SuspendLayout();
            this.NumDefectsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LampsCreatedPanel
            // 
            this.LampsCreatedPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LampsCreatedPanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.LampsCreatedPanel.Controls.Add(this.LampsCreatedTextBlock);
            this.LampsCreatedPanel.Controls.Add(this.LampsCreatedLabel);
            this.LampsCreatedPanel.Location = new System.Drawing.Point(658, 12);
            this.LampsCreatedPanel.Name = "LampsCreatedPanel";
            this.LampsCreatedPanel.Size = new System.Drawing.Size(150, 150);
            this.LampsCreatedPanel.TabIndex = 0;
            // 
            // LampsCreatedTextBlock
            // 
            this.LampsCreatedTextBlock.AutoSize = true;
            this.LampsCreatedTextBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LampsCreatedTextBlock.Location = new System.Drawing.Point(33, 64);
            this.LampsCreatedTextBlock.Name = "LampsCreatedTextBlock";
            this.LampsCreatedTextBlock.Size = new System.Drawing.Size(64, 25);
            this.LampsCreatedTextBlock.TabIndex = 2;
            this.LampsCreatedTextBlock.Text = "lamps";
            // 
            // LampsCreatedLabel
            // 
            this.LampsCreatedLabel.AutoSize = true;
            this.LampsCreatedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LampsCreatedLabel.Location = new System.Drawing.Point(1, 9);
            this.LampsCreatedLabel.Name = "LampsCreatedLabel";
            this.LampsCreatedLabel.Size = new System.Drawing.Size(146, 25);
            this.LampsCreatedLabel.TabIndex = 0;
            this.LampsCreatedLabel.Text = "Lamps Created";
            // 
            // NumDefectsTextBlock
            // 
            this.NumDefectsTextBlock.AutoSize = true;
            this.NumDefectsTextBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumDefectsTextBlock.Location = new System.Drawing.Point(33, 67);
            this.NumDefectsTextBlock.Name = "NumDefectsTextBlock";
            this.NumDefectsTextBlock.Size = new System.Drawing.Size(75, 25);
            this.NumDefectsTextBlock.TabIndex = 1;
            this.NumDefectsTextBlock.Text = "defects";
            // 
            // NumDefectsLabel
            // 
            this.NumDefectsLabel.AutoSize = true;
            this.NumDefectsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumDefectsLabel.Location = new System.Drawing.Point(12, 11);
            this.NumDefectsLabel.Name = "NumDefectsLabel";
            this.NumDefectsLabel.Size = new System.Drawing.Size(124, 25);
            this.NumDefectsLabel.TabIndex = 0;
            this.NumDefectsLabel.Text = "Num Defects";
            this.NumDefectsLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // NumDefectsPanel
            // 
            this.NumDefectsPanel.AccessibleName = "";
            this.NumDefectsPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NumDefectsPanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.NumDefectsPanel.Controls.Add(this.NumDefectsTextBlock);
            this.NumDefectsPanel.Controls.Add(this.NumDefectsLabel);
            this.NumDefectsPanel.Location = new System.Drawing.Point(658, 215);
            this.NumDefectsPanel.Name = "NumDefectsPanel";
            this.NumDefectsPanel.Size = new System.Drawing.Size(150, 150);
            this.NumDefectsPanel.TabIndex = 1;
            // 
            // WorkStationAndonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 459);
            this.Controls.Add(this.NumDefectsPanel);
            this.Controls.Add(this.LampsCreatedPanel);
            this.Name = "WorkStationAndonForm";
            this.Text = "WorkStation Andon";
            this.Load += new System.EventHandler(this.WorkStationAndonForm_Load);
            this.LampsCreatedPanel.ResumeLayout(false);
            this.LampsCreatedPanel.PerformLayout();
            this.NumDefectsPanel.ResumeLayout(false);
            this.NumDefectsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LampsCreatedPanel;
        private System.Windows.Forms.Label LampsCreatedLabel;
        private System.Windows.Forms.Label NumDefectsLabel;
        private System.Windows.Forms.Panel NumDefectsPanel;
        private System.Windows.Forms.Label NumDefectsTextBlock;
        private System.Windows.Forms.Label LampsCreatedTextBlock;
    }
}

