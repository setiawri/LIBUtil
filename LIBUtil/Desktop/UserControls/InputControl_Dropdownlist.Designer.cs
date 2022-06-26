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
            this.label = new System.Windows.Forms.Label();
            this.pbUpdate = new System.Windows.Forms.PictureBox();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbUpdate)).BeginInit();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // dropdownlist
            // 
            this.dropdownlist.DisplayMember = "";
            this.dropdownlist.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dropdownlist.Filter = "";
            this.dropdownlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.dropdownlist.Location = new System.Drawing.Point(0, 23);
            this.dropdownlist.Margin = new System.Windows.Forms.Padding(5);
            this.dropdownlist.Name = "dropdownlist";
            this.dropdownlist.SelectedIndex = -1;
            this.dropdownlist.SelectedItem = null;
            this.dropdownlist.SelectedItemText = "";
            this.dropdownlist.SelectedValue = null;
            this.dropdownlist.Size = new System.Drawing.Size(240, 27);
            this.dropdownlist.TabIndex = 1;
            this.dropdownlist.ValueMember = "";
            this.dropdownlist.SelectedIndexChanged += new System.EventHandler(this.dropdownlist_SelectedIndexChanged);
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.label.Size = new System.Drawing.Size(240, 20);
            this.label.TabIndex = 18;
            this.label.Text = "dropdownlist";
            this.label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pbUpdate
            // 
            this.pbUpdate.BackColor = System.Drawing.Color.Transparent;
            this.pbUpdate.BackgroundImage = global::LIBUtil.Properties.Resources.gear_black;
            this.pbUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbUpdate.Location = new System.Drawing.Point(0, 4);
            this.pbUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.pbUpdate.Name = "pbUpdate";
            this.pbUpdate.Size = new System.Drawing.Size(20, 18);
            this.pbUpdate.TabIndex = 21;
            this.pbUpdate.TabStop = false;
            this.pbUpdate.Click += new System.EventHandler(this.pbUpdate_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.Controls.Add(this.txtFilter);
            this.pnlFilter.Controls.Add(this.pbDelete);
            this.pnlFilter.Location = new System.Drawing.Point(180, 4);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(60, 18);
            this.pnlFilter.TabIndex = 1004;
            // 
            // txtFilter
            // 
            this.txtFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(0, 0);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(42, 18);
            this.txtFilter.TabIndex = 1004;
            this.txtFilter.TabStop = false;
            // 
            // pbDelete
            // 
            this.pbDelete.BackgroundImage = global::LIBUtil.Properties.Resources.delete_button_01;
            this.pbDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbDelete.Location = new System.Drawing.Point(42, 0);
            this.pbDelete.Margin = new System.Windows.Forms.Padding(4);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(18, 18);
            this.pbDelete.TabIndex = 1005;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            // 
            // InputControl_Dropdownlist
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.dropdownlist);
            this.Controls.Add(this.pbUpdate);
            this.Controls.Add(this.label);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InputControl_Dropdownlist";
            this.Size = new System.Drawing.Size(240, 50);
            this.Load += new System.EventHandler(this.InputControl_Dropdownlist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbUpdate)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Dropdownlist dropdownlist;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.PictureBox pbUpdate;
        private System.Windows.Forms.Panel pnlFilter;
        public System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.PictureBox pbDelete;
    }
}
