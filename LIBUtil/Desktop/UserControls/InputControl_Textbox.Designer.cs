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
            this.label = new System.Windows.Forms.Label();
            this.textbox = new System.Windows.Forms.TextBox();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.pnlDelete = new System.Windows.Forms.Panel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtFilter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.pnlDelete.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(180, 19);
            this.label.TabIndex = 999;
            this.label.Text = "textbox";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textbox
            // 
            this.textbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textbox.Location = new System.Drawing.Point(30, 20);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(128, 21);
            this.textbox.TabIndex = 1;
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
            this.pbDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbDelete.Location = new System.Drawing.Point(0, 0);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(22, 22);
            this.pbDelete.TabIndex = 6;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            // 
            // pnlDelete
            // 
            this.pnlDelete.Controls.Add(this.pbDelete);
            this.pnlDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDelete.Location = new System.Drawing.Point(158, 19);
            this.pnlDelete.Name = "pnlDelete";
            this.pnlDelete.Size = new System.Drawing.Size(22, 22);
            this.pnlDelete.TabIndex = 7;
            this.pnlDelete.Visible = false;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.txtFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFilter.Location = new System.Drawing.Point(0, 19);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.pnlFilter.Size = new System.Drawing.Size(30, 22);
            this.pnlFilter.TabIndex = 0;
            this.pnlFilter.Visible = false;
            // 
            // txtFilter
            // 
            this.txtFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtFilter.Location = new System.Drawing.Point(0, 1);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(0);
            this.txtFilter.MaxLength = 5;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(30, 21);
            this.txtFilter.TabIndex = 1000;
            // 
            // InputControl_Textbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textbox);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlDelete);
            this.Controls.Add(this.label);
            this.Name = "InputControl_Textbox";
            this.Size = new System.Drawing.Size(180, 41);
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.pnlDelete.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        public System.Windows.Forms.TextBox textbox;
        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.Panel pnlDelete;
        private System.Windows.Forms.Panel pnlFilter;
        public System.Windows.Forms.TextBox txtFilter;
    }
}
