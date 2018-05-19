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
    [DefaultEvent("SelectedIndexChanged")]
    public partial class Dropdownlist : UserControl
    {
        /*******************************************************************************************************/
        #region VARIABLES

        private string _defaultColumnName = "";

        #endregion
        /*******************************************************************************************************/
        #region PROPERTIES

        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                populate(combobox.DataSource, combobox.DisplayMember, combobox.ValueMember, _defaultColumnName);
            }
        }
        private string _filter = "";

        public string DisplayMember { get { return combobox.DisplayMember; } set { combobox.DisplayMember = value; } }
        public string ValueMember { get { return combobox.ValueMember; } set { combobox.ValueMember = value; } }

        public object DataSource { get { return combobox.DataSource; } }

        public object SelectedValue { get { return combobox.SelectedValue; } set { if (value == null) clearSelection(); else combobox.SelectedValue = value; } }
        public object SelectedItem { get { return combobox.SelectedItem; } set { if (value == null) clearSelection(); else combobox.SelectedItem = value; } }
        public string SelectedItemText { get { return combobox.GetItemText(combobox.SelectedItem); } }
        
        public bool SelectedValueError(string message) { return Util.inputError<ComboBox>(combobox, message); }

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public Dropdownlist()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS


        public bool hasSelectedValue()
        {
            return SelectedValue != null;
        }

        public bool hasItems()
        {
            return Util.getDataView(combobox.DataSource).Count > 0;
        }

        public bool isValidSelectedValue()
        {
            if (SelectedValue == null && !string.IsNullOrEmpty(combobox.Text))
                return Util.inputError<ComboBox>(combobox, "Please select an item from the list or delete text from the dropdownlist");
            return true;
        }

        public void clearSelection()
        {
            combobox.SelectAll();
            combobox.Text = "";
            combobox.SelectedIndex = -1;
        }

        public void focus()
        {
            combobox.Focus();
        }

        public void setAsDefaultControl()
        {
            if (this.Parent != null) this.Parent.TabIndex = 0;
            this.TabIndex = 0;
            combobox.TabIndex = 0;
        }

        public void populate(object data, string displayMember, string valueMember, string defaultColumnName)
        {
            _defaultColumnName = defaultColumnName;
            
            combobox.AutoCompleteMode = AutoCompleteMode.Append;
            combobox.AutoCompleteSource = AutoCompleteSource.ListItems;

            combobox.TextChanged += new System.EventHandler(cb_TextChanged);

            if(string.IsNullOrEmpty(_filter))
                combobox.DataSource = data;
            else
            {
                DataView dvw = Util.getDataView(data);
                dvw.RowFilter = _filter;
            }

            combobox.DisplayMember = displayMember;
            combobox.ValueMember = valueMember;

            if (data != null && !string.IsNullOrEmpty(_defaultColumnName) && (
                (data.GetType() == typeof(DataTable) && ((DataTable)data).Columns.Contains(_defaultColumnName))
                || (data.GetType() == typeof(DataView) && ((DataView)data).Table.Columns.Contains(_defaultColumnName))))
                selectDefaultItem(combobox, _defaultColumnName);
            else
                clearSelection();
        }

        private static void cb_TextChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (string.IsNullOrEmpty(cb.Text))
                cb.SelectedIndex = -1;
        }

        public static void selectDefaultItem(System.Windows.Forms.ComboBox cb, string defaultColumnName)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                cb.SelectedIndex = i;
                if (Convert.ToBoolean(((System.Data.DataRowView)cb.SelectedItem)[defaultColumnName]) == true)
                    break;
            }
        }

        public T getSelectedValue<T>(string columnName)
        {
            return Util.wrapNullable<T>(((DataRowView)combobox.SelectedItem)[columnName]);
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        [Description("Selected Index Changed Event"), Category("_Custom")]
        public event EventHandler SelectedIndexChanged;
        private void dropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(this, e);
        }

        #endregion
        /*******************************************************************************************************/
    }
}
