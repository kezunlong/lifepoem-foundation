using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifepoem.Foundation.Utilities.DBHelpers
{
    public class PagingOption
    {
        /// <summary>
        /// The first record to obtain
        /// start from 1, so Start equals 11 if Length is 10
        /// </summary>
        public int Start { get; set; }

        public int Length { get; set; }

        public string OrderBy { get; set; }

        public bool GetRecordCount { get; set; }

        public int RecordCount { get; set; }

        public string PagingStoredProcedure = "sp_PagingRecord";

        public PagingOption()
        {
            GetRecordCount = false;
        }
    }
}
