using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lifepoem.Foundation.Utilities.Exceptions
{
    public class DataProcedureException : DbException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataProcedureException"/> class.
        /// </summary>
        public DataProcedureException()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataProcedureException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DataProcedureException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataProcedureException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorCode">The error code.</param>
        public DataProcedureException(string message, int errorCode)
            : base(message, errorCode)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataProcedureException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DataProcedureException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataProcedureException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected DataProcedureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion
    }
}
