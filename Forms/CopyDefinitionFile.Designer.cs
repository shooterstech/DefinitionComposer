namespace DefinitionComposer.Forms {
    partial class CopyDefinitionFile {
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
            this.label1 = new System.Windows.Forms.Label();
            this.copyFromTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.properNameTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.typeTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Copy From";
            // 
            // copyFromTextBox
            // 
            this.copyFromTextBox.Location = new System.Drawing.Point(158, 41);
            this.copyFromTextBox.Name = "copyFromTextBox";
            this.copyFromTextBox.ReadOnly = true;
            this.copyFromTextBox.Size = new System.Drawing.Size(388, 23);
            this.copyFromTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Proper Name";
            // 
            // properNameTextBox
            // 
            this.properNameTextBox.Location = new System.Drawing.Point(158, 70);
            this.properNameTextBox.Name = "properNameTextBox";
            this.properNameTextBox.Size = new System.Drawing.Size(388, 23);
            this.properNameTextBox.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(119, 115);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 33);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(271, 115);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 33);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // typeTextBox
            // 
            this.typeTextBox.Location = new System.Drawing.Point(158, 12);
            this.typeTextBox.Name = "typeTextBox";
            this.typeTextBox.ReadOnly = true;
            this.typeTextBox.Size = new System.Drawing.Size(388, 23);
            this.typeTextBox.TabIndex = 6;
            // 
            // CopyDefinitionFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 160);
            this.Controls.Add(this.typeTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.properNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.copyFromTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyDefinitionFile";
            this.ShowIcon = false;
            this.Text = "Copy Definition File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox copyFromTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox properNameTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox typeTextBox;
    }
}