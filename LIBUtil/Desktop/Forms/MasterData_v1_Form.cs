/***************************************************************************************************
 * 
 * To adjust the height of the input panel, select scMain and change the splitter distance
 * 
 * *************************************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LIBUtil.Desktop.UserControls;

namespace LIBUtil.Desktop.Forms
{
    public partial class MasterData_v1_Form : Form
    {
        /*******************************************************************************************************/
        #region SETTINGS

        private const string BUTTONTEXT_SUBMIT_SEARCH = "SEARCH";
        private const string BUTTONTEXT_SUBMIT_ADD = "SUBMIT";
        private const string BUTTONTEXT_SUBMIT_UPDATE = "UPDATE";
        private Color BUTTONCOLOR_DEFAULT = Color.Black;
        private Color BUTTONCOLOR_ACTIVE = Color.DarkOrange;
        private const int QUICKSEARCH_MAXLENGTH = 10;

        #endregion SETTINGS
        /*******************************************************************************************************/
        #region PUBLIC VARIABLES

        public FormModes Mode
        {
            get { return _currentMode; }
            set
            {
                _currentMode = value;
                changeFormMode();
            }
        }
        
        public Guid BrowsedItemSelectionId;
        public string BrowsedItemSelectionDescription;

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region PROTECTED VARIABLES

        protected List<Control> InputToClear = new List<Control>();
        protected List<string> FieldnamesForQuickSearch = new List<string>();
        protected List<InputControl> InputToDisableOnSearch = new List<InputControl>();
        protected bool DoNotClearInputAfterSubmission = false;

        #endregion PROTECTED VARIABLES
        /*******************************************************************************************************/
        #region PRIVATE VARIABLES

        private FormModes _startingMode;
        private FormModes _currentMode;
        private bool _showDataOnLoad = false;

        #endregion PRIVATE VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public MasterData_v1_Form() : this(FormModes.Add, true) { }

        public MasterData_v1_Form(FormModes startingMode, bool showDataOnLoad)
        {
            InitializeComponent();

            _startingMode = startingMode;
            Mode = _startingMode;
            _showDataOnLoad = showDataOnLoad;
            this.ShowIcon = false;
        }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region VIRTUAL METHODS

        protected virtual void setupFields() { }
        protected virtual void setupControlsBasedOnRoles() { }
        protected virtual DataView loadGridviewDataSource() { return null; }
        protected virtual void additionalSettings() { }
        protected virtual bool isValidToPopulateGridViewDataSource() { return true; }
        protected virtual void populateInputFields() { }
        protected virtual Boolean isInputFieldsValid() { return false; }
        protected virtual void update() { }
        protected virtual void add() { }
        protected virtual void updateActiveStatus(Guid id, Boolean activeStatus) { }
        protected virtual void updateDefaultRow(Guid id) { }
        protected virtual void changeStatus_Click(object sender, EventArgs args) { populateGridViewDataSource(true); }
        protected virtual void updateCheckbox1Column(Guid id, bool newValue) { }
        protected virtual void clearInputFields() { }
        protected virtual void btnLog_Click(object sender, EventArgs e) { }
        protected virtual string getSelectedItemDescription(int rowIndex) { return string.Empty; }
        protected virtual void virtual_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        protected virtual void dgv_CellDoubleClick() { }

        #endregion
        /*******************************************************************************************************/
        #region PROTECTED METHODS

        protected void populateInputFieldsForUpdate()
        {
            if (dgv.SelectedRows.Count > 0)
                populateInputFields();
        }

        protected Guid selectedRowID()
        {
            return Util.wrapFirstSelectedRowValueNullable<Guid>(col_dgv_Id);
        }

        protected void disableFieldActive()
        {
            col_dgv_Active.Visible = false;
            chkIncludeInactive.Visible = false;
        }

        protected void enableFieldStatus<T>()
        {
            col_dgv_StatusName.Visible = true;
            addStatusContextMenu<T>(col_dgv_StatusName);
        }

        protected void setColumnsDataPropertyNames(string col_Id, string col_Active, string col_StatusName, string col_StatusId, string col_Default, string col_Checkbox1)
        {
            col_dgv_Id.DataPropertyName = col_Id;
            col_dgv_Active.DataPropertyName = col_Active;
            col_dgv_StatusName.DataPropertyName = col_StatusName;
            col_dgv_StatusId.DataPropertyName = col_StatusId;
            col_dgv_Default.DataPropertyName = col_Default;
            col_dgv_Checkbox1.DataPropertyName = col_Checkbox1;
        }

        /// <summary><para></para></summary>
        protected DataGridViewColumn addColumn<T>(DataGridView dgv, string columnName, string headerText, string dataPropertyName, 
            bool isReadOnly, bool isVisible, string format, bool addToQuickSearchFields, bool activateWordwrap, int? minimumWidth, DataGridViewContentAlignment textAlign)
        {
            DataGridViewColumn column = new DataGridViewColumn();
            column.CellTemplate = (DataGridViewCell)Activator.CreateInstance(typeof(T));
            column.Name = columnName;
            column.HeaderText = headerText;
            column.DataPropertyName = dataPropertyName;
            column.ReadOnly = isReadOnly;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            column.DefaultCellStyle.Format = format;
            column.DefaultCellStyle.Alignment = textAlign;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgv.Columns.Add(column);

            if (minimumWidth != null)
                column.MinimumWidth = (int)minimumWidth;

            if (activateWordwrap) Util.setGridviewColumnWordwrap(column, DataGridViewAutoSizeColumnMode.AllCells);

            if (addToQuickSearchFields) FieldnamesForQuickSearch.Add(dataPropertyName); //add field name to quick search list

            return column;
        }

        #endregion
        /*******************************************************************************************************/
        #region PRIVATE METHODS

        private void setupControls()
        {
            //this.Text += DBUtil.appendTitleWithInfo();

            //make sure child forms show toggle panel properly
            //ptInputPanel.Location = new System.Drawing.Point(0, -1);

            dgv.AutoGenerateColumns = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            
            clearInputFields();

            setupControlsBasedOnRoles();
        }

        private void changeFormMode()
        {
            //reset button colors
            btnSearch.ForeColor = BUTTONCOLOR_DEFAULT;
            btnAdd.ForeColor = BUTTONCOLOR_DEFAULT;
            btnUpdate.ForeColor = BUTTONCOLOR_DEFAULT;

            switch (_currentMode)
            {
                case FormModes.Search:
                    btnSubmit.Text = BUTTONTEXT_SUBMIT_SEARCH;
                    btnCancel.Visible = false;
                    btnSearch.ForeColor = BUTTONCOLOR_ACTIVE;
                    updateGridviewColumnToDisableOnSearch(false);
                    break;
                case FormModes.Add:
                    btnSubmit.Text = BUTTONTEXT_SUBMIT_ADD;
                    btnCancel.Visible = true;
                    btnAdd.ForeColor = BUTTONCOLOR_ACTIVE;
                    updateGridviewColumnToDisableOnSearch(true);
                    break;
                case FormModes.Update:
                    btnSubmit.Text = BUTTONTEXT_SUBMIT_UPDATE;
                    btnCancel.Visible = true;
                    btnUpdate.ForeColor = BUTTONCOLOR_ACTIVE;
                    updateGridviewColumnToDisableOnSearch(true);
                    break;
            }

            updateModeButtonsAvailabilityForGridRow();
            txtQuickSearch.Focus();
        }

        private void updateModeButtonsAvailabilityForGridRow()
        {
            if (dgv.Rows.Count > 0 && dgv.SelectedRows.Count > 0)
            {
                btnLog.Enabled = true;
                //if (GlobalData.UserAccount.role == Roles.User)
                //    btnUpdate.Enabled = false;
                //else
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
                btnLog.Enabled = false;
            }
        }

        private void updateGridviewColumnToDisableOnSearch(bool enabled)
        {
            foreach (InputControl input in InputToDisableOnSearch)
                input.Enabled = enabled;
        }

        protected void populateGridViewDataSource(bool reloadFromDB)
        {
            if (isValidToPopulateGridViewDataSource())
            {
                DataView dvw;
                if (reloadFromDB)
                    dvw = loadGridviewDataSource();
                else
                    dvw = (DataView)dgv.DataSource;

                if(dvw != null)
                    dvw.RowFilter = Util.compileQuickSearchFilter(txtQuickSearch.Text.Trim(), FieldnamesForQuickSearch.ToArray());

                setGridviewDataSource(dvw);
            }

            if (dgv.Rows.Count == 0) btnUpdate.Enabled = false;
        }

        private void setGridviewDataSource(DataView dvw)
        {
            //detach event handlers to avoid triggering events
            dgv.CellContentClick -= new System.Windows.Forms.DataGridViewCellEventHandler(dgv_CellContentClick);
            dgv.SelectionChanged -= new System.EventHandler(dgv_SelectionChanged);

            Util.setGridviewDataSource(dgv, true, true, dvw);

            //reattach event handlers
            dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_CellContentClick);
            dgv.SelectionChanged += new System.EventHandler(dgv_SelectionChanged);
        }

        private void addStatusContextMenu<T>(DataGridViewColumn column)
        {
            column.ContextMenuStrip = new ContextMenuStrip();
            foreach (T status in Util.GetEnumItems<T>())
                column.ContextMenuStrip.Items.Add(new ToolStripMenuItem(Util.GetEnumDescription((Enum)(object)status), null, changeStatus_Click));
        }

        private void selectRow(int rowIndex)
        {
            if (_startingMode == FormModes.Browse)
            {
                BrowsedItemSelectionId = selectedRowID();
                BrowsedItemSelectionDescription = getSelectedItemDescription(rowIndex);
                dgv_CellDoubleClick();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (btnLog.Enabled)
            {
                btnLog.PerformClick();
            }
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void Form_Load(object sender, EventArgs e)
        {
            setupControls();
            setupFields();
            populateGridViewDataSource(true);
            additionalSettings();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (_currentMode != FormModes.Search)
            {
                Mode = FormModes.Search;
                clearInputFields();
                txtQuickSearch.Focus();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (_currentMode != FormModes.Add)
            {
                Mode = FormModes.Add;
                clearInputFields();
                populateGridViewDataSource(true);
                txtQuickSearch.Focus();
            }
        }

        protected virtual void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_currentMode != FormModes.Update)
            {
                Mode = FormModes.Update;
                populateInputFieldsForUpdate();
                txtQuickSearch.Focus();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case FormModes.Search:
                    break;
                case FormModes.Add:
                    if (!isInputFieldsValid())
                        return;
                    add();
                    if (!DoNotClearInputAfterSubmission) clearInputFields();
                    break;
                case FormModes.Update:
                    if (!isInputFieldsValid())
                        return;
                    update();
                    clearInputFields();
                    btnAdd.PerformClick();
                    break;
            }

            populateGridViewDataSource(true);
            if (Mode == FormModes.Update)
            {
                btnUpdate.PerformClick();
            }
            txtQuickSearch.Focus();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (Mode == FormModes.Update)
                btnUpdate.PerformClick();
            else
                clearInputFields();

            txtQuickSearch.Focus();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.PerformClick();
        }

        protected void chkIncludeInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == FormModes.Update)
                btnAdd.PerformClick();

            populateGridViewDataSource(true);
        }

        protected void txtQuickSearch_TextChanged(object sender, EventArgs e)
        {
            if (Mode == FormModes.Update)
            {
                btnAdd.PerformClick();
                txtQuickSearch.Focus();
            }

            populateGridViewDataSource(false);
        }

        protected void lnkClearQuickSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtQuickSearch.Text = "";
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            updateModeButtonsAvailabilityForGridRow();
        }

        protected void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                updateModeButtonsAvailabilityForGridRow();

            if (Util.isColumnMatch(sender, e, col_dgv_Active))
            {
                DataGridViewRow row = dgv.Rows[e.RowIndex];
                updateActiveStatus((Guid)row.Cells[col_dgv_Id.Name].Value, !(bool)((DataGridViewCheckBoxCell)row.Cells[e.ColumnIndex]).Value);
                clearInputFields();
                populateGridViewDataSource(true);
                if (Mode == FormModes.Update) btnUpdate.PerformClick();
            }
            else if (Util.isColumnMatch(sender, e, col_dgv_Default))
            {
                DataGridViewRow row = dgv.Rows[e.RowIndex];
                updateDefaultRow((Guid)row.Cells[col_dgv_Id.Name].Value);
                clearInputFields();
                populateGridViewDataSource(true);
                if (Mode == FormModes.Update) btnUpdate.PerformClick();
            }
            else if (Util.isColumnMatch(sender, e, col_dgv_Checkbox1))
            {
                DataGridViewRow row = dgv.Rows[e.RowIndex];
                updateCheckbox1Column((Guid)row.Cells[col_dgv_Id.Name].Value, !(bool)((DataGridViewCheckBoxCell)row.Cells[e.ColumnIndex]).Value);
                clearInputFields();
                populateGridViewDataSource(true);
                if (Mode == FormModes.Update) btnUpdate.PerformClick();
            }
            else
            {
                virtual_dgv_CellContentClick(sender, e);
            }
        }

        protected void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (Mode == FormModes.Update)
            {
                populateInputFieldsForUpdate();
                txtQuickSearch.Focus();
            }
        }

        private void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex != -1)
                    dgv.Rows[e.RowIndex].Selected = true;
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow(e.RowIndex);
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (_startingMode == FormModes.Browse)
            {
                scMain.Panel1Collapsed = true;
                ptInputPanel.Visible = false;
                col_dgv_Active.Visible = false;
                col_dgv_Default.Visible = false;
                col_dgv_Checkbox1.Visible = false;
            }

            txtQuickSearch.Focus();
        }

        private void dgv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                selectRow(((DataGridView)sender).CurrentRow.Index);
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/
    }
}
