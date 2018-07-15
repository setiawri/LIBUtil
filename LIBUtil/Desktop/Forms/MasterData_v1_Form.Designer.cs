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
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scInputContainer = new System.Windows.Forms.SplitContainer();
            this.scInputLeft = new System.Windows.Forms.SplitContainer();
            this.scInputRight = new System.Windows.Forms.SplitContainer();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlActionButtons = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.col_dgv_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dgv_Default = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_dgv_StatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dgv_StatusId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dgv_Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_dgv_Checkbox1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlQuickSearch = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIncludeInactive = new System.Windows.Forms.CheckBox();
            this.lnkClearQuickSearch = new System.Windows.Forms.LinkLabel();
            this.txtQuickSearch = new System.Windows.Forms.TextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlQuickSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.BackColor = System.Drawing.Color.White;
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
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
            this.scMain.Panel2.Controls.Add(this.dgv);
            this.scMain.Panel2.Controls.Add(this.panel1);
            this.scMain.Size = new System.Drawing.Size(1005, 611);
            this.scMain.SplitterDistance = 320;
            this.scMain.SplitterWidth = 1;
            this.scMain.TabIndex = 0;
            // 
            // scInputContainer
            // 
            this.scInputContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInputContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInputContainer.IsSplitterFixed = true;
            this.scInputContainer.Location = new System.Drawing.Point(0, 26);
            this.scInputContainer.Name = "scInputContainer";
            // 
            // scInputContainer.Panel1
            // 
            this.scInputContainer.Panel1.Controls.Add(this.scInputLeft);
            // 
            // scInputContainer.Panel2
            // 
            this.scInputContainer.Panel2.Controls.Add(this.scInputRight);
            this.scInputContainer.Size = new System.Drawing.Size(1005, 271);
            this.scInputContainer.SplitterDistance = 500;
            this.scInputContainer.TabIndex = 1;
            // 
            // scInputLeft
            // 
            this.scInputLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInputLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInputLeft.IsSplitterFixed = true;
            this.scInputLeft.Location = new System.Drawing.Point(0, 0);
            this.scInputLeft.Name = "scInputLeft";
            this.scInputLeft.Size = new System.Drawing.Size(500, 271);
            this.scInputLeft.SplitterDistance = 249;
            this.scInputLeft.TabIndex = 2;
            // 
            // scInputRight
            // 
            this.scInputRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInputRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInputRight.IsSplitterFixed = true;
            this.scInputRight.Location = new System.Drawing.Point(0, 0);
            this.scInputRight.Name = "scInputRight";
            this.scInputRight.Size = new System.Drawing.Size(501, 271);
            this.scInputRight.SplitterDistance = 249;
            this.scInputRight.TabIndex = 2;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Controls.Add(this.btnLog);
            this.pnlButtons.Controls.Add(this.btnUpdate);
            this.pnlButtons.Controls.Add(this.btnSearch);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1005, 26);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(2, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "ADD NEW";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(227, 2);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 23);
            this.btnLog.TabIndex = 3;
            this.btnLog.Text = "LOG";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(152, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(77, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlActionButtons
            // 
            this.pnlActionButtons.Controls.Add(this.btnSubmit);
            this.pnlActionButtons.Controls.Add(this.btnCancel);
            this.pnlActionButtons.Controls.Add(this.btnReset);
            this.pnlActionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionButtons.Location = new System.Drawing.Point(0, 297);
            this.pnlActionButtons.Name = "pnlActionButtons";
            this.pnlActionButtons.Size = new System.Drawing.Size(1005, 23);
            this.pnlActionButtons.TabIndex = 10;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(2, 0);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 22);
            this.btnSubmit.TabIndex = 93;
            this.btnSubmit.Text = "SUBMIT";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(142, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 22);
            this.btnCancel.TabIndex = 95;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(72, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(70, 22);
            this.btnReset.TabIndex = 94;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
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
            this.dgv.Location = new System.Drawing.Point(0, 28);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1005, 262);
            this.dgv.TabIndex = 3;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseDown);
            this.dgv.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_DataBindingComplete);
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
            this.col_dgv_Default.MinimumWidth = 40;
            this.col_dgv_Default.Name = "col_dgv_Default";
            this.col_dgv_Default.Visible = false;
            // 
            // col_dgv_StatusName
            // 
            this.col_dgv_StatusName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col_dgv_StatusName.HeaderText = "Status";
            this.col_dgv_StatusName.Name = "col_dgv_StatusName";
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.pnlQuickSearch);
            this.panel1.Controls.Add(this.ptInputPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 28);
            this.panel1.TabIndex = 0;
            // 
            // pnlQuickSearch
            // 
            this.pnlQuickSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQuickSearch.Controls.Add(this.label1);
            this.pnlQuickSearch.Controls.Add(this.chkIncludeInactive);
            this.pnlQuickSearch.Controls.Add(this.lnkClearQuickSearch);
            this.pnlQuickSearch.Controls.Add(this.txtQuickSearch);
            this.pnlQuickSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuickSearch.Location = new System.Drawing.Point(30, 0);
            this.pnlQuickSearch.Name = "pnlQuickSearch";
            this.pnlQuickSearch.Size = new System.Drawing.Size(975, 28);
            this.pnlQuickSearch.TabIndex = 97;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quick Search";
            // 
            // chkIncludeInactive
            // 
            this.chkIncludeInactive.AutoSize = true;
            this.chkIncludeInactive.Location = new System.Drawing.Point(199, 6);
            this.chkIncludeInactive.Name = "chkIncludeInactive";
            this.chkIncludeInactive.Size = new System.Drawing.Size(91, 17);
            this.chkIncludeInactive.TabIndex = 1;
            this.chkIncludeInactive.TabStop = false;
            this.chkIncludeInactive.Text = "show inactive";
            this.chkIncludeInactive.UseVisualStyleBackColor = true;
            this.chkIncludeInactive.CheckedChanged += new System.EventHandler(this.chkIncludeInactive_CheckedChanged);
            // 
            // lnkClearQuickSearch
            // 
            this.lnkClearQuickSearch.ActiveLinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearQuickSearch.AutoSize = true;
            this.lnkClearQuickSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkClearQuickSearch.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkClearQuickSearch.LinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearQuickSearch.Location = new System.Drawing.Point(180, 8);
            this.lnkClearQuickSearch.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lnkClearQuickSearch.Name = "lnkClearQuickSearch";
            this.lnkClearQuickSearch.Size = new System.Drawing.Size(15, 13);
            this.lnkClearQuickSearch.TabIndex = 13;
            this.lnkClearQuickSearch.TabStop = true;
            this.lnkClearQuickSearch.Text = "X";
            this.lnkClearQuickSearch.VisitedLinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearQuickSearch.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClearQuickSearch_LinkClicked);
            // 
            // txtQuickSearch
            // 
            this.txtQuickSearch.Location = new System.Drawing.Point(76, 4);
            this.txtQuickSearch.Name = "txtQuickSearch";
            this.txtQuickSearch.Size = new System.Drawing.Size(100, 20);
            this.txtQuickSearch.TabIndex = 0;
            this.txtQuickSearch.TextChanged += new System.EventHandler(this.txtQuickSearch_TextChanged);
            // 
            // ptInputPanel
            // 
            this.ptInputPanel.BackColor = System.Drawing.Color.White;
            this.ptInputPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ptInputPanel.InitialArrowDirection = System.Windows.Forms.ArrowDirection.Up;
            this.ptInputPanel.Location = new System.Drawing.Point(0, 0);
            this.ptInputPanel.Name = "ptInputPanel";
            this.ptInputPanel.Size = new System.Drawing.Size(30, 28);
            this.ptInputPanel.TabIndex = 96;
            this.ptInputPanel.TogglePanel = this.scMain.Panel1;
            // 
            // MasterData_v1_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 611);
            this.Controls.Add(this.scMain);
            this.Name = "MasterData_v1_Form";
            this.Text = "MasterData_v1_Form";
            this.Load += new System.EventHandler(this.Form_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlQuickSearch.ResumeLayout(false);
            this.pnlQuickSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Label label1;
        public System.Windows.Forms.LinkLabel lnkClearQuickSearch;
        public System.Windows.Forms.CheckBox chkIncludeInactive;
        public System.Windows.Forms.Panel pnlActionButtons;
        protected System.Windows.Forms.SplitContainer scInputLeft;
        protected System.Windows.Forms.SplitContainer scInputRight;
        protected System.Windows.Forms.Button btnAdd;
        protected System.Windows.Forms.Button btnUpdate;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnSubmit;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Button btnReset;
        protected System.Windows.Forms.DataGridView dgv;
        protected System.Windows.Forms.SplitContainer scMain;
        protected System.Windows.Forms.TextBox txtQuickSearch;
        protected System.Windows.Forms.Panel pnlButtons;
        protected System.Windows.Forms.SplitContainer scInputContainer;
        protected System.Windows.Forms.Button btnLog;
        protected System.Windows.Forms.DataGridViewTextBoxColumn col_dgv_Id;
        protected System.Windows.Forms.DataGridViewCheckBoxColumn col_dgv_Default;
        protected System.Windows.Forms.DataGridViewTextBoxColumn col_dgv_StatusName;
        protected System.Windows.Forms.DataGridViewTextBoxColumn col_dgv_StatusId;
        protected System.Windows.Forms.DataGridViewCheckBoxColumn col_dgv_Active;
        protected System.Windows.Forms.DataGridViewCheckBoxColumn col_dgv_Checkbox1;
        public UserControls.PanelToggle ptInputPanel;
        protected System.Windows.Forms.Panel pnlQuickSearch;
    }
}