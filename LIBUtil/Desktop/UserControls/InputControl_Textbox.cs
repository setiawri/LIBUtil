﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIBUtil.Desktop.UserControls
{
    [DefaultEvent("isBrowseMode_Clicked")]
    public partial class InputControl_Textbox : InputControl
    {
        /*******************************************************************************************************/
        #region SETTINGS

        private const int ADDITIONALTEXTBOXROWHEIGHT = 15;
        private const int INITIALTEXTBOXHEIGHT = 21;
        private const int INITIALFORMHEIGHT = 41;

        #endregion SETTINGS
        /*******************************************************************************************************/
        #region PROPERTIES

        [Description("Show textbox only"), Category("_Custom")]
        public bool ShowTextboxOnly
        {
            get { return _showTextboxOnly; }
            set
            {
                _showTextboxOnly = value;
                label.Visible = !value;
                if (value)
                    this.Height = textbox.Height;
                else
                    this.Height = textbox.Height + label.Height;
            }
        }
        private bool _showTextboxOnly;

        [Description("Label Text"), Category("_Custom")]
        public string LabelText { get { return label.Text; } set { label.Text = value; } }

        [Description("Text"), Category("_Custom")]
        public string ValueText {
            get { return Util.sanitize(textbox); }
            set { textbox.Text = Util.sanitize(value); }
        }

        [Description("Max Length"), Category("_Custom")]
        public int MaxLength { get { return textbox.MaxLength; } set { textbox.MaxLength = value; } }

        [Description("Multi Line"), Category("_Custom")]
        public bool MultiLine { 
            get { return textbox.Multiline; } 
            set {
                textbox.Multiline = value;
                if (value)
                {
                    int additionalHeight = (_rowCount-1) * ADDITIONALTEXTBOXROWHEIGHT;
                    this.Height = INITIALFORMHEIGHT + additionalHeight;
                    textbox.Height = INITIALTEXTBOXHEIGHT + additionalHeight;
                }
                else
                {
                    this.Height = INITIALFORMHEIGHT;
                    textbox.Height = INITIALTEXTBOXHEIGHT;
                }
            } 
        }

        [Description("Is Browse Mode"), Category("_Custom")]
        public bool IsBrowseMode {
            get { return _isBrowseMode; }
            set
            {
                _isBrowseMode = value;
                ShowDeleteButton = value;
                if(_isBrowseMode)
                {
                    textbox.BackColor = Color.WhiteSmoke;
                    textbox.ReadOnly = true;
                }
                else
                {
                    textbox.BackColor = Color.White;
                    textbox.ReadOnly = false;
                }
            }
        }
        private bool _isBrowseMode = false;

        [Description("Password Char"), Category("_Custom")]
        public char PasswordChar
        {
            get { return textbox.PasswordChar; }
            set { textbox.PasswordChar = value; }
        }

        [Description("Row Count: must be 1 or more"), Category("_Custom")]
        public int RowCount { 
            get { return _rowCount; } 
            set {
                if (value < 1)
                    _rowCount = 1;
                else
                    _rowCount = value;

                //recalculate heights
                MultiLine = MultiLine;
            } 
        }
        private int _rowCount = 1;
        
        [Description("Show delete button"), Category("_Custom")]
        public bool ShowDeleteButton
        {
            get { return pnlDelete.Visible; }
            set { pnlDelete.Visible = value; }
        }
        
        public int Length { get { return textbox.TextLength; } }
        public Guid? ValueGuid = null;

        private ToolTip _textboxTooltip = new ToolTip();

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS
        
        public InputControl_Textbox()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        public void setValue(string textValue, Guid guidValue)
        {
            ValueText = textValue;
            ValueGuid = guidValue;
        }

        public override void reset()
        {
            textbox.Text = "";
            ValueGuid = null;
        }

        public override void focus()
        {
            textbox.Focus();
        }

        public override void setAsDefaultControl()
        {
            this.TabIndex = 0;
            textbox.TabIndex = 0;
        }

        public bool isEmpty() { 
            return string.IsNullOrWhiteSpace(Util.sanitize(textbox));
        }

        public bool isValueError(string message) { return Util.inputError<TextBox>(textbox, message); }

        public bool isNumeric()
        {
            return Util.isNumeric(textbox.Text);
        }

        //known problem: if user delete a comma at the second character of a number when number is 0,000 , error is thrown because curson index is negative
        public static void showInNumeric(TextBox textbox, bool allowDecimal, bool allowNegative)
        {
            if (!textbox.Focused)
                return; //focused is set to false to bypass this method fired when changing the value of the textbox
            else if (textbox.Text == string.Empty)
                return; //no value to process
            else if (!Util.isNumeric(textbox.Text) && !(textbox.Text.Length == 1 && textbox.Text == "-"))
            {
                Util.inputError<TextBox>(textbox, "Must be numeric");
                return;
            }

            string value = textbox.Text;
            int cursorIndex = textbox.SelectionStart;
            if (textbox.SelectionStart == 0)
            {
                //user removed a char and current position is at 0
                if (value.Substring(0, 1) == ",")
                    value = value.Substring(1);

                string newValue = Convert.ToDecimal(value).ToString("N" + getDecimalPoint(value));
                if (newValue == "0")
                    newValue = value;
                setValueAndLooseFocusToBypassEvent(textbox, newValue);
                setCursorPosition(textbox, cursorIndex);
            }
            else
            {
                //save original information
                char[] valueArray = value.ToArray();
                char newChar = valueArray[textbox.SelectionStart - 1];
                string newValue = value;
                string valueBeforeNewChar = value.Substring(0, cursorIndex - 1) + value.Substring(cursorIndex, value.Length - cursorIndex);

                //make sure does not exceed max limit of int

                if (newChar == '.')
                {
                    if (!allowDecimal || valueBeforeNewChar.Contains('.'))
                        newValue = valueBeforeNewChar;
                }
                else if (newChar == '-')
                {
                    if (!allowNegative || cursorIndex != 1 || valueBeforeNewChar.Contains('-'))
                        newValue = valueBeforeNewChar;
                }
                else if (!Util.isNumeric(newChar.ToString()))
                {
                    newValue = valueBeforeNewChar;
                }
                else
                {
                    //decimal info
                    int decimalPointCount = getDecimalPoint(value);
                    int indexOfDecimalPoint = value.IndexOf('.');

                    if (decimalPointCount > 2 && cursorIndex > indexOfDecimalPoint)
                        newValue = valueBeforeNewChar; //exceed 2 decimal point
                    else
                        newValue = Convert.ToDecimal(value).ToString("N" + decimalPointCount);
                }

                setValueAndLooseFocusToBypassEvent(textbox, newValue);
                setCursorPosition(textbox, cursorIndex + newValue.Length - value.Length);
            }
        }

        private static int getDecimalPoint(string value)
        {
            int decimalPointCount = 0;
            int indexOfDecimalPoint = value.IndexOf('.');
            if (indexOfDecimalPoint > -1)
                decimalPointCount = value.Substring(indexOfDecimalPoint + 1).Length;

            return decimalPointCount;
        }

        private static void setValueAndLooseFocusToBypassEvent(TextBox textbox, string value)
        {
            textbox.Enabled = false; //disabling moves focus to a different control. This is checked to bypass event handler
            textbox.Text = value;
            textbox.Enabled = true;
            textbox.Focus(); //set focus back to the control
        }

        private static void setCursorPosition(TextBox textbox, int index)
        {
            textbox.SelectionStart = index;
            textbox.SelectionLength = 0;
        }

        public void selectText()
        {
            textbox.Focus();
            textbox.SelectAll();
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS
            
        [Description("is Browse Mode Clicked Event"), Category("_Custom")]
        public event EventHandler isBrowseMode_Clicked;
        private void textbox_isBrowseMode_Clicked(object sender, EventArgs e)
        {
            if (IsBrowseMode && this.isBrowseMode_Clicked != null)
                this.isBrowseMode_Clicked(this, e);
        }

        [Description("TextChanged Event"), Category("_Custom")]
        public event EventHandler onTextChanged;
        private void textbox_TextChanged(object sender, EventArgs e)
        {
            if (onTextChanged != null)
                this.onTextChanged(this, e);
        }

        [Description("KeyDown Event"), Category("_Custom")]
        public event KeyEventHandler onKeyDown;
        private void textbox_onKeyDown(object sender, KeyEventArgs e)
        {
            if (onKeyDown != null)
                this.onKeyDown(this, e);
        }

        [Description("Focus Enter"), Category("_Custom")]
        public event EventHandler FocusEnter;
        private void textbox_FocusEnter(object sender, EventArgs e)
        {
            if (FocusEnter != null)
                this.FocusEnter(this, e);
        }

        private void textbox_MouseLeave(object sender, EventArgs e)
        {
            _textboxTooltip.RemoveAll();
        }

        private void textbox_MouseHover(object sender, EventArgs e)
        {
            _textboxTooltip.Show(textbox.Text, textbox);
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {
            textbox.Text = string.Empty;
            ValueGuid = null;
            textbox_TextChanged(textbox, EventArgs.Empty);
        }
        
        public static Form browseForm(Desktop.Forms.MasterData_v1_Form form, ref object sender)
        {
            Util.displayForm(null, form);
            if (form.DialogResult == DialogResult.OK)
            {
                Desktop.UserControls.InputControl_Textbox control = (Desktop.UserControls.InputControl_Textbox)sender;
                control.setValue(form.BrowsedItemSelectionDescription, form.BrowsedItemSelectionId);
                control.ParentForm.GetNextControl(control, true).Focus();
            }
            return form;
        }

        #endregion
        /*******************************************************************************************************/
    }
}
