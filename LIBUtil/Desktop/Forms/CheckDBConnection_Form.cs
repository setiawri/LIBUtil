using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIBUtil.Desktop.Forms
{
    public partial class CheckDBConnection_Form : Form
    {
        /*******************************************************************************************************/
        #region SETTINGS

        private const int TIMEOUT = 100; //close form after the specified time
        private const int TIMER_INTERVAL = 100; //raise event every 0.1 second

        #endregion SETTINGS
        /*******************************************************************************************************/
        #region PUBLIC VARIABLES

        public bool isDBConnectionAvailable = false;

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region PRIVATE VARIABLES
            
        private int _timerCounter = 0;
        private bool _showError = true;
        private bool _showProgressBar = true;

        #endregion PRIVATE VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS
            
        public CheckDBConnection_Form(Icon icon, bool showError, bool showProgressBar)
        {
            InitializeComponent();

            this.Icon = icon;
            _showError = showError;
            _showProgressBar = showProgressBar;
        }

        public CheckDBConnection_Form(Icon icon): this(icon, true, true) { }

        public CheckDBConnection_Form() : this(null, true, true) { }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        private void setupControls()
        {
            if (this.Parent == null)
                this.StartPosition = FormStartPosition.CenterScreen;
            else
                this.StartPosition = FormStartPosition.CenterParent;

            timer1.Interval = TIMER_INTERVAL;

            if (!_showProgressBar)
            {
                label1.Visible = false;
                progressBar1.Visible = false;
                this.BackColor = Color.White;
                this.Width = 1;
                this.Height = 1;
                this.ShowInTaskbar = false;
            }
        }

        private void populateData()
        {

        }

        private void resetData()
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Util.testDBConnection();
                Util.DBConnectionTestCompleted = true;
            }
            catch
            {
                Util.DBConnectionTestCompleted = false;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Util.DBConnectionTestCompleted)
            {
                timer1.Stop();
                isDBConnectionAvailable = true;
                this.Close();
            }
            else if (_timerCounter == TIMEOUT)
            {
                timer1.Stop();
                isDBConnectionAvailable = false;
                if(_showError)
                    Util.displayMessageBoxError("There is a problem connecting to the database. Please contact your administrator and try again.");
                this.Close();
            }
            else
            {
                _timerCounter++;
                progressBar1.Value = _timerCounter * 100 / TIMEOUT;
            }
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void Form_Load(object sender, EventArgs e)
        {
            setupControls();
            populateData();

            Util.DBConnectionTestCompleted = false;
            
            timer1.Start();
            backgroundWorker1.RunWorkerAsync();
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/
    }
}
