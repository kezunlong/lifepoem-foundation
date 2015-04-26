using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.Utilities.Logging
{
    /// <summary>
    /// A dummy logger that doesn't log anything.
    /// </summary>
    public class DummyLogger : DefaultLogger
    {
        protected override void WriteLineImp(string messageToWrite, LoggerLevel level)
        {
        }
    }
}
