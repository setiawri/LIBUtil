using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LIBUtil.Desktop.UserControls
{
    public struct FilterValues_InputControl_DateTimePicker
    {
        public DateTime? Value;
        public TimeSpan? ValueTimespan;
        public bool ShowCheckBox;
        public bool Checked;

        public FilterValues_InputControl_DateTimePicker(DateTime? value, TimeSpan? valueTimespan, bool showCheckBox, bool isChecked)
        {
            Value = value;
            ValueTimespan = valueTimespan;
            ShowCheckBox = showCheckBox;
            Checked = isChecked;
        }
    }

    public partial class InputControl_DateTimePicker : InputControl
    {
        public DateTime ResetValue;

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
                if (ShowCheckBox && !datetimepicker.Checked)
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
                        datetimepicker.Value = ResetValue;
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
                if (ShowCheckBox && !datetimepicker.Checked)
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
                    datetimepicker.Value = datetimepicker.Value.Date.Add((TimeSpan)value);
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
                return Util.getAsEndDate(Value);
            }
        }

        public DateTime? ValueAsStartDateFilter
        {
            get
            {
                return Util.getAsStartDate(Value);
            }
        }

        public bool ValueError(string message) { return Util.inputError<DateTimePicker>(datetimepicker, message); }
        
        public FilterValues_InputControl_DateTimePicker FilterValues
        {
            get { return new FilterValues_InputControl_DateTimePicker(Value, ValueTimeSpan, ShowCheckBox, Checked); }
            set
            {
                Value = value.Value;
                ValueTimeSpan = value.ValueTimespan;
                ShowCheckBox = value.ShowCheckBox;
                Checked = value.Checked;
            }
        }

        [Description("Show DateTimePicker only"), Category("_Custom")]
        public bool ShowDateTimePickerOnly
        {
            get { return _ShowDateTimePickerOnly; }
            set
            {
                _ShowDateTimePickerOnly = value;
                label.Visible = !value;
                if (value)
                    this.Height = datetimepicker.Height;
                else
                    this.Height = datetimepicker.Height + label.Height;
            }
        }
        private bool _ShowDateTimePickerOnly = false;

        public DateTimePicker getDatetimePicker { get { return datetimepicker; } }

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public InputControl_DateTimePicker()
        {
            InitializeComponent();

            if (datetimepicker.ShowUpDown)
                ResetValue = datetimepicker.MinDate;
            else
                ResetValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        public override void reset()
        {
            datetimepicker.Value = ResetValue;
            datetimepicker.Checked = DefaultCheckedValue;
        }

        public override void focus() { datetimepicker.Focus(); }

        public override void setAsDefaultControl()
        {
            this.TabIndex = 0;
            datetimepicker.TabIndex = 0;
        }


        public DateTime? getFirstDayOfSelectedMonth() { return Value == null ? Value : getFirstDayOfSelectedMonth((DateTime)Value); }
        public static DateTime getFirstDayOfSelectedMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, 0);
        }

        public DateTime? getLastDayOfSelectedMonth() { return Value == null ? Value : getLastDayOfSelectedMonth((DateTime)Value); }
        public static DateTime getLastDayOfSelectedMonth(DateTime dt)
        {
            return (DateTime)Util.getAsEndDate(new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 0, 0, 0, 0));
        }

        public bool isValidEndDate(InputControl_DateTimePicker idtp_StartTime)
        {
            if (datetimepicker.Checked && idtp_StartTime.Checked)
                return Value > idtp_StartTime.Value;
            else
                return true;
        }

        public bool isValidEndTime(InputControl_DateTimePicker idtp_StartTime)
        {
            if (datetimepicker.Checked && idtp_StartTime.Checked)
                return ValueTimeSpan > idtp_StartTime.ValueTimeSpan;
            else
                return true;
        }

        public DateTime getLastDateOfLastMonth()
        {
            return DateTime.Now.AddMonths(-1).AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day).Date;
        }

        public DateTime getFirstDateOfLastMonth()
        {
            return DateTime.Now.AddMonths(-1).AddDays(-1 *(DateTime.Now.Day - 1)).Date;
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
