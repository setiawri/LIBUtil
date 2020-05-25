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
        public Panel TogglePanel { get; set; }

        [Description("Container Panel"), Category("_Custom")]
        public Panel ContainerPanel { get; set; }

        public Size ContainerPanelOriginalSize { get; set; }

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

        public Position InitialPositionRelativeToPanel
        {
            get {

                if (_initialPositionRelativeToPanel == Position.None)
                {
                    if (ArrowOrientation == Orientation.Horizontal)
                    {
                        if (this.Location.X >= TogglePanel.Location.X + TogglePanel.Width)
                            _initialPositionRelativeToPanel = Position.Right;
                        else
                            _initialPositionRelativeToPanel = Position.Left;
                    }
                    else
                    {
                        if (this.Location.Y < TogglePanel.Location.Y)
                            _initialPositionRelativeToPanel = Position.Top;
                        else
                            _initialPositionRelativeToPanel = Position.Bottom;
                    }
                }
                return _initialPositionRelativeToPanel;
            }
        }
        private Position _initialPositionRelativeToPanel = Position.None;

        [Description("Adjust Location On Click"), Category("_Custom")]
        public bool AdjustLocationOnClick { get; set; }

        public Orientation ArrowOrientation
        {
            get
            {
                if (InitialArrowDirection == ArrowDirection.Left || InitialArrowDirection == ArrowDirection.Right)
                    return Orientation.Horizontal;
                else
                    return Orientation.Vertical;
            }
        }

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
            if(ContainerPanel != null)
            {
                if(ContainerPanelOriginalSize.Width == 0 || ContainerPanelOriginalSize.Height == 0)
                    setContainerPanelOriginalSize();

                if (ContainerPanel.GetType() == typeof(SplitterPanel))
                {
                    SplitContainer parent = (SplitContainer)ContainerPanel.Parent;
                    if (parent.Panel1 == ContainerPanel)
                    {
                        if (ArrowOrientation == Orientation.Horizontal)
                        {
                            if (parent.SplitterDistance != this.Width)
                                parent.SplitterDistance = this.Width;
                            else
                                parent.SplitterDistance = ContainerPanelOriginalSize.Width;
                        }
                        else
                        {
                            if (parent.SplitterDistance != this.Height)
                                parent.SplitterDistance = this.Height;
                            else
                                parent.SplitterDistance = ContainerPanelOriginalSize.Height;
                        }
                    }
                    else
                    {
                        if (ArrowOrientation == Orientation.Horizontal)
                        {
                            if (parent.SplitterDistance != (parent.Width - this.Width))
                                parent.SplitterDistance = parent.Width - this.Width;
                            else
                                parent.SplitterDistance = ContainerPanelOriginalSize.Width;
                        }
                        else
                        {
                            if (parent.SplitterDistance != (parent.Height - this.Height))
                            {
                                parent.SplitterDistance = parent.Height - this.Height;
                            }
                            else
                                parent.SplitterDistance = ContainerPanelOriginalSize.Height;
                        }
                    }
                }
                else
                {
                    if (ArrowOrientation == Orientation.Horizontal)
                    {
                        if (ContainerPanel.Width != this.Width)
                            ContainerPanel.Width = this.Width;
                        else
                            ContainerPanel.Width = ContainerPanelOriginalSize.Width;
                    }
                    else
                    {
                        if (ContainerPanel.Height != this.Height)
                            ContainerPanel.Height = this.Height;
                        else
                            ContainerPanel.Height = ContainerPanelOriginalSize.Height;
                    }
                }
            }
            else if (TogglePanel.GetType() == typeof(SplitterPanel))
            {
                SplitContainer parent = (SplitContainer)TogglePanel.Parent;
                if (parent.Panel1 == TogglePanel)
                {
                    parent.Panel1Collapsed = !parent.Panel1Collapsed;
                    setNewLocationBasedOnOrientation(parent.Panel1);
                }
                else
                {
                    parent.Panel2Collapsed = !parent.Panel2Collapsed;
                    setNewLocationBasedOnOrientation(parent.Panel2);
                }
            }
            else
            {
                TogglePanel.Visible = !TogglePanel.Visible;
                setNewLocationBasedOnOrientation(TogglePanel);
            }

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

        public void setNewLocationBasedOnOrientation(Panel panel)
        {
            if (AdjustLocationOnClick)
            {
                if (ArrowOrientation == Orientation.Horizontal)
                    this.Location = new Point(panel.Location.X + panel.Width * Convert.ToInt16(panel.Visible), this.Location.Y);
                else
                {
                    if (InitialPositionRelativeToPanel == Position.Top)
                    {
                        if (panel.Visible)
                            this.Location = new Point(this.Location.X, this.Location.Y - panel.Height);
                        else
                            this.Location = new Point(this.Location.X, this.Location.Y + panel.Height);
                    }
                    else
                    {
                        if (panel.Visible)
                            this.Location = new Point(this.Location.X, this.Location.Y + panel.Height);
                        else
                            this.Location = new Point(this.Location.X, this.Location.Y - panel.Height);
                    }
                }
            }
        }

        public bool isTogglePanelVisible()
        {
            if(ContainerPanel != null)
            {
                if (ArrowOrientation == Orientation.Horizontal)
                    return ContainerPanel.Width != this.Width;
                else
                    return ContainerPanel.Height != this.Height;
            }
            else if (TogglePanel.GetType() == typeof(SplitterPanel))
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
            else if (direction == ArrowDirection.Up)
                pictureBox.BackgroundImage = _ArrowUp;
            else if (direction == ArrowDirection.Right)
                pictureBox.BackgroundImage = _ArrowRight;
            else
                pictureBox.BackgroundImage = _ArrowDown;
        }

        private Orientation getOrientation()
        {
            if (InitialArrowDirection == ArrowDirection.Left || InitialArrowDirection == ArrowDirection.Right)
                return Orientation.Horizontal;
            else
                return Orientation.Vertical;
            //Point togglePanelLoc = Util.getLocationRelativeToForm(_togglePanel);
            //Point toggleButtonLoc = Util.getLocationRelativeToForm(this);

            //if (toggleButtonLoc.Y >= togglePanelLoc.Y 
            //        && toggleButtonLoc.Y <= togglePanelLoc.Y + _togglePanel.Height * Convert.ToInt16(_togglePanel.Visible))
            //    return Orientation.Horizontal;
            //else
            //    return Orientation.Vertical;
        }

        public void PerformClick()
        {
            toggle();
        }

        public void setContainerPanelOriginalSize()
        {
            if (ContainerPanel.GetType() == typeof(SplitterPanel))
            {
                SplitContainer parent = (SplitContainer)ContainerPanel.Parent;
                ContainerPanelOriginalSize = new Size(parent.SplitterDistance, parent.SplitterDistance);
            }
            else
                ContainerPanelOriginalSize = ContainerPanel.Size; 
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void Form_Load(object sender, EventArgs e)
        {
            setupControls();
            populateData();
        }

        [Description("Click Event"), Category("_Custom")]
        public event EventHandler pictureBox_ClickEvent;
        private void pictureBox_Click(object sender, EventArgs e)
        {
            toggle();
            if (this.pictureBox_ClickEvent != null)
                this.pictureBox_ClickEvent(this, e);
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/
    }
}
