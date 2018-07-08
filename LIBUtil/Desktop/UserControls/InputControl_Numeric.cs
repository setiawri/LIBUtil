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
    [DefaultEvent("isBrowseMode_Clicked")]
    public partial class InputControl_Numeric : InputControl
    {
        /*******************************************************************************************************/
        #region SETTINGS
            
        #endregion SETTINGS
        /*******************************************************************************************************/
        #region PROPERTIES

        [Description("Show textbox only"), Category("_Custom")]
        public bool ShowTextboxOnly
        {
            get { return _showTextboxOnly; }
            set
            {
                _showTextboxOnly = value;
                label.Visible = !value;
                if (value)
                    this.Height = numericUpDown.Height;
                else
                    this.Height = numericUpDown.Height + label.Height;
            }
        }
        private bool _showTextboxOnly;

        [Description("Label Text"), Category("_Custom")]
        public string LabelText { get { return label.Text; } set { label.Text = value; } }
        
        [Description("Decimal Places"), Category("_Custom")]
        public int DecimalPlaces
        {
            get { return numericUpDown.DecimalPlaces; }
            set { numericUpDown.DecimalPlaces = value; }
        }

        [Description("Minimum Value"), Category("_Custom")]
        public decimal MinimumValue
        {
            get { return numericUpDown.Minimum; }
            set { numericUpDown.Minimum = value; }
        }

        [Description("Maximum Value"), Category("_Custom")]
        public decimal MaximumValue
        {
            get { return numericUpDown.Maximum; }
            set { numericUpDown.Maximum = value; }
        }

        [Description("Increment"), Category("_Custom")]
        public decimal Increment
        {
            get { return numericUpDown.Increment; }
            set { numericUpDown.Increment = value; }
        }

        public decimal Value { get { return numericUpDown.Value; } set { numericUpDown.Value = value; } }
        public decimal ValueDecimal { get { return numericUpDown.Value; } }
        public int ValueInt { get { return (int)numericUpDown.Value; } }
        public long ValueLong { get { return (long)numericUpDown.Value; } }

        private ToolTip _textboxTooltip = new ToolTip();

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS
        
        public InputControl_Numeric()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS
            
        public override void reset()
        {
            numericUpDown.Value = 0;
        }

        public override void focus()
        {
            numericUpDown.Focus();
        }

        public override void setAsDefaultControl()
        {
            this.TabIndex = 0;
            numericUpDown.TabIndex = 0;
        }
        
        public bool isValueError(string message) { return Util.inputError<NumericUpDown>(numericUpDown, message); }
        
        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS
            
        [Description("TextChanged Event"), Category("_Custom")]
        public event EventHandler ValueChanged;
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                this.ValueChanged(this, e);
        }
        
        [Description("KeyDown Event"), Category("_Custom")]
        public event KeyEventHandler onKeyDown;
        private void numericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (onKeyDown != null)
                this.onKeyDown(this, e);
        }

        #endregion
        /*******************************************************************************************************/
    }
}
