﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Security.Cryptography;
using System.Web.Mvc;

using LIBUtil.Desktop.UserControls;

namespace LIBUtil
{
    public enum FormModes
    {
        Search,
        Add,
        Update,
        Browse,
        Normal
    }

    public enum Position
    {
        Top,
        Right,
        Bottom,
        Left,
        None
    }

    public enum Days
    {
        Sun,
        Mon,
        Tue,
        Wed,
        Thu,
        Fri,
        Sat
    }

    public enum AnimationEffect { Roll, Slide, Center, Blend }

    public class Util
    {
        public const string TYPE_ARRAY_NAME = "dbo.Array";
        public const string TYPE_ARRAY_STR = "value_str";
        public const string TYPE_ARRAY_INT = "value_int";

        public static List<string> sanitizeList = new List<string> { ";", "|", "'", "\"" };

        //placeholder for MDI parent.
        public static Form MDIParent; 

        /*******************************************************************************************************/
        //Experiment to pass a method (has a parameter) as a parameter
        public void passMethodWithParameters()
        {
            string parameter = "hmm";
            string i = ShowValue(() => methodToPass(parameter));
            Util.displayMessageBox("", i.ToString());
        }
        private T ShowValue<T>(Func<T> method)
        {
            return method();
        }
        private string methodToPass(string i)
        {
            return i;
        }

        /*******************************************************************************************************/
        #region DATABASE QUERY

        public static void addListParameter(System.Data.SqlClient.SqlCommand cmd, string name, DataTable data)
        {
            System.Data.SqlClient.SqlParameter param = cmd.Parameters.Add(name, SqlDbType.Structured);
            param.Value = data;
            param.TypeName = TYPE_ARRAY_NAME;
        }

        #endregion
        /*******************************************************************************************************/
        #region DESKTOP FORMS

        /// <summary><para>Desktop app use only.</para></summary>
        public static void disableFormResize(Form form)
        {
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static void setAsMDIParent(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            form.IsMdiContainer = true;
            MDIParent = form;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowInTaskbar = true;
            //form.ShowDialog();
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static Form displayMDIChild(Form form)
        {
            //check existing forms
            Type type = form.GetType();
            foreach(Form child in MDIParent.MdiChildren)
                if(child.GetType() == type)
                {
                    child.BringToFront();
                    child.Focus();
                    return child;
                }

            //display the new form
            form.MdiParent = MDIParent;
            form.StartPosition = FormStartPosition.CenterParent;
            if (!form.IsDisposed)
            {
                form.Show();
                form.BringToFront();
                return form;
            }

            return null;
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool displayForm(Form form) { return displayForm(null, form, false); }
        public static bool displayForm(Form parentFormToHide, Form form) { return displayForm(parentFormToHide, form, false); }
        public static bool displayForm(Form parentFormToHide, Form form, bool showInTaskbar)
        {
            if (parentFormToHide != null)
                parentFormToHide.Hide();
            
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowInTaskbar = showInTaskbar;
            form.ShowDialog();

            if (parentFormToHide != null && !parentFormToHide.IsDisposed)
                parentFormToHide.Show();

            return form.DialogResult == DialogResult.OK;
        }
        
        /// <summary><para>Desktop app use only.</para></summary>
        public static void setPosition(Form form, Position vertical, Position horizontal)
        {
            var screen = Screen.FromPoint(form.Location);

            int x = screen.WorkingArea.Left;
            int y = screen.WorkingArea.Top;

            if (horizontal == Position.Right)
                x = screen.WorkingArea.Right - form.Width;

            if (vertical == Position.Bottom)
                y = screen.WorkingArea.Bottom - form.Height;

            form.Location = new System.Drawing.Point(x, y);       
        }

        /// <summary>
        ///     Must be called after a column is frozen (if there is any). Otherwise, calculation to hide header checkbox won't be accurate
        /// </summary>
        public static Control addControlToHeader(DataGridView grid, DataGridViewColumn column, Control control)
        {
            setHeaderControlLocation(grid, column, control);

            //reposition checkbox if column width is changed
            grid.Controls.Add(control);
            grid.ColumnWidthChanged += (sender, e) => grid_ColumnWidthChanged(sender, e, column, control);

            //reposition checkbox if horizontal scrollbar is scrolled and hide if checkbox is positioned behind frozen columns
            grid.Scroll += (sender, e) => grid_Scroll_RepositionHeaderControlLocation(sender, e, grid, column, control);

            return control;
        }

        public static void setHeaderControlLocation(DataGridView grid, DataGridViewColumn column, Control control)
        {
            Rectangle rect = grid.GetCellDisplayRectangle(column.DisplayIndex, -1, false);
            // set control to the center of header cell. +1 pixel to position 
            rect.Y = rect.Location.Y + (rect.Height - control.Height) / 2 + 1;
            rect.X = rect.Location.X + (rect.Width - control.Width) / 2 + 2;
            control.Location = rect.Location;
        }

        public static void grid_Scroll_RepositionHeaderControlLocation(object sender, ScrollEventArgs e, DataGridView grid, DataGridViewColumn column, Control control)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                if (control.Location.X < 0)
                    control.Visible = false;
                else
                    control.Visible = true;

                Util.setHeaderControlLocation(grid, column, control);
            }
        }

        public static void grid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e, DataGridViewColumn column, Control control)
        {
            Util.setHeaderControlLocation((DataGridView)sender, column, control);
        }

        public static void removeSelectedGridviewRows(DataGridView gridview)
        {
            for (int i = gridview.Rows.Count - 1; i >= 0; i--)
                if (gridview.SelectedRows.Contains(gridview.Rows[i]))
                    gridview.Rows.RemoveAt(i);
        }
        
        public static void enableControls(bool value, params Panel[] panels)
        {
            foreach (Panel panel in panels)
                foreach (Control control in panel.Controls)
                    control.Enabled = value;
        }

        public static void setControlsVisibility(bool value, params object[] objects)
        {
            foreach (object obj in objects)
            {
                if (obj.GetType() == typeof(Control) || obj.GetType().IsSubclassOf(typeof(Control)))
                    ((Control)obj).Visible = value;
                else if (obj.GetType() == typeof(DataGridViewColumn))
                    ((DataGridViewColumn)obj).Visible = value;
                else if (obj.GetType() == typeof(InputControl) || obj.GetType().IsSubclassOf(typeof(InputControl)))
                    ((InputControl)obj).Visible = value;
            }
        }

        #endregion
        /*******************************************************************************************************/
        #region DESKTOP MESSAGE BOX

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool displayMessageBoxYesNo(string message) { return MessageBox.Show(message, "", MessageBoxButtons.YesNo) == DialogResult.Yes; }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool displayMessageBoxSuccess(string message) { displayMessageBox("SUCCESS:", message); return true; }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool displayMessageBoxError(string message) { displayMessageBox("ERROR:", message); return false; }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool displayMessageBox(string message) { return displayMessageBox("", message); }
        public static bool displayMessageBox(string prefix, string message)
        {
            if (string.IsNullOrEmpty(message))
                return false;
            else
            {
                MessageBox.Show(prefix + " " + message);
                return true;
            }
        }

        /// <summary>
        /// <para>Desktop app use only.</para>
        /// <para>T is type of control.Currently supports: TextBox, ComboBox, DateTimePicker</para>
        /// </summary>
        public static bool inputError<T>(object obj, string errorMessage)
        {
            displayMessageBoxError(errorMessage);
            if (typeof(T) == typeof(TextBox))
            {
                TextBox txt = (TextBox)obj;
                txt.SelectAll();
                txt.Focus();
            }
            else if (typeof(T) == typeof(ComboBox))
            {
                ComboBox cb = (ComboBox)obj;
                cb.Focus();
            }
            else if (typeof(T) == typeof(DateTimePicker))
            {
                DateTimePicker dtp = (DateTimePicker)obj;
                dtp.Focus();
            }

            return false;
        }

        #endregion
        /*******************************************************************************************************/
        #region NULLABLE WRAPPER

        /// <summary><para></para></summary>
        public static object wrapNullable(object value)
        {
            if (value != null && value.GetType() == typeof(string) && string.IsNullOrEmpty((string)value))
                return DBNull.Value;
            else if (value != null && value.GetType() == typeof(Guid) && value.ToString() == (new Guid()).ToString())
                return DBNull.Value;
            else
                return value ?? DBNull.Value;
        }

        /// <summary><para></para></summary>
        public static T wrapClickedCellValueNullable<T>(DataGridViewColumn column, DataGridViewCellEventArgs e)
        {
            return wrapNullable<T>(column.DataGridView.Rows[e.RowIndex].Cells[column.Name].Value);
        }

        /// <summary><para></para></summary>
        public static T wrapFirstSelectedRowValueNullable<T>(DataGridViewColumn column)
        {
            object obj = null;
            if (column.DataGridView.SelectedRows.Count > 0)
                obj = column.DataGridView.SelectedRows[0].Cells[column.Name].Value;
            return wrapNullable<T>(obj);
        }

        /// <summary><para></para></summary>
        public static T wrapNullable<T>(DataRow row, string columnName)
        {
            object obj = null;
            if (row != null)
                obj = row[columnName];
            return wrapNullable<T>(obj);
        }

        /// <summary><para></para></summary>
        public static T wrapNullable<T>(DataGridViewRow row, string columnName)
        {
            object obj = null;
            if (row != null)
                obj = row.Cells[columnName].Value;
            return wrapNullable<T>(obj);
        }

        /// <summary><para></para></summary>
        public static T wrapNullable<T>(object value)
        {
            object val = wrapNullable(value);
            if (val == null || val == DBNull.Value)
            {
                if (typeof(T) == typeof(Guid?))
                {
                    object obj = null;
                    return (T)obj;
                }
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(TimeSpan) && val.GetType() == typeof(DateTime))
            {
                val = ((DateTime)val).TimeOfDay;
            }
            else if (typeof(T) == typeof(TimeSpan?))
            {
                TimeSpan t;
                if (TimeSpan.TryParse(value.ToString(), out t))
                    return (T)Convert.ChangeType(t, Nullable.GetUnderlyingType(typeof(T)));
            }
            else if (typeof(T) == typeof(Guid?))
                return (T)val;
            else if (Nullable.GetUnderlyingType(typeof(T)) != null)
                return (T)Convert.ChangeType(val, Nullable.GetUnderlyingType(typeof(T)));

            return (T)Convert.ChangeType(val, typeof(T));            
        }

        #endregion
        /*******************************************************************************************************/
        #region CLIPBOARD

        public static T getClipboardText<T>()
        {
            object value = Clipboard.GetText();
            if (typeof(T) == typeof(Guid))
            {
                Guid guidValue;
                Guid.TryParse(Clipboard.GetText(), out guidValue);
                value = guidValue;
            }

            return Util.wrapNullable<T>(value);
        }

        #endregion 
        /*******************************************************************************************************/
        #region STRING MANIPULATORS

        public static string getFirstWord(string value)
        {
            string result = System.Text.RegularExpressions.Regex.Replace(value.Split()[0], @"[^0-9a-zA-Z\ ]+", "");
            if (string.IsNullOrEmpty(result))
                result = value;

            return result;
        }

        /// <summary> append the first character to the end </summary>
        public static string getNextRollingText(string value)
        {
            return value.Substring(1) + value.Substring(0, 1);
        }

        /// <summary>
        /// <para>string is sanitized to prevent sql injection</para>
        /// </summary>
        public static string sanitizeString(string str) { return sanitize(str); }
        public static string sanitize(string str)
        {
            foreach(string item in sanitizeList)
                str = (str ?? string.Empty).Replace(item, "");

            return str.Trim();
        }

        /// <summary>
        /// <para>Content of multiple textboxes are sanitized to prevent sql injection</para>
        /// </summary>
        public static string sanitize(params TextBox[] textboxes)
        {
            foreach (TextBox textbox in textboxes)
                textbox.Text = sanitize(textbox.Text);

            if (textboxes.Length == 1)
                return textboxes[0].Text;
            else
                return null;
        }

        /// <summary>
        /// <para>Content of multiple textboxes are sanitized to prevent sql injection</para>
        /// </summary>
        public static void sanitize(params Desktop.UserControls.InputControl_Textbox[] controls)
        {
            foreach (Desktop.UserControls.InputControl_Textbox control in controls)
                sanitize(control.textbox);
        }

        /// <summary>
        /// <para>Textbox content is sanitized to prevent sql injection</para>
        /// </summary>
        public static string sanitizeAndNullifyIfEmpty(TextBox textbox)
        {
            string text = sanitize(textbox);
            if (string.IsNullOrEmpty(text))
                return null;
            else
                return text;
        }

        /// <summary>
        /// <para>newText is added to originalText if applicable and separated by specified delimiter</para>
        /// </summary>
        public static string webAppend(string originalText, string newText, string delimiter) { return append(originalText, newText, delimiter).Replace(Environment.NewLine, "<BR>"); }
        public static string append(string originalText, string newText, string delimiter)
        {
            if (string.IsNullOrEmpty(originalText) && !string.IsNullOrEmpty(newText))
                return newText.Trim();
            else if (string.IsNullOrEmpty(newText))
                return originalText.Trim();
            else
            {
                if (!string.IsNullOrEmpty(originalText)) originalText += delimiter;
                return originalText += newText.Trim();
            }
        }

        /// <summary>
        /// <para>oldValue is compared to newValue and if different, the values are put together using the format provided and appended to originalText</para>
        /// <para>if value is null, then empty string is used</para>
        /// </summary>
        //public static string appendChange(string originalText, object oldValue, object newValue, string format)
        //{
        //    return appendChange(originalText, oldValue, newValue, format, ",");
        //}
        //public static string appendChangeInNewLine(string originalText, object oldValue, object newValue, string format)
        //{
        //    return appendChange(originalText, oldValue, newValue, format, Environment.NewLine);
        //}
        public static string webAppendChange(string originalText, object oldValue, object newValue, string format) { return appendChange(originalText, oldValue, newValue, format).Replace(Environment.NewLine, "<BR>"); }
        public static string appendChange(string originalText, object oldValue, object newValue, string format)
        {
            if (oldValue != null & newValue != null && oldValue.GetType() == typeof(decimal) && newValue.GetType() == typeof(decimal))
            {
                if ((decimal)oldValue != (decimal)newValue)
                    return append(originalText, String.Format(format, oldValue, newValue), Environment.NewLine);
            }
            else if (oldValue != null & newValue != null && oldValue.GetType() == typeof(int) && newValue.GetType() == typeof(int))
            {
                if ((int)oldValue != (int)newValue)
                    return append(originalText, String.Format(format, oldValue, newValue), Environment.NewLine);
            }
            else if (oldValue != null & newValue != null && oldValue.GetType() == typeof(DateTime) && newValue.GetType() == typeof(DateTime))
            {
                if (!oldValue.Equals(newValue))
                    return append(originalText, String.Format(format, DateTime.Parse(oldValue.ToString()), DateTime.Parse(newValue.ToString())), Environment.NewLine);
            }
            else
            {
                string oldV = "";
                string newV = "";
                if (oldValue != null) oldV = oldValue.ToString();
                if (newValue != null) newV = newValue.ToString();

                if (oldV != newV)
                    return append(originalText, String.Format(format, oldV, newV), Environment.NewLine);
            }

            return originalText;
        }

        public static string reverse(string value)
        {
            char[] charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string RandomString(int length)
        {
            return Guid.NewGuid().ToString().Substring(0, length);
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //return chars.Substring(0, new Random().Next(chars.Length-1));
            //return new string(Enumerable.Repeat(chars, length)
            //  .Select(s => s[new Random().Next(s.Length-10)]).ToArray());
        }
        #endregion
        /*******************************************************************************************************/
        #region DESKTOP DATAGRIDVIEW

        public static DataGridView getFocusedDataGridView(Form form)
        {
            Control control = getFocusedControl(form);
            if (control.GetType() == typeof(DataGridView))
                return (DataGridView)control;
            else
                return null;
        }

        public static bool copyContentToClipboardIfGridview(Form form) { return copyContentToClipboardIfGridview(form, true, true, false); }
        public static bool copyContentToClipboardIfGridview(Form form, bool includeHeaders, bool copyAllRows, bool copyCellOnly)
        {
            DataGridView datagridview = Util.getFocusedDataGridView(form);
            if (datagridview == null)
                return false;
            else
            {
                DataGridViewClipboardCopyMode copyMode = datagridview.ClipboardCopyMode;
                if (includeHeaders)
                    datagridview.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                else
                    datagridview.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;

                //save original settings
                DataGridViewSelectedRowCollection selectedRows = datagridview.SelectedRows; ;
                bool isMultiSelect = datagridview.MultiSelect;
                DataGridViewSelectionMode selectionMode = datagridview.SelectionMode;

                if (copyAllRows)
                {
                    datagridview.MultiSelect = true;
                    datagridview.SelectAll();
                }
                else if(copyCellOnly)
                {
                    datagridview.MultiSelect = false;
                    datagridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
                }

                DataObject obj = datagridview.GetClipboardContent();
                if (obj != null)
                    Clipboard.SetDataObject(obj);

                //reapply original settings
                datagridview.MultiSelect = isMultiSelect;
                datagridview.SelectionMode = selectionMode;
                if (copyAllRows || copyCellOnly)
                {
                    //reapply selections
                    datagridview.ClearSelection();
                    foreach (DataGridViewRow row in selectedRows)
                        row.Selected = true;
                }
                datagridview.ClipboardCopyMode = copyMode;

                return true;
            }
        }

        /// <summary>
        ///     Must be called after a column is frozen (if there is any). Otherwise, calculation to hide header checkbox won't be accurate
        /// </summary>
        public static CheckBox addHeaderCheckbox(DataGridView grid, DataGridViewColumn column, string controlName, EventHandler checkedChangedHandler)
        {
            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Name = controlName;
            //datagridview[0, 0].ToolTipText = "sdfsdf";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.BackColor = Color.Transparent;

            setHeaderCheckboxLocation(grid, column, checkboxHeader);

            checkboxHeader.CheckedChanged += new EventHandler(checkedChangedHandler);

            //reposition checkbox if column width is changed
            grid.Controls.Add(checkboxHeader);
            grid.ColumnWidthChanged += (sender, e) => grid_ColumnWidthChanged(sender, e, column, checkboxHeader);

            //reposition checkbox if horizontal scrollbar is scrolled and hide if checkbox is positioned behind frozen columns
            grid.Scroll += (sender, e) => grid_Scroll_RepositionHeaderCheckboxLocation(sender, e, grid, column, checkboxHeader);

            return checkboxHeader;
        }

        public static void setHeaderCheckboxLocation(DataGridView grid, DataGridViewColumn column, CheckBox checkboxHeader)
        {
            Rectangle rect = grid.GetCellDisplayRectangle(column.DisplayIndex, -1, false);
            // set checkbox header to center of header cell. +1 pixel to position 
            rect.Y = rect.Location.Y + (rect.Height - checkboxHeader.Height) / 2 + 1;
            rect.X = rect.Location.X + (rect.Width - checkboxHeader.Width) / 2 + 2;
            checkboxHeader.Location = rect.Location;
        }

        public static void grid_Scroll_RepositionHeaderCheckboxLocation(object sender, ScrollEventArgs e, DataGridView grid, DataGridViewColumn column, CheckBox checkboxHeader)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                if (checkboxHeader.Location.X < 0)
                    checkboxHeader.Visible = false;
                else
                    checkboxHeader.Visible = true;

                Util.setHeaderCheckboxLocation(grid, column, checkboxHeader);
            }
        }

        public static void toggleCheckboxColumn(DataGridView grid, DataGridViewColumn column, CheckBox headerCheckbox)
        {
            int idx = -1;
            if (grid.SelectedRows.Count > 0)
                idx = grid.SelectedRows[0].Index;
            grid.CurrentCell = null; //fix problem where previously manually toggled checkbox doesn't display new value when programmatically changed using header checkbox
            if (idx > -1)
                grid.Rows[idx].Selected = true;

            foreach (DataGridViewRow row in grid.Rows)
                row.Cells[column.Name].Value = headerCheckbox.Checked;

        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static void displayContextMenu(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                grid.ClearSelection();
                grid.Rows[e.RowIndex].Selected = true;
            }
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool clickDataGridViewCheckbox(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
            bool value = !getCheckboxValue(row, e.ColumnIndex);
            row.Cells[e.ColumnIndex].Value = value;
            return value;
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool setCheckboxValue(object sender, DataGridViewCellEventArgs e, bool value)
        {
            DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[e.ColumnIndex];
            if (cell.Value != null)
                cell.Value = value;
            return value;
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool setCheckboxValue(DataGridViewRow row, DataGridViewCheckBoxColumn column, bool value)
        {
            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[column.Index];
            if (cell.Value != null)
                cell.Value = value;
            return value;
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool getCheckboxValue(object sender, DataGridViewCellEventArgs e)
        {
            return getCheckboxValue(((DataGridView)sender).Rows[e.RowIndex], e.ColumnIndex);
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static bool getCheckboxValue(DataGridViewRow row, DataGridViewColumn column) { return getCheckboxValue(row, column.Index); }
        public static bool getCheckboxValue(DataGridViewRow row, int columnIndex)
        {
            bool value = false;
            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[columnIndex];
            if (cell.Value != null)
                Boolean.TryParse(cell.Value.ToString(), out value);
            return value;
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static T getClickedRowValue<T>(object sender, DataGridViewCellEventArgs e, DataGridViewColumn column) { return Util.wrapNullable<T>(getClickedRowValue(sender, e, column.Index)); }
        public static object getClickedRowValue(object sender, DataGridViewCellEventArgs e, DataGridViewColumn column) { return getClickedRowValue(sender, e, column.Index); }
        public static object getClickedRowValue(object sender, DataGridViewCellEventArgs e) { return getClickedRowValue(sender, e, e.ColumnIndex); }
        public static object getClickedRowValue(object sender, DataGridViewCellEventArgs e, int columnIndex)
        {
            return ((DataGridView)sender).Rows[e.RowIndex].Cells[columnIndex].Value;
        }
        
        /// <summary><para>Desktop app use only.</para></summary>
        public static Guid getSelectedRowID(DataGridView grid, DataGridViewColumn IdColumn) { return (Guid)getSelectedRowValue(grid, IdColumn); }
        public static Guid getSelectedRowID(object sender, DataGridViewColumn IdColumn) { return (Guid)getSelectedRowValue(sender, IdColumn); }
        public static object getSelectedRowValue(object sender, DataGridViewColumn column) { return getSelectedRowValue((DataGridView)sender, column); }
        public static object getSelectedRowValue(DataGridView grid, DataGridViewColumn column)
        {
            if (grid.Rows.Count > 0)
                return grid.SelectedRows[0].Cells[column.Name].Value;
            else
                return null;
        }

        public static bool selectedItemIsNotNull(DataGridView dgv, DataGridViewColumn column)
        {
            object value = Util.getSelectedRowValue(dgv, column);
            return value != null && value != DBNull.Value;
        }

        public static object getRowValue(DataGridViewRow row, DataGridViewColumn column)
        {
            return row.Cells[column.Name].Value;
        }

        public static void setRowValue(object sender, DataGridViewCellEventArgs e, DataGridViewColumn column, object value) { setRowValue(((DataGridView)sender).Rows[e.RowIndex], column, value); }
        public static void setRowValue(DataGridViewRow row, DataGridViewColumn column, object value)
        {
            row.Cells[column.Name].Value = value;
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static object setSelectedRowValue(DataGridView grid, DataGridViewColumn column, object value)
        {
            return grid.SelectedRows[0].Cells[column.Name].Value = value;
        }

        /// <summary><para>Desktop app use only.</para></summary>er, e, column.Index); }
        public static object getClickedCellValue(object sender, DataGridViewCellEventArgs e) { return ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value; }

        /// <summary><para>Desktop app use only.</para></summary>er, e, column.Index); }
        public static void selectClickedCellContent(object sender, DataGridViewCellEventArgs e) { ((DataGridView)sender).BeginEdit(true); }

        /// <summary><para>Desktop app use only.</para></summary>
        public static void disableSort(DataGridView grid)
        {
            grid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        /// <summary><para>Desktop app use only.</para></summary>
        public static void setColumnFormat(DataGridViewColumn column, string format, DataGridViewContentAlignment alignment)
        {
            column.DefaultCellStyle.Alignment = alignment;
            column.DefaultCellStyle.Format = format;
        }

        /// <summary><para></para></summary>
        public static void populateDataGridView(DataGridView dgv, object data)
        {
            DataGridViewColumn sortColumn = dgv.SortedColumn;
            System.Windows.Forms.SortOrder sortDirection = dgv.SortOrder;

            dgv.DataSource = data;

            if (sortDirection != System.Windows.Forms.SortOrder.None)
                dgv.Sort(sortColumn, Util.getListSortDirection(sortDirection));
        }

        /// <summary><para></para></summary>
        public static ListSortDirection getListSortDirection(System.Windows.Forms.SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case System.Windows.Forms.SortOrder.Ascending:
                    return ListSortDirection.Ascending;
                case System.Windows.Forms.SortOrder.Descending:
                    return ListSortDirection.Descending;
                default:
                    return ListSortDirection.Ascending;
            }
        }

        /// <summary><para></para></summary>
        public static void setFirstDisplayedScrollingRowIndex(DataGridView grid, int topRowIndex, int selectedRowIndex)
        {
            if (grid.Rows.Count > 0 && topRowIndex > -1)
            {
                if (grid.Rows.Count > topRowIndex)
                    grid.FirstDisplayedScrollingRowIndex = topRowIndex;
                else
                    grid.FirstDisplayedScrollingRowIndex = grid.Rows.Count - 1;
            }

            if (selectedRowIndex > -1 && grid.Rows.Count > 0)
            {
                grid.ClearSelection();
                if (selectedRowIndex < grid.Rows.Count)
                    grid.Rows[selectedRowIndex].Selected = true;
                else
                {
                    if (topRowIndex < grid.Rows.Count)
                        grid.Rows[topRowIndex].Selected = true;
                    else
                        grid.Rows[grid.Rows.Count - 1].Selected = true;
                }
            }
        }
        
        /// <summary><para></para></summary>
        public static bool isColumnMatch(object sender, DataGridViewCellEventArgs e, params DataGridViewColumn[] column)
        {
            var senderGrid = (DataGridView)sender;
            if (e.RowIndex > -1 && senderGrid.Columns[e.ColumnIndex].GetType() == column[0].GetType() && column.Contains(senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn))
                return true;
            else
                return false;
        }

        /// <summary><para></para></summary>
        public static bool isColumnMatch(object sender, DataGridViewDataErrorEventArgs e, DataGridViewColumn column)
        {
            var senderGrid = (DataGridView)sender;
            if (e.RowIndex > -1 && senderGrid.Columns[e.ColumnIndex].GetType() == column.GetType())
                if (senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn == column)
                    return true;

            return false;
        }

        /// <summary><para>set columnAutoSizeMode to null to retain its original value</para></summary>
        public static void setGridviewColumnWordwrap(DataGridViewColumn column, DataGridViewAutoSizeColumnMode? columnAutoSizeMode)
        {
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if(columnAutoSizeMode != null)
                column.AutoSizeMode = (DataGridViewAutoSizeColumnMode)columnAutoSizeMode;
            column.DataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        /// <summary><para></para></summary>
        public static void clearWhenSelected(DataGridView dgv)
        {
            dgv.SelectionChanged += new System.EventHandler(clearDataGridViewOnSelectionChanged);
        }

        /// <summary><para>For DataGridView</para></summary>
        public static void clearDataGridViewOnSelectionChanged(object sender, EventArgs e)
        {
            ((DataGridView)sender).ClearSelection(); //disable cell color change when user click on it
        }

        public static void updateFontStyle(DataGridViewColumn column, FontStyle fontStyle)
        {
            Font font;
            if (column.DefaultCellStyle.Font != null)
                font = column.DefaultCellStyle.Font;
            else
                font = column.DataGridView.FindForm().Font;
            
            column.DefaultCellStyle.Font = new Font(font.FontFamily, font.Size, fontStyle);
        }

        public static void updateForeColor(DataGridViewColumn column, Color color)
        {
            column.DefaultCellStyle.ForeColor = color;
            column.DefaultCellStyle.SelectionForeColor = color;
        }

        public static void updateForeColorAndStyle(DataGridViewColumn column, Color color, FontStyle fontStyle)
        {
            updateFontStyle(column, fontStyle);
            updateForeColor(column, color);
        }

        public static void setGridviewDataSource(DataGridView grid, object data) { setGridviewDataSource(grid, data, false, 0, true, true); }
        public static void setGridviewDataSource(DataGridView grid, bool rememberRowPosition, bool reapplySort, object data) { setGridviewDataSource(grid, data, false, 0, rememberRowPosition, reapplySort); }        
        public static void setGridviewDataSource(DataGridView grid, object data, bool showProgressBar, int TimeoutSeconds, bool rememberRowPosition, bool reapplySort)
        {
            //save top row index
            int topRowIndex = grid.FirstDisplayedScrollingRowIndex;
            int selectedRowIndex = -1;
            if (grid.Rows.Count > 0 && grid.SelectedRows.Count > 0)
                selectedRowIndex = grid.SelectedRows[0].Index;

            //save sorting
            DataGridViewColumn sortColumn = grid.SortedColumn;
            ListSortDirection sortOrder = ListSortDirection.Ascending;
            if (grid.SortOrder == System.Windows.Forms.SortOrder.Descending) sortOrder = ListSortDirection.Descending;

            //improve performance while binding datasource
            DataGridViewRowHeadersWidthSizeMode dataGridViewRowHeadersWidthSizeMode = grid.RowHeadersWidthSizeMode;
            DataGridViewAutoSizeRowsMode dataGridViewAutoSizeRowsMode = grid.AutoSizeRowsMode;
            DataGridViewAutoSizeColumnsMode dataGridViewAutoSizeColumnsMode = grid.AutoSizeColumnsMode;
            DataGridViewColumnHeadersHeightSizeMode dataGridViewColumnHeadersHeightSizeMode = grid.ColumnHeadersHeightSizeMode;
            grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; 
            //grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; //removed because this is causing long load time during update in customer form.
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //update datasource 
            if (!showProgressBar)
                grid.DataSource = data;
            else
                new BackgroundProcess(TimeoutSeconds, grid, data).run();

            //reapply original settings
            grid.RowHeadersWidthSizeMode = dataGridViewRowHeadersWidthSizeMode;
            //grid.AutoSizeRowsMode = dataGridViewAutoSizeRowsMode;
            grid.AutoSizeColumnsMode = dataGridViewAutoSizeColumnsMode;
            grid.ColumnHeadersHeightSizeMode = dataGridViewColumnHeadersHeightSizeMode;

            //reapply sorting
            if (reapplySort && sortColumn != null)
                grid.Sort(sortColumn, sortOrder);

            //display top row index 
            if (rememberRowPosition)
                Util.setFirstDisplayedScrollingRowIndex(grid, topRowIndex, selectedRowIndex);
        }

        #endregion
        /*******************************************************************************************************/
        #region ENUMS

        public static T parseEnum<T>(object value)
        {
            if (value == null)
                return default(T);

            try
            {
                return (T)Enum.Parse(typeof(T), value.ToString());
            }
            catch
            {
                return GetEnumFromDescription<T>(value.ToString());
            }
        }

        public static T GetEnumFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }

        public static DataTable parseEnum<T>(DataTable dataTable, string targetColumnName, string enumIDColumn)
        {
            if (!dataTable.Columns.Contains(targetColumnName))
                Util.addColumnToTable<string>(dataTable, targetColumnName, "");

            foreach (DataRow dr in dataTable.Rows)
                if (dr[enumIDColumn] != DBNull.Value)
                    dr[targetColumnName] = Util.GetEnumDescription((Enum)(object)Util.parseEnum<T>(dr[enumIDColumn]));

            return dataTable;
        }

        public static IEnumerable<T> GetEnumItems<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string GetEnumDescription(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static string GetEnumDescription<T>(object value)
        {
            return Util.GetEnumDescription((Enum)(object)Util.parseEnum<T>(value));
        }

        public static string GetEnumName<T>(int value)
        {
            return Enum.GetName(typeof(T), value);
        }

        #endregion
        /*******************************************************************************************************/
        #region DATATABLE

        /// <summary><para></para></summary>
        public static DataRow getFirstRow(DataTable datatable)
        {
            if (datatable != null && datatable.Rows.Count > 0)
                return datatable.Rows[0];
            else
                return null;
        }

        /// <summary><para></para></summary>
        public static DataTable setDataTablePrimaryKey(DataTable dt, string primaryKeyColumnName)
        {
            DataColumn[] keyColumns = new DataColumn[1];
            keyColumns[0] = dt.Columns[primaryKeyColumnName];
            dt.PrimaryKey = keyColumns;
            return dt;
        }

        public static List<T> convertToList<T>(DataTable datatable, string columnName)
        {
            return datatable.Rows.OfType<DataRow>()
                        .Select(dr => dr.Field<T>(columnName)).ToList();
        }

        public static DataTable copyValuesToArrayTable(params Guid?[] values)
        {
            DataTable datatable = createArrayTable();
            foreach (Guid? value in values)
                if (value != null) datatable.Rows.Add(value.ToString(), null);
            return datatable;
        }

        public static DataTable createArrayTable()
        {
            DataTable datatable = new DataTable();
            addColumnToTable<string>(datatable, TYPE_ARRAY_STR, "");
            addColumnToTable<int>(datatable, TYPE_ARRAY_INT, 0);
            return datatable;
        }

        public static DataTable copyIntToArrayTable(params int[] values)
        {
            DataTable datatable = createArrayTable();
            if (values != null)
                foreach (int value in values)
                    datatable.Rows.Add(null, value);
            return datatable;
        }

        public static DataColumn addColumnToTable<T>(DataTable dt, string columnName, object defaultValue)
        {
            DataColumn dc = new DataColumn(columnName, typeof(T));

            if (typeof(T) == typeof(bool))
                dc.DefaultValue = Convert.ToBoolean(defaultValue);
            else if (typeof(T) == typeof(int))
                dc.DefaultValue = Convert.ToInt32(defaultValue);
            else if (typeof(T) == typeof(decimal))
                dc.DefaultValue = Convert.ToDecimal(defaultValue);

            dt.Columns.Add(dc);
            return dc;
        }

        public static string compileQuickSearchFilter(string keyword, string[] fieldNames)
        {
            string filter = "";
            foreach (string word in keyword.Split())
            {
                if (!string.IsNullOrEmpty(word.Trim()))
                {
                    foreach (string fieldname in fieldNames)
                        filter = Util.append(filter, string.Format("{0} LIKE '%{1}%'", fieldname, word), " OR ");

                    //filter = Util.append(filter, string.Format("CONVERT({0},System.String) LIKE '%{1}%'", fieldname, word), "OR");
                }
            }
            return filter;
        }

        /// <summary><para></para></summary>
        public static DataTable sortData(DataTable datatable, string column1Name, System.Windows.Forms.SortOrder? column1Direction, string column2Name, System.Windows.Forms.SortOrder? column2Direction)
        {
            DataView dvw = datatable.DefaultView;
            dvw.Sort = compileSortPhrase(column1Name, column1Direction, column2Name, column2Direction);
            return dvw.ToTable();
        }

        /// <summary><para></para></summary>
        private static string compileSortPhrase(string column1Name, System.Windows.Forms.SortOrder? column1Direction, string column2Name, System.Windows.Forms.SortOrder? column2Direction)
        {
            string str = "";
            if (!string.IsNullOrEmpty(column1Name))
                str = Util.append(str, string.Format("{0} {1}", column1Name, getDirectionString(column1Direction)), ",");
            if (!string.IsNullOrEmpty(column2Name))
                str = Util.append(str, string.Format("{0} {1}", column2Name, getDirectionString(column2Direction)), ",");
            return str;
        }

        /// <summary><para></para></summary>
        private static string getDirectionString(System.Windows.Forms.SortOrder? direction)
        {
            if (direction == System.Windows.Forms.SortOrder.Descending)
                return "DESC ";
            else
                return "ASC";
        }

        public static DataView getDataView(object data)
        {
            if (data.GetType() == typeof(DataView))
                return (DataView)data;
            else
                return ((DataTable)data).DefaultView;
        }

        public static DataTable getDataTable(object data)
        {
            if (data.GetType() == typeof(DataTable))
                return (DataTable)data;
            else
                return ((DataView)data).Table;
        }

        /// <summary></summary>
        /// <para>operation: example: SUM, COUNT, AVG</para>
        public static decimal compute(DataTable dt, string operation, string columnName, string filter)
        {
            if (dt == null || dt.Columns.IndexOf(columnName) == -1) return 0;

            object sum = dt.Compute(String.Format("{0}({1})", operation, columnName), filter);
            if (sum != DBNull.Value)
                return Convert.ToDecimal(sum.ToString());
            else
                return 0;
        }

        #endregion
        /*******************************************************************************************************/
        #region TREEVIEW

        #endregion
        /*******************************************************************************************************/
        #region NUMBERS

        public static decimal zeroNonNumericString(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch { return 0; }
        }

        public static bool isNumeric(string str)
        {
            decimal output;
            return Decimal.TryParse(str, out output);
        }

        #endregion
        /*******************************************************************************************************/
        #region BOOLEANS

        public static bool convertToBool(int value)
        {
            if (value == 0)
                return false;
            else
                return true;
        }

        public static int convertToInt(bool value)
        {
            if (value)
                return 1;
            else
                return 0;
        }

        #endregion
        /*******************************************************************************************************/
        #region TOOLSTRIPMENU

        public static void setAvailability(params ToolStripMenuItem[] menus)
        {
            foreach (ToolStripMenuItem menu in menus)
            {
                bool available = false;
                foreach (ToolStripDropDownItem item in menu.DropDownItems)
                {
                    if (item.Available)
                    {
                        available = true;
                        break;
                    }
                }
                menu.Available = available;
            }
        }

        #endregion
        /*******************************************************************************************************/
        #region NETWORKING

        public static bool isValidIPv4(string ipString)
        {
            if (ipString.Count(c => c == '.') != 3)
                return false;

            System.Net.IPAddress ipAddress;
            return System.Net.IPAddress.TryParse(ipString, out ipAddress);
        }

        public static string getIPv4()
        {
            System.Net.IPAddress[] addresses = Array.FindAll(System.Net.Dns.GetHostEntry(string.Empty).AddressList, a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            return addresses[0].ToString();
        }

        public static string getMACAddress()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
        }

        #endregion
        /*******************************************************************************************************/
        #region DESKTOP CONFIG FILE

        //not working because of user permission issue. configuration opens config in temporary folder.
        /// <summary><para>Desktop app use only.</para></summary>
        //public static void updateConfigVariable(string key, string value)
        //{
        //    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
        //    {
        //        config.AppSettings.Settings.Remove(key);
        //    }

        //    config.AppSettings.Settings.Add(key, value);
        //    config.Save(ConfigurationSaveMode.Modified);
        //    ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        //}

        public static string getAppConfigConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static string getConfigVariable(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static bool isMachineNameEqualConfigVariable(string key)
        {
            return Environment.MachineName.ToUpper() == getConfigVariable(key).ToUpper();
        }

        public static string saveAppData(string filename, string value)
        {
            if (filename == null)
                return "";

            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);
            System.IO.File.WriteAllText(filepath, value);
            return value;
        }

        public static string getAppData(string filename)
        {
            if(!string.IsNullOrWhiteSpace(filename))
            {
                string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);
                if (File.Exists(filepath))
                    return File.ReadAllText(filepath);
            }

            return null;
        }

        public static void removeAppData(string filename)
        {
            string filepath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);
            System.IO.File.Delete(filepath);
        }

        #endregion
        /*******************************************************************************************************/
        #region SERVER

        public static DateTime getServerTime()
        {
            DateTime timestamp = new DateTime();
            try
            {
                using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DBConnection.ConnectionString))
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SERVER_get_timestamp", sqlConnection))
                using (System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlParameter return_value = cmd.Parameters.Add("@timestamp", SqlDbType.DateTime);
                    return_value.Direction = ParameterDirection.Output;

                    if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                    cmd.ExecuteNonQuery();

                    timestamp = Convert.ToDateTime(return_value.Value);
                }
            }
            catch { }

            return timestamp;

            //CREATE PROCEDURE [dbo].[SERVER_get_timestamp]
            //    @timestamp datetime OUTPUT
            //AS
            //BEGIN
            //    SELECT @timestamp = CURRENT_TIMESTAMP
            //END
        }

        #endregion
        /*******************************************************************************************************/
        #region DESKTOP CONTROLS

        /// <summary><para>Desktop app use only.</para></summary>
        public static void relocateToCenter(Control control)
        {
            System.Drawing.Size parentSize = control.Parent.Size;
            control.Location = new System.Drawing.Point(parentSize.Width/2 - control.Width/2, parentSize.Height / 2 - control.Height / 2);
        }

        public static void fitTextToLabelHeight(Label label)
        {
            if (TextRenderer.MeasureText(label.Text, new Font(label.Font.FontFamily, label.Font.Size, label.Font.Style)).Height < label.Height)
            {
                while (TextRenderer.MeasureText(label.Text, new Font(label.Font.FontFamily, label.Font.Size, label.Font.Style)).Height < label.Height)
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size + 0.5f, label.Font.Style);
            }
            else
            {
                while (TextRenderer.MeasureText(label.Text, new Font(label.Font.FontFamily, label.Font.Size, label.Font.Style)).Height >= label.Height)
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size - 0.5f, label.Font.Style);
            }
        }

        public static void fitTextToLabel(Label label)
        {
            if(!string.IsNullOrWhiteSpace(label.Text))
            {
                fitTextToLabelHeight(label); //fits text vertically
            }

            //fits text horizontally
            while (TextRenderer.MeasureText(label.Text, new Font(label.Font.FontFamily, label.Font.Size, label.Font.Style)).Width >= label.Width)
                label.Font = new Font(label.Font.FontFamily, label.Font.Size - 0.5f, label.Font.Style);
        }

        public static Control getFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }

        #endregion
        /*******************************************************************************************************/
        #region MUTEX

        /// <summary><para>Desktop app use only. Prevent opening multiple instances of this application</para></summary>
        public static void ensureSingleInstance(Action methodToRun)
        {
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(false, "Global\\" + DBConnection.APPGUID))
            {
                if (!mutex.WaitOne(0, false))
                    LIBUtil.Util.displayMessageBoxError("Program is already running");
                else
                    methodToRun();
            }
        }

        //version with more complete checks to avoid problems. currently not working. copied from stackoverflow
        public static void runInMutex()
        {
            //using System.Runtime.InteropServices;
            //using System.Reflection;
            //using System.Threading;
            //using System.Security.AccessControl;
            //using System.Security.Principal;


            ////get application guid defined in AssemblyInfo.cs
            //string appGuid = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();

            ////unique id for global mutex- Global prefix means it is global to the machine
            //string mutexId = string.Format("Global\\{{{0}}}", appGuid);

            ////store a return value in Mutex() constructor call
            //bool createdNew;

            ////setting up security for multi-user usage. Work also on localized systems (don't just use "Everyone")
            //var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
            //var securitySettings = new MutexSecurity();
            //securitySettings.AddAccessRule(allowEveryoneRule);

            ////set to prevent race condition on security settings
            //using (var mutex = new Mutex(false, mutexId, out createdNew, securitySettings))
            //{
            //    var hasHandle = false;
            //    try
            //    {
            //        try
            //        {
            //            //may want to time out here instead of waiting forever
            //            //mutex.WaitOne(Timeout.Infinite, false);

            //            hasHandle = mutex.WaitOne(5000, false);
            //            if (hasHandle == false)
            //                throw new TimeoutException("Program is already running");


            //        }
            //        catch (AbandonedMutexException)
            //        {
            //            //mutex was abandoned in another process. It will still get acquired
            //            hasHandle = true;
            //        }

            //        runApplication();
            //    }
            //    finally
            //    {
            //        if (hasHandle)
            //            mutex.ReleaseMutex();
            //    }
            //}
        }

        #endregion
        /*******************************************************************************************************/
        #region DESKTOP PRINTING

        public static bool print(bool showPrintDialog, bool showPrintStatus, Panel printPanel) { return print(showPrintDialog, showPrintStatus, printPanel, 30); }
        public static bool print(bool showPrintDialog, bool showPrintStatus, Panel printPanel, int leftMargin)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += delegate (object sender, System.Drawing.Printing.PrintPageEventArgs e)
            {
                LIBUtil.Util.printControl(printPanel, e);
            };
            doc.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(leftMargin, 0, 0, 0);

            if (!showPrintStatus)
                doc.PrintController = new StandardPrintController();

            if (!showPrintDialog)
                doc.Print();
            else
            {
                PrintDialog dlgSettings = new PrintDialog();
                dlgSettings.Document = doc;

                if (dlgSettings.ShowDialog() == DialogResult.OK)
                    doc.Print();
                else
                    return false;
            }

            return true;
        }

        public static void printControl(Panel obj, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(obj.Width, obj.Height);
            obj.DrawToBitmap(bmp, new Rectangle(0, 0, obj.Width, obj.Height));
            e.Graphics.DrawImage((Image)bmp, x, y);
        }

        #endregion
        /*******************************************************************************************************/
        #region DATE AND TIME MANIPULATORS

        public static DateTime addMonths(int additionalMonths, bool isFirstOfMonth, bool isLastOfMonth)
        {
            return addMonths(DateTime.Now, additionalMonths, isFirstOfMonth, isLastOfMonth);
        }

        public static DateTime addMonths(DateTime datetime, int additionalMonths, bool isFirstOfMonth, bool isLastOfMonth)
        {
            if (isLastOfMonth)
                isFirstOfMonth = true;

            DateTime newDate = datetime.AddMonths(additionalMonths);

            int day = datetime.Day;
            if (isFirstOfMonth)
                day = 1;

            newDate = new DateTime(newDate.Year, newDate.Month, day);

            if (isLastOfMonth)
                newDate = newDate.AddMonths(1).AddDays(-1);

            return newDate;
        }

        public static DateTime? getFirstDayOfSelectedMonth(DateTime dt)
        {
            if (dt == null)
                return null;
            else
                return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime? getLastDayOfSelectedMonth(DateTime dt)
        {
            if (dt == null)
                return null;
            else
                return ((DateTime)getFirstDayOfSelectedMonth(dt)).AddMonths(1).AddSeconds(-1);
        }

        public static DateTime? getAsStartDate(DateTime? dt)
        {
            if (dt != null)
            {
                DateTime date = (DateTime)dt;
                dt = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            }
            return dt;
        }

        public static DateTime? getAsEndDate(DateTime? dt)
        {
            if (dt == null)
                return null;
            else
                return ((DateTime)getAsStartDate(dt)).AddDays(1).AddSeconds(-1);
        }

        public static DateTime standardizeTimeToIgnoreDate(DateTime datetime)
        {
            return new DateTime(1970, 1, 1, datetime.Hour, datetime.Minute, 0);
        }

        public static DateTime? standardizeTimeToIgnoreDate(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
                return null;
            else
                return new DateTime(1970, 1, 1, Convert.ToInt32(time.Split('_')[0]), Convert.ToInt32(time.Split('_')[1]), 0);
        }

        #endregion
        /*******************************************************************************************************/
        #region CONTROL LOCATION

        public static Point getLocationRelativeToForm(Control control)
        {            
            Form form = control.FindForm();
            Point controlLoc = form.PointToScreen(control.Location);
            Point relativeLoc = new Point(controlLoc.X - form.Location.X, controlLoc.Y - form.Location.Y);

            return relativeLoc;
        }

        #endregion
        /*******************************************************************************************************/
        #region DESKTOP CONTROL ANIMATION

        public static void Animate(Control ctl, AnimationEffect effect, int msec, int angle)
        {
            int flags = effmap[(int)effect];
            if (ctl.Visible) { flags |= 0x10000; angle += 180; }
            else
            {
                if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                else if (effect == AnimationEffect.Blend) throw new ArgumentException();
            }
            flags |= dirmap[(angle % 360) / 45];
            bool ok = AnimateWindow(ctl.Handle, msec, flags);
            if (!ok) throw new Exception("Animation failed");
            ctl.Visible = !ctl.Visible;
        }

        private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
        private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);

        #endregion
        /*******************************************************************************************************/
        #region RADIO BUTTON

        public static object getActiveRadioButton(Panel panel)
        {
            return panel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
        }

        public static DayOfWeek getDayOfWeekFromActiveRadioButtonTag(Panel panel)
        {
            RadioButton rb = (RadioButton)getActiveRadioButton(panel);
            return (DayOfWeek)rb.Tag;
        }

        #endregion
        /*******************************************************************************************************/
        #region FONT SIZE MANIPULATION

        public static Font updateFontSize(Font font, int difference)
        {
            return new Font(font.FontFamily, font.Size + difference, font.Style);
        }

        public static Label fitLabelFontSizeIntoContainer(Label label, Control container)
        {
            label.Height = container.Height; //set the height to match the container
            //resize font size to fit container
            int textheight = TextRenderer.MeasureText(label.Text, label.Font).Height;
            if (textheight > label.Height)
            {
                while (textheight > label.Height)
                {
                    label.Font = LIBUtil.Util.updateFontSize(label.Font, -2);
                    textheight = TextRenderer.MeasureText(label.Text, label.Font).Height;
                }
            }
            else if (textheight < label.Height)
            {
                while (textheight < label.Height)
                {
                    label.Font = LIBUtil.Util.updateFontSize(label.Font, 2);
                    textheight = TextRenderer.MeasureText(label.Text, label.Font).Height;
                }
                //make font smaller than container
                label.Font = LIBUtil.Util.updateFontSize(label.Font, -2);
                textheight = TextRenderer.MeasureText(label.Text, label.Font).Height;
            }

            return label;
        }

        #endregion
        /*******************************************************************************************************/
        #region HASHING

        private const int SALT_LENGTH = 10;

        public bool compare(string nonHashedValue, string hashedValue)
        {
            return hashedValue == hashString(nonHashedValue, hashedValue.Substring(hashedValue.Length - SALT_LENGTH, SALT_LENGTH));
        }

        public static string hashString(string Value)
        {
            string salt = createSalt();
            return hashString(Value, salt);
        }

        public static string hashString(string Value, string Salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(Value + Salt);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);

            return Convert.ToBase64String(inArray) + Salt; //produces 28 characters in addition to length of salt
        }

        public static string createSalt()
        {
            string salt = "";
            while (salt.Length < SALT_LENGTH)
                salt += new Guid();
            return salt.Substring(0, SALT_LENGTH);
        }

        public static string hashStringWithoutSalt(string Value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(Value);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        #endregion
        /*******************************************************************************************************/
        #region TOOLTIP

        public static void setTooltip(Control control, string message)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 500;
            tooltip.ShowAlways = true;
            tooltip.SetToolTip(control, message);
        }

        #endregion TOOLTIP
        /*******************************************************************************************************/

        public static string incrementHexNumber(string value)
        {
            int intFromHex = 0;
            if(!string.IsNullOrEmpty(value))
                intFromHex = int.Parse(value, System.Globalization.NumberStyles.HexNumber) + 1;
            return intFromHex.ToString("X").PadLeft(value.Length, '0');
        }

    }
}
