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
    [DefaultEvent("UpdateLink_Click")]
    public partial class InputControl_Dropdownlist : InputControl
    {
        /*******************************************************************************************************/
        #region SETTINGS

        public const int LABEL_DEFAULTLEFTPADDING = 22;

        #endregion
        /*******************************************************************************************************/
        #region PROPERTIES

        [Description("Label Text"), Category("_Custom")]
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; setLabelPadding(); }
        }

        [Description("Show dropdownlist only"), Category("_Custom")]
        public bool ShowDropdownlistOnly
        {
            get { return _showDropdownlistOnly; }
            set
            {
                _showDropdownlistOnly = value;
                label.Visible = !value;
                if (value)
                {
                    //HideUpdateLink = true;
                    //HideFilter = true;
                    this.Height = dropdownlist.Height;
                }
                else
                   this.Height = dropdownlist.Height + label.Height;
            }
        }
        private bool _showDropdownlistOnly = false;

        [Description("Hide update link"), Category("_Custom")]
        public bool HideUpdateLink
        {
            get { return _hideUpdateLink; }
            set {
                _hideUpdateLink = value;
                pbUpdate.Visible = !value;
                setLabelPadding();
            }
        }
        private bool _hideUpdateLink = false;
        
        [Description("Hide filter"), Category("_Custom")]
        public bool HideFilter
        {
            get { return _hideFilter; }
            set
            {
                _hideFilter = value;
                txtFilter.Visible = !value;
                pnlFilter.Visible = !value;
            }
        }
        private bool _hideFilter = false;

        [Description("Disable Text Input"), Category("_Custom")]
        public bool DisableTextInput
        {
            get { return dropdownlist.combobox.DropDownStyle == ComboBoxStyle.DropDownList; }
            set
            {
                if (value)
                    dropdownlist.combobox.DropDownStyle = ComboBoxStyle.DropDownList;
                else
                    dropdownlist.combobox.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        public object SelectedValue { get { return dropdownlist.SelectedValue; } set { dropdownlist.SelectedValue = value; } }
        public object SelectedItem { get { return dropdownlist.SelectedItem; } set { dropdownlist.SelectedItem = value; } }
        public string SelectedItemText { get { return dropdownlist.SelectedItemText; } set { dropdownlist.SelectedItemText = value; } }
        public int SelectedIndex { get { return dropdownlist.SelectedIndex; } set { dropdownlist.SelectedIndex = value; } }

        public bool SelectedValueError(string message) { return dropdownlist.SelectedValueError(message); }

        public bool isValidSelectedValue() { return dropdownlist.isValidSelectedValue(); }

        public Dropdownlist Dropdownlist { get { return dropdownlist; } }

        public PictureBox UpdateLink { get { return pbUpdate; } }

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public InputControl_Dropdownlist()
        {
            InitializeComponent();
        }
        
        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        public void clearItems() { dropdownlist.populate(null, null, null, null); }

        public override void reset() { dropdownlist.clearSelection(); txtFilter.Text = ""; }

        public override void focus() { dropdownlist.Focus(); }

        public override void setAsDefaultControl() { dropdownlist.setAsDefaultControl(); }

        public void populate<T>()
        {
            dropdownlist.populate<T>();
            reset();
        }

        public void populate(object data, string displayMember, string valueMember, string defaultColumnName)
        {
            dropdownlist.populate(data, displayMember, valueMember, defaultColumnName);
        }

        public bool hasSelectedValue()
        {
            return dropdownlist.hasSelectedValue();
        }

        public bool hasItems()
        {
            return dropdownlist.hasItems();
        }

        public T getSelectedValue<T>(string columnName)
        {
            return dropdownlist.getSelectedValue<T>(columnName);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            Util.sanitize(txtFilter);
            DataView dvw = (DataView)dropdownlist.DataSource;
            if (dvw != null)
                dropdownlist.Filter = string.Format("{0} LIKE '%{1}%'", dropdownlist.DisplayMember, txtFilter.Text);
        }

        public void populateWithTime(int startHour, int startMinute, int endHour, int endMinute, int intervalMinutes, string columnName, string format)
        {
            TimeSpan startTime = new TimeSpan(startHour, startMinute, 0);
            TimeSpan endTime = new TimeSpan(endHour, endMinute, 0);

            if (intervalMinutes <= 0)
                Util.displayMessageBoxError("Interval Minutes cannot be zero or less");
            else if (endTime < startTime)
                Util.displayMessageBoxError("End Hour cannot be before than the start hour");

            DataTable datatable = new DataTable();
            Util.addColumnToTable<string>(datatable, "text", null);
            Util.addColumnToTable<string>(datatable, columnName, null);
            
            for (TimeSpan i = startTime; i <= endTime; i=i.Add(new TimeSpan(0, intervalMinutes, 0)))
                datatable.Rows.Add(string.Format(format, i), string.Format(format, i));

            dropdownlist.populate(datatable, "text", columnName, null);
        }

        public void populateWithEnum<T>()
        {
            populate(null, null, null, null);

            var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(value => new
                {
                    Description = (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description ?? value.ToString(),
                    Value = value
                })
                .OrderBy(item => item.Value.ToString())
                .ToList();

            dropdownlist.combobox.DataSource = list;
            dropdownlist.combobox.DisplayMember = "Description";
            dropdownlist.combobox.ValueMember = "Value";

            reset();
        }

        public void setLabelPadding()
        {
            if (pbUpdate.Visible)
                label.Padding = new Padding(LABEL_DEFAULTLEFTPADDING, 0, 0, 0);
            else
                label.Padding = new Padding(0);
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        [Description("Selected Item Changed Event"), Category("_Custom")]
        public event EventHandler SelectedIndexChanged;
        private void dropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(this, e);
        }

        [Description("Button Click Event"), Category("_Custom")]
        public event EventHandler UpdateLink_Click;
        private void pbUpdate_Click(object sender, EventArgs e)
        {
            if (this.UpdateLink_Click != null)
                this.UpdateLink_Click(this, e);
        }

        private void InputControl_Dropdownlist_Load(object sender, EventArgs e)
        {
            setLabelPadding();
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
        }

        #endregion
        /*******************************************************************************************************/
    }
}
