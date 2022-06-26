namespace LIBUtil.Desktop.UserControls
{
    partial class DatagridviewPaging
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageNo = new System.Windows.Forms.Label();
            this.in_PageSize = new LIBUtil.Desktop.UserControls.InputControl_Numeric();
            this.SuspendLayout();
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Location = new System.Drawing.Point(73, 0);
            this.btnFirstPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(37, 28);
            this.btnFirstPage.TabIndex = 1;
            this.btnFirstPage.Text = "<<";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.button_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(109, 0);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(31, 28);
            this.btnPrevious.TabIndex = 2;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.button_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Location = new System.Drawing.Point(216, 0);
            this.btnLastPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(37, 28);
            this.btnLastPage.TabIndex = 4;
            this.btnLastPage.Text = ">>";
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.button_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(187, 0);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(31, 28);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.button_Click);
            // 
            // lblPageNo
            // 
            this.lblPageNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPageNo.Location = new System.Drawing.Point(140, -1);
            this.lblPageNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageNo.Name = "lblPageNo";
            this.lblPageNo.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblPageNo.Size = new System.Drawing.Size(47, 27);
            this.lblPageNo.TabIndex = 5;
            this.lblPageNo.Text = "1/1";
            this.lblPageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // in_PageSize
            // 
            this.in_PageSize.Checked = false;
            this.in_PageSize.DecimalPlaces = 0;
            this.in_PageSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.in_PageSize.HideUpDown = false;
            this.in_PageSize.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.in_PageSize.LabelText = "numeric";
            this.in_PageSize.Location = new System.Drawing.Point(0, 0);
            this.in_PageSize.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.in_PageSize.MaximumValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.in_PageSize.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.in_PageSize.Name = "in_PageSize";
            this.in_PageSize.ShowAllowDecimalCheckbox = false;
            this.in_PageSize.ShowCheckbox = false;
            this.in_PageSize.ShowTextboxOnly = true;
            this.in_PageSize.Size = new System.Drawing.Size(73, 28);
            this.in_PageSize.TabIndex = 0;
            this.in_PageSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // DatagridviewPaging
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.in_PageSize);
            this.Controls.Add(this.lblPageNo);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirstPage);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DatagridviewPaging";
            this.Size = new System.Drawing.Size(253, 28);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageNo;
        private InputControl_Numeric in_PageSize;
    }
}
