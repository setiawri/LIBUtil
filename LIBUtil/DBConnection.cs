﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LIBUtil
{
    public struct SqlQueryParameter
    {
        public string ColumnName;
        public SqlDbType SqlDbType;
        public object Value;

        public SqlQueryParameter(string columnName, SqlDbType sqlDbType, object value)
        {
            ColumnName = columnName;
            SqlDbType = sqlDbType;
            Value = value;
        }
    }
    
    public class SqlQueryTableParameter
    {
        public string ColumnName;
        public DataTable Values;
    }
    public class SqlQueryTableParameterGuid: SqlQueryTableParameter
    {
        public SqlQueryTableParameterGuid(string columnName, Guid?[] values)
        {
            ColumnName = columnName;
            Values = Util.copyValuesToArrayTable(values);
        }
    }
    public class SqlQueryTableParameterInt : SqlQueryTableParameter
    {
        public SqlQueryTableParameterInt(string columnName, int[] values)
        {
            ColumnName = columnName;
            Values = Util.copyIntToArrayTable(values);
        }
    }

    public struct SqlQueryResult
    {
        public DataTable Datatable;
        public bool IsSuccessful;
        public string ValueString;
        public int ValueInt;
        public decimal ValueDecimal;
        public bool ValueBoolean;
        public Guid ValueGuid;

        public SqlQueryResult(DataTable datatable, bool isSuccessful, string valueString, int valueInt, decimal valueDecimal, bool valueBoolean, Guid valueGuid)
        {
            Datatable = datatable;
            IsSuccessful = isSuccessful;
            ValueString = valueString;
            ValueInt = valueInt;
            ValueDecimal = valueDecimal;
            ValueBoolean = valueBoolean;
            ValueGuid = valueGuid;
        }

        public SqlQueryResult updateDatatable(DataTable datatable)
        {
            return new SqlQueryResult(datatable, IsSuccessful, ValueString, ValueInt, ValueDecimal, ValueBoolean, ValueGuid);
        }
    }

    public enum QueryTypes
    {
        FillByAdapter,
        ExecuteNonQuery
    }

    public class DBConnection
    {
        /*******************************************************************************************************/
        #region EXECUTE QUERY

        public static SqlQueryTableParameter[] createTableParameters(params SqlQueryTableParameter[] parameters)
        {
            return parameters;
        }

        public static SqlQueryResult query(SqlConnection sqlConnection, QueryTypes querytype, string storedprocedurename, params SqlQueryParameter[] parameters) { return query(sqlConnection, querytype, false, false, false, false, false, storedprocedurename, null, parameters); }
        public static SqlQueryResult query(SqlConnection sqlConnection, QueryTypes querytype, bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, string storedprocedurename, params SqlQueryParameter[] parameters) { return query(sqlConnection, querytype, hasReturnValueString, hasReturnValueInt, hasReturnValueDecimal, hasReturnValueBoolean, hasReturnValueGuid, storedprocedurename, null, parameters); }
        public static SqlQueryResult query(SqlConnection sqlConnection, QueryTypes querytype, string storedprocedurename, SqlQueryTableParameter[] tableparameters, params SqlQueryParameter[] parameters) { return query(sqlConnection, querytype, false, false, false, false, false, storedprocedurename, tableparameters, parameters); }
        public static SqlQueryResult query(SqlConnection sqlConnection, QueryTypes querytype, bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, string storedprocedurename, SqlQueryTableParameter[] tableparameters, params SqlQueryParameter[] parameters)
        {
            return executeQuery(sqlConnection, querytype, hasReturnValueString, hasReturnValueInt, hasReturnValueDecimal, hasReturnValueBoolean, hasReturnValueGuid, storedprocedurename, tableparameters, parameters);
        }

        public static SqlQueryResult query(QueryTypes querytype, string storedprocedurename, params SqlQueryParameter[] parameters) { return query(querytype, false, false, false, false, false, storedprocedurename, null, parameters); }
        public static SqlQueryResult query(QueryTypes querytype, bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, string storedprocedurename, params SqlQueryParameter[] parameters) { return query(querytype, hasReturnValueString, hasReturnValueInt, hasReturnValueDecimal, hasReturnValueBoolean, hasReturnValueGuid, storedprocedurename, null, parameters); }
        public static SqlQueryResult query(QueryTypes querytype, string storedprocedurename, SqlQueryTableParameter[] tableparameters, params SqlQueryParameter[] parameters) { return query(querytype, false, false, false, false, false, storedprocedurename, tableparameters, parameters); }
        public static SqlQueryResult query(QueryTypes querytype, bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, string storedprocedurename, SqlQueryTableParameter[] tableparameters, params SqlQueryParameter[] parameters)
        {
            SqlQueryResult result;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                result = executeQuery(sqlConnection, querytype, hasReturnValueString, hasReturnValueInt, hasReturnValueDecimal, hasReturnValueBoolean, hasReturnValueGuid, storedprocedurename, tableparameters, parameters);

            return result;
        }

        private static SqlQueryResult executeQuery(SqlConnection sqlConnection, QueryTypes querytype, bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, string storedprocedurename, SqlQueryTableParameter[] tableparameters, params SqlQueryParameter[] parameters)
        {
            SqlQueryResult result = new SqlQueryResult();
            SqlParameter returnValueString = null;
            SqlParameter returnValueInt = null;
            SqlParameter returnValueDecimal = null;
            SqlParameter returnValueBoolean = null;
            SqlParameter returnValueGuid = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand(storedprocedurename, sqlConnection))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null && parameters.Length > 0)
                        foreach (SqlQueryParameter parameter in parameters)
                            cmd.Parameters.Add("@" + parameter.ColumnName, parameter.SqlDbType).Value = parameter.Value;

                    if(tableparameters != null && tableparameters.Length > 0)
                        foreach(SqlQueryTableParameter tableparameter in tableparameters)
                            Util.addListParameter(cmd, "@" + tableparameter.ColumnName, tableparameter.Values);

                    if(hasReturnValueString)
                    {
                        returnValueString = cmd.Parameters.Add("@returnValueString", SqlDbType.NVarChar, 100);
                        returnValueString.Direction = ParameterDirection.Output;
                    }
                    if (hasReturnValueInt)
                    {
                        returnValueInt = cmd.Parameters.Add("@returnValueInt", SqlDbType.Int);
                        returnValueInt.Direction = ParameterDirection.Output;
                    }
                    if (hasReturnValueDecimal)
                    {
                        returnValueDecimal = cmd.Parameters.Add("@returnValueDecimal", SqlDbType.Decimal);
                        returnValueDecimal.Direction = ParameterDirection.Output;
                    }
                    if (hasReturnValueBoolean)
                    {
                        returnValueBoolean = cmd.Parameters.Add("@returnValueBoolean", SqlDbType.Bit);
                        returnValueBoolean.Direction = ParameterDirection.Output;
                    }
                    if (hasReturnValueGuid)
                    {
                        returnValueGuid = cmd.Parameters.Add("@returnValueGuid", SqlDbType.UniqueIdentifier);
                        returnValueGuid.Direction = ParameterDirection.Output;
                    }

                    //open connection
                    if (sqlConnection.State != ConnectionState.Open)
                        sqlConnection.Open();

                    //execute command
                    if (querytype == QueryTypes.ExecuteNonQuery)
                        cmd.ExecuteNonQuery();
                    else if (querytype == QueryTypes.FillByAdapter)
                    {
                        adapter.SelectCommand = cmd;
                        result.Datatable = new DataTable();
                        adapter.Fill(result.Datatable);
                    }

                    if (hasReturnValueString)
                        result.ValueString = returnValueString.Value.ToString();
                    if (hasReturnValueInt)
                        result.ValueInt = Convert.ToInt32(returnValueInt.Value);
                    if (hasReturnValueDecimal)
                        result.ValueDecimal = Convert.ToDecimal(returnValueDecimal.Value);
                    if (hasReturnValueBoolean)
                        result.ValueBoolean = Convert.ToBoolean(returnValueBoolean.Value);
                    if (hasReturnValueGuid)
                        if(returnValueGuid.Value != DBNull.Value)
                            result.ValueGuid = (Guid)returnValueGuid.Value;

                    result.IsSuccessful = true;
                }
            }
            catch (System.Exception ex) { result.IsSuccessful = false; Util.displayMessageBoxError(ex.Message); }

            return result;
        }

        #endregion
        /*******************************************************************************************************/
        #region CONNECTION STRING

        public static bool hasDBConnection = false;
        public static string ConnectionString;
        public static string ConnectionString_DefaultParams;
        public static string ConnectionString_Username;
        public static string ConnectionString_Password;

        public static string APPGUID; //System.Windows.Forms.Application.ProductName to get app name

        public static string ServerName { get { return ConnectionStringBuilder.DataSource; } }

        public static string DatabaseName { get { return ConnectionStringBuilder.InitialCatalog; } }

        public static SqlConnectionStringBuilder ConnectionStringBuilder
        {
            get { return new SqlConnectionStringBuilder(Util.getAppData(APPGUID)); }
            set { Util.saveAppData(APPGUID, value.ConnectionString); }
        }

        /// <summary>
        /// Input is sanitized before used to update DBConnection
        /// </summary>
        public static void update(Desktop.UserControls.InputControl_Textbox serverName, Desktop.UserControls.InputControl_Textbox databaseName)
        {
            Util.sanitize(serverName, databaseName);
            DBConnection.update(serverName.ValueText, databaseName.ValueText);
        }

        public static void update(string serverName, string databaseName)
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder;
            builder.DataSource = serverName;
            builder.InitialCatalog = databaseName;
            ConnectionStringBuilder = builder;

            ConnectionString = getConnectionString();
        }

        public static string getConnectionString()
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder;
            if (string.IsNullOrEmpty(builder.ConnectionString))
                return Util.saveAppData(DBConnection.APPGUID, ConnectionString_DefaultParams);
            else
            {
                builder.UserID = ConnectionString_Username;
                builder.Password = ConnectionString_Password;
                return builder.ConnectionString;
            }
        }

        public static void initialize(string defaultParams, string username, string password)
        {
            //get App Guid from entry assembly (Set in project's assembly information)
            APPGUID = ((System.Runtime.InteropServices.GuidAttribute)System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false).GetValue(0)).Value.ToString();

            ConnectionString_DefaultParams = defaultParams;
            ConnectionString_Username = username;
            ConnectionString_Password = password;
            ConnectionString = getConnectionString();
        }

        #endregion
        /*******************************************************************************************************/
    }
}
