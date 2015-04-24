using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.Utilities.DBHelpers
{
    /// <summary>
    /// Description: The data procedure used to access OleDb Server database.
    /// Author: Vincent Ke(Vincent.Ke@irco.com)
    /// </summary>
    public class OleDbProcedure : DefaultDataProcedure
    {
        #region Constructor

        public OleDbProcedure(string connectionString) : base(connectionString) { }

        public OleDbProcedure(string commandText, string connectionString) : base(commandText, connectionString) { }

        public OleDbProcedure(string commandText, string connectionString, CommandType commandType) : base(commandText, connectionString, commandType) { }

        #endregion

        #region Properties

        public new OleDbParameterCollection Parameters
        {
            get { return (_cmd as OleDbCommand).Parameters; }
        }

        #endregion

        #region Virtual Functions Implementation

        protected override DbConnection GetDbConnection()
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        protected override DbCommand GetDbCommand()
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = _conn as OleDbConnection;
            return command;
        }

        protected override DbDataAdapter GetDataAdapter()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(_cmd as OleDbCommand);
            return adapter;
        }

        protected override int GetLastInsertId()
        {
            string sql = "SELECT @@identity";

            string commandText = this.CommandText;
            CommandType commandType = this.CommandType;

            this.CommandText = sql;
            this.CommandType = System.Data.CommandType.Text;
            int lastInsertId = int.Parse(ExecuteScalar().ToString());

            this.CommandText = commandText;
            this.CommandType = commandType;

            return lastInsertId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="orderBy"></param>
        protected override void ConvertPagingCommand(int start, int length, string orderBy)
        {
        }

        #endregion
    }
}
