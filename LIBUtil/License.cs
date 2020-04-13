using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LIBUtil
{
    public class License
    {
        public const string COL_DB_Id = "Id";
        public const string COL_DB_HashedMACAddress = "HashedMACAddress";
        public const string COL_DB_Timestamp = "Timestamp";

        public static bool isDeviceRegistered = false;

        public static bool validate(string value)
        {
            /*************************
             * License format:
             * 
             * 2 digits year
             * 1 digit first letter of dayofweek (M:monday, T:Tuesday/Thursday, S:Saturday/Sunday)
             * 2 digits date (02, 07, 12, 28, 31)
             * 2 digits hour (00, 09, 12, 18, 23)
             * 2 digits month
             * 
             ************************/

            DateTime date = DateTime.Now;
            string validLicense = string.Format("{0}{1}{2}{3}{4}",
                    date.ToString("yy"),
                    date.ToString("ddd").Substring(0,1),
                    date.ToString("dd"),
                    date.ToString("HH"),
                    date.ToString("MM")
                );
            validLicense = Util.reverse(validLicense);

            if (value.ToUpper() != validLicense)
                return false;
            else
                addDevice();

            return true;
        }

        public static void addDevice()
        {
            string hashedMACAddress = Util.hashStringWithoutSalt(Util.getMACAddress());
            Guid Id = Guid.NewGuid();
            SqlQueryResult result = DBConnection.query(
                false,
                DBConnection.ActiveSqlConnection,
                QueryTypes.ExecuteNonQuery,
                "LicensedDevices_add",
                new SqlQueryParameter(COL_DB_Id, SqlDbType.UniqueIdentifier, Id),
                new SqlQueryParameter(COL_DB_HashedMACAddress, SqlDbType.NVarChar, hashedMACAddress)
            );

            if (result.IsSuccessful)
            {
                isDeviceRegistered = true;
                Util.displayMessageBoxSuccess("License is valid. Thank you!");
            }
        }

        public static bool isRegistered()
        {
            string hashedMACAddress = Util.hashStringWithoutSalt(Util.getMACAddress());
            SqlQueryResult result = DBConnection.query(
                false,
                DBConnection.ActiveSqlConnection,
                QueryTypes.ExecuteNonQuery,
                false, false, false, true, false,
                "LicensedDevices_isExist_HashedMACAddress",
                new SqlQueryParameter(COL_DB_HashedMACAddress, SqlDbType.NVarChar, hashedMACAddress)
                );
            isDeviceRegistered = result.ValueBoolean;
            return result.ValueBoolean;
        }
    }
}
