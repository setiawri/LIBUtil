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
            this.lblHideUpDownArrow = new System.Windows.Forms.Label();
            this.checkbox = new System.Windows.Forms.CheckBox();
            this.pnlNumericUpDown = new System.Windows.Forms.Panel();
            this.chkShowDecimal = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.pnlNumericUpDown.SuspendLayout();
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
            this.label.Text = "numeric";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown
            // 
            this.numericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.numericUpDown.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown.Size = new System.Drawing.Size(165, 21);
            this.numericUpDown.TabIndex = 1000;
            this.numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown.ThousandsSeparator = true;
            this.numericUpDown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown.Enter += new System.EventHandler(this.numericUpDown_Enter);
            this.numericUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_KeyDown);
            // 
            // lblHideUpDownArrow
            // 
            this.lblHideUpDownArrow.BackColor = System.Drawing.Color.White;
            this.lblHideUpDownArrow.Location = new System.Drawing.Point(2, 1);
            this.lblHideUpDownArrow.Name = "lblHideUpDownArrow";
            this.lblHideUpDownArrow.Size = new System.Drawing.Size(16, 19);
            this.lblHideUpDownArrow.TabIndex = 1001;
            this.lblHideUpDownArrow.Visible = false;
            // 
            // checkbox
            // 
            this.checkbox.AutoSize = true;
            this.checkbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkbox.Location = new System.Drawing.Point(0, 19);
            this.checkbox.Name = "checkbox";
            this.checkbox.Size = new System.Drawing.Size(15, 22);
            this.checkbox.TabIndex = 1002;
            this.checkbox.UseVisualStyleBackColor = true;
            this.checkbox.Visible = false;
            this.checkbox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // pnlNumericUpDown
            // 
            this.pnlNumericUpDown.Controls.Add(this.lblHideUpDownArrow);
            this.pnlNumericUpDown.Controls.Add(this.numericUpDown);
            this.pnlNumericUpDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNumericUpDown.Location = new System.Drawing.Point(15, 19);
            this.pnlNumericUpDown.Name = "pnlNumericUpDown";
            this.pnlNumericUpDown.Size = new System.Drawing.Size(165, 22);
            this.pnlNumericUpDown.TabIndex = 1003;
            // 
            // chkShowDecimal
            // 
            this.chkShowDecimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowDecimal.AutoSize = true;
            this.chkShowDecimal.Location = new System.Drawing.Point(162, 4);
            this.chkShowDecimal.Name = "chkShowDecimal";
            this.chkShowDecimal.Size = new System.Drawing.Size(15, 14);
            this.chkShowDecimal.TabIndex = 1004;
            this.chkShowDecimal.UseVisualStyleBackColor = true;
            this.chkShowDecimal.CheckedChanged += new System.EventHandler(this.chkShowDecimal_CheckedChanged);
            // 
            // InputControl_Numeric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkShowDecimal);
            this.Controls.Add(this.pnlNumericUpDown);
            this.Controls.Add(this.checkbox);
            this.Controls.Add(this.label);
            this.Name = "InputControl_Numeric";
            this.Size = new System.Drawing.Size(180, 41);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.pnlNumericUpDown.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Label lblHideUpDownArrow;
        private System.Windows.Forms.CheckBox checkbox;
        private System.Windows.Forms.Panel pnlNumericUpDown;
        private System.Windows.Forms.CheckBox chkShowDecimal;
    }
}
