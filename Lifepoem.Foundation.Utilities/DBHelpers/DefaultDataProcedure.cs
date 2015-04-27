using Lifepoem.Foundation.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lifepoem.Foundation.Utilities.DBHelpers
{
    /// <summary>
    /// Description: Default implementation of data procedure. It will implement all the common tasks of each Database Engine, for example, SqlServer or OleDb.
    /// Author: Vincent Ke
    /// </summary>
    public abstract class DefaultDataProcedure : IDataProcedure
    {
        #region Fields

        protected DbCommand _cmd = null;
        protected DbConnection _conn = null;

        #endregion

        #region Constructor

        public DefaultDataProcedure(string connectionString)
        {
            this.connectionString = connectionString;

            _conn = GetDbConnection();
            _cmd = GetDbCommand();
        }

        public DefaultDataProcedure(string commandText, string connectionString)
            : this(commandText, connectionString, CommandType.StoredProcedure)
        {
        }

        public DefaultDataProcedure(string commandText, string connectionString, CommandType commandType)
        {
            this.connectionString = connectionString;

            _conn = GetDbConnection();
            _cmd = GetDbCommand();
            _cmd.CommandText = commandText;
            _cmd.CommandType = CommandType;
        }

        ~DefaultDataProcedure()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        protected string connectionString = string.Empty;

        public string ConnectionString
        {
            get { return connectionString; }
        }

        public ConnectionState ConnectionState
        {
            get { return _conn.State; }
        }

        public Int32 CommandTimeout
        {
            get { return _cmd.CommandTimeout; }
            set { _cmd.CommandTimeout = value; }
        }

        public string CommandText
        {
            get { return _cmd.CommandText; }
            set { _cmd.CommandText = value; }
        }

        public CommandType CommandType
        {
            get { return _cmd.CommandType; }
            set { _cmd.CommandType = value; }
        }

        public DbParameterCollection Parameters
        {
            get { return _cmd.Parameters; }
        }

        public bool IsTransactional
        {
            get { return _cmd.Transaction != null; }
        }

        #endregion

        #region Transaction

        public void BeginTransaction()
        {
            _cmd.Transaction = _conn.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _cmd.Transaction = _conn.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            if (!IsTransactional)
            {
                throw new DataProcedureException("An attempt was made to commit a transaction when no trancation was created.");
            }

            _cmd.Transaction.Commit();

            if (_cmd.Transaction != null)
            {
                _cmd.Transaction.Dispose();
                _cmd.Transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            //if _cmd.Transaction.Commit(); throws an exception like Timeout, _cmd.Transaction will becomes null strangely.
            /*
            if (!IsTransactional)
            {
                throw new SqlProcedureException("An attempt was made to roll back a transaction when no transaction was created.");
            }
            */

            if (_cmd.Transaction != null)
            {
                _cmd.Transaction.Rollback();

                if (_cmd.Transaction != null)
                {
                    _cmd.Transaction.Dispose();
                    _cmd.Transaction = null;
                }
            }
        }

        #endregion

        #region Methods

        public int ExecuteNonQuery()
        {
            CheckNullParametersForCommand();
            return _cmd.ExecuteNonQuery();
        }

        public int ExecuteNonQuery(out int lastInsertId)
        {
            CheckNullParametersForCommand();
            int res = _cmd.ExecuteNonQuery();
            if (res != 0)
                lastInsertId = GetLastInsertId();
            else
                lastInsertId = 0;
            return res;
        }

        public object ExecuteScalar()
        {
            CheckNullParametersForCommand();
            return _cmd.ExecuteScalar();
        }

        public System.Data.IDataReader ExecuteDataReader()
        {
            CheckNullParametersForCommand();
            return _cmd.ExecuteReader();
        }

        public System.Data.DataTable ExecuteDataTable()
        {
            return ExecuteDataTable(CultureInfo.CurrentCulture);
        }

        public System.Data.DataTable ExecuteDataTable(CultureInfo culture)
        {
            DataSet ds = ExecuteDataSet(culture);
            return ds.Tables[0];
        }

        public System.Data.DataSet ExecuteDataSet()
        {
            return ExecuteDataSet(CultureInfo.CurrentCulture);
        }

        public System.Data.DataSet ExecuteDataSet(CultureInfo culture)
        {
            CheckNullParametersForCommand();
            IDbDataAdapter adapter = GetDataAdapter();

            DataSet ds = new DataSet();
            ds.Locale = culture;
            adapter.Fill(ds);
            return ds;
        }

        public ConnectionState OpenConnection()
        {
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            return _conn.State;
        }

        public ConnectionState CloseConnection()
        {
            if (_conn.State != ConnectionState.Closed)
            {
                _conn.Close();
            }

            return _conn.State;
        }

        public void Clear()
        {
            _cmd.CommandText = null;
            _cmd.Parameters.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!(_conn == null))
                {
                    _conn.Dispose();
                    _conn = null;
                }

                if (!(_cmd == null))
                {
                    _cmd.Dispose();
                    _cmd = null;
                }
            }
        }

        #endregion

        #region Paging functions

        public DataTable ExecuteDataTable(int start, int length, string orderBy)
        {
            ConvertPagingCommand(start, length, orderBy);
            return ExecuteDataTable(CultureInfo.CurrentCulture);
        }

        protected abstract void ConvertPagingCommand(int start, int length, string orderBy);

        #endregion

        #region Virtual functions

        protected abstract DbCommand GetDbCommand();

        protected abstract DbDataAdapter GetDataAdapter();

        protected abstract DbConnection GetDbConnection();

        protected abstract int GetLastInsertId();

        


        /// <summary>
        /// Check and Fix the null parameters of the DB Command
        /// Vincent Ke: 2014/9/20
        /// Whenever you use one of the following method, it will throw exception if item.Name is null
        ///     sqlProc.Parameters.AddWithValue("@Name", item.Name);
        ///     sqlProc.Parameters.Add("@Name", SqlDbType.VarChar, 50, item.Name);
        /// You have to use:
        ///    if (item.Name == null)
        ///    {
        ///        sqlProc.Parameters.AddWithValue("@Name", System.DBNull.Value);
        ///    }
        ///    else
        ///    {
        ///        sqlProc.Parameters.AddWithValue("@Name", item.Name);
        ///    }
        /// And that's tedious, so in order to use the simple syntax of 
        /// sqlProc.Parameters.AddWithValue("@Name", item.Name);
        /// we add this method to check and fix the parameters with null values
        /// </summary>
        protected virtual void CheckNullParametersForCommand()
        {
            if (_cmd != null && _cmd.Parameters != null)
            {
                foreach (DbParameter p in _cmd.Parameters)
                {
                    if (p != null
                        && p.Value == null
                        && (p.Direction == ParameterDirection.Input || p.Direction == ParameterDirection.InputOutput))
                    {
                        p.Value = DBNull.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Set the empty parameters to null
        /// For example, when you import data from flat file, set the empty column to NULL, not ''.
        /// </summary>
        public void SetEmptyParametersToNull()
        {
            if (_cmd != null && _cmd.Parameters != null)
            {
                foreach (DbParameter p in _cmd.Parameters)
                {
                    if (p != null
                        && string.IsNullOrEmpty(p.Value.ToString())
                        && (p.Direction == ParameterDirection.Input || p.Direction == ParameterDirection.InputOutput))
                    {
                        p.Value = DBNull.Value;
                    }
                }
            }
        }

        #endregion
    }
}
