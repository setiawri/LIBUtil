using System;
using System.Drawing;
using System.Data;

namespace LIBUtil.Desktop.Classes
{
    public class ReceiptPrinter
    {
        /*******************************************************************************************************/
        #region PUBLIC VARIABLES            
            
        public string FontFamily = "Arial Bold";

        public StringFormat PlaceLeft { get; set; }
        public StringFormat PlaceCenter { get; set; }
        public StringFormat PlaceRight { get; set; }

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region PRIVATE VARIABLES            

        private const float LEADING = 4;

        private System.Drawing.Printing.PrintDocument _printDocument = new System.Drawing.Printing.PrintDocument();
        private Brush BRUSH = Brushes.Black;

        private float _startX;
        private float _startY;
        private float _offset;
        private SizeF _layoutSize;
        
        #endregion PRIVATE VARIABLES
        /*******************************************************************************************************/
        #region DATABASE FIELDS

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public ReceiptPrinter(int printAreaWidth)
        {
            _startX = 0;
            _startY = LEADING;
            _offset = 0;

            //setup
            PlaceLeft = new StringFormat(StringFormatFlags.NoClip);
            PlaceCenter = new StringFormat(PlaceLeft);
            PlaceRight = new StringFormat(PlaceLeft);
            PlaceCenter.Alignment = StringAlignment.Center;
            PlaceRight.Alignment = StringAlignment.Far;
            PlaceLeft.Alignment = StringAlignment.Near;

            _layoutSize = new SizeF(printAreaWidth - _offset * 2, new Font(FontFamily, 14).GetHeight());
        }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region DATABASE METHODS

        #endregion DATABASE METHODS
        /*******************************************************************************************************/
        #region CLASS METHODS
            
        public static void print(int printAreaWidth, string callNo, int printQty)
        {
            for (int i = printQty; i > 0; i--)
                new ReceiptPrinter(printAreaWidth).print(callNo, PrintLayout.get(null));
        }

        private void print(string callNo, DataTable printLayout)
        {
            _printDocument.PrintController = new System.Drawing.Printing.StandardPrintController();
            _printDocument.PrintPage += delegate (object sender, System.Drawing.Printing.PrintPageEventArgs e)
            {
                for(int i=0; i<printLayout.Rows.Count; i++)
                {
                    DataRow row = printLayout.Rows[i];
                    if(!Convert.ToBoolean(row[PrintLayout.COL_DB_Hide]) && (int)row[PrintLayout.COL_DB_FontSize] > 0)
                        addLine(
                            callNo,
                            Util.wrapNullable<string>(row, PrintLayout.COL_DB_Text),
                            Util.wrapNullable<int>(row, PrintLayout.COL_DB_FontSize),
                            getTextAlign(Util.wrapNullable<int>(row, PrintLayout.COL_DB_TextAlign_enumid)),
                            e,
                            i == printLayout.Rows.Count - 1);
                }
            };

            print();
        }

        private StringFormat getTextAlign(int textAlign)
        {
            if (textAlign == (int)PrintLayoutTextAlign.Left)
                return PlaceLeft;
            else if (textAlign == (int)PrintLayoutTextAlign.Center)
                return PlaceCenter;
            else
                return PlaceRight;
        }

        private void print()
        {
            try
            {
                _printDocument.Print();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }
        }

        private void addLine(string callNo, string text, int fontsize, StringFormat placement, System.Drawing.Printing.PrintPageEventArgs e, bool isLastRow)
        {
            string tag_callno = "{no}";
            string tag_date = "{date}";
            string tag_day = "{day}";
            string tag_time = "{time}";

            Font font = new Font(FontFamily, fontsize);
            float lineheight = font.GetHeight() + LEADING;
            RectangleF layout = new RectangleF(new PointF(_startX, _startY + _offset), _layoutSize);

            string newText = text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                newText = newText.Replace(tag_callno, callNo);
                newText = newText.Replace(tag_date, string.Format("{0:dd/MM/yy}", DateTime.Now));
                newText = newText.Replace(tag_day, string.Format("{0:dddd}", DateTime.Now));
                newText = newText.Replace(tag_time, string.Format("{0:HH:mm}", DateTime.Now));
            }

            //last row
            if (isLastRow)
            {
                if (string.IsNullOrWhiteSpace(text))
                    newText = ".";
            }
            //Util.displayMessageBox("", newText);
            e.Graphics.DrawString(newText, font, BRUSH, layout, placement);

            _offset = _offset + lineheight;
        }
        
        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    int printAreaWidth = 300;
        //    Graphics graphics = e.Graphics;

        //    Font font10 = new Font("Courier New", 10);
        //    Font font12 = new Font("Courier New", 12);
        //    Font font14 = new Font("Courier New", 14);

        //    float leading = 4;
        //    float lineheight10 = font10.GetHeight() + leading;
        //    float lineheight12 = font12.GetHeight() + leading;
        //    float lineheight14 = font14.GetHeight() + leading;

        //    float startX = 0;
        //    float startY = leading;
        //    float Offset = 0;

        //    StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
        //    StringFormat formatCenter = new StringFormat(formatLeft);
        //    StringFormat formatRight = new StringFormat(formatLeft);

        //    formatCenter.Alignment = StringAlignment.Center;
        //    formatRight.Alignment = StringAlignment.Far;
        //    formatLeft.Alignment = StringAlignment.Near;

        //    SizeF layoutSize = new SizeF(printAreaWidth - Offset * 2, lineheight14);
        //    RectangleF layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

        //    Brush brush = Brushes.Black;

        //    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
        //    graphics.DrawString("Antrian: " + _printString, font14, brush, layout, formatCenter);
        //    Offset = Offset + lineheight14 + 40;

        //    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
        //    graphics.DrawString(".", font14, brush, layout, formatLeft);
        //    Offset = Offset + lineheight14;

        //    //layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
        //    //graphics.DrawString("Recept No XXX:", font14, brush, layout, formatLeft);
        //    //Offset = Offset + lineheight14;
        //    //layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
        //    //graphics.DrawString("Date :" + DateTime.Today, font12, brush, layout, formatLeft);
        //    //Offset = Offset + lineheight12;
        //    //layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
        //    //graphics.DrawString("".PadRight(46, '_'), font10, brush, layout, formatLeft);
        //    //Offset = Offset + lineheight10;
        //    //layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

        //    //graphics.DrawString("copyright SO", font10, brush, layout, formatRight);
        //    //Offset = Offset + lineheight10;

        //    font10.Dispose(); font12.Dispose(); font14.Dispose();
        //}

        #endregion CLASS METHODS
        /*******************************************************************************************************/
    }
}
