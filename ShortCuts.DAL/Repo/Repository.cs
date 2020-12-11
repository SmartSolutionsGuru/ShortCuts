using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using ShortCuts.DAL.DatabaseHelpers;

namespace ShortCuts.DAL.Repo
{
    public class Repository : IRepository
    {
        #region  Query
        /// <summary>
        /// A simple function get the Query and return result
        /// </summary>
        /// <param name="query"> Query in form of string</param>
        /// <param name="parameters">parameters if there is</param>
        /// <returns>Returnns the resultset in form of Dictonery</returns>
        public async Task<List<Dictionary<string, object>>> QueryAsync(string query, Dictionary<string, object> parameters = null)
        {
            List<Dictionary<string, object>> resultValues = null;
            var con = new SQLiteConnection(ConnectionInfo.Instance.ConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con?.Open();
            }
            await Task.Run(() =>
            {
                try
                {
                    IDbCommand cmd = new SQLiteCommand(commandText: query, connection: con);
                    if (parameters != null)
                    {
                        var keys = parameters.Keys.ToList();
                        foreach (var key in keys)
                        {
                            var val = parameters[key];
                            if ((key == @"v_TableName" || key == @"v_database_name") && val is string)
                                cmd.CommandText = cmd?.CommandText?.Replace(key, val as string);

                            SQLiteParameter parameter = cmd.CreateParameter() as SQLiteParameter;
                            parameter.ParameterName = key;
                            parameter.DbType = GetDbType(val);
                            if (val == null)
                            {
                                parameter.Value = DBNull.Value;
                            }
                            else
                            {
                                parameter.Value = val;
                            }
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (reader.Read())
                    {
                        resultValues = resultValues ?? new List<Dictionary<string, object>>();
                        Dictionary<string, object> resultDict = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            resultDict[reader.GetName(i)] = reader[i] != DBNull.Value ? reader[i] : null;
                        }
                        resultValues.Add(resultDict);
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    con.Close();
                }
            });
            return resultValues;
        }
        #endregion

        #region Non Query
        /// <summary>
        /// Function For CRUD Opreations 
        /// Like Addintion Updateion And Deletions
        /// </summary>
        /// <param name="query"> Query for Opreations</param>
        /// <param name="parameters"> Parameters For Inserting Or Deletion Or Updating Values</param>
        /// <param name="outParameters"></param>
        /// <returns></returns>
        public async Task<int> NonQueryAsync(string query = null, Dictionary<string, object> parameters = null, Dictionary<string, object> outParameters = null)
        {
            int retVal = 0;
            await Task.Run(() =>
            {
                try
                {
                    var con = new SQLiteConnection(ConnectionInfo.Instance.ConnectionString);
                    if (con.State == ConnectionState.Closed)
                    {
                        con?.Open();
                    }
                    try
                    {

                        IDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = query;

                        if (parameters != null)
                        {
                            var keys = parameters.Keys.ToList();
                            foreach (var key in keys)
                            {
                                var value = parameters[key];

                                var parameter = cmd.CreateParameter();
                                parameter.ParameterName = key;
                                parameter.DbType = GetDbType(value);
                                parameter.Value = value;

                                cmd.Parameters.Add(parameter);
                            }
                        }

                        if (outParameters != null)
                        {
                            var keys = outParameters.Keys.ToList();
                            foreach (var key in keys)
                            {
                                var value = parameters[key];

                                var parameter = cmd.CreateParameter();
                                parameter.ParameterName = key;
                                parameter.DbType = GetDbType(value);
                                parameter.Value = value;
                                parameter.Direction = ParameterDirection.Output;

                                cmd.Parameters.Add(parameter);
                            }
                        }
                        retVal = cmd.ExecuteNonQuery();
                        if (outParameters != null)
                        {
                            var keys = outParameters.Keys.ToList();
                            foreach (var key in keys)
                            {
                                outParameters[key] = cmd.Parameters[key];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex?.Message?.ToLower()?.Contains("duplicate") == true)
                            retVal = -999;
                        else
                            ex.ToString();
                    }
                    finally
                    {
                        con.Dispose();
                    }
                }
                catch (Exception ex)
                {

                    ex.ToString();
                }
            });
            return retVal;
        }
        #endregion

        #region Public Helpers
        /// <summary>
        /// Creating Db Connection 
        /// </summary>
        /// <param name="connection"></param>
        public void CreateConnection(ConnectionInfo connection)
        {
            if (string.IsNullOrEmpty(ConnectionInfo.Instance.ConnectionString))
            {
                string path = Environment.CurrentDirectory;
                var fileName = "Assets\\ShortCuts.db";
                string[] appPath = path.Split(new string[] { "bin" }, StringSplitOptions.None);
                ConnectionInfo.Instance.ConnectionString = string.Concat("URI=file:", appPath[0], fileName);
            }
        }
        #endregion

        #region Private Helper Methods
        private DbType GetDbType(object value)
        {
            DbType dbtype = DbType.String;
            switch (value)
            {
                case "System.Int16":
                    dbtype = DbType.Int16;
                    break;
                case "System.Int32":
                    dbtype = DbType.Int32;
                    break;
                case "System.Int64":
                    dbtype = DbType.Int64;
                    break;
                case "System.UInt16":
                    dbtype = DbType.UInt16;
                    break;
                case "System.UInt32":
                    dbtype = DbType.UInt32;
                    break;
                case "System.UInt64":
                    dbtype = DbType.UInt64;
                    break;
                case "System.Double":
                    dbtype = DbType.Double;
                    break;
                case "System.Decimal":
                    dbtype = DbType.Decimal;
                    break;
                case "System.DateTime":
                    dbtype = DbType.DateTime;
                    break;
                case "System.Boolean":
                case "Boolean":
                case "bool":
                    dbtype = DbType.Boolean;
                    break;
                case "System.Byte":
                    dbtype = DbType.Byte;
                    break;
                case "System.Byte[]":
                    dbtype = DbType.Binary;
                    break;
                case "System.String":
                case "string":
                    dbtype = DbType.String;
                    break;
                default:
                    break;

            }
            return dbtype;
        }
        #endregion
    }
}
