using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunnerStationStatusViewer
{
    public partial class Form1 : Form
    {
        private DatabaseManager dbManager;
        private int selectedWorkstation = 1;
        private bool playedBeep = false;
        private long elapsed = 0;
        private bool beepEnabled = true;
        private object lockObj;
        private bool debugMode = false;
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
            SetDebugVisibility();
            Task.Run(UpdateGUI);
        }

        private void SetDebugVisibility()
        {
            enableDebugModeToolStripMenuItem.Visible = bool.Parse(ConfigurationManager.AppSettings["EnableDebugMode"]);
        }

        private void SetLegendLabels()
        {
            legendLabel3.Text = $"Last {dbManager.GetBinWarningAmount().ToString()} Items";
            legendPanel1.BackColor = ParseColor("colour1");
            legendPanel2.BackColor = ParseColor("colour2");
            legendPanel3.BackColor = ParseColor("colour3");
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
                harnessPanel.BackColor = GetPanelColor(harnessCount, "Harness");
                reflectorPanel.BackColor = GetPanelColor(reflectorCount, "Reflector");
                housingPanel.BackColor = GetPanelColor(housingCount, "Housing");
                lensPanel.BackColor = GetPanelColor(lensCount, "Lens");
                bulbPanel.BackColor = GetPanelColor(bulbCount, "Bulb");
                bezelPanel.BackColor = GetPanelColor(bezelCount, "Bezel");
            }
        }

        private Color ParseColor(string colourKey)
        {
            string[] colourStrings = ConfigurationManager.AppSettings[colourKey].Split(',');
            int r = Int32.Parse(colourStrings[0]);
            int g = Int32.Parse(colourStrings[1]);
            int b = Int32.Parse(colourStrings[2]);


            return Color.FromArgb(r, g, b);
        }

        private Color GetPanelColor(int count, string partName)
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
                color = ParseColor("colour3");
                // If we notice a panel in the red, we play a beep sound to notify the user
                if (!playedBeep && beepEnabled) // Use this boolean to avoid constant beeping
                {
                    Console.Beep();
                    playedBeep = true; // Set 'playedBeep' to true to avoid constant beeping
                    // It is reset in the UpdateGUI function
                }
            }
            else if (count <= binSize / 2)
            {
                color = ParseColor("colour2");
            }
            else
            {
                color = ParseColor("colour1");
            }
            return color;
        }

        private void SetWarningMessage()
        {
            string message = string.Empty;
            bool warning = dbManager.GetWarningMessage(out message);
            warningLabel.Text = message;
            if (warning && !playedBeep && beepEnabled)// If we received a warning and haven't played a beep, played a beep
            {
                Console.Beep(1000, 50);
                playedBeep = true;
            }
        }

        private async void UpdateGUI()
        {
            while (true)
            {
                // Use the UpdateGUI event to track elapsed ms
                elapsed += 100;
                if (elapsed % Int32.Parse(ConfigurationManager.AppSettings["BeepDelay"]) == 0)
                {
                    playedBeep = false;
                }
                // Update the UI every 5 seconds.
                if (elapsed % 5000 == 0)
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

        private void harnessCountLabel_DoubleClick(object sender, EventArgs e)
        {
            if (debugMode)
            {
                int workstationId = ((KeyValuePair<int, string>)workstationComboBox.SelectedItem).Key;
                dbManager.RefillBin("Harness", workstationId);
            }
        }

        private void lensCountLabel_DoubleClick(object sender, EventArgs e)
        {
            if (debugMode)
            {
                int workstationId = ((KeyValuePair<int, string>)workstationComboBox.SelectedItem).Key;
                dbManager.RefillBin("Lens", workstationId);
            }
        }

        private void bezelCountLabel_DoubleClick(object sender, EventArgs e)
        {
            if (debugMode)
            {
                int workstationId = ((KeyValuePair<int, string>)workstationComboBox.SelectedItem).Key;
                dbManager.RefillBin("Bezel", workstationId);
            }
        }

        private void reflectorCountLabel_DoubleClick(object sender, EventArgs e)
        {
            if (debugMode)
            {
                int workstationId = ((KeyValuePair<int, string>)workstationComboBox.SelectedItem).Key;
                dbManager.RefillBin("Reflector", workstationId);
            }
        }
        private void bulbCountLabel_DoubleClick(object sender, EventArgs e)
        {
            if (debugMode)
            {
                int workstationId = ((KeyValuePair<int, string>)workstationComboBox.SelectedItem).Key;
                dbManager.RefillBin("Bulb", workstationId);
            }
        }

        private void housingCountLabel_DoubleClick(object sender, EventArgs e)
        {
            if (debugMode)
            {
                int workstationId = ((KeyValuePair<int, string>)workstationComboBox.SelectedItem).Key;
                dbManager.RefillBin("Housing", workstationId);
            }
        }

        private void refreshWorkstationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWorkstationComboBoxItems();
        }

        private void enableDebugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugMode = enableDebugModeToolStripMenuItem.Checked;
            if (debugMode)
            {
                debugModeLabel.Text = "Debug Mode : ENABLED";
            }
            else
            {
                debugModeLabel.Text = "";
            }
        }

        private void workstationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPartsCountLabels();
            SetLegendLabels();
            SetWarningMessage();
        }
    }
}
