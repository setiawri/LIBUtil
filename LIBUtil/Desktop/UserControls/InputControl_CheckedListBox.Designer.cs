namespace LIBUtil.Desktop.UserControls
{
    partial class InputControl_CheckedListBox
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
            this.label = new System.Windows.Forms.Label();
            this.checkedlistbox = new System.Windows.Forms.CheckedListBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.chk = new System.Windows.Forms.CheckBox();
            this.lnkClearFilter = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.label.Size = new System.Drawing.Size(240, 23);
            this.label.TabIndex = 1;
            this.label.Text = "checkedlistbox";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkedlistbox
            // 
            this.checkedlistbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedlistbox.FormattingEnabled = true;
            this.checkedlistbox.Location = new System.Drawing.Point(0, 23);
            this.checkedlistbox.Margin = new System.Windows.Forms.Padding(4);
            this.checkedlistbox.Name = "checkedlistbox";
            this.checkedlistbox.Size = new System.Drawing.Size(240, 84);
            this.checkedlistbox.TabIndex = 0;
            this.checkedlistbox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedlistbox_ItemCheck);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(180, 3);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(45, 18);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // chk
            // 
            this.chk.Location = new System.Drawing.Point(-5, 0);
            this.chk.Margin = new System.Windows.Forms.Padding(4);
            this.chk.Name = "chk";
            this.chk.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.chk.Size = new System.Drawing.Size(28, 27);
            this.chk.TabIndex = 0;
            this.chk.UseVisualStyleBackColor = true;
            this.chk.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // lnkClearFilter
            // 
            this.lnkClearFilter.ActiveLinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkClearFilter.AutoSize = true;
            this.lnkClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkClearFilter.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkClearFilter.LinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearFilter.Location = new System.Drawing.Point(227, 7);
            this.lnkClearFilter.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lnkClearFilter.Name = "lnkClearFilter";
            this.lnkClearFilter.Size = new System.Drawing.Size(15, 13);
            this.lnkClearFilter.TabIndex = 2;
            this.lnkClearFilter.TabStop = true;
            this.lnkClearFilter.Text = "X";
            this.lnkClearFilter.VisitedLinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearFilter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClearFilter_LinkClicked);
            // 
            // InputControl_CheckedListBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.lnkClearFilter);
            this.Controls.Add(this.checkedlistbox);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.chk);
            this.Controls.Add(this.label);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InputControl_CheckedListBox";
            this.Size = new System.Drawing.Size(240, 107);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.CheckedListBox checkedlistbox;
        public System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.CheckBox chk;
        public System.Windows.Forms.LinkLabel lnkClearFilter;
    }
}
