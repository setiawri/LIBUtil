namespace LIBUtil.Desktop.UserControls
{
    partial class InputControl_Textbox
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
            this.panel = new System.Windows.Forms.Panel();
            this.textbox = new System.Windows.Forms.TextBox();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.textbox);
            this.panel.Controls.Add(this.pbDelete);
            this.panel.Controls.Add(this.txtFilter);
            this.panel.Controls.Add(this.label);
            this.panel.Location = new System.Drawing.Point(0, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(240, 47);
            this.panel.TabIndex = 0;
            // 
            // textbox
            // 
            this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textbox.Location = new System.Drawing.Point(40, 21);
            this.textbox.Margin = new System.Windows.Forms.Padding(4);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(176, 24);
            this.textbox.TabIndex = 1001;
            this.textbox.Click += new System.EventHandler(this.textbox_isBrowseMode_Clicked);
            this.textbox.TextChanged += new System.EventHandler(this.textbox_TextChanged);
            this.textbox.Enter += new System.EventHandler(this.textbox_FocusEnter);
            this.textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_onKeyDown);
            this.textbox.MouseLeave += new System.EventHandler(this.textbox_MouseLeave);
            this.textbox.MouseHover += new System.EventHandler(this.textbox_MouseHover);
            // 
            // pbDelete
            // 
            this.pbDelete.BackgroundImage = global::LIBUtil.Properties.Resources.delete_button_01;
            this.pbDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbDelete.Location = new System.Drawing.Point(216, 21);
            this.pbDelete.Margin = new System.Windows.Forms.Padding(4);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(24, 26);
            this.pbDelete.TabIndex = 1002;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtFilter.Location = new System.Drawing.Point(0, 21);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(0);
            this.txtFilter.MaxLength = 5;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(40, 24);
            this.txtFilter.TabIndex = 1004;
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(240, 21);
            this.label.TabIndex = 1003;
            this.label.Text = "textbox";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InputControl_Textbox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.panel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InputControl_Textbox";
            this.Size = new System.Drawing.Size(240, 50);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox pbDelete;
        public System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label;
        public System.Windows.Forms.TextBox textbox;
    }
}
