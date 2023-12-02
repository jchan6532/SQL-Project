using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Security.Policy;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace RunnerStationStatusViewer
{
    public partial class Form1 : Form
    {
        private DatabaseManager dbManager;
        private bool playedVoice = false;
        private long elapsed = 0;
        private bool voiceEnabled = true;
        private object lockObj;
        private delegate void uiDelegate();
        
        public Form1()
        {
            InitializeComponent();
            dbManager = new DatabaseManager(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lockObj = new object();
            dbManager.InsertBinWarningAmount();
            SetWorkstationComboBoxItems();
            SetPartsCountLabels();
            SetLegendLabels();
            SetWarningMessage();
            Task.Run(UpdateGUI);
        }

        private void SetLegendLabels()
        {
            legendLabel3.Text = $"Last {dbManager.GetBinWarningAmount().ToString()} Items";
        }

        private void SetWorkstationComboBoxItems()
        {
            workstationComboBox.DataSource = new BindingSource(dbManager.GetWorkstationNames(), null);
            workstationComboBox.DisplayMember = "Value";
            workstationComboBox.ValueMember = "Key";
        }

        private void SetPartsCountLabels()
        {
            if (workstationComboBox.SelectedIndex != -1)
            {
                int workstationId = ((KeyValuePair<int, string>)workstationComboBox.SelectedItem).Key;

                // Get the parts counts
                int harnessCount = dbManager.GetPartCount("Harness", workstationId);
                int reflectorCount = dbManager.GetPartCount("Reflector", workstationId);
                int housingCount = dbManager.GetPartCount("Housing", workstationId);
                int lensCount = dbManager.GetPartCount("Lens", workstationId);
                int bulbCount = dbManager.GetPartCount("Bulb", workstationId);
                int bezelCount = dbManager.GetPartCount("Bezel", workstationId);
                // Set the panel text
                harnessCountLabel.Text = harnessCount.ToString();
                reflectorCountLabel.Text = reflectorCount.ToString();
                housingCountLabel.Text = housingCount.ToString();
                lensCountLabel.Text = lensCount.ToString();
                bulbCountLabel.Text = bulbCount.ToString();
                bezelCountLabel.Text = bezelCount.ToString();
                // Set the panel colours
                harnessPanel.BackColor = UpdatePanel(harnessCount, "Harness", harnessProgressBar);
                reflectorPanel.BackColor = UpdatePanel(reflectorCount, "Reflector", reflectorProgressBar);
                housingPanel.BackColor = UpdatePanel(housingCount, "Housing", housingProgressBar);
                lensPanel.BackColor = UpdatePanel(lensCount, "Lens", lensProgressBar);
                bulbPanel.BackColor = UpdatePanel(bulbCount, "Bulb", bulbProgressBar);
                bezelPanel.BackColor = UpdatePanel(bezelCount, "Bezel", bezelProgressBar);
            }
        }
        private Color UpdatePanel(int count, string partName, ColorableProgressBar progressBar)
        {
            int binSize = dbManager.GetPartBinSize(partName);
            int binWarning = dbManager.GetBinWarningAmount();
            string[] colourStrings = new string[3];
            int r = 0;
            int g = 0;
            int b = 0;
            Color color = new Color();
            
            if (count <= binWarning)
            {
                color = Color.FromArgb(255, 128, 128);
            }
            else if (count <= binSize / 2)
            {
                color = Color.FromArgb(255, 255, 128);
            }
            else if (count <= binSize)
            {
                color = Color.FromArgb(128, 255, 128);
            }
            else
            {
                color = Color.FromArgb(128, 128, 255);
            }
            UpdateProgressBar(progressBar, count, binSize, color);
            return color;
        }

        private void UpdateProgressBar(ColorableProgressBar progressBar, int count, int binSize, Color color)
        {
            if (count > binSize)
            {
                count = binSize;
            }
            progressBar.ForeColor = color;
            progressBar.Maximum = binSize;
            progressBar.Value = count;
        }

        private void SetWarningMessage()
        {
            string message;
            int workstationId = ((KeyValuePair<int, string>)workstationComboBox.SelectedItem).Key;
            bool warning = dbManager.GetWarningMessage(workstationId, out message);
            warningLabel.Text = message;
            if
                (warning && !playedVoice &&
                 voiceEnabled) // If we received a warning and haven't played a beep, played a beep
            {
                var tts = new SpeechSynthesizer();
                tts.SetOutputToDefaultAudioDevice();
                tts.SpeakAsync(message);
                //Console.Beep(1000, 50);
                playedVoice = true;
            }
        }

        private async void UpdateGUI()
        {
            while (true)
            {
                // Use the UpdateGUI event to track elapsed ms
                elapsed += 100;
                if (elapsed % 5000 == 0)
                {
                    playedVoice = false;
                }
                // Update the UI every 1 seconds.
                if (elapsed % 500 == 0)
                {
                    lock (lockObj)
                    {
                        BeginInvoke(new uiDelegate(SetPartsCountLabels));
                        BeginInvoke(new uiDelegate(SetWarningMessage));
                        BeginInvoke(new uiDelegate(SetLegendLabels));
                    }
                }
                Thread.Sleep(100);
            }
        }

        private void refreshWorkstationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWorkstationComboBoxItems();
        }

        private void disableVoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            voiceEnabled = !voiceEnabled;
        }

        private void workstationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPartsCountLabels();
            SetLegendLabels();
            SetWarningMessage();
        }
    }
}
