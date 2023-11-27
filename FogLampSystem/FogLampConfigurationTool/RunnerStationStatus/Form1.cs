using System.Configuration;
using System.Media;

namespace RunnerStationStatus
{
    public partial class Form1 : Form
    {
        private DatabaseManager dbManager;
        private int selectedWorkstation = 1;
        private bool playedBeep = false;
        private int elapsed = 0;

        public Form1()
        {
            InitializeComponent();
            dbManager = new DatabaseManager(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWorkstationComboBoxItems();
            SetPartsCountLabels();
            Task.Run(UpdateGUI);
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

        private Color GetPanelColor(int count, string partName)
        {
            int binSize = dbManager.GetPartBinSize(partName);
            int binWarning = dbManager.GetBinWarningAmount();
            Color color = new Color();
            if (count <= binWarning)
            {
                color = Color.FromArgb(255, 128, 128);
                // If we notice a panel in the red, we play a beep sound to notify the user
                if (!playedBeep) // Use this boolean to avoid constant beeping
                {
                    Console.Beep();
                    playedBeep = true; // Set 'playedBeep' to true to avoid constant beeping
                    // It is reset in the UpdateGUI function
                }
            }
            else if (count <= binSize / 2)
            {
                color = Color.FromArgb(255, 255, 128);
            }
            else
            {
                color = Color.FromArgb(128, 255, 128);
            }
            return color;
        }

        private void SetWarningMessage()
        {
            string message = string.Empty;
            bool warning = dbManager.GetWarningMessage(out message);
            warningLabel.Text = message;
            if (warning && !playedBeep)// If we received a warning and haven't played a beep, played a beep
            {
                Console.Beep(1000,50);
                playedBeep = true;
            }
        }

        private async void UpdateGUI()
        {
            while (true)
            {
                BeginInvoke(SetPartsCountLabels);
                BeginInvoke(SetWarningMessage);
                // Use the UpdateGUI event to track elapsed ms
                elapsed += 100;
                if (elapsed >= Int32.Parse(ConfigurationManager.AppSettings["BeepDelay"]))
                {
                    playedBeep = false;
                    elapsed = 0;
                }
                Thread.Sleep(100);
            }
        }
    }
}