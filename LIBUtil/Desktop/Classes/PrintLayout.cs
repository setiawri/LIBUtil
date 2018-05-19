using System;
using System.Data;
using System.Data.SqlClient;
using LIBUtil;

namespace LIBUtil.Desktop.Classes
{
    public enum PrintLayoutTextAlign
    {
        Left,
        Center,
        Right
    }

    public class PrintLayout
    {
        /*******************************************************************************************************/
        #region PUBLIC VARIABLES

        public Guid Id;
        public int LineNumber;
        public bool Hide;
        public string Text;
        public int FontSize;
        public PrintLayoutTextAlign TextAlign;

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region DATABASE FIELDS

        public const string COL_DB_Id = "Id";
        public const string COL_DB_LineNumber = "LineNumber";
        public const string COL_DB_Hide = "Hide";
        public const string COL_DB_Text = "Text";
        public const string COL_DB_FontSize = "FontSize";
        public const string COL_DB_TextAlign_enumid = "TextAlign_enumid";

        public const string COL_TextAlign_description = "TextAlign_description";

        #endregion PUBLIC VARIABLES
        /*******************************************************************************************************/
        #region CONSTRUCTOR METHODS

        public PrintLayout(Guid id)
        {
            DataRow row = getRow(id);
            Id = id;
            LineNumber = Util.wrapNullable<int>(row, COL_DB_LineNumber);
            Hide = Util.wrapNullable<bool>(row, COL_DB_Hide);
            Text = Util.wrapNullable<string>(row, COL_DB_Text);
            FontSize = Util.wrapNullable<int>(row, COL_DB_FontSize);
            TextAlign = Util.parseEnum<PrintLayoutTextAlign>(Util.wrapNullable<int>(row, COL_DB_TextAlign_enumid));
        }

        public PrintLayout() { }

        #endregion CONSTRUCTOR METHODS
        /*******************************************************************************************************/
        #region DATABASE METHODS

        public static void add()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnection.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("PrintLayout_add", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { Util.displayMessageBoxError(ex.Message); }
        }

        public static DataRow getRow(Guid id) { return Util.getFirstRow(get(id)); }

        public static DataTable get(Guid? id)
        {
            DataTable datatable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnection.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("PrintLayout_get", sqlConnection))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@" + COL_DB_Id, SqlDbType.UniqueIdentifier).Value = Util.wrapNullable(id);

                    adapter.SelectCommand = cmd;
                    adapter.Fill(datatable);
                }
            }
            catch (Exception ex) { Util.displayMessageBoxError(ex.Message); }

            Util.parseEnum<PrintLayoutTextAlign>(datatable, COL_TextAlign_description, COL_DB_TextAlign_enumid);

            return datatable;
        }

        public static void update(SqlConnection sqlConnection, Guid id, bool hide, string text, int fontSize, int textAlign)
        {
            SqlQueryResult result = DBConnection.query(
                sqlConnection,
                QueryTypes.ExecuteNonQuery,
                "PrintLayout_update",
                new SqlQueryParameter(COL_DB_Id, SqlDbType.UniqueIdentifier, id),
                new SqlQueryParameter(COL_DB_Hide, SqlDbType.Bit, hide),
                new SqlQueryParameter(COL_DB_Text, SqlDbType.NVarChar, Util.wrapNullable(text)),
                new SqlQueryParameter(COL_DB_FontSize, SqlDbType.Int, fontSize),
                new SqlQueryParameter(COL_DB_TextAlign_enumid, SqlDbType.Int, textAlign)
            );
        }
        
        public static string delete(Guid id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnection.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("PrintLayout_delete", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@" + COL_DB_Id, SqlDbType.UniqueIdentifier).Value = id;

                    if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { Util.displayMessageBoxError(ex.Message); }

            return string.Empty;
        }

        #endregion DATABASE METHODS
        /*******************************************************************************************************/
        #region CLASS METHODS
            
        #endregion CLASS METHODS
        /*******************************************************************************************************/
    }
}

