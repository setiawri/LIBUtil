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
    [DefaultProperty("LabelText")]
    [DefaultEvent("Item_Checked")]
    public partial class InputControl_CheckedListBox : InputControl
    {
        /*******************************************************************************************************/
        #region VARIABLES

        private List<object> _checkedItemsID = new List<object>();
        private string _displayMember;
        private string _valueMember;

        #endregion
        /*******************************************************************************************************/
        #region PROPERTIES

        [Description("Label Text"), Category("_Custom")]
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        [Description("Show list only"), Category("_Custom")]
        public bool ShowListOnly
        {
            get { return _showListOnly; }
            set
            {
                _showListOnly = value;
                chk.Visible = label.Visible = txtFilter.Visible = lnkClearFilter.Visible = !value;
            }
        }
        private bool _showListOnly;
        
        public bool noItemSelectedError(string message) { return Util.inputError<CheckedListBox>(checkedlistbox, message); }

        public bool hasItemSelected() { return checkedlistbox.CheckedItems.Count > 0; }

        public CheckedListBox CheckedListBox { get { return checkedlistbox; } }

        public TextBox FilterText { get { return txtFilter; } }

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public InputControl_CheckedListBox()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        public override void reset()
        {
            if (ShowListOnly)
                setAllItems(false);
            else
            {
                //save filter text and reapply
                string filter = txtFilter.Text;
                txtFilter.Text = string.Empty;
                setAllItems(false);
                txtFilter.Text = filter;
            }
        }

        public override void focus() { txtFilter.Focus(); }

        public override void setAsDefaultControl() 
        {
            this.TabIndex = 0;
            txtFilter.TabIndex = 0;
            checkedlistbox.TabIndex = 1;
            chk.TabIndex = 2;
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            setAllItems(chk.Checked);

            if (this.Item_Checked != null)
                this.Item_Checked(this, e);
        }

        public void populate<T>()
        {
            _displayMember = "text";
            _valueMember = "value";

            DataTable datatable = new DataTable();
            Util.addColumnToTable<string>(datatable, _displayMember, null);
            Util.addColumnToTable<int>(datatable, _valueMember, null);

            foreach (T item in Util.GetEnumItems<T>())
                datatable.Rows.Add(Util.GetEnumDescription((Enum)(object)item), Convert.ToInt16((object)item));

            populateCheckedListBox(checkedlistbox, datatable, _displayMember, _valueMember);
        }

        public void populate(object data, string displayMember, string valueMember)
        {
            populateCheckedListBox(checkedlistbox, data, displayMember, valueMember);
            _displayMember = displayMember;
            _valueMember = valueMember;
        }

        public string getDataViewRowFilter(string fieldName, Type type) { return compileRowFilterString(checkedlistbox, fieldName, type); }

        public string getDBFilterString(string fieldName, Type type) { return compileDBFilterString(checkedlistbox, fieldName, type); }

        public void setAllItems(bool isChecked)
        {
            for (int i = 0; i < checkedlistbox.Items.Count; i++)
                if(isChecked)
                    setItemCheckState(i, CheckState.Checked);
                else
                    setItemCheckState(i, CheckState.Unchecked);
        }

        public static void populateCheckedListBox(CheckedListBox clb, object data, string displayMember, string valueMember)
        {
            clb.DataSource = data;
            clb.DisplayMember = displayMember;
            clb.ValueMember = valueMember;
            clb.CheckOnClick = true;
            clb.ClearSelected();
        }

        public DataTable getCheckedItemsInArrayTable(string columnName)
        {
            //remove filter to get all checked items
            int selectedIdx = checkedlistbox.SelectedIndex;

            string filter = txtFilter.Text;
            txtFilter.Text = "";
            DataTable datatable = copySelectionToArrayTable(checkedlistbox, columnName);
            txtFilter.Text = filter;

            checkedlistbox.SelectedIndex = selectedIdx;

            return datatable;
        }

        public static DataTable copySelectionToArrayTable(CheckedListBox clb, string columnName)
        {
            DataTable datatable = Util.createArrayTable();
            foreach (object item in clb.CheckedItems)
                datatable.Rows.Add(new string[] { ((DataRowView)item)[columnName].ToString(), null });
            return datatable;
        }

        public DataTable copySelectionToDataTable(string textColumnName, string valueColumnName)
        {
            DataTable datatable = new DataTable();
            Util.addColumnToTable<object>(datatable, textColumnName, "");
            Util.addColumnToTable<object>(datatable, valueColumnName, 0);

            foreach (object item in checkedlistbox.CheckedItems)
                datatable.Rows.Add(new object[] { ((DataRowView)item)[textColumnName], ((DataRowView)item)[valueColumnName] });
            return datatable;
        }

        public static string compileRowFilterString(CheckedListBox clb, string fieldName, Type type)
        {
            if (clb.CheckedItems.Count == 0)
                return "";
            else
                return string.Format("{0} IN ({1})", fieldName, compileDBFilterString(clb, fieldName, type));
        }

        public static string compileDBFilterString(CheckedListBox clb, string fieldName, Type type)
        {
            string filter = "";
            foreach (object item in clb.CheckedItems)
            {
                if (type == typeof(Guid))
                    filter = Util.append(filter, string.Format("Convert('{0}', 'System.Guid')", ((DataRowView)item)[0].ToString()), ",");
                else if (type == typeof(string))
                    filter = Util.append(filter, string.Format("'{0}'", ((DataRowView)item)[0].ToString()), ",");
            }
            return filter;
        }
        
        public static List<T> copySelectionToList<T>(CheckedListBox clb, string columnName)
        {
            List<T> list = new List<T>();
            foreach (object obj in clb.CheckedItems)
            {
                list.Add(Util.wrapNullable<T>(((DataRowView)obj)[columnName]));
            }

            return list;
        }

        private void setItemCheckState(int index, CheckState checkstate)
        {
            checkedlistbox.ItemCheck -= checkedlistbox_ItemCheck;
            checkedlistbox.SetItemCheckState(index, checkstate);
            checkedlistbox.ItemCheck += checkedlistbox_ItemCheck;

            //sync change to list
            object id = getItemGuid(index);
            if (checkstate == CheckState.Unchecked)
                _checkedItemsID.Remove(getItemGuid(index));
            else
                if (!_checkedItemsID.Contains(id))
                    _checkedItemsID.Add(id);
        }

        private object getItemGuid(int index)
        {
            return ((DataRowView)checkedlistbox.Items[index])[_valueMember];
        }

        public static void clearCheckedItems(CheckedListBox clb)
        {
            for (int i = 0; i < clb.Items.Count; i++)
                clb.SetItemCheckState(i, CheckState.Unchecked);
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void txtFilter_TextChanged(object sender, EventArgs e)
       {
            Util.sanitize(txtFilter);
            DataView dvw = (DataView)checkedlistbox.DataSource;
            if(dvw != null)
            {
                //apply filter
                dvw.RowFilter = string.Format("{0} LIKE '%{1}%'", _displayMember, txtFilter.Text);
                checkedlistbox.DataSource = dvw;

                //reapply checked items
                for (int i = 0; i < checkedlistbox.Items.Count; i++)
                    if (_checkedItemsID.Contains(getItemGuid(i)))
                        setItemCheckState(i, CheckState.Checked);
            }
        }

        private void lnkClearFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtFilter.Text = "";
        }

        [Description("Item Checked Event"), Category("_Custom")]
        public event EventHandler Item_Checked;
        private void checkedlistbox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //uncheck global checkbox
            if(chk.Checked)
            {
                chk.CheckedChanged -= chk_CheckedChanged;
                chk.Checked = false;
                chk.CheckedChanged += chk_CheckedChanged;
            }

            //apply change to checkedlistbox
            setItemCheckState(e.Index, e.NewValue);

            if (this.Item_Checked != null)
                this.Item_Checked(this, e);
        }

        #endregion
        /*******************************************************************************************************/
    }
}
