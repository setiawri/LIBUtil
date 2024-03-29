﻿/***************************************************************************************************
 * 
 * To adjust the height of the input panel, select scMain and change the splitter distance
 * 
 * *************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

using LIBUtil.Desktop.UserControls;

namespace LIBUtil.Desktop.Forms
{
    public partial class MasterData_v1_Form : Form
    {
        /*******************************************************************************************************/
        #region SETTINGS

        private const string BUTTONTEXT_SUBMIT_SEARCH = "FILTER";
        private const string BUTTONTEXT_SUBMIT_ADD = "SUBMIT";
        private const string BUTTONTEXT_SUBMIT_UPDATE = "UPDATE";
        private Color BUTTONCOLOR_DEFAULT = Color.Black;
        private Color BUTTONCOLOR_ACTIVE = Color.DarkOrange;
        private const int QUICKSEARCH_MAXLENGTH = 10;
        private string masterDataFormName;

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
        protected List<InputControl> InputToDisableOnUpdate = new List<InputControl>();
        protected List<InputControl> InputToDisableOnAdd = new List<InputControl>();
        protected List<InputControl> InputToDisablePermanently = new List<InputControl>();
        protected bool DoNotClearInputAfterSubmission = false;
        protected bool isFormShown = false;
        protected bool IsBtnUpdateEnabled = true;

        #endregion PROTECTED VARIABLES
        /*******************************************************************************************************/
        #region PRIVATE VARIABLES

        private FormModes _startingMode;
        private FormModes _currentMode;
        private bool _showDataOnLoad = false;
        private bool _showProgressBarOnPopulate = false;
        private int _timerTimeoutSeconds = 0;
        Dictionary<Control, object> filterValues = new Dictionary<Control, object>();

        #endregion PRIVATE VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public MasterData_v1_Form() : this(FormModes.Add, true, false, 0) { }
        public MasterData_v1_Form(FormModes startingMode, bool showDataOnLoad) : this(startingMode, showDataOnLoad, false, 0) { }
        public MasterData_v1_Form(FormModes startingMode, bool showDataOnLoad, bool showProgressBarOnPopulate, int timerTimeoutSeconds)
        {
            InitializeComponent();

            masterDataFormName = this.Name;
            _startingMode = startingMode;
            _showDataOnLoad = showDataOnLoad;
            _showProgressBarOnPopulate = showProgressBarOnPopulate;
            _timerTimeoutSeconds = timerTimeoutSeconds;
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
        protected virtual void updateInputPanelControls() { }

        protected virtual void populateGridViewDataSource(bool reloadFromDB)
        {
            if (isValidToPopulateGridViewDataSource())
            {
                DataView dvw = null;
                if (!reloadFromDB)
                    dvw = (DataView)dgv.DataSource;

                if(reloadFromDB || dvw == null)
                    dvw = loadGridviewDataSource();

                if (dvw != null)
                    dvw.RowFilter = Util.compileQuickSearchFilter(itxt_QuickSearch.ValueText.Trim(), FieldnamesForQuickSearch.ToArray());

                setGridviewDataSource(dvw);
            }

            if (dgv.Rows.Count == 0) btnUpdate.Enabled = false;
        }

        #endregion
        /*******************************************************************************************************/
        #region PROTECTED METHODS

        protected void populateInputFieldsForUpdate()
        {
            if (dgv.SelectedRows.Count > 0)
            {
                clearInputFields();
                populateInputFields();
            }
        }

        protected Guid selectedRowID()
        {
            return Util.wrapFirstSelectedRowValueNullable<Guid>(col_dgv_Id);
        }

        //remove - added in setColumnsDataPropertyNames if col_Active is null, then disable
        //protected void disableFieldActive()
        //{
        //    col_dgv_Active.Visible = false;
        //    chkIncludeInactive.Visible = false;
        //}

        protected void enableFieldStatus<T>()
        {
            col_dgv_StatusName.Visible = true;
            addStatusContextMenu<T>(col_dgv_StatusName);
        }

        protected void setColumnsDataPropertyNames(string col_Id, string col_Active, string col_StatusName, string col_StatusId, string col_Default, string col_Checkbox1)
        {
            col_dgv_Id.DataPropertyName = col_Id;
            col_dgv_StatusName.DataPropertyName = col_StatusName;
            col_dgv_StatusId.DataPropertyName = col_StatusId;
            col_dgv_Default.DataPropertyName = col_Default;

            col_dgv_Checkbox1.DataPropertyName = col_Checkbox1;
            if (col_Checkbox1 != null)
                col_dgv_Checkbox1.Visible = true;

            if (col_Active != null)
                col_dgv_Active.DataPropertyName = col_Active;
            else
            {
                col_dgv_Active.Visible = false;
                chkIncludeInactive.Visible = false;
            }

            if (col_Default != null)
            {
                col_dgv_Default.DataPropertyName = col_Default;
                col_dgv_Default.Visible = true;
            }
        }

        /// <summary><para></para></summary>
        protected DataGridViewColumn addColumn<T>(DataGridView dgv, string columnName, string headerText, string dataPropertyName,
            bool isReadOnly, bool isVisible, string format, bool addToQuickSearchFields, bool activateWordwrap, int? minimumWidth, DataGridViewContentAlignment textAlign)
        {
            return addColumn<T>(dgv, columnName, headerText, dataPropertyName, isReadOnly, isVisible, format, addToQuickSearchFields, activateWordwrap, minimumWidth, null, textAlign);
        }
        protected DataGridViewColumn addColumn<T>(DataGridView dgv, string columnName, string headerText, string dataPropertyName, 
            bool isReadOnly, bool isVisible, string format, bool addToQuickSearchFields, bool activateWordwrap, int? minimumWidth, int? maxWidth, DataGridViewContentAlignment textAlign)
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
            column.Visible = isVisible;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgv.Columns.Add(column);

            if (minimumWidth != null)
                column.MinimumWidth = (int)minimumWidth;

            if (maxWidth != null)
            {
                column.Width = (int)maxWidth;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            if (activateWordwrap) Util.setGridviewColumnWordwrap(column, null);

            if (addToQuickSearchFields) FieldnamesForQuickSearch.Add(dataPropertyName); //add field name to quick search list

            return column;
        }

        #endregion
        /*******************************************************************************************************/
        #region PRIVATE METHODS

        private void setupControls()
        {
            //make sure child forms show toggle panel properly
            //ptInputPanel.Location = new System.Drawing.Point(0, -1);

            dgv.AutoGenerateColumns = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            
            clearInputFields();
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
                    updateInputControls(InputToDisableOnAdd, true, false);
                    updateInputControls(InputToDisableOnUpdate, true, false);
                    updateInputControls(InputToDisableOnSearch, false, true);
                    break;
                case FormModes.Add:
                    btnSubmit.Text = BUTTONTEXT_SUBMIT_ADD;
                    btnCancel.Visible = true;
                    btnAdd.ForeColor = BUTTONCOLOR_ACTIVE;
                    updateInputControls(InputToDisableOnSearch, true, false);
                    updateInputControls(InputToDisableOnUpdate, true, false);
                    updateInputControls(InputToDisableOnAdd, false, false);
                    break;
                case FormModes.Update:
                    btnSubmit.Text = BUTTONTEXT_SUBMIT_UPDATE;
                    btnCancel.Visible = true;
                    btnUpdate.ForeColor = BUTTONCOLOR_ACTIVE;
                    updateInputControls(InputToDisableOnAdd, true, false);
                    updateInputControls(InputToDisableOnSearch, true, false);
                    updateInputControls(InputToDisableOnUpdate, false, false);
                    break;
                case FormModes.Browse:
                    scMain.Panel1Collapsed = true;
                    ptInputPanel.Visible = false;
                    col_dgv_Active.Visible = false;
                    col_dgv_Default.Visible = false;
                    col_dgv_Checkbox1.Visible = false;
                    break;
            }

            if(isChildFormInitialized()) updateInputPanelControls();
            updateModeButtonsAvailabilityForGridRow();
            itxt_QuickSearch.Focus();
        }

        private bool isChildFormInitialized()
        {
            return this.Name != masterDataFormName;
        }

        private void updateModeButtonsAvailabilityForGridRow()
        {
            if (dgv.Rows.Count > 0 && dgv.SelectedRows.Count > 0)
                btnUpdate.Enabled = IsBtnUpdateEnabled;
            else
                btnUpdate.Enabled = false;
        }

        private void updateInputControls(List<InputControl> inputToDisable, bool enabled, bool reset)
        {
            foreach (InputControl input in inputToDisable)
            {
                if(inputToDisable == InputToDisablePermanently || !InputToDisablePermanently.Contains(input))
                    input.Enabled = enabled;

                if (reset)
                    input.reset();
            }
        }

        private void setGridviewDataSource(DataView dvw)
        {
            //detach event handlers to avoid triggering events
            dgv.CellContentClick -= new DataGridViewCellEventHandler(dgv_CellContentClick);
            dgv.SelectionChanged -= new EventHandler(dgv_SelectionChanged);

            Util.setGridviewDataSource(dgv, dvw, _showProgressBarOnPopulate, _timerTimeoutSeconds, true, true);

            //reattach event handlers
            dgv.CellContentClick += new DataGridViewCellEventHandler(dgv_CellContentClick);
            dgv.SelectionChanged += new EventHandler(dgv_SelectionChanged);
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

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public void saveFilterValues()
        {
            processFilterValues(scInputContainer, true);
        }

        private void reapplyFilterValues()
        {
            if(filterValues.Count > 0)
                processFilterValues(scInputContainer, false);
        }

        private void processFilterValues(Control panel, bool isSave)
        {
            if (panel.GetType() == typeof(SplitContainer))
            {
                processFilterValues(((SplitContainer)panel).Panel1, isSave);
                processFilterValues(((SplitContainer)panel).Panel2, isSave);
            }
            else if (panel.GetType() == typeof(Panel))
                processFilterValues(panel, isSave);
            else
            {
                foreach (Control control in panel.Controls)
                {
                    if (control.GetType() == typeof(SplitContainer))
                    {
                        processFilterValues(((SplitContainer)control).Panel1, isSave);
                        processFilterValues(((SplitContainer)control).Panel2, isSave);
                    }
                    else if (control.GetType() == typeof(Panel))
                        processFilterValues(control, isSave);
                    else if (!isSave && !filterValues.ContainsKey(control))
                        continue; //do nothing and iterate to the next control
                    else if (control.GetType() == typeof(InputControl_Textbox))
                    {
                        if (isSave && !filterValues.ContainsKey(control))
                            filterValues.Add(control, ((InputControl_Textbox)control).FilterValues);
                        else
                        {
                            if (isSave)
                                filterValues[control] = ((InputControl_Textbox)control).FilterValues;
                            else
                                ((InputControl_Textbox)control).FilterValues = (FilterValues_InputControl_Textbox)filterValues[control];
                        }
                    }
                    else if (control.GetType() == typeof(InputControl_Numeric))
                    {
                        if (isSave && !filterValues.ContainsKey(control))
                            filterValues.Add(control, ((InputControl_Numeric)control).FilterValues);
                        else
                        {
                            if (isSave)
                                filterValues[control] = ((InputControl_Numeric)control).FilterValues;
                            else
                                ((InputControl_Numeric)control).FilterValues = (FilterValues_InputControl_Numeric)filterValues[control];
                        }
                    }
                    else if (control.GetType() == typeof(InputControl_DateTimePicker))
                    {
                        if (isSave && !filterValues.ContainsKey(control))
                            filterValues.Add(control, ((InputControl_DateTimePicker)control).FilterValues);
                        else
                        {
                            if (isSave)
                                filterValues[control] = ((InputControl_DateTimePicker)control).FilterValues;
                            else
                                ((InputControl_DateTimePicker)control).FilterValues = (FilterValues_InputControl_DateTimePicker)filterValues[control];
                        }
                    }
                    else if (control.GetType() == typeof(InputControl_Dropdownlist))
                    {
                        if (isSave && !filterValues.ContainsKey(control))
                            filterValues.Add(control, ((InputControl_Dropdownlist)control).SelectedItem);
                        else
                        {
                            if (isSave)
                                filterValues[control] = ((InputControl_Dropdownlist)control).SelectedItem;
                            else
                                ((InputControl_Dropdownlist)control).SelectedItem = filterValues[control];
                        }
                    }
                }
            }
        }
        
        protected T getFilterValue<T>(Control control)
        {
            object obj = null;

            if (filterValues.Count > 0 && filterValues.ContainsKey(control))
            {
                if (control.GetType() == typeof(InputControl_Textbox))
                {
                    if (typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?))
                        obj = ((FilterValues_InputControl_Textbox)filterValues[control]).ValueGuid;
                    else if (typeof(T) == typeof(string))
                        obj = ((FilterValues_InputControl_Textbox)filterValues[control]).ValueText;
                }
                else if (control.GetType() == typeof(InputControl_Numeric))
                {
                    FilterValues_InputControl_Numeric item = ((FilterValues_InputControl_Numeric)filterValues[control]);
                    if (item.ShowCheckBox && item.Checked)
                    {
                        obj = ((FilterValues_InputControl_Numeric)filterValues[control]).Value;
                    }
                }
                else if (control.GetType() == typeof(InputControl_DateTimePicker))
                {
                    FilterValues_InputControl_DateTimePicker item = ((FilterValues_InputControl_DateTimePicker)filterValues[control]);
                    if(item.ShowCheckBox && item.Checked)
                    {
                        if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
                            obj = item.Value;
                        else if (typeof(T) == typeof(TimeSpan) || typeof(T) == typeof(TimeSpan?))
                            obj = item.ValueTimespan;
                    }
                }
                else if (control.GetType() == typeof(InputControl_Dropdownlist))
                    obj = ((InputControl_Dropdownlist)control).SelectedValue;
            }

            return Util.wrapNullable<T>(obj);
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void Form_Load(object sender, EventArgs e)
        {
            setupControls();
            setupFields();
            additionalSettings();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (_currentMode != FormModes.Search)
            {
                Mode = FormModes.Search;
                clearInputFields();
                reapplyFilterValues();
                itxt_QuickSearch.Focus();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (_currentMode != FormModes.Add)
            {
                Mode = FormModes.Add;
                clearInputFields();
                //populateGridViewDataSource(true);
                itxt_QuickSearch.Focus();
            }
        }

        protected virtual void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_currentMode != FormModes.Update)
            {
                Mode = FormModes.Update;
                populateInputFieldsForUpdate();
                itxt_QuickSearch.Focus();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case FormModes.Search:
                    saveFilterValues();
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
                    if(btnAdd.Enabled)
                    {
                        clearInputFields();
                        btnAdd.PerformClick();
                    }
                    break;
            }

            populateGridViewDataSource(true);
            if (Mode == FormModes.Update)
            {
                btnUpdate.PerformClick();
            }
            itxt_QuickSearch.Focus();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (Mode == FormModes.Update)
                btnUpdate.PerformClick();
            else 
            {
                clearInputFields();
                if (Mode == FormModes.Search)
                {
                    filterValues.Clear();
                    saveFilterValues();
                }
            }

            itxt_QuickSearch.Focus();
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

        protected void lnkClearQuickSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            itxt_QuickSearch.ValueText = "";
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
                updateActiveStatus(selectedRowID(), !Util.getCheckboxValue(sender, e));
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
                updateCheckbox1Column(selectedRowID(), !Util.getCheckboxValue(sender, e));
                clearInputFields();
                populateGridViewDataSource(true);
                if (Mode == FormModes.Update) btnUpdate.PerformClick();
            }

            virtual_dgv_CellContentClick(sender, e);
        }

        protected void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (Mode == FormModes.Update)
            {
                populateInputFieldsForUpdate();
                itxt_QuickSearch.Focus();
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
            dgv_CellDoubleClick();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            isFormShown = true;
            if (_startingMode == FormModes.Browse)
            {
                scMain.Panel1Collapsed = true;
                ptInputPanel.Visible = false;
                col_dgv_Active.Visible = false;
                col_dgv_Default.Visible = false;
                col_dgv_Checkbox1.Visible = false;
            }

            setupControlsBasedOnRoles();

            Mode = _startingMode;
            updateInputControls(InputToDisablePermanently, false, false);

            if(_showDataOnLoad)
                populateGridViewDataSource(true);

            itxt_QuickSearch.Focus();
        }

        private void dgv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                selectRow(((DataGridView)sender).CurrentRow.Index);
                dgv_CellDoubleClick();
            }
        }

        private void itxt_QuickSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (Mode == FormModes.Update)
                    btnAdd.PerformClick();

                populateGridViewDataSource(false);
                itxt_QuickSearch.Focus();
            }
        }

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            populateGridViewDataSource(true);
            itxt_QuickSearch.Focus();
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/
    }
}
