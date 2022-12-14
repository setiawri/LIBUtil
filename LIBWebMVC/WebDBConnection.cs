using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;

using LIBUtil;
using System.Linq;

namespace LIBWebMVC
{
    public class WebDBConnection
    {
        public const string PARAM_RETURNVALUE = "RETURNVALUE";

        /**********************************************************************************************************************************************************/

        public static string GetNextHex(Database database, string tableName, string columnName)
        {
            SqlParameter output = new SqlParameter(PARAM_RETURNVALUE, SqlDbType.VarChar, 5) { Direction = ParameterDirection.Output };

            string sql = string.Format(@"

	                -- INCREMENT LAST HEX NUMBER
	                DECLARE @HexLength int = 5, @LastHex_String varchar(5), @NewNo varchar(5)
	                SELECT @LastHex_String = ISNULL(MAX({1}),'') From {2}	
	                DECLARE @LastHex_Int int
	                SELECT @LastHex_Int = CONVERT(INT, CONVERT(VARBINARY, REPLICATE('0', LEN(@LastHex_String)%2) + @LastHex_String, 2)) --@LastHex_String length must be even number of digits to convert to int
	                SET @NewNo = RIGHT(CONVERT(NVARCHAR(10), CONVERT(VARBINARY(8), @LastHex_Int + 1), 1),@HexLength)

                    SELECT @ReturnValue = @NewNo

                ", PARAM_RETURNVALUE, columnName, tableName);

            database.ExecuteSqlCommand(sql, output);

            return output.Value.ToString();
        }

        public static string GetValues(Database database, string sql)
        {
            SqlParameter output = new SqlParameter(PARAM_RETURNVALUE, SqlDbType.VarChar, 1000) { Direction = ParameterDirection.Output };

            database.ExecuteSqlCommand(sql, output);

            return output.Value.ToString();
        }

        public static bool IsExist(Database database, string tableName, params SqlParameter[] parameters)
        {
            string whereClause = string.Empty;
            foreach (SqlParameter param in parameters)
            {
                if (param.ParameterName.ToLower() == "id")
                    whereClause = string.Format("AND (@{0} IS NULL OR ({0} <> @{0}))", param.ParameterName);
                else
                    whereClause = Util.append(whereClause, string.Format("[{0}] = @{0}", param.ParameterName), " AND ");
            }

            return database.SqlQuery<int>(string.Format(@"SELECT COUNT(*) FROM {0} WHERE 1=1 {1}", tableName, whereClause), parameters).FirstOrDefault() > 0;
        }

        public static void Insert(Database database, string tableName, params SqlParameter[] parameters)
        {
            string fields = string.Empty;
            string values = string.Empty;
            foreach (SqlParameter param in parameters)
            {
                fields = Util.append(fields, "["+ param.ParameterName + "]", ",");
                values = Util.append(values, "@" + param.ParameterName, ",");
            }

            database.ExecuteSqlCommand(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, fields, values), parameters);
        }

        public static void Update(Database database, string tableName, params SqlParameter[] parameters)
        {
            string fields = string.Empty;
            string whereClause = string.Empty;
            foreach (SqlParameter param in parameters)
            {
                if (param.ParameterName.ToLower() == "id")
                    whereClause = string.Format("{0}.[{1}] = @{1}", tableName, param.ParameterName);
                else
                    fields = Util.append(fields, string.Format("[{0}] = @{0}", param.ParameterName), ", ");
            }

            database.ExecuteSqlCommand(string.Format("UPDATE {0} SET {1} WHERE {2}", tableName, fields, whereClause), parameters);
        }

        public static void UpdateAllRows(Database database, string tableName, params SqlParameter[] parameters)
        {
            string fields = string.Empty;
            foreach (SqlParameter param in parameters)
                fields = Util.append(fields, string.Format("[{0}] = @{0}", param.ParameterName), ", ");

            database.ExecuteSqlCommand(string.Format("UPDATE {0} SET {1}", tableName, fields), parameters);
        }

        public static void Delete(Database database, string tableName, params SqlParameter[] parameters)
        {
            string fields = string.Empty;
            string whereClause = string.Empty;
            foreach (SqlParameter param in parameters)
            {
                if (param.ParameterName.ToLower() == "id")
                    whereClause = string.Format("{0}.[{1}] = @{1}", tableName, param.ParameterName);
                else
                    fields = Util.append(fields, string.Format("[{0}] = @{0}", param.ParameterName), ", ");
            }

            database.ExecuteSqlCommand(string.Format("DELETE {0} WHERE {1}", tableName, whereClause), parameters);
        }

        public static void DeleteAllRows(Database database, string tableName)
        {
            database.ExecuteSqlCommand(string.Format("DELETE {0}", tableName));
        }

        public static void Execute(Database database, string sql, params SqlParameter[] parameters)
        {
            database.ExecuteSqlCommand(sql, parameters);
        }

    }

    /*******************************************************************************************************/

}
