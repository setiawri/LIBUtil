using System;
using System.Data;
using System.Windows.Forms;

namespace LIBUtil.Desktop.Forms
{
    public partial class MessageBox_Form : Form
    {
        /*******************************************************************************************************/
        #region SETTINGS

        #endregion SETTINGS
        /*******************************************************************************************************/
        #region PUBLIC VARIABLES
            
        public bool ShowButtons
        {
            get { return pnlButtons.Visible; }
            set { pnlButtons.Visible = value; }
        }

        public bool ShowFrame
        {
            get { return this.FormBorderStyle != FormBorderStyle.None; }
            set {
                if (value)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    lblMessage.BorderStyle = BorderStyle.None;
                    pnlButtons.BorderStyle = BorderStyle.None;
                }
                else
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                    lblMessage.BorderStyle = BorderStyle.FixedSingle;
                    pnlButtons.BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }

        public bool ShowFrameButtons
        {
            get { return this.ControlBox; }
            set { this.ControlBox = value; }
        }

        public int FontSize
        {
            get { return (int)lblMessage.Font.Size; }
            set { lblMessage.Font = new System.Drawing.Font(lblMessage.Font.FontFamily, (float)value); }
        }

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region PRIVATE VARIABLES

        Func<string> _methodToRun;
        
        #endregion PRIVATE VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        //public MessageBox_Form() : this(null, null) { }
        public MessageBox_Form(Func<string> methodToRun, string message, int? fontSize, int? secondToAutoClose, bool showFrame, bool showFrameButtons, bool showButtons, System.Drawing.Icon icon)
        {
            InitializeComponent();

            _methodToRun = methodToRun;
            lblMessage.Text = message;
            ShowFrame = showFrame;
            ShowFrameButtons = showFrameButtons;
            ShowButtons = showButtons;
            if (fontSize != null)
                FontSize = (int)fontSize;
            if (icon != null)
                this.Icon = icon;
            if (secondToAutoClose != null)
            {
                timerAutoClose.Interval = (int)secondToAutoClose * 1000;
                timerAutoClose.Start();
            }
        }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        private void setupControls()
        {
        }
        
        private void populateData()
        {
        }

        private void resetData()
        {

        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void Form_Load(object sender, EventArgs e)
        {
            setupControls();
            populateData();
        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MessageBox_Form_Shown(object sender, EventArgs e)
        {
            if (_methodToRun != null)
                _methodToRun();
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/
    }
}
