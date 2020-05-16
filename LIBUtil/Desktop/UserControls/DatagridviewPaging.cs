using System;
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
    public partial class DatagridviewPaging : UserControl
    {
        /*******************************************************************************************************/
        #region SETTINGS

        public const int DEFAULT_PAGESIZE = 50;

        #endregion SETTINGS
        /*******************************************************************************************************/
        #region PUBLIC VARIABLES

        public int DefaultPageSize = DEFAULT_PAGESIZE;

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region PROPERTIES

        public int PageSize { get { return in_PageSize.ValueInt; } set { in_PageSize.Value = Convert.ToInt32(value); } }

        public int CurrentPageNo { get { return _currentPageNo; } set { _currentPageNo = value; setPageNo(); } }
        private int _currentPageNo = 1;

        public int PageCount { get { return _pageCount; } set { _pageCount = value; setPageNo(); } }
        private int _pageCount = 1;

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region PRIVATE VARIABLES

        #endregion PRIVATE VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public DatagridviewPaging() : this(DEFAULT_PAGESIZE) { }
        public DatagridviewPaging(int defaultPageSize) { InitializeComponent(); DefaultPageSize = defaultPageSize; }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        private void setupControls()
        {
            in_PageSize.Value = DefaultPageSize;
            setPageNo();
        }

        private void resetData()
        {

        }

        private void setPageNo()
        {
            lblPageNo.Text = string.Format("{0}/{1}", CurrentPageNo, PageCount);
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void Form_Load(object sender, EventArgs e)
        {
            setupControls();
        }

        [Description("Button Click Event"), Category("_Custom")]
        public event EventHandler Button_Click;
        private void button_Click(object sender, EventArgs e)
        {
            if (sender == btnFirstPage)
                CurrentPageNo = 1;
            else if (sender == btnLastPage)
                CurrentPageNo = PageCount;
            else if (sender == btnPrevious)
            {
                if (CurrentPageNo > 1)
                    CurrentPageNo--;
            }
            else if (sender == btnNext)
            {
                if (CurrentPageNo < PageCount)
                    CurrentPageNo++;
            }

            if (this.Button_Click != null)
                this.Button_Click(this, e);
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/
    }
}
