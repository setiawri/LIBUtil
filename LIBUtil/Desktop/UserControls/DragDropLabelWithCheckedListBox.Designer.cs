namespace LIBUtil.Desktop.UserControls
{
    partial class DragDropLabelWithCheckedListBox
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
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label.Size = new System.Drawing.Size(98, 20);
            this.label.TabIndex = 0;
            this.label.Text = "label";
            // 
            // checkedlistbox
            // 
            this.checkedlistbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedlistbox.FormattingEnabled = true;
            this.checkedlistbox.Location = new System.Drawing.Point(0, 20);
            this.checkedlistbox.Name = "checkedlistbox";
            this.checkedlistbox.Size = new System.Drawing.Size(98, 78);
            this.checkedlistbox.TabIndex = 1;
            // 
            // DragDropLabelWithCheckedListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkedlistbox);
            this.Controls.Add(this.label);
            this.Name = "DragDropLabelWithCheckedListBox";
            this.Size = new System.Drawing.Size(98, 98);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.CheckedListBox checkedlistbox;
    }
}
