namespace LIBUtil.Desktop.UserControls
{
    partial class Dropdownlist
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
            this.combobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // combobox
            // 
            this.combobox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.combobox.FormattingEnabled = true;
            this.combobox.Location = new System.Drawing.Point(0, 0);
            this.combobox.Name = "combobox";
            this.combobox.Size = new System.Drawing.Size(180, 21);
            this.combobox.TabIndex = 15;
            this.combobox.SelectedIndexChanged += new System.EventHandler(this.dropdownlist_SelectedIndexChanged);
            // 
            // Dropdownlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.combobox);
            this.Name = "Dropdownlist";
            this.Size = new System.Drawing.Size(180, 21);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox combobox;
    }
}
