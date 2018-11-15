using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace HK.DBManager {
    public sealed class DBManager : IDisposable
    {
        private IDbConnection idbConnection;
        private IDataReader idataReader;
        private IDbCommand idbCommand;
        private DataProvider providerType;
        private IDbTransaction idbTransaction = null;
        private IDbDataParameter[] idbParameters = null;
        private string strConnection;

        public DBManager()
        {
        }

        public DBManager(DataProvider providerType)
        {
            this.providerType = providerType;
        }

        public DBManager(string connectionString)
        {
            this.providerType = DataProvider.SqlServer;
            this.strConnection = connectionString;
        }

        private IDbConnection Connection
        {
            get
            {
                return idbConnection;
            }
        }

        private IDataReader DataReader
        {
            get
            {
                return idataReader;
            }
            set
            {
                idataReader = value;
            }
        }

        private DataProvider ProviderType
        {
            get
            {
                return providerType;
            }
            set
            {
                providerType = value;
            }
        }

        private string ConnectionString
        {
            get
            {
                return strConnection;
            }
            set
            {
                strConnection = value;
            }
        }

        private IDbCommand Command
        {
            get
            {
                return idbCommand;
            }
        }

        private IDbTransaction Transaction
        {
            get
            {
                return idbTransaction;
            }
        }

        private IDbDataParameter[] Parameters
        {
            get
            {
                return idbParameters;
            }
        }

        private void OpenConnection()
        {
            idbConnection =
            DBManagerFactory.GetConnection(this.providerType);
            idbConnection.ConnectionString = this.ConnectionString;
            if (idbConnection.State != ConnectionState.Open)
                idbConnection.Open();
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
        }

        private void CloseConnection()
        {
            if (idbConnection.State != ConnectionState.Closed)
                idbConnection.Close();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.CloseConnection();
            this.idbCommand = null;
            this.idbTransaction = null;
            this.idbConnection = null;
        }

        private void CreateParameters(int paramsCount)
        {
            idbParameters = new IDbDataParameter[paramsCount];
            idbParameters = DBManagerFactory.GetParameters(this.ProviderType, paramsCount);
        }

        private void AddParameters(int index, string paramName, object objValue)
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].Value = objValue;
            }
        }

        private void BeginTransaction()
        {
            if (this.idbTransaction == null)
                idbTransaction =
                DBManagerFactory.GetTransaction(this.ProviderType);
            this.idbCommand.Transaction = idbTransaction;
        }

        private void CommitTransaction()
        {
            if (this.idbTransaction != null)
                this.idbTransaction.Commit();
            idbTransaction = null;
        }

        private void RollbackTransaction()
        {
            if (this.idbTransaction != null)
                this.idbTransaction.Rollback();
            idbTransaction = null;
        }

        //private IDataReader ExecuteReader(CommandType commandType, string commandText)
        //{
        //    this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
        //    idbCommand.Connection = this.Connection;
        //    PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
        //    this.DataReader = idbCommand.ExecuteReader();
        //    idbCommand.Parameters.Clear();
        //    return this.DataReader;
        //}

        //private void CloseReader()
        //{
        //    if (this.DataReader != null)
        //        this.DataReader.Close();
        //}

        private void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
        {
            foreach (IDbDataParameter idbParameter in commandParameters)
            {
                if ((idbParameter.Direction == ParameterDirection.InputOutput)
                &&
                (idbParameter.Value == null))
                {
                    idbParameter.Value = DBNull.Value;
                }
                command.Parameters.Add(idbParameter);
            }
        }

        private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
        {
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText) {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            PrepareCommand(idbCommand, this.Connection, this.Transaction,
            commandType, commandText, this.Parameters);
            int returnValue = idbCommand.ExecuteNonQuery();
            idbCommand.Parameters.Clear();
            return returnValue;
        }

        public object ExecuteScalar(CommandType commandType, string commandText) {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            PrepareCommand(idbCommand, this.Connection, this.Transaction,
            commandType,
            commandText, this.Parameters);
            var returnValue = idbCommand.ExecuteScalar();
            idbCommand.Parameters.Clear();
            return returnValue;
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText) {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            PrepareCommand(idbCommand, this.Connection, this.Transaction,
            commandType,
            commandText, this.Parameters);
            IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter
            (this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }

        public DataTable ExecuteDataSet<T>(string storedProcedure, List<T> list, DataSet dsDataDetail = null) {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            int paramcount = props.Count;
            CreateParameters(paramcount);
            for (int i = 0; i < paramcount; i++) {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType) {
                    Type itemType = prop.PropertyType.GetGenericArguments()[0];
                    DataTable dt = dsDataDetail.Tables[itemType.Name.ToString()];
                    AddParameters(i, prop.Name + "Type", dsDataDetail.Tables[itemType.Name.ToString()]);
                }
                else {
                    AddParameters(i, prop.Name, prop.GetValue(list[0]));
                }
            }

            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            idbCommand.Parameters.Clear();
            return dataSet.Tables[0];
        }

        public DataTable ExecuteDataSet<T>(string storedProcedure, List<T> data) {

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            int paramcount = props.Count;
            CreateParameters(paramcount);
            for (int i = 0; i < paramcount; i++) {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType) {
                    throw new Exception();
                }
                else {
                    AddParameters(i, prop.Name, prop.GetValue(data[0]));
                }
            }

            return ExecuteDataSet(storedProcedure);
        }

        private DataTable ExecuteDataSet(string storedProcedure) {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            idbCommand.Parameters.Clear();
            return dataSet.Tables[0];
        }

        public DataTable GetDataTable<T>(string storedProcedure, List<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            int paramcount = props.Count;
            CreateParameters(paramcount);
            for (int i = 0; i < paramcount; i++)
            {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                }
                else
                {
                    AddParameters(i, prop.Name, prop.GetValue(list[0]) ?? DBNull.Value);
                }
            }

            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult.Tables[0];
        }

        public DataSet GetDataSet<T>(string storedProcedure, List<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            int paramcount = props.Count;
            CreateParameters(paramcount);
            for (int i = 0; i < paramcount; i++)
            {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                }
                else
                {
                    AddParameters(i, prop.Name, prop.GetValue(list[0]) ?? DBNull.Value);
                }
            }

            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }

        public DataTable GetDataTable<T>(string storedProcedure, T obj)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            int paramcount = props.Count;
            CreateParameters(paramcount);
            for (int i = 0; i < paramcount; i++)
            {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                }
                else
                {
                    AddParameters(i, prop.Name, prop.GetValue(obj) ?? DBNull.Value);
                }
            }

            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult.Tables[0];
        }

        public DataSet GetDataSet<T>(string storedProcedure, T obj)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            int paramcount = props.Count;
            CreateParameters(paramcount);
            for (int i = 0; i < paramcount; i++)
            {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                }
                else
                {
                    AddParameters(i, prop.Name, prop.GetValue(obj) ?? DBNull.Value);
                }
            }

            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }
        public DataSet GetDataSet(string storedProcedure)
        {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }
        public DataSet Post<T>(string storedProcedure, List<T> list, DataSet dsInputData = null)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            var primarykey = typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(KeyAttribute), false)).Select(e => e.Name).ToList();
            var listtypes = typeof(T).GetProperties().Where(prop => prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>)).Select(e => e.Name).ToList();
            int paramcount = props.Count;
            int j = 0;
            int dsInputDataCount = dsInputData == null ? 0 : dsInputData.Tables.Count;
            CreateParameters(paramcount + dsInputDataCount - listtypes.Count() - primarykey.Count());
            for (int i = 0; i < paramcount; i++)
            {
                if (!primarykey.Contains(props[i].Name.ToString()))
                {
                    var prop = props[i];

                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        //Type itemType = prop.PropertyType.GetGenericArguments()[0];
                        DataTable dt = dsInputData.Tables[prop.Name.ToString()];
                        AddParameters(j, prop.Name + "Type", dsInputData.Tables[prop.Name.ToString()]);
                        dsInputData.Tables.Remove(prop.Name.ToString());
                    }
                    else
                    {
                        AddParameters(j, prop.Name, prop.GetValue(list[0]) ?? DBNull.Value);
                    }
                    j++;
                }
            }
            if (dsInputDataCount > 0)
            {
                foreach (var dt in dsInputData.Tables)
                {
                    AddParameters(j++, dt.ToString() + "Type", dt);
                }
            }

            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }

        public DataSet Post<T>(string storedProcedure, T obj, DataSet dsInputData = null) where T : class
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            var primarykey = typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(KeyAttribute), false)).Select(e => e.Name).ToList();
            var listtypes = typeof(T).GetProperties().Where(prop => prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>)).Select(e => e.Name).ToList();
            int paramcount = props.Count;
            int j = 0;
            int dsInputDataCount = dsInputData == null ? 0 : dsInputData.Tables.Count;
            CreateParameters(paramcount + dsInputDataCount - listtypes.Count() - primarykey.Count());
            for (int i = 0; i < paramcount; i++)
            {

                if (!primarykey.Contains(props[i].Name.ToString()))
                {
                    var prop = props[i];

                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        //Type itemType = prop.PropertyType.GetGenericArguments()[0];
                        DataTable dt = dsInputData.Tables[prop.Name.ToString()];
                        AddParameters(j, prop.Name + "Type", dsInputData.Tables[prop.Name.ToString()]);
                        dsInputData.Tables.Remove(prop.Name.ToString());
                    }
                    else
                    {
                        AddParameters(j, prop.Name, prop.GetValue(obj) ?? DBNull.Value);
                    }
                    j++;
                }
            }
            if (dsInputDataCount > 0)
            {
                foreach (var dt in dsInputData.Tables)
                {
                    AddParameters(j++, dt.ToString() + "Type", dt);
                }
            }

            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }
        public DataSet Put<T>(string storedProcedure, List<T> list, DataSet dsInputData = null)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            var primarykey = typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(KeyAttribute), false)).Select(e => e.Name).ToList();
            var listtypes = typeof(T).GetProperties().Where(prop => prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>)).Select(e => e.Name).ToList();
            int paramcount = props.Count;
            int dsInputDataCount = dsInputData == null ? 0 : dsInputData.Tables.Count;
            CreateParameters(paramcount + dsInputDataCount - listtypes.Count() - primarykey.Count());
            int j = 0;
            for (int i = 0; i < paramcount; i++)
            {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    //Type itemType = prop.PropertyType.GetGenericArguments()[0];
                    DataTable dt = dsInputData.Tables[prop.Name.ToString()];
                    AddParameters(i, prop.Name + "Type", dsInputData.Tables[prop.Name.ToString()]);
                    dsInputData.Tables.Remove(prop.Name.ToString());
                }
                else
                {
                    AddParameters(i, prop.Name, prop.GetValue(list[0]) ?? DBNull.Value);
                }
                j++;
            }
            if (dsInputDataCount > 0)
            {
                foreach (var dt in dsInputData.Tables)
                {
                    AddParameters(j++, dt.ToString() + "Type", dt);
                }
            }
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }

        public DataSet Put<T>(string storedProcedure, T obj, DataSet dsInputData = null)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            var primarykey = typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(KeyAttribute), false)).Select(e => e.Name).ToList();
            var listtypes = typeof(T).GetProperties().Where(prop => prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>)).Select(e => e.Name).ToList();
            int paramcount = props.Count;
            int dsInputDataCount = dsInputData == null ? 0 : dsInputData.Tables.Count;
            CreateParameters(paramcount + dsInputDataCount - listtypes.Count() - primarykey.Count());
            int j = 0;
            for (int i = 0; i < paramcount; i++)
            {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    //Type itemType = prop.PropertyType.GetGenericArguments()[0];
                    DataTable dt = dsInputData.Tables[prop.Name.ToString()];
                    AddParameters(i, prop.Name + "Type", dsInputData.Tables[prop.Name.ToString()]);
                    dsInputData.Tables.Remove(prop.Name.ToString());
                }
                else
                {
                    AddParameters(i, prop.Name, prop.GetValue(obj) ?? DBNull.Value);
                }
                j++;
            }
            if (dsInputDataCount > 0)
            {
                foreach (var dt in dsInputData.Tables)
                {
                    AddParameters(j++, dt.ToString() + "Type", dt);
                }
            }
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }
        public DataSet Delete(string storedProcedure, string input, string sqlParameter)
        {
            CreateParameters(1);
            AddParameters(0, sqlParameter, input);
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }

        public DataSet Delete(string storedProcedure, int input, string sqlParameter)
        {
            CreateParameters(1);
            AddParameters(0, sqlParameter, input);
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.OpenConnection();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, CommandType.StoredProcedure,
                storedProcedure, this.Parameters);
            var dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            var dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            idbCommand.Parameters.Clear();
            return dsResult;
        }


    }
}
