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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WorkstationAndonDisplay
{
    public partial class AndonPanel : UserControl
    {
        public string PartName { get; set; }
        public int WorkstationId { get; set; }
        public int Amount { get; set; }
        public int RefillAmount { get; set; }

        private delegate void guiDelegate();
        public string ConnectionString { get; private set; }

        public AndonPanel(string partName, int workstationId)
        {
            PartName = partName;
            WorkstationId = workstationId;
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            InitializeComponent();
        }


        private void AndonPanel_Load(object sender, EventArgs e)
        {
            partNameLabel.Text = PartName;
            Task.Run(guiUpdate);
        }


        private void guiUpdate()
        {
            while (true)
            {
                BeginInvoke(new guiDelegate(UpdateElements));
                Thread.Sleep(1000);
            }
        }

        private void UpdateElements()
        {
            DatabaseManager dbManager = new DatabaseManager(ConnectionString);
            int partCount = dbManager.GetPartCount(PartName, WorkstationId);
            int binSize = dbManager.GetPartBinSize(PartName);
            int binWarning = dbManager.GetBinWarningAmount();

            RefillAmount = binWarning;
            Amount = partCount;

            partCountLabel.Text = Amount.ToString();
            partBinStatusBar.Maximum = binSize;
            partBinStatusBar.Value = partCount > binSize ? binSize : partCount;

            Color color = new Color();

            if (partCount <= binWarning)
            {
                color = Color.FromArgb(255, 128, 128);
            }
            else if (partCount <= binSize / 2)
            {
                color = Color.FromArgb(255, 255, 128);
            }
            else if (partCount <= binSize)
            {
                color = Color.FromArgb(128, 255, 128);
            }
            else
            {
                color = Color.FromArgb(128, 128, 255);
            }

            partNamePanel.BackColor = color;
            partBinStatusBar.ForeColor = color;
        }
    }
}
