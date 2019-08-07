namespace BWTool
{
    partial class BWForm
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
            this.components = new System.ComponentModel.Container();
            this.ToolChoosingTabcontrol = new System.Windows.Forms.TabControl();
            this.BrainWalletTab = new System.Windows.Forms.TabPage();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.SeparatorTextBox = new System.Windows.Forms.TextBox();
            this.sha256Label = new System.Windows.Forms.Label();
            this.SeparatorEnterCheckBox = new System.Windows.Forms.CheckBox();
            this.Sha256NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SeparatorLabel = new System.Windows.Forms.Label();
            this.CompressedCheckBox = new System.Windows.Forms.CheckBox();
            this.MiningStartButton = new System.Windows.Forms.Button();
            this.TextSelectGroupBox = new System.Windows.Forms.GroupBox();
            this.AddressLookupLoadButton = new System.Windows.Forms.Button();
            this.PasswordLoadButton = new System.Windows.Forms.Button();
            this.ProgressRichTextBox = new System.Windows.Forms.RichTextBox();
            this.MininingInformationToolstrip = new System.Windows.Forms.ToolStrip();
            this.ProgressLabel = new System.Windows.Forms.ToolStripLabel();
            this.MiningProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PasswordListOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.AddressOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MinerInfoUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.ToolChoosingTabcontrol.SuspendLayout();
            this.BrainWalletTab.SuspendLayout();
            this.OptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sha256NumericUpDown)).BeginInit();
            this.TextSelectGroupBox.SuspendLayout();
            this.MininingInformationToolstrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolChoosingTabcontrol
            // 
            this.ToolChoosingTabcontrol.Controls.Add(this.BrainWalletTab);
            this.ToolChoosingTabcontrol.Controls.Add(this.tabPage2);
            this.ToolChoosingTabcontrol.Location = new System.Drawing.Point(-4, 3);
            this.ToolChoosingTabcontrol.Name = "ToolChoosingTabcontrol";
            this.ToolChoosingTabcontrol.SelectedIndex = 0;
            this.ToolChoosingTabcontrol.Size = new System.Drawing.Size(1173, 590);
            this.ToolChoosingTabcontrol.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.ToolChoosingTabcontrol.TabIndex = 0;
            // 
            // BrainWalletTab
            // 
            this.BrainWalletTab.Controls.Add(this.OptionsGroupBox);
            this.BrainWalletTab.Controls.Add(this.MiningStartButton);
            this.BrainWalletTab.Controls.Add(this.TextSelectGroupBox);
            this.BrainWalletTab.Controls.Add(this.ProgressRichTextBox);
            this.BrainWalletTab.Controls.Add(this.MininingInformationToolstrip);
            this.BrainWalletTab.Location = new System.Drawing.Point(4, 22);
            this.BrainWalletTab.Name = "BrainWalletTab";
            this.BrainWalletTab.Padding = new System.Windows.Forms.Padding(3);
            this.BrainWalletTab.Size = new System.Drawing.Size(1165, 564);
            this.BrainWalletTab.TabIndex = 0;
            this.BrainWalletTab.Text = "Brainwallet Mining";
            this.BrainWalletTab.UseVisualStyleBackColor = true;
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.SeparatorTextBox);
            this.OptionsGroupBox.Controls.Add(this.sha256Label);
            this.OptionsGroupBox.Controls.Add(this.SeparatorEnterCheckBox);
            this.OptionsGroupBox.Controls.Add(this.Sha256NumericUpDown);
            this.OptionsGroupBox.Controls.Add(this.SeparatorLabel);
            this.OptionsGroupBox.Controls.Add(this.CompressedCheckBox);
            this.OptionsGroupBox.Location = new System.Drawing.Point(519, 22);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(413, 140);
            this.OptionsGroupBox.TabIndex = 15;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // SeparatorTextBox
            // 
            this.SeparatorTextBox.Location = new System.Drawing.Point(89, 22);
            this.SeparatorTextBox.Name = "SeparatorTextBox";
            this.SeparatorTextBox.Size = new System.Drawing.Size(100, 20);
            this.SeparatorTextBox.TabIndex = 7;
            this.SeparatorTextBox.TextChanged += new System.EventHandler(this.SeparatorTextBox_TextChanged);
            // 
            // sha256Label
            // 
            this.sha256Label.AutoSize = true;
            this.sha256Label.Location = new System.Drawing.Point(11, 96);
            this.sha256Label.Name = "sha256Label";
            this.sha256Label.Size = new System.Drawing.Size(178, 13);
            this.sha256Label.TabIndex = 14;
            this.sha256Label.Text = "Count of sha256 operation on string:";
            // 
            // SeparatorEnterCheckBox
            // 
            this.SeparatorEnterCheckBox.AutoSize = true;
            this.SeparatorEnterCheckBox.Checked = true;
            this.SeparatorEnterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SeparatorEnterCheckBox.Location = new System.Drawing.Point(222, 25);
            this.SeparatorEnterCheckBox.Name = "SeparatorEnterCheckBox";
            this.SeparatorEnterCheckBox.Size = new System.Drawing.Size(81, 17);
            this.SeparatorEnterCheckBox.TabIndex = 8;
            this.SeparatorEnterCheckBox.Text = "Using Enter";
            this.SeparatorEnterCheckBox.UseVisualStyleBackColor = true;
            // 
            // Sha256NumericUpDown
            // 
            this.Sha256NumericUpDown.Location = new System.Drawing.Point(222, 94);
            this.Sha256NumericUpDown.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.Sha256NumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Sha256NumericUpDown.Name = "Sha256NumericUpDown";
            this.Sha256NumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.Sha256NumericUpDown.TabIndex = 13;
            this.Sha256NumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SeparatorLabel
            // 
            this.SeparatorLabel.AutoSize = true;
            this.SeparatorLabel.Location = new System.Drawing.Point(11, 23);
            this.SeparatorLabel.Name = "SeparatorLabel";
            this.SeparatorLabel.Size = new System.Drawing.Size(56, 13);
            this.SeparatorLabel.TabIndex = 6;
            this.SeparatorLabel.Text = "Separator:";
            // 
            // CompressedCheckBox
            // 
            this.CompressedCheckBox.AutoSize = true;
            this.CompressedCheckBox.Location = new System.Drawing.Point(16, 62);
            this.CompressedCheckBox.Name = "CompressedCheckBox";
            this.CompressedCheckBox.Size = new System.Drawing.Size(173, 17);
            this.CompressedCheckBox.TabIndex = 12;
            this.CompressedCheckBox.Text = "Lookup compressed addresses";
            this.CompressedCheckBox.UseVisualStyleBackColor = true;
            // 
            // MiningStartButton
            // 
            this.MiningStartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MiningStartButton.Location = new System.Drawing.Point(464, 197);
            this.MiningStartButton.Name = "MiningStartButton";
            this.MiningStartButton.Size = new System.Drawing.Size(100, 44);
            this.MiningStartButton.TabIndex = 11;
            this.MiningStartButton.Text = "Start Mining";
            this.MiningStartButton.UseVisualStyleBackColor = true;
            this.MiningStartButton.Click += new System.EventHandler(this.MiningStartButton_Click);
            // 
            // TextSelectGroupBox
            // 
            this.TextSelectGroupBox.Controls.Add(this.AddressLookupLoadButton);
            this.TextSelectGroupBox.Controls.Add(this.PasswordLoadButton);
            this.TextSelectGroupBox.Location = new System.Drawing.Point(27, 22);
            this.TextSelectGroupBox.Name = "TextSelectGroupBox";
            this.TextSelectGroupBox.Size = new System.Drawing.Size(360, 140);
            this.TextSelectGroupBox.TabIndex = 10;
            this.TextSelectGroupBox.TabStop = false;
            this.TextSelectGroupBox.Text = "Select password and lookup txt files";
            // 
            // AddressLookupLoadButton
            // 
            this.AddressLookupLoadButton.Location = new System.Drawing.Point(229, 35);
            this.AddressLookupLoadButton.Name = "AddressLookupLoadButton";
            this.AddressLookupLoadButton.Size = new System.Drawing.Size(100, 56);
            this.AddressLookupLoadButton.TabIndex = 9;
            this.AddressLookupLoadButton.Text = "Load Bitcoin lookup address list";
            this.AddressLookupLoadButton.UseVisualStyleBackColor = true;
            this.AddressLookupLoadButton.Click += new System.EventHandler(this.AddressLookupLoadButton_Click);
            // 
            // PasswordLoadButton
            // 
            this.PasswordLoadButton.Location = new System.Drawing.Point(6, 35);
            this.PasswordLoadButton.Name = "PasswordLoadButton";
            this.PasswordLoadButton.Size = new System.Drawing.Size(98, 56);
            this.PasswordLoadButton.TabIndex = 3;
            this.PasswordLoadButton.Text = "Load TextFile";
            this.PasswordLoadButton.UseVisualStyleBackColor = true;
            this.PasswordLoadButton.Click += new System.EventHandler(this.PasswordLoadButton_Click);
            // 
            // ProgressRichTextBox
            // 
            this.ProgressRichTextBox.Location = new System.Drawing.Point(-4, 247);
            this.ProgressRichTextBox.Name = "ProgressRichTextBox";
            this.ProgressRichTextBox.ReadOnly = true;
            this.ProgressRichTextBox.Size = new System.Drawing.Size(936, 218);
            this.ProgressRichTextBox.TabIndex = 5;
            this.ProgressRichTextBox.Text = "";
            // 
            // MininingInformationToolstrip
            // 
            this.MininingInformationToolstrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MininingInformationToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressLabel,
            this.MiningProgressBar});
            this.MininingInformationToolstrip.Location = new System.Drawing.Point(3, 536);
            this.MininingInformationToolstrip.Name = "MininingInformationToolstrip";
            this.MininingInformationToolstrip.Size = new System.Drawing.Size(1159, 25);
            this.MininingInformationToolstrip.TabIndex = 4;
            this.MininingInformationToolstrip.Text = "toolStrip1";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(16, 22);
            this.ProgressLabel.Text = "...";
            // 
            // MiningProgressBar
            // 
            this.MiningProgressBar.Name = "MiningProgressBar";
            this.MiningProgressBar.Size = new System.Drawing.Size(100, 22);
            this.MiningProgressBar.Step = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1165, 564);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Incremental private key bruteforce";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PasswordListOpenFileDialog
            // 
            this.PasswordListOpenFileDialog.Filter = "TXT files|*.txt";
            this.PasswordListOpenFileDialog.Title = "Open Password Text File";
            // 
            // AddressOpenFileDialog
            // 
            this.AddressOpenFileDialog.Filter = "TXT files|*.txt";
            this.AddressOpenFileDialog.Title = "Open address Text File";
            // 
            // MinerInfoUpdateTimer
            // 
            this.MinerInfoUpdateTimer.Tick += new System.EventHandler(this.Uiupdate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Soon...";
            // 
            // BWForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 615);
            this.Controls.Add(this.ToolChoosingTabcontrol);
            this.Name = "BWForm";
            this.Text = "BWTOOLS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BWForm_FormClosing);
            this.ToolChoosingTabcontrol.ResumeLayout(false);
            this.BrainWalletTab.ResumeLayout(false);
            this.BrainWalletTab.PerformLayout();
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sha256NumericUpDown)).EndInit();
            this.TextSelectGroupBox.ResumeLayout(false);
            this.MininingInformationToolstrip.ResumeLayout(false);
            this.MininingInformationToolstrip.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ToolChoosingTabcontrol;
        private System.Windows.Forms.TabPage BrainWalletTab;
        private System.Windows.Forms.Button PasswordLoadButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip MininingInformationToolstrip;
        private System.Windows.Forms.ToolStripLabel ProgressLabel;
        private System.Windows.Forms.ToolStripProgressBar MiningProgressBar;
        private System.Windows.Forms.RichTextBox ProgressRichTextBox;
        private System.Windows.Forms.OpenFileDialog PasswordListOpenFileDialog;
        private System.Windows.Forms.CheckBox SeparatorEnterCheckBox;
        private System.Windows.Forms.TextBox SeparatorTextBox;
        private System.Windows.Forms.Label SeparatorLabel;
        private System.Windows.Forms.GroupBox TextSelectGroupBox;
        private System.Windows.Forms.Button AddressLookupLoadButton;
        private System.Windows.Forms.OpenFileDialog AddressOpenFileDialog;
        private System.Windows.Forms.Button MiningStartButton;
        private System.Windows.Forms.CheckBox CompressedCheckBox;
        private System.Windows.Forms.Label sha256Label;
        private System.Windows.Forms.NumericUpDown Sha256NumericUpDown;
        private System.Windows.Forms.Timer MinerInfoUpdateTimer;
        private System.Windows.Forms.GroupBox OptionsGroupBox;
        private System.Windows.Forms.Label label1;
    }
}

