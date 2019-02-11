using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using CleanArch.Core.Model;
using CleanArch.Core.Entities;
using CleanArch.Core.Repository.Quotation;
using System.Text;
using System.Text.RegularExpressions;
using CleanArch.Infrastructure.Core;
using Oracle.ManagedDataAccess.Client;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Linq;

namespace CleanArch.Infrastructure.Core
{
    public abstract class DBFactoryCore
    {
        protected readonly string FOCISConnectionString = string.Empty;
        protected readonly string SSPConnectionString = string.Empty;

        //IConfiguration configuration;
        //public DBFactoryCore(IConfiguration _configuration)
        //{
        //    configuration = _configuration;
            
        //}       
        public DBFactoryCore()
        {
           
            //var dfd=this.GetConnection();
            //var SSPString = this.GetConnection();
            //SSPConnectionString =Convert.ToString(SSPString);
            //FOCISConnectionString = "";// System.Configuration.ConfigurationManager.ConnectionStrings["FOCiS"].ConnectionString;
        }
        public string GetConnectionString(IConfiguration configuration, DBConnectionMode dbConnectionMode)
        {
            string FOCISConnectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            string SSPConnectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            var connectionString = dbConnectionMode == DBConnectionMode.FOCiS ? FOCISConnectionString : SSPConnectionString;
            //configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            return connectionString;
        }
        private void Read(string connectionString, string commandText, IDbDataParameter[] parameters, CommandType commandType, Action<IDataReader> act)
        {
            using (OracleConnection mConnection = new OracleConnection(connectionString))
            {
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.CommandText = commandText;
                    mCommand.CommandType = commandType;
                    if (parameters != null && parameters.Length > 0)
                    {
                        mCommand.Parameters.Clear();
                        mCommand.BindByName = true;
                        foreach (OracleParameter param in parameters)
                            mCommand.Parameters.Add(param);
                    }
                    try
                    {
                        using (OracleDataReader reader = mCommand.ExecuteReader())
                            while (reader.Read()) act(reader);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(commandText, ex);
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        internal void ReadText(string connectionString, string commandText, IDbDataParameter[] parameters, Action<IDataReader> act)
        {
            Read(connectionString, commandText, parameters, CommandType.Text, act);
        }

        internal IEnumerable<dynamic> ReadDynamic(DBConnectionMode dbConnectionMode, string commandText, IDbDataParameter[] parameters, CommandType commandType)
        {
            using (OracleConnection mConnection = new OracleConnection(
                    (dbConnectionMode == DBConnectionMode.FOCiS ? FOCISConnectionString : SSPConnectionString)
                ))
            {
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.CommandText = commandText;
                    mCommand.CommandType = commandType;
                    if (parameters != null && parameters.Length > 0)
                    {
                        mCommand.Parameters.Clear();
                        mCommand.BindByName = true;
                        foreach (OracleParameter param in parameters)
                            mCommand.Parameters.Add(param);
                    }
                    using (OracleDataReader reader = mCommand.ExecuteReader())
                    {

                        var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                        foreach (IDataRecord record in reader as IEnumerable)
                        {
                            var expando = new ExpandoObject() as IDictionary<string, object>;
                            foreach (var name in names)
                                expando[name.ToLowerInvariant()] = record[name];

                            yield return expando;
                        }
                    }
                }
            }
        }

        internal Tkey ExecuteScalar<Tkey>(string connectionString, string commandText, IDbDataParameter[] parameters)
        {
            using (OracleConnection mConnection = new OracleConnection(connectionString))
            {
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.CommandText = commandText;
                    mCommand.CommandType = CommandType.Text;
                    if (parameters != null && parameters.Length > 0)
                    {
                        mCommand.Parameters.Clear();
                        foreach (OracleParameter param in parameters)
                            mCommand.Parameters.Add(param);
                    }
                    try
                    {
                        object value = mCommand.ExecuteScalar();
                        if (value == null || value == DBNull.Value) return default(Tkey);
                        return (Tkey)Convert.ChangeType(value, typeof(Tkey));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(commandText, ex);
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        internal void ReadProcedure(string connectionString, string procName, IDbDataParameter[] parameters, Action<IDataReader> act)
        {
            Read(connectionString, procName, parameters, CommandType.StoredProcedure, act);
        }

        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            string mPropertyName = string.Empty;
            try
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    mPropertyName = prop.Name;
                    try
                    {
                        if (!object.Equals(dr[mPropertyName], DBNull.Value))
                            prop.SetValue(obj, dr[mPropertyName], null);
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                }
                list.Add(obj);
            }
            catch (ArgumentException ax)
            {
                throw new ArgumentException(mPropertyName + " : " + ax.Message, ax);
            }

            return list;
        }

        public static T DataReaderMapToObject<T>(IDataReader dr)
        {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            string mPropertyName = string.Empty;
            try
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    mPropertyName = prop.Name;
                    try
                    {
                        if (!object.Equals(dr[mPropertyName], DBNull.Value))
                            prop.SetValue(obj, dr[mPropertyName], null);
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                }
            }
            catch (ArgumentException ax)
            {
                throw new ArgumentException(mPropertyName + " : " + ax.Message, ax);
            }

            return obj;
        }

        internal DataTable ReturnDatatable(DBConnectionMode dbConnectionMode, string commandText, IDbDataParameter[] parameters)
        {
            DataTable dtData = FillDatatableReturn(dbConnectionMode, commandText, parameters);
            return dtData;
        }

        private DataTable FillDatatableReturn(DBConnectionMode dbConnectionMode, string commandText, IDbDataParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (OracleConnection mConnection = new OracleConnection(
                    (dbConnectionMode == DBConnectionMode.FOCiS ? FOCISConnectionString : SSPConnectionString)
                ))
            {
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.CommandText = commandText;
                    mCommand.CommandType = CommandType.Text;
                    if (parameters != null && parameters.Length > 0)
                    {
                        mCommand.Parameters.Clear();
                        mCommand.BindByName = true;
                        foreach (OracleParameter param in parameters)
                            mCommand.Parameters.Add(param);
                    }
                    try
                    {
                        using (OracleDataReader reader = mCommand.ExecuteReader())
                            dt.Load(reader);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(commandText, ex);
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                    return dt;
                }
            }
        }

        internal DataTable ReturnCursorDatatable(DBConnectionMode dbConnectionMode, string commandText, IDbDataParameter[] parameters)
        {
            DataTable dtData = FillDatatableBasedonOutParameterReturn(dbConnectionMode, commandText, parameters);
            return dtData;
        }

        private DataTable FillDatatableBasedonOutParameterReturn(DBConnectionMode dbConnectionMode, string commandText, IDbDataParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (OracleConnection mConnection = new OracleConnection(
                    (dbConnectionMode == DBConnectionMode.FOCiS ? FOCISConnectionString : SSPConnectionString)
                ))
            {
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.CommandText = commandText;
                    mCommand.CommandType = CommandType.StoredProcedure;


                    if (parameters != null && parameters.Length > 0)
                    {
                        mCommand.Parameters.Clear();
                        mCommand.BindByName = true;
                        foreach (OracleParameter param in parameters)
                            mCommand.Parameters.Add(param);
                    }
                    try
                    {
                        using (OracleDataReader reader = mCommand.ExecuteReader())
                            dt.Load(reader);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(commandText, ex);
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                    return dt;
                }
            }
        }
    }

    public enum DBConnectionMode
    {
        FOCiS,
        SSP
    }
    public class DataSettings
    {
        public string DataConnectionString { get; set; }
    }
    public class DataSettingsManager
    {
        private const string _dataSettingsFilePath = "App_Data/dataSettings.json";
        public virtual DataSettings LoadSettings()
        {
            var text = File.ReadAllText(_dataSettingsFilePath);
            if (string.IsNullOrEmpty(text))
                return new DataSettings();

            //get data settings from the JSON file  
            DataSettings settings = JsonConvert.DeserializeObject<DataSettings>(text);
            return settings;
        }

    }
    public static class MyClass
    {
        public static T ConvertToEntity<T>(this DataRow tableRow) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();
            try
            {
                foreach (DataColumn col in tableRow.Table.Columns)
                {
                    string colName = col.ColumnName;

                    // Look for the object's property with the columns name, ignore case
                    PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    // did we find the property ?
                    if (pInfo != null)
                    {
                        object val = tableRow[colName];

                        // is this a Nullable<> type
                        bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null && !string.IsNullOrEmpty(pInfo.PropertyType.ToString()));
                        if (IsNullable)
                        {
                            if (val is System.DBNull)
                            {
                                val = null;
                            }
                            else
                            {
                                // Convert the db type into the T we have in our Nullable<T> type
                                val = Convert.ChangeType(val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                            }
                        }
                        else
                        {
                            // Convert the db type into the type of the property in our entity
                            if (val is System.DBNull)
                            {
                                if (pInfo.PropertyType.FullName is System.String)
                                {
                                }
                                else
                                {
                                    val = 0;
                                }

                            }
                            val = Convert.ChangeType(val, pInfo.PropertyType);
                        }
                        // Set the value of the property with the value from the db
                        pInfo.SetValue(returnObject, val, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            // return the entity object with values
            return returnObject;
        }

        public static T DataReaderMapToObject<T>(this DataRow tableRow, DataColumnCollection dc)
        {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            string mPropertyName = string.Empty;
            try
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    mPropertyName = prop.Name;
                    try
                    {
                        if (dc.Contains(mPropertyName))
                        {
                            if (!object.Equals(tableRow[mPropertyName], DBNull.Value))
                                prop.SetValue(obj, tableRow[mPropertyName], null);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                }
            }
            catch (ArgumentException ax)
            {
                throw new ArgumentException(mPropertyName + " : " + ax.Message, ax);
            }

            return obj;
        }

        public static List<T> ConvertToList<T>(this DataTable table) where T : new()
        {
            Type t = typeof(T);

            // Create a list of the entities we want to return
            List<T> returnObject = new List<T>();

            // Iterate through the DataTable's rows
            foreach (DataRow dr in table.Rows)
            {
                // Convert each row into an entity object and add to the list
                //T newRow = dr.ConvertToEntity<T>();
                T newRow = DataReaderMapToObject<T>(dr, table.Columns);
                returnObject.Add(newRow);
            }

            // Return the finished list
            return returnObject;
        }

        public static T ConvertToEntity<T>(this DataTable table) where T : new()
        {
            Type t = typeof(T);

            // Create a list of the entities we want to return
            T returnObject = new T();

            // Iterate through the DataTable's rows
            foreach (DataRow dr in table.Rows)
            {
                // Convert each row into an entity object and add to the list
                //T newRow = dr.ConvertToEntity<T>();
                returnObject = DataReaderMapToObject<T>(dr, table.Columns);

            }

            // Return the finished list
            return returnObject;
        }
    }
}
