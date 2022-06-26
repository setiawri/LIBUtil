namespace LIBUtil.Desktop.UserControls
{
    partial class InputControl_Numeric
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
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.checkbox = new System.Windows.Forms.CheckBox();
            this.chkAllowDecimal = new System.Windows.Forms.CheckBox();
            this.lblHideUpDownArrow = new System.Windows.Forms.Label();
            this.pnlNumericUpDown = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.pnlNumericUpDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(240, 20);
            this.label.TabIndex = 999;
            this.label.Text = "numeric";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown
            // 
            this.numericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numericUpDown.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown.Size = new System.Drawing.Size(225, 26);
            this.numericUpDown.TabIndex = 1000;
            this.numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown.ThousandsSeparator = true;
            this.numericUpDown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown.Enter += new System.EventHandler(this.numericUpDown_Enter);
            this.numericUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_KeyDown);
            // 
            // checkbox
            // 
            this.checkbox.AutoSize = true;
            this.checkbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkbox.Location = new System.Drawing.Point(0, 20);
            this.checkbox.Margin = new System.Windows.Forms.Padding(4);
            this.checkbox.Name = "checkbox";
            this.checkbox.Size = new System.Drawing.Size(15, 30);
            this.checkbox.TabIndex = 1002;
            this.checkbox.UseVisualStyleBackColor = true;
            this.checkbox.Visible = false;
            this.checkbox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // chkAllowDecimal
            // 
            this.chkAllowDecimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowDecimal.AutoSize = true;
            this.chkAllowDecimal.Location = new System.Drawing.Point(226, 5);
            this.chkAllowDecimal.Margin = new System.Windows.Forms.Padding(4);
            this.chkAllowDecimal.Name = "chkAllowDecimal";
            this.chkAllowDecimal.Size = new System.Drawing.Size(15, 14);
            this.chkAllowDecimal.TabIndex = 1004;
            this.chkAllowDecimal.UseVisualStyleBackColor = true;
            this.chkAllowDecimal.Visible = false;
            this.chkAllowDecimal.CheckedChanged += new System.EventHandler(this.chkShowDecimal_CheckedChanged);
            // 
            // lblHideUpDownArrow
            // 
            this.lblHideUpDownArrow.BackColor = System.Drawing.Color.White;
            this.lblHideUpDownArrow.Location = new System.Drawing.Point(1, 1);
            this.lblHideUpDownArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHideUpDownArrow.Name = "lblHideUpDownArrow";
            this.lblHideUpDownArrow.Size = new System.Drawing.Size(21, 24);
            this.lblHideUpDownArrow.TabIndex = 1001;
            this.lblHideUpDownArrow.Visible = false;
            // 
            // pnlNumericUpDown
            // 
            this.pnlNumericUpDown.Controls.Add(this.lblHideUpDownArrow);
            this.pnlNumericUpDown.Controls.Add(this.numericUpDown);
            this.pnlNumericUpDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNumericUpDown.Location = new System.Drawing.Point(15, 24);
            this.pnlNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.pnlNumericUpDown.Name = "pnlNumericUpDown";
            this.pnlNumericUpDown.Size = new System.Drawing.Size(225, 26);
            this.pnlNumericUpDown.TabIndex = 1003;
            // 
            // InputControl_Numeric
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.chkAllowDecimal);
            this.Controls.Add(this.pnlNumericUpDown);
            this.Controls.Add(this.checkbox);
            this.Controls.Add(this.label);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InputControl_Numeric";
            this.Size = new System.Drawing.Size(240, 50);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.pnlNumericUpDown.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.CheckBox checkbox;
        private System.Windows.Forms.CheckBox chkAllowDecimal;
        private System.Windows.Forms.Label lblHideUpDownArrow;
        private System.Windows.Forms.Panel pnlNumericUpDown;
    }
}
