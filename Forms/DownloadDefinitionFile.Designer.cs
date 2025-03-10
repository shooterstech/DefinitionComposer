namespace DefinitionComposer.Forms {
    partial class DownloadDefinitionFile {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.setNameTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.definitionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.downloadButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // setNameTextBox
            // 
            this.setNameTextBox.Location = new System.Drawing.Point(12, 82);
            this.setNameTextBox.Name = "setNameTextBox";
            this.setNameTextBox.Size = new System.Drawing.Size(284, 22);
            this.setNameTextBox.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 19);
            this.label8.TabIndex = 25;
            this.label8.Text = "Definition Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 24;
            this.label3.Text = "Set Name";
            // 
            // definitionTypeComboBox
            // 
            this.definitionTypeComboBox.FormattingEnabled = true;
            this.definitionTypeComboBox.Location = new System.Drawing.Point(12, 33);
            this.definitionTypeComboBox.Name = "definitionTypeComboBox";
            this.definitionTypeComboBox.Size = new System.Drawing.Size(284, 24);
            this.definitionTypeComboBox.TabIndex = 27;
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(39, 194);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(90, 34);
            this.downloadButton.TabIndex = 28;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(175, 194);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(90, 35);
            this.closeButton.TabIndex = 29;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(12, 110);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(284, 78);
            this.messageTextBox.TabIndex = 30;
            // 
            // Download_Definition_File
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 240);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.definitionTypeComboBox);
            this.Controls.Add(this.setNameTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Download_Definition_File";
            this.Text = "Download Definition File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox setNameTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox definitionTypeComboBox;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox messageTextBox;
    }
}