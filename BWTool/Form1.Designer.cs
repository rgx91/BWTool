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
            this.RandomStringGroupBox = new System.Windows.Forms.GroupBox();
            this.CharsetRandomLabel = new System.Windows.Forms.Label();
            this.CharSetTextBox = new System.Windows.Forms.TextBox();
            this.RandomLabel = new System.Windows.Forms.Label();
            this.CountOfCharsUpDown = new System.Windows.Forms.NumericUpDown();
            this.UseRandomCheckBox = new System.Windows.Forms.CheckBox();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LoadBitcoinAddressList = new System.Windows.Forms.Button();
            this.UntilTextbox = new System.Windows.Forms.TextBox();
            this.LookupCompressedCheckbox = new System.Windows.Forms.CheckBox();
            this.FromLabel = new System.Windows.Forms.Label();
            this.Hexbox = new System.Windows.Forms.CheckBox();
            this.FromTextbox = new System.Windows.Forms.TextBox();
            this.DecrementRadio = new System.Windows.Forms.RadioButton();
            this.UntilLabel = new System.Windows.Forms.Label();
            this.IncrementRadio = new System.Windows.Forms.RadioButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordListOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.AddressOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MinerInfoUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.ToolChoosingTabcontrol.SuspendLayout();
            this.BrainWalletTab.SuspendLayout();
            this.RandomStringGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfCharsUpDown)).BeginInit();
            this.OptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sha256NumericUpDown)).BeginInit();
            this.TextSelectGroupBox.SuspendLayout();
            this.MininingInformationToolstrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolChoosingTabcontrol
            // 
            this.ToolChoosingTabcontrol.Controls.Add(this.BrainWalletTab);
            this.ToolChoosingTabcontrol.Controls.Add(this.tabPage2);
            this.ToolChoosingTabcontrol.Controls.Add(this.tabPage1);
            this.ToolChoosingTabcontrol.Controls.Add(this.tabPage3);
            this.ToolChoosingTabcontrol.Location = new System.Drawing.Point(-4, 4);
            this.ToolChoosingTabcontrol.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ToolChoosingTabcontrol.Name = "ToolChoosingTabcontrol";
            this.ToolChoosingTabcontrol.SelectedIndex = 0;
            this.ToolChoosingTabcontrol.Size = new System.Drawing.Size(1368, 681);
            this.ToolChoosingTabcontrol.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.ToolChoosingTabcontrol.TabIndex = 0;
            this.ToolChoosingTabcontrol.SelectedIndexChanged += new System.EventHandler(this.ToolChoosingTabcontrol_SelectedIndexChanged);
            // 
            // BrainWalletTab
            // 
            this.BrainWalletTab.Controls.Add(this.RandomStringGroupBox);
            this.BrainWalletTab.Controls.Add(this.OptionsGroupBox);
            this.BrainWalletTab.Controls.Add(this.MiningStartButton);
            this.BrainWalletTab.Controls.Add(this.TextSelectGroupBox);
            this.BrainWalletTab.Controls.Add(this.ProgressRichTextBox);
            this.BrainWalletTab.Controls.Add(this.MininingInformationToolstrip);
            this.BrainWalletTab.Location = new System.Drawing.Point(4, 24);
            this.BrainWalletTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BrainWalletTab.Name = "BrainWalletTab";
            this.BrainWalletTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BrainWalletTab.Size = new System.Drawing.Size(1360, 653);
            this.BrainWalletTab.TabIndex = 0;
            this.BrainWalletTab.Text = "Brainwallet Mining";
            this.BrainWalletTab.UseVisualStyleBackColor = true;
            // 
            // RandomStringGroupBox
            // 
            this.RandomStringGroupBox.Controls.Add(this.CharsetRandomLabel);
            this.RandomStringGroupBox.Controls.Add(this.CharSetTextBox);
            this.RandomStringGroupBox.Controls.Add(this.RandomLabel);
            this.RandomStringGroupBox.Controls.Add(this.CountOfCharsUpDown);
            this.RandomStringGroupBox.Controls.Add(this.UseRandomCheckBox);
            this.RandomStringGroupBox.Location = new System.Drawing.Point(904, 36);
            this.RandomStringGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RandomStringGroupBox.Name = "RandomStringGroupBox";
            this.RandomStringGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RandomStringGroupBox.Size = new System.Drawing.Size(385, 151);
            this.RandomStringGroupBox.TabIndex = 16;
            this.RandomStringGroupBox.TabStop = false;
            this.RandomStringGroupBox.Text = "Random string mining";
            // 
            // CharsetRandomLabel
            // 
            this.CharsetRandomLabel.AutoSize = true;
            this.CharsetRandomLabel.Location = new System.Drawing.Point(7, 75);
            this.CharsetRandomLabel.Name = "CharsetRandomLabel";
            this.CharsetRandomLabel.Size = new System.Drawing.Size(149, 15);
            this.CharsetRandomLabel.TabIndex = 4;
            this.CharsetRandomLabel.Text = "Using user Selected chars:";
            // 
            // CharSetTextBox
            // 
            this.CharSetTextBox.Location = new System.Drawing.Point(7, 97);
            this.CharSetTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CharSetTextBox.Name = "CharSetTextBox";
            this.CharSetTextBox.Size = new System.Drawing.Size(334, 23);
            this.CharSetTextBox.TabIndex = 3;
            // 
            // RandomLabel
            // 
            this.RandomLabel.AutoSize = true;
            this.RandomLabel.Location = new System.Drawing.Point(165, 38);
            this.RandomLabel.Name = "RandomLabel";
            this.RandomLabel.Size = new System.Drawing.Size(69, 15);
            this.RandomLabel.TabIndex = 2;
            this.RandomLabel.Text = "Char count:";
            // 
            // CountOfCharsUpDown
            // 
            this.CountOfCharsUpDown.Location = new System.Drawing.Point(259, 30);
            this.CountOfCharsUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CountOfCharsUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.CountOfCharsUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CountOfCharsUpDown.Name = "CountOfCharsUpDown";
            this.CountOfCharsUpDown.Size = new System.Drawing.Size(81, 23);
            this.CountOfCharsUpDown.TabIndex = 1;
            this.CountOfCharsUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // UseRandomCheckBox
            // 
            this.UseRandomCheckBox.AutoSize = true;
            this.UseRandomCheckBox.Location = new System.Drawing.Point(7, 34);
            this.UseRandomCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UseRandomCheckBox.Name = "UseRandomCheckBox";
            this.UseRandomCheckBox.Size = new System.Drawing.Size(136, 19);
            this.UseRandomCheckBox.TabIndex = 0;
            this.UseRandomCheckBox.Text = "Using random chars";
            this.UseRandomCheckBox.UseVisualStyleBackColor = true;
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.SeparatorTextBox);
            this.OptionsGroupBox.Controls.Add(this.sha256Label);
            this.OptionsGroupBox.Controls.Add(this.SeparatorEnterCheckBox);
            this.OptionsGroupBox.Controls.Add(this.Sha256NumericUpDown);
            this.OptionsGroupBox.Controls.Add(this.SeparatorLabel);
            this.OptionsGroupBox.Controls.Add(this.CompressedCheckBox);
            this.OptionsGroupBox.Location = new System.Drawing.Point(458, 26);
            this.OptionsGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OptionsGroupBox.Size = new System.Drawing.Size(412, 161);
            this.OptionsGroupBox.TabIndex = 15;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // SeparatorTextBox
            // 
            this.SeparatorTextBox.Location = new System.Drawing.Point(104, 26);
            this.SeparatorTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SeparatorTextBox.Name = "SeparatorTextBox";
            this.SeparatorTextBox.Size = new System.Drawing.Size(116, 23);
            this.SeparatorTextBox.TabIndex = 7;
            this.SeparatorTextBox.TextChanged += new System.EventHandler(this.SeparatorTextBox_TextChanged);
            // 
            // sha256Label
            // 
            this.sha256Label.AutoSize = true;
            this.sha256Label.Location = new System.Drawing.Point(11, 111);
            this.sha256Label.Name = "sha256Label";
            this.sha256Label.Size = new System.Drawing.Size(207, 15);
            this.sha256Label.TabIndex = 14;
            this.sha256Label.Text = "Count of sha256 operation on string:";
            // 
            // SeparatorEnterCheckBox
            // 
            this.SeparatorEnterCheckBox.AutoSize = true;
            this.SeparatorEnterCheckBox.Checked = true;
            this.SeparatorEnterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SeparatorEnterCheckBox.Location = new System.Drawing.Point(259, 29);
            this.SeparatorEnterCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SeparatorEnterCheckBox.Name = "SeparatorEnterCheckBox";
            this.SeparatorEnterCheckBox.Size = new System.Drawing.Size(89, 19);
            this.SeparatorEnterCheckBox.TabIndex = 8;
            this.SeparatorEnterCheckBox.Text = "Using Enter";
            this.SeparatorEnterCheckBox.UseVisualStyleBackColor = true;
            // 
            // Sha256NumericUpDown
            // 
            this.Sha256NumericUpDown.Location = new System.Drawing.Point(259, 109);
            this.Sha256NumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.Sha256NumericUpDown.Size = new System.Drawing.Size(140, 23);
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
            this.SeparatorLabel.Location = new System.Drawing.Point(11, 26);
            this.SeparatorLabel.Name = "SeparatorLabel";
            this.SeparatorLabel.Size = new System.Drawing.Size(62, 15);
            this.SeparatorLabel.TabIndex = 6;
            this.SeparatorLabel.Text = "Separator:";
            // 
            // CompressedCheckBox
            // 
            this.CompressedCheckBox.AutoSize = true;
            this.CompressedCheckBox.Location = new System.Drawing.Point(18, 71);
            this.CompressedCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CompressedCheckBox.Name = "CompressedCheckBox";
            this.CompressedCheckBox.Size = new System.Drawing.Size(196, 19);
            this.CompressedCheckBox.TabIndex = 12;
            this.CompressedCheckBox.Text = "Lookup compressed addresses";
            this.CompressedCheckBox.UseVisualStyleBackColor = true;
            // 
            // MiningStartButton
            // 
            this.MiningStartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MiningStartButton.Location = new System.Drawing.Point(610, 217);
            this.MiningStartButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MiningStartButton.Name = "MiningStartButton";
            this.MiningStartButton.Size = new System.Drawing.Size(116, 51);
            this.MiningStartButton.TabIndex = 11;
            this.MiningStartButton.Text = "Start Mining";
            this.MiningStartButton.UseVisualStyleBackColor = true;
            this.MiningStartButton.Click += new System.EventHandler(this.MiningStartButton_Click);
            // 
            // TextSelectGroupBox
            // 
            this.TextSelectGroupBox.Controls.Add(this.AddressLookupLoadButton);
            this.TextSelectGroupBox.Controls.Add(this.PasswordLoadButton);
            this.TextSelectGroupBox.Location = new System.Drawing.Point(31, 26);
            this.TextSelectGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextSelectGroupBox.Name = "TextSelectGroupBox";
            this.TextSelectGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextSelectGroupBox.Size = new System.Drawing.Size(357, 161);
            this.TextSelectGroupBox.TabIndex = 10;
            this.TextSelectGroupBox.TabStop = false;
            this.TextSelectGroupBox.Text = "Select password and lookup txt files";
            // 
            // AddressLookupLoadButton
            // 
            this.AddressLookupLoadButton.Location = new System.Drawing.Point(204, 41);
            this.AddressLookupLoadButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AddressLookupLoadButton.Name = "AddressLookupLoadButton";
            this.AddressLookupLoadButton.Size = new System.Drawing.Size(116, 64);
            this.AddressLookupLoadButton.TabIndex = 9;
            this.AddressLookupLoadButton.Text = "Load Bitcoin lookup address list";
            this.AddressLookupLoadButton.UseVisualStyleBackColor = true;
            this.AddressLookupLoadButton.Click += new System.EventHandler(this.AddressLookupLoadButton_Click);
            // 
            // PasswordLoadButton
            // 
            this.PasswordLoadButton.Location = new System.Drawing.Point(7, 41);
            this.PasswordLoadButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PasswordLoadButton.Name = "PasswordLoadButton";
            this.PasswordLoadButton.Size = new System.Drawing.Size(115, 64);
            this.PasswordLoadButton.TabIndex = 3;
            this.PasswordLoadButton.Text = "Load Password text file";
            this.PasswordLoadButton.UseVisualStyleBackColor = true;
            this.PasswordLoadButton.Click += new System.EventHandler(this.PasswordLoadButton_Click);
            // 
            // ProgressRichTextBox
            // 
            this.ProgressRichTextBox.Location = new System.Drawing.Point(7, 274);
            this.ProgressRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ProgressRichTextBox.Name = "ProgressRichTextBox";
            this.ProgressRichTextBox.ReadOnly = true;
            this.ProgressRichTextBox.Size = new System.Drawing.Size(1337, 251);
            this.ProgressRichTextBox.TabIndex = 5;
            this.ProgressRichTextBox.Text = "";
            // 
            // MininingInformationToolstrip
            // 
            this.MininingInformationToolstrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MininingInformationToolstrip.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MininingInformationToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressLabel,
            this.MiningProgressBar});
            this.MininingInformationToolstrip.Location = new System.Drawing.Point(3, 624);
            this.MininingInformationToolstrip.Name = "MininingInformationToolstrip";
            this.MininingInformationToolstrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.MininingInformationToolstrip.Size = new System.Drawing.Size(1354, 25);
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
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(1360, 653);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Private key search using numbers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LoadBitcoinAddressList);
            this.groupBox1.Controls.Add(this.UntilTextbox);
            this.groupBox1.Controls.Add(this.LookupCompressedCheckbox);
            this.groupBox1.Controls.Add(this.FromLabel);
            this.groupBox1.Controls.Add(this.Hexbox);
            this.groupBox1.Controls.Add(this.FromTextbox);
            this.groupBox1.Controls.Add(this.DecrementRadio);
            this.groupBox1.Controls.Add(this.UntilLabel);
            this.groupBox1.Controls.Add(this.IncrementRadio);
            this.groupBox1.Location = new System.Drawing.Point(276, 31);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(806, 154);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search options";
            // 
            // LoadBitcoinAddressList
            // 
            this.LoadBitcoinAddressList.Location = new System.Drawing.Point(669, 64);
            this.LoadBitcoinAddressList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LoadBitcoinAddressList.Name = "LoadBitcoinAddressList";
            this.LoadBitcoinAddressList.Size = new System.Drawing.Size(116, 64);
            this.LoadBitcoinAddressList.TabIndex = 14;
            this.LoadBitcoinAddressList.Text = "Load Bitcoin lookup address list";
            this.LoadBitcoinAddressList.UseVisualStyleBackColor = true;
            this.LoadBitcoinAddressList.Click += new System.EventHandler(this.LoadBitcoinAddressList_Click);
            // 
            // UntilTextbox
            // 
            this.UntilTextbox.Location = new System.Drawing.Point(137, 56);
            this.UntilTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UntilTextbox.Name = "UntilTextbox";
            this.UntilTextbox.Size = new System.Drawing.Size(416, 23);
            this.UntilTextbox.TabIndex = 3;
            this.UntilTextbox.Text = "10000000";
            this.UntilTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UntilTextbox_KeyUp);
            // 
            // LookupCompressedCheckbox
            // 
            this.LookupCompressedCheckbox.AutoSize = true;
            this.LookupCompressedCheckbox.Location = new System.Drawing.Point(584, 29);
            this.LookupCompressedCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LookupCompressedCheckbox.Name = "LookupCompressedCheckbox";
            this.LookupCompressedCheckbox.Size = new System.Drawing.Size(196, 19);
            this.LookupCompressedCheckbox.TabIndex = 13;
            this.LookupCompressedCheckbox.Text = "Lookup compressed addresses";
            this.LookupCompressedCheckbox.UseVisualStyleBackColor = true;
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Location = new System.Drawing.Point(17, 30);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(65, 15);
            this.FromLabel.TabIndex = 0;
            this.FromLabel.Text = "Start from:";
            // 
            // Hexbox
            // 
            this.Hexbox.AutoSize = true;
            this.Hexbox.Location = new System.Drawing.Point(311, 104);
            this.Hexbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Hexbox.Name = "Hexbox";
            this.Hexbox.Size = new System.Drawing.Size(129, 19);
            this.Hexbox.TabIndex = 6;
            this.Hexbox.Text = "Using hexadecimal";
            this.Hexbox.UseVisualStyleBackColor = true;
            this.Hexbox.CheckedChanged += new System.EventHandler(this.Hexbox_CheckedChanged);
            // 
            // FromTextbox
            // 
            this.FromTextbox.Location = new System.Drawing.Point(137, 22);
            this.FromTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FromTextbox.Name = "FromTextbox";
            this.FromTextbox.Size = new System.Drawing.Size(416, 23);
            this.FromTextbox.TabIndex = 1;
            this.FromTextbox.Text = "1";
            this.FromTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FromTextbox_KeyUp);
            // 
            // DecrementRadio
            // 
            this.DecrementRadio.AutoSize = true;
            this.DecrementRadio.Location = new System.Drawing.Point(144, 102);
            this.DecrementRadio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DecrementRadio.Name = "DecrementRadio";
            this.DecrementRadio.Size = new System.Drawing.Size(85, 19);
            this.DecrementRadio.TabIndex = 5;
            this.DecrementRadio.Text = "Decrement";
            this.DecrementRadio.UseVisualStyleBackColor = true;
            // 
            // UntilLabel
            // 
            this.UntilLabel.AutoSize = true;
            this.UntilLabel.Location = new System.Drawing.Point(17, 64);
            this.UntilLabel.Name = "UntilLabel";
            this.UntilLabel.Size = new System.Drawing.Size(37, 15);
            this.UntilLabel.TabIndex = 2;
            this.UntilLabel.Text = "Until:";
            // 
            // IncrementRadio
            // 
            this.IncrementRadio.AutoSize = true;
            this.IncrementRadio.Checked = true;
            this.IncrementRadio.Location = new System.Drawing.Point(18, 102);
            this.IncrementRadio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IncrementRadio.Name = "IncrementRadio";
            this.IncrementRadio.Size = new System.Drawing.Size(81, 19);
            this.IncrementRadio.TabIndex = 4;
            this.IncrementRadio.TabStop = true;
            this.IncrementRadio.Text = "Increment";
            this.IncrementRadio.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1360, 653);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Blockchain parser";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Soon...";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage3.Size = new System.Drawing.Size(1360, 653);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "I found some bitcoin with this program!";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(420, 169);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(248, 15);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "1M2gDmAVqToASzzRXaehgxrZN5hV8LscFv";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(385, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(464, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Than send the half of it for me ;) my address:";
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
            // BWForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 709);
            this.Controls.Add(this.ToolChoosingTabcontrol);
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BWForm";
            this.Text = "BWTOOLS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BWForm_FormClosing);
            this.ToolChoosingTabcontrol.ResumeLayout(false);
            this.BrainWalletTab.ResumeLayout(false);
            this.BrainWalletTab.PerformLayout();
            this.RandomStringGroupBox.ResumeLayout(false);
            this.RandomStringGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfCharsUpDown)).EndInit();
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sha256NumericUpDown)).EndInit();
            this.TextSelectGroupBox.ResumeLayout(false);
            this.MininingInformationToolstrip.ResumeLayout(false);
            this.MininingInformationToolstrip.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.GroupBox RandomStringGroupBox;
        private System.Windows.Forms.Label CharsetRandomLabel;
        private System.Windows.Forms.TextBox CharSetTextBox;
        private System.Windows.Forms.Label RandomLabel;
        private System.Windows.Forms.NumericUpDown CountOfCharsUpDown;
        private System.Windows.Forms.CheckBox UseRandomCheckBox;
        private System.Windows.Forms.Label UntilLabel;
        private System.Windows.Forms.TextBox FromTextbox;
        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.RadioButton DecrementRadio;
        private System.Windows.Forms.RadioButton IncrementRadio;
        private System.Windows.Forms.TextBox UntilTextbox;
        private System.Windows.Forms.CheckBox Hexbox;
        private System.Windows.Forms.CheckBox LookupCompressedCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button LoadBitcoinAddressList;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

