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
    public partial class DragDropLabelWithCheckedListBox : UserControl
    {
        /*******************************************************************************************************/
        #region VARIABLES

        private int DefaultHeight = 98;

        #endregion
        /*******************************************************************************************************/
        #region PROPERTIES
        
        public CheckedListBox CheckedListBox { get { return checkedlistbox; } }

        [Description("Label Text"), Category("_Custom")]
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        [Description("Allow Show checkedlistbox"), Category("_Custom")]
        public bool AllowShowCheckedListBox
        {
            get { return _allowShowCheckedListBox; }
            set
            {
                _allowShowCheckedListBox = value;
                checkedlistbox.Visible = label.Visible = !value;
            }
        }
        private bool _allowShowCheckedListBox = false;

        #endregion
        /*******************************************************************************************************/
        #region CONSTRUCTORS

        public DragDropLabelWithCheckedListBox()
        {
            InitializeComponent();

            setupControls();
        }

        #endregion
        /*******************************************************************************************************/
        #region METHODS

        private void setupControls()
        {
            SetCheckedListBoxVisibility(false);
        }

        public void SetCheckedListBoxVisibility(bool value)
        {
            checkedlistbox.Visible = value;
            if(value)
                this.Height = DefaultHeight;
            else
                this.Height = label.Height;

        }

        public void PopulateCheckedListBox(DataTable data, string DisplayMember, string ValueMember)
        {
            InputControl_CheckedListBox.populateCheckedListBox(checkedlistbox, data, DisplayMember, ValueMember);
        }

        #endregion
        /*******************************************************************************************************/
        #region EVENT HANDLERS


        #endregion
        /*******************************************************************************************************/
    }
}
