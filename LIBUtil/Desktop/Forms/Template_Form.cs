using System;
using System.Windows.Forms;

namespace BinaMitraTextile
{
    public partial class Template_Form : Form
    {
        /*******************************************************************************************************/
        #region SETTINGS

        #endregion SETTINGS
        /*******************************************************************************************************/
        #region PUBLIC VARIABLES

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region PROPERTIES

        #endregion PROPERTIES
        /*******************************************************************************************************/
        #region PRIVATE VARIABLES

        private bool _isFormShown = false;

        #endregion PRIVATE VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public Template_Form() : this(null) { }
        public Template_Form(Guid? Id) { InitializeComponent(); }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region METHODS

        private void setupControlsBasedOnRoles()
        {

        }

        private void setupControls()
        {
            //Settings.setGeneralSettings(this);

            //grid.AutoGenerateColumns = false;
            //grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //grid.Columns[Method.Name].DataPropertyName = SalePayment.COL_METHODNAME;
        }

        private void populatePageData()
        {
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region EVENT HANDLERS

        private void Form_Load(object sender, EventArgs e)
        {
            setupControls();
            setupControlsBasedOnRoles();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            _isFormShown = true;
            populatePageData();
        }

        #endregion EVENT HANDLERS
        /*******************************************************************************************************/

    }
}
