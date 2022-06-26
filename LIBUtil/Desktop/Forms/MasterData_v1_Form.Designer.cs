namespace LIBUtil.Desktop.Forms
{
    partial class MasterData_v1_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterData_v1_Form));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scInputContainer = new System.Windows.Forms.SplitContainer();
            this.scInputLeft = new System.Windows.Forms.SplitContainer();
            this.scInputRight = new System.Windows.Forms.SplitContainer();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlActionButtons = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.scContent = new System.Windows.Forms.SplitContainer();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.col_dgv_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dgv_Default = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_dgv_StatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dgv_StatusId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dgv_Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_dgv_Checkbox1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlRowInfo = new System.Windows.Forms.Panel();
            this.pnlRowInfoContent = new System.Windows.Forms.Panel();
            this.pnlRowInfoHeaderContainer = new System.Windows.Forms.Panel();
            this.pnlRowInfoHeader = new System.Windows.Forms.Panel();
            this.ptRowInfo = new LIBUtil.Desktop.UserControls.PanelToggle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlQuickSearch = new System.Windows.Forms.Panel();
            this.chkIncludeInactive = new System.Windows.Forms.CheckBox();
            this.itxt_QuickSearch = new LIBUtil.Desktop.UserControls.InputControl_Textbox();
            this.pbLog = new System.Windows.Forms.PictureBox();
            this.pbRefresh = new System.Windows.Forms.PictureBox();
            this.ptInputPanel = new LIBUtil.Desktop.UserControls.PanelToggle();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scInputContainer)).BeginInit();
            this.scInputContainer.Panel1.SuspendLayout();
            this.scInputContainer.Panel2.SuspendLayout();
            this.scInputContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scInputLeft)).BeginInit();
            this.scInputLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scInputRight)).BeginInit();
            this.scInputRight.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlActionButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scContent)).BeginInit();
            this.scContent.Panel1.SuspendLayout();
            this.scContent.Panel2.SuspendLayout();
            this.scContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.pnlRowInfo.SuspendLayout();
            this.pnlRowInfoHeaderContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlQuickSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefresh)).BeginInit();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.BackColor = System.Drawing.Color.White;
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Margin = new System.Windows.Forms.Padding(4);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.BackColor = System.Drawing.Color.White;
            this.scMain.Panel1.Controls.Add(this.scInputContainer);
            this.scMain.Panel1.Controls.Add(this.pnlButtons);
            this.scMain.Panel1.Controls.Add(this.pnlActionButtons);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scContent);
            this.scMain.Panel2.Controls.Add(this.panel1);
            this.scMain.Size = new System.Drawing.Size(1340, 752);
            this.scMain.SplitterDistance = 320;
            this.scMain.SplitterWidth = 1;
            this.scMain.TabIndex = 0;
            // 
            // scInputContainer
            // 
            this.scInputContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInputContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInputContainer.IsSplitterFixed = true;
            this.scInputContainer.Location = new System.Drawing.Point(0, 32);
            this.scInputContainer.Margin = new System.Windows.Forms.Padding(4);
            this.scInputContainer.Name = "scInputContainer";
            // 
            // scInputContainer.Panel1
            // 
            this.scInputContainer.Panel1.Controls.Add(this.scInputLeft);
            // 
            // scInputContainer.Panel2
            // 
            this.scInputContainer.Panel2.Controls.Add(this.scInputRight);
            this.scInputContainer.Size = new System.Drawing.Size(1340, 260);
            this.scInputContainer.SplitterDistance = 500;
            this.scInputContainer.SplitterWidth = 5;
            this.scInputContainer.TabIndex = 1;
            // 
            // scInputLeft
            // 
            this.scInputLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInputLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInputLeft.IsSplitterFixed = true;
            this.scInputLeft.Location = new System.Drawing.Point(0, 0);
            this.scInputLeft.Margin = new System.Windows.Forms.Padding(4);
            this.scInputLeft.Name = "scInputLeft";
            this.scInputLeft.Size = new System.Drawing.Size(500, 260);
            this.scInputLeft.SplitterDistance = 249;
            this.scInputLeft.SplitterWidth = 5;
            this.scInputLeft.TabIndex = 2;
            // 
            // scInputRight
            // 
            this.scInputRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInputRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInputRight.IsSplitterFixed = true;
            this.scInputRight.Location = new System.Drawing.Point(0, 0);
            this.scInputRight.Margin = new System.Windows.Forms.Padding(4);
            this.scInputRight.Name = "scInputRight";
            this.scInputRight.Size = new System.Drawing.Size(835, 260);
            this.scInputRight.SplitterDistance = 249;
            this.scInputRight.SplitterWidth = 5;
            this.scInputRight.TabIndex = 2;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Controls.Add(this.btnUpdate);
            this.pnlButtons.Controls.Add(this.btnSearch);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(4);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(2);
            this.pnlButtons.Size = new System.Drawing.Size(1340, 32);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 28);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "ADD NEW";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(203, 2);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 28);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(103, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "FILTER";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlActionButtons
            // 
            this.pnlActionButtons.Controls.Add(this.btnSubmit);
            this.pnlActionButtons.Controls.Add(this.btnCancel);
            this.pnlActionButtons.Controls.Add(this.btnReset);
            this.pnlActionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionButtons.Location = new System.Drawing.Point(0, 292);
            this.pnlActionButtons.Margin = new System.Windows.Forms.Padding(4);
            this.pnlActionButtons.Name = "pnlActionButtons";
            this.pnlActionButtons.Size = new System.Drawing.Size(1340, 28);
            this.pnlActionButtons.TabIndex = 10;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(3, 0);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(93, 27);
            this.btnSubmit.TabIndex = 93;
            this.btnSubmit.Text = "SUBMIT";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(189, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 27);
            this.btnCancel.TabIndex = 95;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(96, 0);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(93, 27);
            this.btnReset.TabIndex = 94;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // scContent
            // 
            this.scContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContent.Location = new System.Drawing.Point(0, 34);
            this.scContent.Name = "scContent";
            this.scContent.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scContent.Panel1
            // 
            this.scContent.Panel1.Controls.Add(this.dgv);
            // 
            // scContent.Panel2
            // 
            this.scContent.Panel2.Controls.Add(this.pnlRowInfo);
            this.scContent.Size = new System.Drawing.Size(1340, 397);
            this.scContent.SplitterDistance = 251;
            this.scContent.TabIndex = 5;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_dgv_Id,
            this.col_dgv_Default,
            this.col_dgv_StatusName,
            this.col_dgv_StatusId,
            this.col_dgv_Active,
            this.col_dgv_Checkbox1});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(4);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1340, 251);
            this.dgv.TabIndex = 3;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseDown);
            this.dgv.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_DataBindingComplete);
            this.dgv.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgv_PreviewKeyDown);
            // 
            // col_dgv_Id
            // 
            this.col_dgv_Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_dgv_Id.HeaderText = "Id";
            this.col_dgv_Id.Name = "col_dgv_Id";
            this.col_dgv_Id.Visible = false;
            // 
            // col_dgv_Default
            // 
            this.col_dgv_Default.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.col_dgv_Default.HeaderText = "Default";
            this.col_dgv_Default.MinimumWidth = 45;
            this.col_dgv_Default.Name = "col_dgv_Default";
            this.col_dgv_Default.Visible = false;
            // 
            // col_dgv_StatusName
            // 
            this.col_dgv_StatusName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_dgv_StatusName.HeaderText = "Status";
            this.col_dgv_StatusName.Name = "col_dgv_StatusName";
            this.col_dgv_StatusName.ReadOnly = true;
            this.col_dgv_StatusName.Visible = false;
            // 
            // col_dgv_StatusId
            // 
            this.col_dgv_StatusId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_dgv_StatusId.HeaderText = "Status Id";
            this.col_dgv_StatusId.Name = "col_dgv_StatusId";
            this.col_dgv_StatusId.Visible = false;
            // 
            // col_dgv_Active
            // 
            this.col_dgv_Active.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.col_dgv_Active.HeaderText = "Active";
            this.col_dgv_Active.MinimumWidth = 40;
            this.col_dgv_Active.Name = "col_dgv_Active";
            this.col_dgv_Active.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_dgv_Active.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col_dgv_Active.Width = 40;
            // 
            // col_dgv_Checkbox1
            // 
            this.col_dgv_Checkbox1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.col_dgv_Checkbox1.HeaderText = "Checkbox1";
            this.col_dgv_Checkbox1.MinimumWidth = 40;
            this.col_dgv_Checkbox1.Name = "col_dgv_Checkbox1";
            this.col_dgv_Checkbox1.Visible = false;
            // 
            // pnlRowInfo
            // 
            this.pnlRowInfo.Controls.Add(this.pnlRowInfoContent);
            this.pnlRowInfo.Controls.Add(this.pnlRowInfoHeaderContainer);
            this.pnlRowInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRowInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlRowInfo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlRowInfo.Name = "pnlRowInfo";
            this.pnlRowInfo.Size = new System.Drawing.Size(1340, 142);
            this.pnlRowInfo.TabIndex = 4;
            // 
            // pnlRowInfoContent
            // 
            this.pnlRowInfoContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRowInfoContent.Location = new System.Drawing.Point(0, 26);
            this.pnlRowInfoContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlRowInfoContent.Name = "pnlRowInfoContent";
            this.pnlRowInfoContent.Size = new System.Drawing.Size(1340, 116);
            this.pnlRowInfoContent.TabIndex = 1;
            // 
            // pnlRowInfoHeaderContainer
            // 
            this.pnlRowInfoHeaderContainer.BackColor = System.Drawing.SystemColors.Control;
            this.pnlRowInfoHeaderContainer.Controls.Add(this.pnlRowInfoHeader);
            this.pnlRowInfoHeaderContainer.Controls.Add(this.ptRowInfo);
            this.pnlRowInfoHeaderContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRowInfoHeaderContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlRowInfoHeaderContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlRowInfoHeaderContainer.Name = "pnlRowInfoHeaderContainer";
            this.pnlRowInfoHeaderContainer.Size = new System.Drawing.Size(1340, 26);
            this.pnlRowInfoHeaderContainer.TabIndex = 0;
            // 
            // pnlRowInfoHeader
            // 
            this.pnlRowInfoHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRowInfoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRowInfoHeader.Location = new System.Drawing.Point(26, 0);
            this.pnlRowInfoHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlRowInfoHeader.Name = "pnlRowInfoHeader";
            this.pnlRowInfoHeader.Size = new System.Drawing.Size(1314, 26);
            this.pnlRowInfoHeader.TabIndex = 6;
            // 
            // ptRowInfo
            // 
            this.ptRowInfo.AdjustLocationOnClick = true;
            this.ptRowInfo.BackColor = System.Drawing.Color.White;
            this.ptRowInfo.ContainerPanel = this.scContent.Panel2;
            this.ptRowInfo.ContainerPanelOriginalSize = new System.Drawing.Size(0, 0);
            this.ptRowInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.ptRowInfo.InitialArrowDirection = System.Windows.Forms.ArrowDirection.Down;
            this.ptRowInfo.Location = new System.Drawing.Point(0, 0);
            this.ptRowInfo.Margin = new System.Windows.Forms.Padding(5);
            this.ptRowInfo.MinimumSplitterDistance = 100;
            this.ptRowInfo.Name = "ptRowInfo";
            this.ptRowInfo.Size = new System.Drawing.Size(26, 26);
            this.ptRowInfo.TabIndex = 5;
            this.ptRowInfo.TogglePanel = null;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.pnlQuickSearch);
            this.panel1.Controls.Add(this.ptInputPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1340, 34);
            this.panel1.TabIndex = 0;
            // 
            // pnlQuickSearch
            // 
            this.pnlQuickSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQuickSearch.Controls.Add(this.chkIncludeInactive);
            this.pnlQuickSearch.Controls.Add(this.itxt_QuickSearch);
            this.pnlQuickSearch.Controls.Add(this.pbLog);
            this.pnlQuickSearch.Controls.Add(this.pbRefresh);
            this.pnlQuickSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuickSearch.Location = new System.Drawing.Point(34, 0);
            this.pnlQuickSearch.Margin = new System.Windows.Forms.Padding(4);
            this.pnlQuickSearch.Name = "pnlQuickSearch";
            this.pnlQuickSearch.Size = new System.Drawing.Size(1306, 34);
            this.pnlQuickSearch.TabIndex = 97;
            // 
            // chkIncludeInactive
            // 
            this.chkIncludeInactive.AutoSize = true;
            this.chkIncludeInactive.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkIncludeInactive.Location = new System.Drawing.Point(186, 0);
            this.chkIncludeInactive.Margin = new System.Windows.Forms.Padding(4);
            this.chkIncludeInactive.Name = "chkIncludeInactive";
            this.chkIncludeInactive.Size = new System.Drawing.Size(91, 32);
            this.chkIncludeInactive.TabIndex = 1;
            this.chkIncludeInactive.TabStop = false;
            this.chkIncludeInactive.Text = "show inactive";
            this.chkIncludeInactive.UseVisualStyleBackColor = true;
            this.chkIncludeInactive.CheckedChanged += new System.EventHandler(this.chkIncludeInactive_CheckedChanged);
            // 
            // itxt_QuickSearch
            // 
            this.itxt_QuickSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.itxt_QuickSearch.IsBrowseMode = false;
            this.itxt_QuickSearch.LabelText = "textbox";
            this.itxt_QuickSearch.Location = new System.Drawing.Point(66, 0);
            this.itxt_QuickSearch.Margin = new System.Windows.Forms.Padding(4);
            this.itxt_QuickSearch.MaxLength = 32767;
            this.itxt_QuickSearch.MultiLine = false;
            this.itxt_QuickSearch.Name = "itxt_QuickSearch";
            this.itxt_QuickSearch.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.itxt_QuickSearch.PasswordChar = '\0';
            this.itxt_QuickSearch.RowCount = 1;
            this.itxt_QuickSearch.ShowDeleteButton = true;
            this.itxt_QuickSearch.ShowFilter = false;
            this.itxt_QuickSearch.ShowTextboxOnly = true;
            this.itxt_QuickSearch.Size = new System.Drawing.Size(120, 32);
            this.itxt_QuickSearch.TabIndex = 5;
            this.itxt_QuickSearch.ValueText = "";
            this.itxt_QuickSearch.onKeyDown += new System.Windows.Forms.KeyEventHandler(this.itxt_QuickSearch_KeyDown);
            // 
            // pbLog
            // 
            this.pbLog.BackColor = System.Drawing.Color.White;
            this.pbLog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbLog.BackgroundImage")));
            this.pbLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLog.Location = new System.Drawing.Point(33, 0);
            this.pbLog.Margin = new System.Windows.Forms.Padding(4);
            this.pbLog.Name = "pbLog";
            this.pbLog.Size = new System.Drawing.Size(33, 32);
            this.pbLog.TabIndex = 22;
            this.pbLog.TabStop = false;
            this.pbLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // pbRefresh
            // 
            this.pbRefresh.BackColor = System.Drawing.Color.White;
            this.pbRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbRefresh.BackgroundImage")));
            this.pbRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRefresh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbRefresh.Location = new System.Drawing.Point(0, 0);
            this.pbRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.pbRefresh.Name = "pbRefresh";
            this.pbRefresh.Size = new System.Drawing.Size(33, 32);
            this.pbRefresh.TabIndex = 21;
            this.pbRefresh.TabStop = false;
            this.pbRefresh.Click += new System.EventHandler(this.pbRefresh_Click);
            // 
            // ptInputPanel
            // 
            this.ptInputPanel.AdjustLocationOnClick = false;
            this.ptInputPanel.BackColor = System.Drawing.Color.White;
            this.ptInputPanel.ContainerPanel = null;
            this.ptInputPanel.ContainerPanelOriginalSize = new System.Drawing.Size(0, 0);
            this.ptInputPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ptInputPanel.InitialArrowDirection = System.Windows.Forms.ArrowDirection.Up;
            this.ptInputPanel.Location = new System.Drawing.Point(0, 0);
            this.ptInputPanel.Margin = new System.Windows.Forms.Padding(5);
            this.ptInputPanel.MinimumSplitterDistance = 100;
            this.ptInputPanel.Name = "ptInputPanel";
            this.ptInputPanel.Size = new System.Drawing.Size(34, 34);
            this.ptInputPanel.TabIndex = 96;
            this.ptInputPanel.TogglePanel = this.scMain.Panel1;
            // 
            // MasterData_v1_Form
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1340, 752);
            this.Controls.Add(this.scMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MasterData_v1_Form";
            this.Text = "MasterData_v1_Form";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scInputContainer.Panel1.ResumeLayout(false);
            this.scInputContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scInputContainer)).EndInit();
            this.scInputContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scInputLeft)).EndInit();
            this.scInputLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scInputRight)).EndInit();
            this.scInputRight.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlActionButtons.ResumeLayout(false);
            this.scContent.Panel1.ResumeLayout(false);
            this.scContent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContent)).EndInit();
            this.scContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.pnlRowInfo.ResumeLayout(false);
            this.pnlRowInfoHeaderContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlQuickSearch.ResumeLayout(false);
            this.pnlQuickSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefresh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.CheckBox chkIncludeInactive;
        public System.Windows.Forms.Panel pnlActionButtons;
        public System.Windows.Forms.SplitContainer scInputLeft;
        public System.Windows.Forms.SplitContainer scInputRight;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.Button btnSearch;
        public System.Windows.Forms.Button btnSubmit;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnReset;
        public System.Windows.Forms.SplitContainer scMain;
        public System.Windows.Forms.Panel pnlButtons;
        public System.Windows.Forms.SplitContainer scInputContainer;
        public UserControls.PanelToggle ptInputPanel;
        public System.Windows.Forms.Panel pnlQuickSearch;
        public System.Windows.Forms.Panel pnlRowInfo;
        public System.Windows.Forms.Panel pnlRowInfoHeaderContainer;
        public System.Windows.Forms.Panel pnlRowInfoHeader;
        public UserControls.PanelToggle ptRowInfo;
        public System.Windows.Forms.Panel pnlRowInfoContent;
        public System.Windows.Forms.DataGridView dgv;
        public System.Windows.Forms.DataGridViewTextBoxColumn col_dgv_Id;
        public System.Windows.Forms.DataGridViewCheckBoxColumn col_dgv_Default;
        public System.Windows.Forms.DataGridViewTextBoxColumn col_dgv_StatusName;
        public System.Windows.Forms.DataGridViewTextBoxColumn col_dgv_StatusId;
        public System.Windows.Forms.DataGridViewCheckBoxColumn col_dgv_Active;
        public System.Windows.Forms.DataGridViewCheckBoxColumn col_dgv_Checkbox1;
        public System.Windows.Forms.PictureBox pbLog;
        public System.Windows.Forms.PictureBox pbRefresh;
        public UserControls.InputControl_Textbox itxt_QuickSearch;
        public System.Windows.Forms.SplitContainer scContent;
    }
}