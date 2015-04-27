using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifepoem.Foundation.Utilities.DBHelpers
{
    public class PagingOption
    {
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
