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
    /// Description: The interface of data procedure, used to access database.
    /// Author: Vincent Ke
    /// </summary>
    public interface IDataProcedure : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        string CommandText { get; set; }

        /// <summary>
        /// Gets or sets the command type.
        /// </summary>
        CommandType CommandType { get; set; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        DbParameterCollection Parameters { get; }

        #endregion

        #region Transaction

        /// <summary>
        /// Begins a transaction
        /// In order to use the transaction, typically the DAL class includes an IDataProcedure object
        /// that will be shared in all Repository classes within the DAL layer.
        /// Because all the Repository classes use the same IDataProcedure, so the Transaction function works.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Begins a transaction
        /// </summary>
        /// <param name="isolationLevel"></param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Commit the transaction
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollback the transaction
        /// </summary>
        void RollbackTransaction();

        #endregion

        #region Retrieval Methods

        /// <summary>
        /// Executes the command text and returns the number of rows affected.
        /// </summary>
        /// <returns></returns>
        int ExecuteNonQuery();

        /// <summary>
        /// Executes the command text and returns the scalar value.
        /// </summary>
        /// <returns>The scalar value returned.</returns>
        object ExecuteScalar();

        /// <summary>
        /// Executes the command text and returns the resulting data in a forward-only, as-available data structure.
        /// </summary>
        /// <returns></returns>
        IDataReader ExecuteDataReader();

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataTable" />.
        /// </summary>
        /// <returns></returns>
        DataTable ExecuteDataTable();

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataTable" />.
        /// This method is used for paging data
        /// </summary>
        /// <param name="start">The start record number(the first will be 1)</param>
        /// <param name="length">The total number of records that will be returned</param>
        /// <param name="orderBy">Order by clause, support: COLUMN1 ASC, COLUMN2 DESC</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(int start, int length, string orderBy);

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataTable" />.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>Returns a <see cref="System.Data.DataTable" /></returns>
        DataTable ExecuteDataTable(CultureInfo culture);

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataSet" />
        /// </summary>
        /// <returns>Returns a <see cref="System.Data.DataSet" /></returns>
        DataSet ExecuteDataSet();

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataSet" />
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>Returns a <see cref="System.Data.DataSet" /></returns>
        DataSet ExecuteDataSet(CultureInfo culture);

        /// <summary>
        /// Clears the existing command text and parameters.
        /// Does not close any existing connections.
        /// </summary>
        void Clear();

        #endregion
    }
}
