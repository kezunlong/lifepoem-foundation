using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.Utilities.DBHelpers
{
    /// <summary>
    /// Description: The data procedure used to access SQL Server database.
    /// Author: Vincent Ke
    /// </summary>
    public class SqlProcedure : DefaultDataProcedure
    {
        #region Constructor

        public SqlProcedure(string connectionString) : base(connectionString) { }

        public SqlProcedure(string commandText, string connectionString) : base(commandText, connectionString) { }

        public SqlProcedure(string commandText, string connectionString, CommandType commandType) : base(commandText, connectionString, commandType) { }

        #endregion

        #region Properties

        public new SqlParameterCollection Parameters
        {
            get { return (_cmd as SqlCommand).Parameters; }
        }

        #endregion

        #region Virtual Functions Implementation

        protected override DbConnection GetDbConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        protected override DbCommand GetDbCommand()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = _conn as SqlConnection;
            return command;
        }

        protected override DbDataAdapter GetDataAdapter()
        {
            SqlDataAdapter adapter = new SqlDataAdapter(_cmd as SqlCommand);
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

        //protected override int GetLastInsertId()
        //{
        //    string sql = "SELECT @@identity";
        //    SqlProcedure proc = new SqlProcedure(sql, ConnectionString);
        //    proc.CommandType = System.Data.CommandType.Text;
        //    return int.Parse(proc.ExecuteScalar().ToString());
        //}

        /// <summary>
        /// Use Row_Number function to implement high efficiency paging.
        /// It's better to use a stored procedure instead.
        /// You can use this general purpose method in case you don't have a stored procedure.
        /// The first record will have index 1, so the second page(page number = 10): start = 11, length = 10
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="orderBy"></param>
        protected override void ConvertPagingCommand(int start, int length, string orderBy)
        {
            string sql = string.Format(@"
WITH C AS
(
	SELECT *, ROW_NUMBER() OVER (ORDER BY {0}) AS Row 
	FROM (
		{1}
	) AS T1
)
SELECT TOP {2} * 
FROM C
WHERE Row >= {3} and Row < {4}",
                                string.IsNullOrEmpty(orderBy) ? "(SELECT 0)" : orderBy,
                                this.CommandText,
                                length,
                                start,
                                start + length);

            this.CommandText = sql;
        }

        #endregion
    }
}
