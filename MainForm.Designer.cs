﻿namespace DefinitionComposer {
	partial class MainForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.definitionFolderTextBox = new System.Windows.Forms.TextBox();
			this.ownerIdTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ownerInformationTextBox = new System.Windows.Forms.TextBox();
			this.properNameTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.disciplineComboBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.subDisciplineTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tagsTextBox = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.specificationTextBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.typeOfDefinitionTextBox = new System.Windows.Forms.TextBox();
			this.namespaceTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.uploadButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.majorVersionUpDown = new System.Windows.Forms.NumericUpDown();
			this.updateAvailLabel = new System.Windows.Forms.Label();
			this.reDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.majorVersionUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 448);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Definition Folder";
			// 
			// definitionFolderTextBox
			// 
			this.definitionFolderTextBox.Location = new System.Drawing.Point(12, 470);
			this.definitionFolderTextBox.Name = "definitionFolderTextBox";
			this.definitionFolderTextBox.ReadOnly = true;
			this.definitionFolderTextBox.Size = new System.Drawing.Size(284, 25);
			this.definitionFolderTextBox.TabIndex = 2;
			// 
			// ownerIdTextBox
			// 
			this.ownerIdTextBox.Location = new System.Drawing.Point(103, 164);
			this.ownerIdTextBox.Name = "ownerIdTextBox";
			this.ownerIdTextBox.ReadOnly = true;
			this.ownerIdTextBox.Size = new System.Drawing.Size(193, 25);
			this.ownerIdTextBox.TabIndex = 4;
			this.ownerIdTextBox.TextChanged += new System.EventHandler(this.ownerIdTextBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 167);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 19);
			this.label2.TabIndex = 3;
			this.label2.Text = "Owner Id";
			// 
			// ownerInformationTextBox
			// 
			this.ownerInformationTextBox.Location = new System.Drawing.Point(12, 195);
			this.ownerInformationTextBox.Multiline = true;
			this.ownerInformationTextBox.Name = "ownerInformationTextBox";
			this.ownerInformationTextBox.ReadOnly = true;
			this.ownerInformationTextBox.Size = new System.Drawing.Size(284, 65);
			this.ownerInformationTextBox.TabIndex = 5;
			this.ownerInformationTextBox.WordWrap = false;
			// 
			// properNameTextBox
			// 
			this.properNameTextBox.Location = new System.Drawing.Point(12, 102);
			this.properNameTextBox.Name = "properNameTextBox";
			this.properNameTextBox.ReadOnly = true;
			this.properNameTextBox.Size = new System.Drawing.Size(284, 25);
			this.properNameTextBox.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 19);
			this.label3.TabIndex = 6;
			this.label3.Text = "Proper Name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 269);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 19);
			this.label4.TabIndex = 8;
			this.label4.Text = "Namespace";
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(311, 554);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(148, 32);
			this.saveButton.TabIndex = 12;
			this.saveButton.Tag = "validate";
			this.saveButton.Text = "Reload and Validate";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.validateButton_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 300);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 19);
			this.label5.TabIndex = 13;
			this.label5.Text = "Discipline";
			// 
			// disciplineComboBox
			// 
			this.disciplineComboBox.Enabled = false;
			this.disciplineComboBox.FormattingEnabled = true;
			this.disciplineComboBox.Location = new System.Drawing.Point(103, 297);
			this.disciplineComboBox.Name = "disciplineComboBox";
			this.disciplineComboBox.Size = new System.Drawing.Size(193, 27);
			this.disciplineComboBox.TabIndex = 14;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 327);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 19);
			this.label6.TabIndex = 15;
			this.label6.Text = "Sub-Discipline";
			// 
			// subDisciplineTextBox
			// 
			this.subDisciplineTextBox.Location = new System.Drawing.Point(12, 349);
			this.subDisciplineTextBox.Name = "subDisciplineTextBox";
			this.subDisciplineTextBox.ReadOnly = true;
			this.subDisciplineTextBox.Size = new System.Drawing.Size(284, 25);
			this.subDisciplineTextBox.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(8, 383);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 19);
			this.label7.TabIndex = 17;
			this.label7.Text = "Tags";
			// 
			// tagsTextBox
			// 
			this.tagsTextBox.Location = new System.Drawing.Point(103, 380);
			this.tagsTextBox.Multiline = true;
			this.tagsTextBox.Name = "tagsTextBox";
			this.tagsTextBox.ReadOnly = true;
			this.tagsTextBox.Size = new System.Drawing.Size(193, 65);
			this.tagsTextBox.TabIndex = 18;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.recentToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(772, 25);
			this.menuStrip1.TabIndex = 19;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.reDownloadToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// downloadToolStripMenuItem
			// 
			this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
			this.downloadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.downloadToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.downloadToolStripMenuItem.Text = "Download";
			this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.closeToolStripMenuItem.Text = "Close";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// recentToolStripMenuItem
			// 
			this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
			this.recentToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
			this.recentToolStripMenuItem.Text = "Recent";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// specificationTextBox
			// 
			this.specificationTextBox.Location = new System.Drawing.Point(311, 35);
			this.specificationTextBox.Multiline = true;
			this.specificationTextBox.Name = "specificationTextBox";
			this.specificationTextBox.ReadOnly = true;
			this.specificationTextBox.Size = new System.Drawing.Size(449, 513);
			this.specificationTextBox.TabIndex = 21;
			this.specificationTextBox.TabStop = false;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(8, 30);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 19);
			this.label8.TabIndex = 22;
			this.label8.Text = "Type of Definition";
			// 
			// typeOfDefinitionTextBox
			// 
			this.typeOfDefinitionTextBox.Location = new System.Drawing.Point(12, 52);
			this.typeOfDefinitionTextBox.Name = "typeOfDefinitionTextBox";
			this.typeOfDefinitionTextBox.ReadOnly = true;
			this.typeOfDefinitionTextBox.Size = new System.Drawing.Size(284, 25);
			this.typeOfDefinitionTextBox.TabIndex = 23;
			// 
			// namespaceTextBox
			// 
			this.namespaceTextBox.Location = new System.Drawing.Point(103, 266);
			this.namespaceTextBox.Name = "namespaceTextBox";
			this.namespaceTextBox.ReadOnly = true;
			this.namespaceTextBox.Size = new System.Drawing.Size(193, 25);
			this.namespaceTextBox.TabIndex = 24;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(8, 135);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(103, 19);
			this.label9.TabIndex = 25;
			this.label9.Text = "Major Version";
			// 
			// uploadButton
			// 
			this.uploadButton.Location = new System.Drawing.Point(465, 554);
			this.uploadButton.Name = "uploadButton";
			this.uploadButton.Size = new System.Drawing.Size(129, 32);
			this.uploadButton.TabIndex = 28;
			this.uploadButton.Tag = "upload";
			this.uploadButton.Text = "Upload";
			this.uploadButton.UseVisualStyleBackColor = true;
			this.uploadButton.Click += new System.EventHandler(this.validateButton_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(600, 554);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(160, 32);
			this.button2.TabIndex = 29;
			this.button2.Text = "Open to Edit";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.openToEditButton_Click);
			// 
			// majorVersionUpDown
			// 
			this.majorVersionUpDown.Location = new System.Drawing.Point(118, 133);
			this.majorVersionUpDown.Name = "majorVersionUpDown";
			this.majorVersionUpDown.Size = new System.Drawing.Size(74, 25);
			this.majorVersionUpDown.TabIndex = 30;
			// 
			// updateAvailLabel
			// 
			this.updateAvailLabel.AutoSize = true;
			this.updateAvailLabel.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.updateAvailLabel.ForeColor = System.Drawing.Color.Blue;
			this.updateAvailLabel.Location = new System.Drawing.Point(200, 135);
			this.updateAvailLabel.Name = "updateAvailLabel";
			this.updateAvailLabel.Size = new System.Drawing.Size(96, 19);
			this.updateAvailLabel.TabIndex = 31;
			this.updateAvailLabel.Text = "Update Avail";
			// 
			// reDownloadToolStripMenuItem
			// 
			this.reDownloadToolStripMenuItem.Name = "reDownloadToolStripMenuItem";
			this.reDownloadToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.reDownloadToolStripMenuItem.Text = "Re-Download";
			this.reDownloadToolStripMenuItem.Click += new System.EventHandler(this.reDownloadToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(772, 605);
			this.Controls.Add(this.updateAvailLabel);
			this.Controls.Add(this.majorVersionUpDown);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.uploadButton);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.namespaceTextBox);
			this.Controls.Add(this.typeOfDefinitionTextBox);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.specificationTextBox);
			this.Controls.Add(this.tagsTextBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.subDisciplineTextBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.disciplineComboBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.properNameTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ownerInformationTextBox);
			this.Controls.Add(this.ownerIdTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.definitionFolderTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Definition Composer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.majorVersionUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox definitionFolderTextBox;
		private System.Windows.Forms.TextBox ownerIdTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox ownerInformationTextBox;
		private System.Windows.Forms.TextBox properNameTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox disciplineComboBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox subDisciplineTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tagsTextBox;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox specificationTextBox;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox typeOfDefinitionTextBox;
        private System.Windows.Forms.TextBox namespaceTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown majorVersionUpDown;
        private System.Windows.Forms.Label updateAvailLabel;
		private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reDownloadToolStripMenuItem;
	}
}

