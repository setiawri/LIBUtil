using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIBUtil.Desktop.UserControls
{
    public partial class PanelToggle : UserControl
    {

        /*******************************************************************************************************/
        #region SETTINGS

        #endregion SETTINGS
        /*******************************************************************************************************/
        #region PUBLIC VARIABLES

        private Image _ArrowLeft = Properties.Resources.arrow_left;
        private Image _ArrowUp = Properties.Resources.arrow_up;
        private Image _ArrowRight = Properties.Resources.arrow_right;
        private Image _ArrowDown = Properties.Resources.arrow_down;

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region PROPERTIES

        [Description("Toggle Panel"), Category("_Custom")]
        public Panel TogglePanel
        {
            get { return _togglePanel; }
            set {
                _togglePanel = value;

                //if(_togglePanel != null && _togglePanel.FindForm() != null)
                //{
                //    Point togglePanelLoc = Util.getLocationRelativeToForm(_togglePanel);
                //    Point toggleButtonLoc = Util.getLocationRelativeToForm(this);

                //    if (ArrowOrientation == Orientation.Horizontal)
                //    {
                //        if (toggleButtonLoc.X > togglePanelLoc.X)
                //            setDirection(ArrowDirection.Left);
                //        else
                //            setDirection(ArrowDirection.Right);
                //    }
                //    else
                //    {
                //        if (toggleButtonLoc.Y > togglePanelLoc.Y)
                //            setDirection(ArrowDirection.Up);
                //        else
                //            setDirection(ArrowDirection.Down);
                //    }
                //}
            }
        }
        private Panel _togglePanel;
        
        [Description("Initial Arrow Direction"), Category("_Custom")]
        public ArrowDirection InitialArrowDirection
        {
            get { return _initialArrowDirection; }
            set {
                _initialArrowDirection = value;
                setDirection(_initialArrowDirection);
            }
        }
        private ArrowDirection _initialArrowDirection = ArrowDirection.Left;

        public Orientation ArrowOrientation { get { return getOrientation(); } }

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region PRIVATE VARIABLES

        #endregion PRIVATE VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public PanelToggle() : this(null) { }
        public PanelToggle(Guid? id) { InitializeComponent(); }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        private void setupControls()
        {
            setupControlsBasedOnRoles();
        }

        private void setupControlsBasedOnRoles()
        {

        }

        private void populateData()
        {

        }

        private void resetData()
        {

        }

        public void toggle()
        {
            if(TogglePanel.GetType() == typeof(SplitterPanel))
            {
                SplitContainer parent = (SplitContainer)TogglePanel.Parent;
                if (parent.Panel1 == TogglePanel)
                    parent.Panel1Collapsed = !parent.Panel1Collapsed;
                else
                    parent.Panel2Collapsed = !parent.Panel2Collapsed;
            }
            else
                TogglePanel.Visible = !TogglePanel.Visible;

            //set arrow image
            if (pictureBox.BackgroundImage == _ArrowLeft)
                setDirection(ArrowDirection.Right);
            else if (pictureBox.BackgroundImage == _ArrowRight)
                setDirection(ArrowDirection.Left);
            else if (pictureBox.BackgroundImage == _ArrowUp)
                setDirection(ArrowDirection.Down);
            else
                setDirection(ArrowDirection.Up);
        }

        public bool isTogglePanelVisible()
        {
            if (TogglePanel.GetType() == typeof(SplitterPanel))
            {
                SplitContainer parent = (SplitContainer)TogglePanel.Parent;
                if (parent.Panel1 == TogglePanel)
                    return !parent.Panel1Collapsed;
                else
                    return !parent.Panel2Collapsed;
            }
            else
                return TogglePanel.Visible;
        }

        public void setArrowImages(Image left, Image up, Image right, Image down)
        {
            _ArrowLeft = left;
            _ArrowUp = up;
            _ArrowRight = right;
            _ArrowDown = down;
        }

        private void setDirection(ArrowDirection direction)
        {
            _initialArrowDirection = direction;
            if (direction == ArrowDirection.Left)
                pictureBox.BackgroundImage = _ArrowLeft;
            else if(direction == ArrowDirection.Up)
                pictureBox.BackgroundImage = _ArrowUp;
            else if (direction == ArrowDirection.Right)
                pictureBox.BackgroundImage = _ArrowRight;
            else
                pictureBox.BackgroundImage = _ArrowDown;
        }

        private Orientation getOrientation()
        {
            Point togglePanelLoc = Util.getLocationRelativeToForm(_togglePanel);
            Point toggleButtonLoc = Util.getLocationRelativeToForm(this);

            if (toggleButtonLoc.X >= togglePanelLoc.X + _togglePanel.Width 
                    && toggleButtonLoc.Y >= togglePanelLoc.Y 
                    && toggleButtonLoc.Y <= togglePanelLoc.Y + _togglePanel.Height)
                return Orientation.Horizontal;
            else
                return Orientation.Vertical;
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void Form_Load(object sender, EventArgs e)
        {
            setupControls();
            populateData();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            toggle();
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/
    }
}
