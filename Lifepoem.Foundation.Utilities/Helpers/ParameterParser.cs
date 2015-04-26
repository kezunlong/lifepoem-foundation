using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifepoem.Foundation.Utilities
{
    /// <summary>
    /// This class is used to parse the application parameters.
    /// For example, the parameters of the Console/Windows Form applications.
    /// </summary>
    public sealed class ParameterParser
    {
        #region Properties

        private List<KeyValuePair<string, string>> parameters;

        public List<KeyValuePair<string, string>> Parameters
        {
            get
            {
                return parameters;
            }
        }

        #endregion

        #region Constructor

        public ParameterParser(string[] args)
        {
            ParseParameters(args);
        }

        #endregion

        #region Public methods

        public bool ParameterExists(string parameterName)
        {
            return Parameters.Any(p => p.Key == parameterName);
        }

        public string GetParameterValue(string parameterName)
        {
            return Parameters.FirstOrDefault(p => p.Key == parameterName).Value;
        }

        #endregion

        #region Help methods

        private void ParseParameters(string[] args)
        {
            parameters = new List<KeyValuePair<string, string>>();
            int i = 0;
            while (i < args.Length)
            {
                string pname = string.Empty;
                string pvalue = string.Empty;
                if (IsParameterName(args[i]))
                {
                    pname = args[i];
                    i++;
                    if (!IsParameterName(args[i]))
                    {
                        pvalue = args[i];
                        i++;
                    }
                    parameters.Add(new KeyValuePair<string, string>(pname, pvalue));
                }
                else
                {
                    throw new Exception(string.Format("Invalid parameter {0}.", args[i]));
                }
            }
        }

        private bool IsParameterName(string s)
        {
            return s.StartsWith("-") && s.Length > 1;
        }

        #endregion
    }
}
