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
    /// <summary>
    /// The code behind class for the andon panel user control, it encapsulates all business logic required for each bin part
    /// </summary>
    public partial class AndonPanel : UserControl
    {
        /// <summary>
        /// The part name for the panel
        /// </summary>
        public string PartName
        {
            get; 
            set;
        }

        /// <summary>
        /// The workstation ID for the current andon display
        /// </summary>
        public int WorkstationId
        {
            get;
            set;
        }

        /// <summary>
        /// The current part count
        /// </summary>
        public int Amount
        {
            get;
            set;
        }

        /// <summary>
        /// The refill amount or max bin capacity for the part, it remains constant
        /// </summary>
        public int RefillAmount
        { 
            get;
            set;
        }

        private delegate void guiDelegate();

        /// <summary>
        /// The SQL connection string
        /// </summary>
        public string ConnectionString
        { 
            get;
            private set;
        }

        /// <summary>
        /// Parameterized constructor taking in a part name and work station ID that describe the bin panel
        /// </summary>
        /// <param name="partName">The name of the part</param>
        /// <param name="workstationId">The workstation ID that the part bin belongs to</param>
        public AndonPanel(string partName, int workstationId)
        {
            PartName = partName;
            WorkstationId = workstationId;
            ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            InitializeComponent();
        }

        /// <summary>
        /// Event handler when the andon panel loads up in the form
        /// </summary>
        /// <param name="sender">the sender of the event, so in this case the user control</param>
        /// <param name="e">event arguments that provide extra information for the event</param>
        private void AndonPanel_Load(object sender, EventArgs e)
        {
            partNameLabel.Text = PartName;
            Task.Run(guiUpdate);
        }

        /// <summary>
        /// Periodically updates the user interface for the panel, updating elements within the panel like part count, part name,
        /// part max bin capacity, part status bar, etc.
        /// </summary>
        private void guiUpdate()
        {
            while (true)
            {
                BeginInvoke(new guiDelegate(UpdateElements));
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Main method for updating the UI control elements that display the part information, like part count, part name, part max cpacity,
        /// part status bar.
        /// </summary>
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
