namespace LIBUtil.Desktop.UserControls
{
    partial class InputControl_Dropdownlist
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
            this.dropdownlist = new LIBUtil.Desktop.UserControls.Dropdownlist();
            this.lnkClearFilter = new System.Windows.Forms.LinkLabel();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.pbUpdate = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // dropdownlist
            // 
            this.dropdownlist.DisplayMember = "";
            this.dropdownlist.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dropdownlist.Filter = "";
            this.dropdownlist.Location = new System.Drawing.Point(0, 19);
            this.dropdownlist.Name = "dropdownlist";
            this.dropdownlist.SelectedItem = null;
            this.dropdownlist.SelectedValue = null;
            this.dropdownlist.Size = new System.Drawing.Size(180, 22);
            this.dropdownlist.TabIndex = 1;
            this.dropdownlist.ValueMember = "";
            this.dropdownlist.SelectedIndexChanged += new System.EventHandler(this.dropdownlist_SelectedIndexChanged);
            // 
            // lnkClearFilter
            // 
            this.lnkClearFilter.ActiveLinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkClearFilter.AutoSize = true;
            this.lnkClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkClearFilter.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkClearFilter.LinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearFilter.Location = new System.Drawing.Point(167, 3);
            this.lnkClearFilter.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lnkClearFilter.Name = "lnkClearFilter";
            this.lnkClearFilter.Size = new System.Drawing.Size(15, 13);
            this.lnkClearFilter.TabIndex = 0;
            this.lnkClearFilter.TabStop = true;
            this.lnkClearFilter.Text = "X";
            this.lnkClearFilter.VisitedLinkColor = System.Drawing.Color.DarkOrange;
            this.lnkClearFilter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClearFilter_LinkClicked);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(132, 0);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(35, 18);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.label.Size = new System.Drawing.Size(180, 19);
            this.label.TabIndex = 18;
            this.label.Text = "dropdownlist";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbUpdate
            // 
            this.pbUpdate.BackColor = System.Drawing.Color.Transparent;
            this.pbUpdate.BackgroundImage = global::LIBUtil.Properties.Resources.gear_black;
            this.pbUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbUpdate.Location = new System.Drawing.Point(0, 2);
            this.pbUpdate.Name = "pbUpdate";
            this.pbUpdate.Size = new System.Drawing.Size(15, 15);
            this.pbUpdate.TabIndex = 21;
            this.pbUpdate.TabStop = false;
            this.pbUpdate.Click += new System.EventHandler(this.pbUpdate_Click);
            // 
            // InputControl_Dropdownlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dropdownlist);
            this.Controls.Add(this.pbUpdate);
            this.Controls.Add(this.lnkClearFilter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label);
            this.Name = "InputControl_Dropdownlist";
            this.Size = new System.Drawing.Size(180, 41);
            ((System.ComponentModel.ISupportInitialize)(this.pbUpdate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Dropdownlist dropdownlist;
        public System.Windows.Forms.LinkLabel lnkClearFilter;
        public System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.PictureBox pbUpdate;
    }
}
