using System.Data.SqlClient;
using System.Data.Entity;

using LIBUtil;

namespace LIBWebMVC
{
    public class WebDBConnection
    {
        /**********************************************************************************************************************************************************/
        public static void Insert(Database database, string tableName, params SqlParameter[] parameters)
        {
            string fields = "";
            string values = "";
            foreach (SqlParameter parameter in parameters)
            {
                fields = Util.append(fields, parameter.ParameterName, ",");
                values = Util.append(values, "@" + parameter.ParameterName, ",");
            }

            database.ExecuteSqlCommand(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, fields, values), parameters);
        }

        public static void Update(Database database, string tableName, params SqlParameter[] parameters)
        {
            string fields = "";
            string whereClause = "";
            foreach (SqlParameter param in parameters)
            {
                if (param.ParameterName.ToLower() == "id")
                    whereClause = string.Format("{0}.{1} = @{1}", tableName, param.ParameterName);
                else
                    fields = Util.append(fields, string.Format("{0} = @{0}", param.ParameterName), ", ");
            }

            database.ExecuteSqlCommand(string.Format("UPDATE {0} SET {1} WHERE {2}", tableName, fields, whereClause), parameters);
        }

        public static void Delete(Database database, string tableName, params SqlParameter[] parameters)
        {
            string fields = "";
            string whereClause = "";
            foreach (SqlParameter param in parameters)
            {
                if (param.ParameterName.ToLower() == "id")
                    whereClause = string.Format("{0}.{1} = @{1}", tableName, param.ParameterName);
                else
                    fields = Util.append(fields, string.Format("{0} = @{0}", param.ParameterName), ", ");
            }

            database.ExecuteSqlCommand(string.Format("DELETE {0} WHERE {1}", tableName, whereClause), parameters);
        }

        /*******************************************************************************************************/

    }
}
