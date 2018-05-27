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
    public partial class InputControl_DateTimePicker : InputControl
    {
        /*******************************************************************************************************/
        #region PROPERTIES

        [Description("Label Text"), Category("_Custom")]
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        [Description("Format"), Category("_Custom")]
        public DateTimePickerFormat Format
        {
            get { return datetimepicker.Format; }
            set { datetimepicker.Format = value; }
        }

        [Description("CustomFormat"), Category("_Custom")]
        public string CustomFormat
        {
            get { return datetimepicker.CustomFormat ?? ""; }
            set { datetimepicker.CustomFormat = value.Trim(); }
        }

        [Description("Show CheckBox"), Category("_Custom")]
        public bool ShowCheckBox
        {
            get { return datetimepicker.ShowCheckBox; }
            set { datetimepicker.ShowCheckBox = value; }
        }

        public DateTime? Value
        {
            get
            {
                if (!datetimepicker.Checked)
                    return null;
                else
                    return datetimepicker.Value;
            }
            set
            {
                if (value == null)
                    datetimepicker.Checked = false;
                else
                {
                    datetimepicker.Checked = true;
                    datetimepicker.Value = (DateTime)value;
                }
            }
        }

        public bool ValueError(string message) { return Util.inputError<DateTimePicker>(datetimepicker, message); }
        
        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public InputControl_DateTimePicker()
        {
            InitializeComponent();
        }
        
        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        public override void reset() { datetimepicker.Value = DateTime.Now; datetimepicker.Checked = false; }

        public override void focus() { datetimepicker.Focus(); }

        public override void setAsDefaultControl()
        {
            this.TabIndex = 0;
            datetimepicker.TabIndex = 0;
        }


        public DateTime getFirstDayOfSelectedMonth() { return getFirstDayOfSelectedMonth((DateTime)Value); }
        public static DateTime getFirstDayOfSelectedMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, 0);
        }

        public DateTime getLastDayOfSelectedMonth() { return getLastDayOfSelectedMonth((DateTime)Value); }
        public static DateTime getLastDayOfSelectedMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 0, 0, 0, 0);
        }

        [Description("Value Changed Event"), Category("_Custom")]
        public event EventHandler ValueChanged;
        private void dropdownlist_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, e);

            datetimepicker.Checked = true;
        }

        #endregion METHODS
        /*******************************************************************************************************/
    }
}
