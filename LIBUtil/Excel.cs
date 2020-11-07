using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace LIBUtil
{
    public class ExcelCellFormat
    {
        public int Width = 5;
        public bool FontBold = false;
        public string Text;
        public int ColumnIndex;

        public ExcelHorizontalAlignment? Alignment { 
            get { if (_alignment == null) return ExcelHorizontalAlignment.Left; else return _alignment; } 
            set { _alignment = value; } }
        private ExcelHorizontalAlignment? _alignment;

        public ExcelCellFormat(int width, int columnIndex, string text) : this(width, columnIndex, text, false, null) { }
        public ExcelCellFormat(int width, int columnIndex, string text, bool fontBold, ExcelHorizontalAlignment? alignment)
        {
            Text = text;
            Width = width;
            ColumnIndex = columnIndex;
            Alignment = alignment;
            FontBold = fontBold;
        }
    }

    public class Excel
    {
        public static ActionResult GenerateExcelReport(string filename, ExcelPackage excelPackage)
        {
            MemoryStream fileStream = new MemoryStream();
            excelPackage.SaveAs(fileStream);
            fileStream.Position = 0;

            return new FileStreamResult(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = filename };
        }

        public static void SetWorkbookProperties(ExcelPackage p)
        {
            p.Workbook.Properties.Author = "CAJ";
            p.Workbook.Properties.Title = "Export to Excel";
        }

        public static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[1];
            ws.Name = sheetName;
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";

            return ws;
        }

        public static void editCellGroup(ExcelWorksheet ws, int rowIdx, int colIdx, int colWidth, object groupValue, ExcelHorizontalAlignment? alignment, params ExcelCellFormat[] values)
        {
            editCell(ws, rowIdx, colIdx, colWidth, groupValue, null, values.Length-1, 0, null);
            foreach(ExcelCellFormat value in values)
            {
                editCell(ws, rowIdx+1, value.ColumnIndex, value.Width, value.Text, null, 0, 0, null);
            }
        }

        public static void editCell(ExcelWorksheet ws, int rowIdx, int colIdx, int colWidth, object value, string format, int horizontalMergeCount, int verticalMergeCount, ExcelHorizontalAlignment? alignment)
        {
            ws.Column(colIdx).Width = colWidth;
            mergeCells(ws, rowIdx, colIdx, horizontalMergeCount, verticalMergeCount);
            editCell(ws, rowIdx, colIdx, value, format, alignment);
        }

        public static void editCell(ExcelWorksheet ws, int rowIdx, int colIdx, object value, string format, ExcelHorizontalAlignment? alignment)
        {
            editCell(ws, rowIdx, colIdx, value, format);
            if (alignment != null)
                ws.Cells[rowIdx, colIdx].Style.HorizontalAlignment = (ExcelHorizontalAlignment)alignment;
        }

        public static void editCell(ExcelWorksheet ws, int rowIdx, int colIdx, object value, string format)
        {
            if(!string.IsNullOrEmpty(format))
                ws.Cells[rowIdx, colIdx].Style.Numberformat.Format = format;

            ws.Cells[rowIdx, colIdx].Value = value;
        }

        public static object getCellValue(ExcelWorksheet ws, int rowIdx, int colIdx)
        {
            return ws.Cells[rowIdx, colIdx].Value;
        }

        public static void mergeCells(ExcelWorksheet ws, int rowIdx, int colIdx, int horizontalMergeCount, int verticalMergeCount)
        {
            ws.Cells[rowIdx, colIdx, rowIdx + verticalMergeCount, colIdx + horizontalMergeCount].Merge = true;
        }

        public static void setCellBorders(ExcelWorksheet ws, int startRowIdx, int startColIdx, int endRowIdx, int endColIdx, ExcelBorderStyle borderStyle)
        {
            Border cellBorder = ws.Cells[startRowIdx, startColIdx, endRowIdx, endColIdx].Style.Border;
            cellBorder.Left.Style = cellBorder.Top.Style = cellBorder.Right.Style = cellBorder.Bottom.Style = borderStyle;
        }

        ////integer (not really needed unless you need to round numbers, Excel will use default cell properties)
        //ws.Cells["A1:A25"].Style.Numberformat.Format = "0";

        ////integer without displaying the number 0 in the cell
        //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

        ////number with 1 decimal place
        //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

        ////number with 2 decimal places
        //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

        ////number with 2 decimal places and thousand separator
        //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

        ////number with 2 decimal places and thousand separator and money symbol
        //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

        ////percentage (1 = 100%, 0.01 = 1%)
        //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";

        ////accounting number format
        //ws.Cells["A1:A25"].Style.Numberformat.Format = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";

        ////default DateTime pattern
        //worksheet.Cells["A1:A25"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

        ////custom DateTime pattern
        //worksheet.Cells["A1:A25"].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
        /******************************************************************************************************************************************************/
    }
}