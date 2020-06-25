using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NBitcoin;
using System.Numerics;

namespace BWTool
{
    public partial class BWForm : Form
    {
        BrainwalletMiner miner;
        RandomStringMiner StringMiner;
        IncrementalSearch incremental;
        Stopwatch stopWatch = new Stopwatch();
        public BWForm()
        {
            InitializeComponent();

        }


        private void PasswordLoadButton_Click(object sender, EventArgs e)
        {
            if (PasswordListOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                ProgressRichTextBox.AppendText($"Selected password file: {PasswordListOpenFileDialog.FileName}{Environment.NewLine}");
            }
        }

        private void SeparatorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SeparatorTextBox.Text.Length > 0 && SeparatorTextBox.Text.Length < 2)
            {
                SeparatorEnterCheckBox.Checked = false;


            }
            else
            {
                SeparatorEnterCheckBox.Checked = true;
                SeparatorTextBox.Clear();
            }
        }

        private void AddressLookupLoadButton_Click(object sender, EventArgs e)
        {
            if (AddressOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                ProgressRichTextBox.AppendText($"Selected lookup address list: {AddressOpenFileDialog.FileName}{Environment.NewLine}");
            }
        }
        private bool InputisValid()
        {
            if (ToolChoosingTabcontrol.SelectedIndex == 1)
            {
                if (FromTextbox.Text.Length > 0 && UntilTextbox.Text.Length > 0 && AddressOpenFileDialog.FileName != "")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Please fill the missing values!");
                    return false;
                }
            }
            else
            {
                if (AddressOpenFileDialog.FileName == "" && PasswordListOpenFileDialog.FileName == "" && !UseRandomCheckBox.Checked)
                {
                    MessageBox.Show("Please select address file and password file first!");

                    return false;

                }
                else if (AddressOpenFileDialog.FileName == "" && UseRandomCheckBox.Checked)
                {
                    MessageBox.Show("Please select address file first!");
                    return false;
                }
                else
                {
                    return true;
                }
            }


        }

        private void MiningStartButton_Click(object sender, EventArgs e)
        {
            if (ToolChoosingTabcontrol.SelectedIndex == 1)
            {
                if (InputisValid())
                {
                    if (MiningStartButton.Text == "Start Mining")
                    {
                        MiningStartButton.Text = "Stop Mining";
                        bool increment = IncrementRadio.Checked ? true : false;
                        incremental = new IncrementalSearch(AddressOpenFileDialog.FileName, LookupCompressedCheckbox.Checked, Hexbox.Checked, FromTextbox.Text, UntilTextbox.Text,increment,this,ProgressRichTextBox);
                        incremental.StartIncrementalSearch();
                        MinerInfoUpdateTimer.Start();
                        stopWatch.Start();


                    }
                    else
                    {
                        stopMiner();
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
            if (UseRandomCheckBox.Checked)
            {
                if (InputisValid())
                {
                    if (MiningStartButton.Text == "Start Mining")
                    {
                        MiningStartButton.Text = "Stop Mining";
                        if (CharSetTextBox.Text.Length > 0)
                        {
                            StringMiner = new RandomStringMiner((int)CountOfCharsUpDown.Value, AddressOpenFileDialog.FileName, CompressedCheckBox.Checked, (int)Sha256NumericUpDown.Value, CharSetTextBox.Text);
                        }
                        else
                        {
                            StringMiner = new RandomStringMiner((int)CountOfCharsUpDown.Value, AddressOpenFileDialog.FileName, CompressedCheckBox.Checked, (int)Sha256NumericUpDown.Value);
                        }
                        StringMiner.StartRandomStringMiner();
                        MinerInfoUpdateTimer.Start();
                        stopWatch.Start();


                    }
                    else
                    {
                        stopMiner();
                    }
                    return;
                }

            }
            else
            {
                if (InputisValid())
                {
                    char separator = '\0';
                    if (SeparatorEnterCheckBox.Checked)
                    {
                        separator = '\r';
                    }
                    else
                    {
                        separator = Convert.ToChar(SeparatorTextBox.Text);
                    }
                    if (MiningStartButton.Text == "Start Mining")
                    {
                        MiningStartButton.Text = "Stop Mining";

                        miner = new BrainwalletMiner(PasswordListOpenFileDialog.FileName, AddressOpenFileDialog.FileName, CompressedCheckBox.Checked, separator, (int)Sha256NumericUpDown.Value);
                        miner.Start();
                        MinerInfoUpdateTimer.Start();
                        stopWatch.Start();

                    }
                    else
                    {
                        stopMiner();
                    }
                }
            }



        }
        void stopMiner()
        {
            MiningStartButton.Text = "Start Mining";
            miner?.Stop();
            StringMiner?.StopRandomStringMiner();
            incremental?.StopRandomStringMiner();
            MinerInfoUpdateTimer.Stop();
            MiningProgressBar.Value = 0;
            ProgressLabel.Text = "";
            stopWatch.Reset();
            //If the job was done by miner thread minerStillRunning bool should be false this will be skipped
            //If the cancellation was requested by user this should be true
            while (MinerInfo.minerStillRunning)
            {
                //Cancellation was already requested in miner thread, waiting a few milliseconds to completing
                Thread.Sleep(10);
            }
            ProgressRichTextBox.AppendText(MinerInfo.minerThreadResults);
            MinerInfo.defaultingValues();
        }
        private void Uiupdate(object sender, EventArgs e)
        {
            if (!MinerInfo.minerStillRunning)
            {
                stopMiner();
                return;
            }
            double timeLeft;
            try
            {
                timeLeft = ((double)stopWatch.Elapsed.TotalSeconds / MinerInfo.currentlyProcessed) * (MinerInfo.lengthOfJob - MinerInfo.currentlyProcessed);
            }
            catch (DivideByZeroException)
            {

                timeLeft = double.NaN;
            }

            ProgressLabel.Text = $"Tried keys: {MinerInfo.countOfTriedKeys},elapsed seconds: {stopWatch.Elapsed.TotalSeconds.ToString("N1")}, average keys perc second: {(MinerInfo.countOfTriedKeys / stopWatch.Elapsed.TotalSeconds).ToString("N2")}, estimated time left: {Math.Round(timeLeft)} seconds, miner thread info:{Environment.NewLine}{MinerInfo.minerThreadInfo}";
            double progressValue = 100 / (MinerInfo.lengthOfJob / (double)MinerInfo.currentlyProcessed);
            int percentValue = (int)Math.Round(progressValue);
            MiningProgressBar.Value = percentValue;
            //Job was done by the thread already
            if (!MinerInfo.minerStillRunning)
            {
                stopMiner();


            }
        }
        private void BWForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MinerInfo.minerStillRunning)
            {
                stopMiner();
            }

        }

        private void ToolChoosingTabcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ToolChoosingTabcontrol.SelectedTab.Controls.Contains(MiningStartButton) && ToolChoosingTabcontrol.SelectedIndex<2)
            {

                ToolChoosingTabcontrol.SelectedTab.Controls.Add(MiningStartButton);
                ToolChoosingTabcontrol.SelectedTab.Controls.Add(ProgressRichTextBox);
                ToolChoosingTabcontrol.SelectedTab.Controls.Add(MininingInformationToolstrip);
            }
        }

        private void AllowOnlyNumbersAbove0AndBelowBitcoinMaxValue(TextBox textBox)
        {
            if (textBox.Text.Length > 0)
            {
                if (!Helper.OnlyHexInString(textBox.Text) && !Helper.IsDigitsOnly(textBox.Text))
                {
                    textBox.Text = "";
                    return;
                }
                if (Helper.IsDigitsOnly(textBox.Text))
                {
                    var number = BigInteger.Parse(textBox.Text, NumberStyles.Integer);
                    if (number.CompareTo(1) < 0)
                    {
                        MessageBox.Show("Value must be bigger than 0!");
                        textBox.Text = "";
                    }
                    else if (number.CompareTo(BigInteger.Parse("115792089237316195423570985008687907852837564279074904382605163141518161494336")) == 1)
                    {
                        MessageBox.Show("Value must be smaller than bitcoin max value!");
                        textBox.Text = "";
                    }
                }
                else if (Helper.OnlyHexInString(textBox.Text))
                {
                    var number = BigInteger.Parse("0" + textBox.Text, NumberStyles.HexNumber);
                    if (number.CompareTo(1) < 0)
                    {
                        MessageBox.Show("Value must be bigger than 0!");
                        textBox.Text = "";
                    }
                    else if (number.CompareTo(BigInteger.Parse("0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364140", NumberStyles.HexNumber)) == 1)
                    {
                        MessageBox.Show("Value must be smaller than bitcoin max value!");
                        textBox.Text = "";
                    }
                }



            }
        }
        private void Hexbox_CheckedChanged(object sender, EventArgs e)
        {

            FromTextbox.Text = Helper.ConvertBigintegerTextBetweenHexAndDecimal(FromTextbox.Text, Hexbox.Checked);
            UntilTextbox.Text = Helper.ConvertBigintegerTextBetweenHexAndDecimal(UntilTextbox.Text, Hexbox.Checked);
        }
        private void UntilTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAbove0AndBelowBitcoinMaxValue((TextBox)sender);

        }

        private void FromTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAbove0AndBelowBitcoinMaxValue((TextBox)sender);
        }

        private void LoadBitcoinAddressList_Click(object sender, EventArgs e)
        {
            if (AddressOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                ProgressRichTextBox.AppendText($"Selected lookup address list: {AddressOpenFileDialog.FileName}{Environment.NewLine}");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("Bitcoin:1M2gDmAVqToASzzRXaehgxrZN5hV8LscFv");
        }
    }
}
