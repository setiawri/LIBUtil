using System;
using System.Data;
using System.Data.SqlClient;

namespace LIBUtil
{
    public class Settings
    {

        /*******************************************************************************************************/
        #region METHODS
        public static int getIntValue(Guid GUID, int defaultValue)
        {
            DataRow row = get(GUID);
            if (row == null)
                return defaultValue;
            else
                return Convert.ToInt32(row[COL_DB_Value_Int]);
        }

        public static string getStringValue(Guid GUID)
        {
            DataRow row = get(GUID);
            if (row == null)
                return "";
            else
                return row[COL_DB_Value_String].ToString();
        }

        public static Guid? getGuidValue(Guid GUID)
        {
            string value = getStringValue(GUID);
            if (string.IsNullOrEmpty(value))
                return null;
            else
                return new Guid(value);
        }

        public static bool getBoolValue(Guid GUID)
        {
            if (getIntValue(GUID, 0) == 0)
                return false;
            else
                return true;
        }

        public static DateTime? getDateTimeValue(Guid GUID)
        {
            DataRow row = get(GUID);
            if (row == null)
                return null;
            else
                return Util.wrapNullable<DateTime?>(row[COL_DB_Value_DateTime]);
        }

        #endregion METHODS
        /*******************************************************************************************************/
        #region DATABASE FIELDS

        public const string COL_DB_Id = "Id";
        public const string COL_DB_Value_Int = "Value_Int";
        public const string COL_DB_Value_String = "Value_String";
        public const string COL_DB_Value_DateTime = "Value_DateTime";

        #endregion DATABASE FIELDS
        /*******************************************************************************************************/
        #region DATABASE METHODS

        public static DataRow get(Guid? id)
        {
            SqlQueryResult result = DBConnection.query(
                false,
                DBConnection.ActiveSqlConnection,
                QueryTypes.FillByAdapter,
                "Settings_get",
                new SqlQueryParameter(COL_DB_Id, SqlDbType.UniqueIdentifier, Util.wrapNullable(id))
                );
            return Util.getFirstRow(result.Datatable);
        }

        public static void update(Guid id, int value) { update(id, value, null, null, null); }
        public static void update(Guid id, string value) { update(id, null, value, null, null); }
        public static void update(Guid id, Guid? value) { update(id, null, value.ToString(), null, null); }
        public static void update(Guid id, bool value) { update(id, null, null, value, null); }
        public static void update(Guid id, DateTime? value) { update(id, null, null, null, value); }
        public static void update(Guid id, int? intValue, string stringValue, bool? boolValue, DateTime? datetimeValue)
        {
            if (boolValue != null)
                intValue = Util.convertToInt((bool)boolValue);

            SqlQueryResult result = DBConnection.query(
                false,
                DBConnection.ActiveSqlConnection,
                QueryTypes.ExecuteNonQuery,
                "Settings_update",
                new SqlQueryParameter(COL_DB_Id, SqlDbType.UniqueIdentifier, id),
                new SqlQueryParameter(COL_DB_Value_Int, SqlDbType.Int, Util.wrapNullable(intValue)),
                new SqlQueryParameter(COL_DB_Value_String, SqlDbType.VarChar, Util.wrapNullable(stringValue)),
                new SqlQueryParameter(COL_DB_Value_DateTime, SqlDbType.DateTime, Util.wrapNullable(datetimeValue))
            );
        }

        #endregion DATABASE METHODS
        /*******************************************************************************************************/

    }
}
