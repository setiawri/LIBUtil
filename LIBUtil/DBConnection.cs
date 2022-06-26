using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace LIBUtil
{
    public enum ConnectionPorts
    {
        None = 0,
        [Description("1433")]
        port1433 = 1433,
        [Description("1434")]
        port1434 = 1434,
        [Description("1435")]
        port1435 = 1435,
    };

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
        public SqlQueryTableParameterGuid(string columnName, Guid?[] values) : this(columnName, Util.copyValuesToArrayTable(values)) { }

        public SqlQueryTableParameterGuid(string columnName, DataTable values)
        {
            ColumnName = columnName;
            Values = values;
        }
    }
    public class SqlQueryTableParameterInt : SqlQueryTableParameter
    {
        public SqlQueryTableParameterInt(string columnName, int[] values) : this(columnName, Util.copyIntToArrayTable(values)) { }

        public SqlQueryTableParameterInt(string columnName, DataTable values)
        {
            ColumnName = columnName;
            Values = values;
        }
    }

    public struct SqlQuery
    {
        public SqlConnection sqlConnection;
        public QueryTypes querytype;
        public bool hasReturnValueString;
        public bool hasReturnValueInt;
        public bool hasReturnValueDecimal;
        public bool hasReturnValueBoolean;
        public bool hasReturnValueGuid;
        public string sqlOrStoredProcedureName;
        public bool isStoredProcedure;
        public SqlQueryTableParameter[] tableparameters;
        public SqlQueryParameter[] parameters;

        public SqlQueryResult result;
               
        public bool isQueryCompleted;

        public void execute()
        {
            isQueryCompleted = false;
            isStoredProcedure = true;

            if (sqlConnection != null)
                result = DBConnection.executeQuery(sqlConnection, querytype, hasReturnValueString, hasReturnValueInt, hasReturnValueDecimal, hasReturnValueBoolean, hasReturnValueGuid, sqlOrStoredProcedureName, isStoredProcedure, tableparameters, parameters);
            else
            {
                using(new SqlConnection(DBConnection.ConnectionString))
                    result = DBConnection.executeQuery(sqlConnection, querytype, hasReturnValueString, hasReturnValueInt, hasReturnValueDecimal, hasReturnValueBoolean, hasReturnValueGuid, sqlOrStoredProcedureName, isStoredProcedure, tableparameters, parameters);
            }

            isQueryCompleted = true;
        }
    }

    public class SqlQueryResult
    {
        public DataSet Dataset = new DataSet();
        public DataTable Datatable 
        { 
            get { if (Dataset.Tables.Count > 0) return Dataset.Tables[0]; else return null; } 
            set { Dataset.Tables.Clear(); Dataset.Tables.Add(value); }
        }

        public bool IsSuccessful;
        public string ValueString;
        public int ValueInt;
        public decimal ValueDecimal;
        public bool ValueBoolean;
        public Guid ValueGuid;

        public SqlQueryResult() { }
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
        public static bool DBConnectionTestCompleted;
        private static bool _MultipleSQLConnectionUse = true;

        /*******************************************************************************************************/
        #region EXECUTE QUERY

        public static SqlQueryTableParameter[] createTableParameters(params SqlQueryTableParameter[] parameters)
        {
            return parameters;
        }

        public static SqlQueryResult query(bool showProgressBar, SqlConnection sqlConnection, QueryTypes querytype, string sqlOrStoredProcedureName, params SqlQueryParameter[] parameters) { return query(showProgressBar, sqlConnection, querytype, false, false, false, false, false, sqlOrStoredProcedureName, null, parameters); }
        public static SqlQueryResult query(bool showProgressBar, SqlConnection sqlConnection, QueryTypes querytype, bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, string sqlOrStoredProcedureName, params SqlQueryParameter[] parameters) { return query(showProgressBar, sqlConnection, querytype, hasReturnValueString, hasReturnValueInt, hasReturnValueDecimal, hasReturnValueBoolean, hasReturnValueGuid, sqlOrStoredProcedureName, null, parameters); }
        public static SqlQueryResult query(bool showProgressBar, SqlConnection sqlConnection, QueryTypes querytype, string sqlOrStoredProcedureName, SqlQueryTableParameter[] tableparameters, params SqlQueryParameter[] parameters) { return query(showProgressBar, sqlConnection, querytype, false, false, false, false, false, sqlOrStoredProcedureName, tableparameters, parameters); }
        public static SqlQueryResult query(bool showProgressBar, SqlConnection sqlConnection, QueryTypes querytype, bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, string sqlOrStoredProcedureName, SqlQueryTableParameter[] tableparameters, params SqlQueryParameter[] parameters)
        {
            _sqlQuery = new SqlQuery()
            {
                sqlConnection = sqlConnection,
                querytype = querytype,
                hasReturnValueString = hasReturnValueString,
                hasReturnValueInt = hasReturnValueInt,
                hasReturnValueDecimal = hasReturnValueDecimal,
                hasReturnValueBoolean = hasReturnValueBoolean,
                hasReturnValueGuid = hasReturnValueGuid,
                sqlOrStoredProcedureName = sqlOrStoredProcedureName,
                tableparameters = tableparameters,
                parameters = parameters
            };

            if (!showProgressBar)
            {
                _sqlQuery.execute();
            }
            else
            {
                _timer = new System.Windows.Forms.Timer();
                _timer.Tick += new System.EventHandler(timer_Tick);
                _timer.Interval = 100;

                BackgroundWorker queryBackgroundWorker = new BackgroundWorker();
                queryBackgroundWorker.DoWork += new DoWorkEventHandler(queryBackgroundWorker_DoWork);

                _sqlQuery.isQueryCompleted = false;
                _timer.Start();
                queryBackgroundWorker.RunWorkerAsync();

                Util.displayForm(null, _progressBarForm, false);
            }

            return _sqlQuery.result;
        }

        private static System.Windows.Forms.Timer _timer;
        private const int TIMERTIMOUT = 100;
        private static Desktop.Forms.ProgressBar_Form _progressBarForm = new Desktop.Forms.ProgressBar_Form();
        private static SqlQuery _sqlQuery;
        public static System.Windows.Forms.Timer SqlConnectionTimer;
        private static void queryBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _sqlQuery.execute();
        }

        private static void timer_Tick(object sender, EventArgs e)
        {
            if (_sqlQuery.isQueryCompleted)
            {
                _timer.Stop();
                _progressBarForm.Close();
            }

            //apply TIME OUT HERE
            //_timer.Stop();
            //_progressBarForm.Close();
            //Util.displayMessageBoxError("TIMEOUT. Please try again");
        }

        public static SqlQueryResult executeQuery(string connectionString, string sqlOrStoredProcedureName, bool isStoredProcedure, 
            bool hasReturnValueString, params SqlQueryParameter[] parameters)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            return executeQuery(sqlConnection, QueryTypes.FillByAdapter, hasReturnValueString, false, false, false, false, sqlOrStoredProcedureName, isStoredProcedure, null, parameters);
        }
        public static SqlQueryResult executeQuery(string connectionString, string sqlOrStoredProcedureName, bool isStoredProcedure,
            bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, 
            params SqlQueryParameter[] parameters)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            return executeQuery(sqlConnection, QueryTypes.FillByAdapter, hasReturnValueString, hasReturnValueInt, hasReturnValueDecimal, hasReturnValueBoolean, hasReturnValueGuid, sqlOrStoredProcedureName, isStoredProcedure, null, parameters);
        }
        public static SqlQueryResult executeQuery(SqlConnection sqlConnection, QueryTypes querytype, 
            bool hasReturnValueString, bool hasReturnValueInt, bool hasReturnValueDecimal, bool hasReturnValueBoolean, bool hasReturnValueGuid, 
            string sqlOrStoredProcedureName, bool isStoredProcedure, SqlQueryTableParameter[] tableparameters, params SqlQueryParameter[] parameters)
        {
            SqlQueryResult result = new SqlQueryResult();
            SqlParameter returnValueString = null;
            SqlParameter returnValueInt = null;
            SqlParameter returnValueDecimal = null;
            SqlParameter returnValueBoolean = null;
            SqlParameter returnValueGuid = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlOrStoredProcedureName, sqlConnection))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    if (isStoredProcedure)
                        cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandTimeout = 3000;

                    if (parameters != null && parameters.Length > 0)
                        foreach (SqlQueryParameter parameter in parameters)
                            cmd.Parameters.Add("@" + parameter.ColumnName, parameter.SqlDbType).Value = parameter.Value;

                    if(tableparameters != null && tableparameters.Length > 0)
                        foreach(SqlQueryTableParameter tableparameter in tableparameters)
                            Util.addListParameter(cmd, "@" + tableparameter.ColumnName, tableparameter.Values);

                    if(hasReturnValueString)
                    {
                        returnValueString = cmd.Parameters.Add("@returnValueString", SqlDbType.NVarChar, 1000);
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
                        adapter.Fill(result.Dataset);
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

                    if (!_MultipleSQLConnectionUse)
                        sqlConnection.Close();
                }
            }
            catch (Exception ex) { result.IsSuccessful = false; Util.displayMessageBoxError(ex.Message); }

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
            {
                Util.saveAppData(DBConnection.APPGUID, ConnectionString_DefaultParams);
                builder = ConnectionStringBuilder;
            }

            if(ConnectionString_Username != null && ConnectionString_Password != null)
            {
                builder.UserID = ConnectionString_Username;
                builder.Password = ConnectionString_Password;
            }
            return builder.ConnectionString;
        }

        public static string ConnectionInfo
        {
            get
            {
                string connectionInfo = getConnectionString();
                if (connectionInfo.IndexOf(';') > 0)
                    connectionInfo = connectionInfo.Substring(0, connectionInfo.IndexOf(';'));
                return connectionInfo;
            }
        }

        public static void initialize(bool multipleSQLConnectionUse, string defaultParams, string username, string password)
        {
            //get App Guid from entry assembly (Set in project's assembly information)
            APPGUID = ((System.Runtime.InteropServices.GuidAttribute)System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false).GetValue(0)).Value.ToString();

            _MultipleSQLConnectionUse = multipleSQLConnectionUse;

            ConnectionString_DefaultParams = defaultParams;
            ConnectionString_Username = username;
            ConnectionString_Password = password;
            ConnectionString = getConnectionString();
        }
        
        public static void populatePorts(Desktop.UserControls.InputControl_Dropdownlist iddl)
        {
            iddl.populate(typeof(ConnectionPorts));
        }

        public static SqlConnection ActiveSqlConnection
        {
            get
            {
                if(!_MultipleSQLConnectionUse)
                {
                    //temporary fix for error: “There is already an open DataReader associated with this Command which must be closed first"
                    return new SqlConnection(getConnectionString());
                }
                else
                {
                    if (_ActiveSqlConnection == null)
                        _ActiveSqlConnection = new SqlConnection(getConnectionString());

                    if (!string.IsNullOrEmpty(_ActiveSqlConnection.ConnectionString) && _ActiveSqlConnection.State == ConnectionState.Closed)
                        _ActiveSqlConnection.Open();

                    if (SqlConnectionTimer == null)
                    {
                        SqlConnectionTimer = new System.Windows.Forms.Timer();
                        SqlConnectionTimer.Tick += new EventHandler(SqlConnectionTimer_Ticked);
                        SqlConnectionTimer.Interval = 2000;
                        SqlConnectionTimer.Start();
                    }

                    _LastSqlConnectionUsed = DateTime.Now;

                    return _ActiveSqlConnection;
                }
            }
        }
        private static SqlConnection _ActiveSqlConnection;
        private static DateTime _LastSqlConnectionUsed;

        private static void SqlConnectionTimer_Ticked(Object myObject, EventArgs myEventArgs)
        {
            terminateActiveSqlConnection();
        }
        private static TimeSpan _ActiveSqlConnectionOpenTime = new TimeSpan(0, 0, 3);

        public static void terminateActiveSqlConnection()
        {
            if (_ActiveSqlConnection != null && _ActiveSqlConnection.State == ConnectionState.Open && DateTime.Now - _LastSqlConnectionUsed > _ActiveSqlConnectionOpenTime)
                _ActiveSqlConnection.Close();

            if (SqlConnectionTimer != null && (_ActiveSqlConnection == null || _ActiveSqlConnection.State == ConnectionState.Closed))
                SqlConnectionTimer.Stop();
        }

        /// <summary><para></para></summary>
        public static bool isDBConnectionAvailable(System.Drawing.Icon icon, bool showError, bool showProgressBar)
        {
            var form = new Desktop.Forms.CheckDBConnection_Form(icon, showError, showProgressBar);
            Util.displayForm(null, form, false);
            return hasDBConnection = form.isDBConnectionAvailable;
        }

        /// <summary><para></para></summary>
        public static void testDBConnection()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
            }
        }

        #endregion
        /*******************************************************************************************************/

        /// <summary><para>Web MVC use only.</para></summary>
        public static SqlParameter getUnsanitizedSqlParameter(string name, object value) { return getSqlParameter(name, value, false); }
        public static SqlParameter getSqlParameter(string name, object value) { return getSqlParameter(name, value, true); }
        public static SqlParameter getSqlParameter(string name, object value, bool sanitize)
        {
            if (value != null && value.GetType() == typeof(string))
            {
                if (sanitize)
                    value = Util.sanitize(value.ToString());
                else
                    value = value.ToString();
            }

            return new SqlParameter(name, Util.wrapNullable(value));
        }

        public static string getWebConfigConnectionString(string key)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static DataTable getDataTable(string connectionString, string sqlOrStoredProcedureName, bool isStoredProcedure, params SqlParameter[] parameters)
        {
            DataSet dataset = getDataSet(connectionString, sqlOrStoredProcedureName, isStoredProcedure, parameters);
            if (dataset.Tables.Count > 0)
                return dataset.Tables[0];
            else
                return null;
        }
        public static DataSet getDataSet(string connectionString, string sqlOrStoredProcedureName, bool isStoredProcedure, params SqlParameter[] parameters)
        {
            DataSet dataset = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sqlOrStoredProcedureName, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                if(isStoredProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null && parameters.Length > 0)
                    foreach (SqlParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);

                adapter.Fill(dataset);
            }
            return dataset;
        }

        public static string getWebConnectionString(string datasource, string DBName, string userID, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder("Integrated Security=False;Persist Security Info=False;");
            builder.InitialCatalog = DBName;
            builder.DataSource = datasource;
            builder.UserID = userID;
            builder.Password = password;

            return builder.ConnectionString;
        }

    }
}
