﻿namespace LIBUtil.Desktop.UserControls
{
    partial class InputControl_DateTimePicker
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
            this.datetimepicker = new System.Windows.Forms.DateTimePicker();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // datetimepicker
            // 
            this.datetimepicker.Checked = false;
            this.datetimepicker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.datetimepicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.datetimepicker.Location = new System.Drawing.Point(0, 20);
            this.datetimepicker.Name = "datetimepicker";
            this.datetimepicker.Size = new System.Drawing.Size(180, 21);
            this.datetimepicker.TabIndex = 4;
            this.datetimepicker.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.datetimepicker.ValueChanged += new System.EventHandler(this.dropdownlist_ValueChanged);
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(180, 18);
            this.label.TabIndex = 19;
            this.label.Text = "dropdownlist";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InputControl_DateTimePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label);
            this.Controls.Add(this.datetimepicker);
            this.Name = "InputControl_DateTimePicker";
            this.Size = new System.Drawing.Size(180, 41);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker datetimepicker;
        private System.Windows.Forms.Label label;
    }
}
