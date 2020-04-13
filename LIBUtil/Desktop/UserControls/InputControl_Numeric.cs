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
    public struct FilterValues_InputControl_Numeric
    {
        public decimal Value;
        public bool ShowCheckBox;
        public bool Checked;

        public FilterValues_InputControl_Numeric(decimal value, bool showCheckBox, bool isChecked)
        {
            Value = value;
            ShowCheckBox = showCheckBox;
            Checked = isChecked;
        }
    }

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
                {
                    chkAllowDecimal.Visible = ShowAllowDecimalCheckbox;
                    this.Height = numericUpDown.Height + 3;
                }
                else
                {
                    chkAllowDecimal.Visible = false;
                    this.Height = numericUpDown.Height + label.Height;
                }
            }
        }
        private bool _showTextboxOnly;

        [Description("Hide Up Down"), Category("_Custom")]
        public bool HideUpDown
        {
            get { return lblHideUpDownArrow.Visible; }
            set { lblHideUpDownArrow.Visible = value; }
        }

        [Description("Show Checkbox"), Category("_Custom")]
        public bool ShowCheckbox
        {
            get { return checkbox.Visible; }
            set { checkbox.Visible = value; numericUpDown.Enabled = !checkbox.Visible; }
        }

        [Description("Show Allow Decimal Checkbox"), Category("_Custom")]
        public bool ShowAllowDecimalCheckbox
        {
            get { return chkAllowDecimal.Visible; }
            set { chkAllowDecimal.Visible = value; }
        }

        [Description("Label Text"), Category("_Custom")]
        public string LabelText { get { return label.Text; } set { label.Text = value; } }
        
        [Description("Decimal Places"), Category("_Custom")]
        public int DecimalPlaces
        {
            get { return numericUpDown.DecimalPlaces; }
            set { numericUpDown.DecimalPlaces = value; }
        }

        [Description("Value"), Category("_Custom")]
        public decimal? Value
        {
            get {
                if (ShowCheckbox && !checkbox.Checked)
                    return null;
                else
                    return numericUpDown.Value;
            }
            set {
                if (ShowCheckbox)
                    checkbox.Checked = numericUpDown.Enabled = (value != null);

                if (value < numericUpDown.Minimum)
                    Util.displayMessageBoxError("Cannot be less than " + numericUpDown.Minimum);
                else if (value > numericUpDown.Maximum)
                    Util.displayMessageBoxError("Cannot be more than " + numericUpDown.Maximum);
                else
                {
                    numericUpDown.Value = Util.zeroNonNumericString(value);
                    numericUpDown.Tag = value;
                }
            }
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

        public decimal ValueDecimal { get { return numericUpDown.Value; } }
        public int ValueInt { get { return (int)numericUpDown.Value; } }
        public long ValueLong { get { return (long)numericUpDown.Value; } }
        public bool Checked { get { return checkbox.Checked; } set { checkbox.Checked = value; } }
        public decimal PreviousValue { get { return (numericUpDown.Tag == null) ? 0 : (decimal)numericUpDown.Tag; } }

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
            if (!numericUpDown.Enabled)
                checkbox.Checked = false;
            else if (ShowCheckbox)
                checkbox.Checked = false;
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

        public FilterValues_InputControl_Numeric FilterValues
        {
            get { return new FilterValues_InputControl_Numeric(Util.zeroNonNumericString(Value), ShowCheckbox, checkbox.Checked); }
            set
            {
                Value = value.Value;
                ShowCheckbox = value.ShowCheckBox;
                checkbox.Checked = value.Checked;
            }
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        [Description("TextChanged Event"), Category("_Custom")]
        public event EventHandler ValueChanged;
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                this.ValueChanged(this, e);

            numericUpDown.Tag = numericUpDown.Value; //saves value to tag for PreviousValue property
        }
        
        [Description("KeyDown Event"), Category("_Custom")]
        public event KeyEventHandler onKeyDown;
        private void numericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (onKeyDown != null)
                this.onKeyDown(this, e);
        }

        private void numericUpDown_Enter(object sender, EventArgs e)
        {
            numericUpDown.Select(0, numericUpDown.Text.Length);
        }

        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown.Enabled = checkbox.Checked;
        }

        private void chkShowDecimal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllowDecimal.Checked)
                numericUpDown.DecimalPlaces = 2;
            else
                numericUpDown.DecimalPlaces = 0;
        }

        #endregion
        /*******************************************************************************************************/
    }
}
