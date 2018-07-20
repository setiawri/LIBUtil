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

        [Description("Show Up And Down"), Category("_Custom")]
        public bool ShowUpAndDown
        {
            get { return datetimepicker.ShowUpDown; }
            set { datetimepicker.ShowUpDown = value; }
        }

        [Description("Show CheckBox"), Category("_Custom")]
        public bool ShowCheckBox
        {
            get { return datetimepicker.ShowCheckBox; }
            set { datetimepicker.ShowCheckBox = value; }
        }
        
        [Description("Default Checked Value"), Category("_Custom")]
        public bool DefaultCheckedValue
        {
            get { return _DefaultCheckedValue; }
            set { _DefaultCheckedValue = datetimepicker.Checked = value; }
        }
        private bool _DefaultCheckedValue;

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
                    if(((DateTime)value).Year < datetimepicker.MinDate.Year)
                        datetimepicker.Value = ((DateTime)value).AddYears(datetimepicker.MinDate.Year-((DateTime)value).Year);
                    else
                       datetimepicker.Value = (DateTime)value;
                }
            }
        }

        [Description("Checked"), Category("_Custom")]
        public bool Checked
        {
            get { return datetimepicker.Checked; }
            set { datetimepicker.Checked = value; }
        }

        public TimeSpan? ValueTimeSpan
        {
            get
            {
                if (!datetimepicker.Checked)
                    return null;
                else
                    return datetimepicker.Value.TimeOfDay;
            }
            set
            {
                if (value == null)
                    datetimepicker.Checked = false;
                else
                {
                    datetimepicker.Checked = true;
                    datetimepicker.Value = datetimepicker.MinDate.Add((TimeSpan)value);
                }
            }
        }

        public string ValueTimeSpanString
        {
            get
            {
                if (ValueTimeSpan == null)
                    return null;
                else
                    return ValueTimeSpan.ToString();
            }
        }

        public DateTime? ValueAsEndDateFilter
        {
            get
            {
                DateTime? dt = Value;
                if(dt != null)
                {
                    DateTime date = (DateTime)dt;
                    dt = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                }
                return dt;
            }
        }

        public DateTime? ValueAsStartDateFilter
        {
            get
            {
                DateTime? dt = Value;
                if (dt != null)
                {
                    DateTime date = (DateTime)dt;
                    dt = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                }
                return dt;
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

        public override void reset()
        {
            if (datetimepicker.ShowUpDown)
                datetimepicker.Value = datetimepicker.MinDate;
            else
                datetimepicker.Value = DateTime.Now;
            datetimepicker.Checked = DefaultCheckedValue;
        }

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

        public bool isValidEndDate(InputControl_DateTimePicker startControl)
        {
            return Value > startControl.Value;
        }

        public bool isValidEndTime(InputControl_DateTimePicker idtp_StartTime)
        {
            return ValueTimeSpan > idtp_StartTime.ValueTimeSpan;
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        [Description("Value Changed Event"), Category("_Custom")]
        public event EventHandler ValueChanged;
        private void dropdownlist_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, e);
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/
    }
}
