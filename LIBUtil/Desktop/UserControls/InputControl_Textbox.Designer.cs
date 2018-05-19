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
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(180, 19);
            this.label.TabIndex = 4;
            this.label.Text = "textbox";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textbox
            // 
            this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textbox.Location = new System.Drawing.Point(0, 19);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(180, 20);
            this.textbox.TabIndex = 5;
            this.textbox.Click += new System.EventHandler(this.textbox_isBrowseMode_Clicked);
            this.textbox.TextChanged += new System.EventHandler(this.textbox_TextChanged);
            this.textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_onKeyDown);
            this.textbox.MouseLeave += new System.EventHandler(this.textbox_MouseLeave);
            this.textbox.MouseHover += new System.EventHandler(this.textbox_MouseHover);
            // 
            // InputControl_Textbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textbox);
            this.Controls.Add(this.label);
            this.Name = "InputControl_Textbox";
            this.Size = new System.Drawing.Size(180, 41);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        public System.Windows.Forms.TextBox textbox;
    }
}
