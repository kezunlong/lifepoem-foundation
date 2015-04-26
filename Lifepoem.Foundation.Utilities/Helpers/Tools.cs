using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Lifepoem.Foundation.Utilities
{
    public static class Tools
    {
        #region Convert<TInput, TOutput>

        /// <summary>
        /// Convert an object to a specific data type
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TOutput Convert<TInput, TOutput>(TInput obj)
        {
            return Convert<TInput, TOutput>(obj, default(TOutput));
        }

        /// <summary>
        /// Convert an object to a specific data type
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TOutput Convert<TInput, TOutput>(TInput obj, TOutput defaultValue)
        {
            try
            {
                // DBNull can't be converted to other types, however it will cost longer time if we use exception to handle DBNull
                if (obj is System.DBNull || obj == null)
                {
                    return defaultValue;
                }
                return (TOutput)System.Convert.ChangeType(obj, typeof(TOutput), System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string ConvertString<TInput>(TInput obj)
        {
            return Convert(obj, string.Empty);
        }

        public static decimal ConvertDecimal<TInput>(TInput obj)
        {
            return Convert(obj, default(decimal));
        }

        public static bool GetBoolean(object data)
        {
            if (data == DBNull.Value)
                return false;
            else
                return System.Convert.ToBoolean(data);
        }

        public static DateTime? ConvertNullableDateTime(object data)
        {
            if (data == DBNull.Value)
                return null;
            else if (data == null || data.ToString() == "")
                return null;
            else
            {
                DateTime result;
                if (DateTime.TryParse(data.ToString(), out result))
                    return result;
                else
                    return null;
            }
        }

        public static TOutput ConvertEnum<TOutput>(int value, TOutput defaultValue)
        {
            if (Enum.IsDefined(typeof(TOutput), value))
                return (TOutput)Enum.ToObject(typeof(TOutput), value);
            else
                return defaultValue;
        }

        public static TOutput ConvertEnum<TOutput>(string value, TOutput defaultValue)
        {
            if (Enum.IsDefined(typeof(TOutput), value))
                return (TOutput)Enum.Parse(typeof(TOutput), value);
            else
                return defaultValue;
        }

        #endregion

        #region String Functions

        public static string CombineStringList(List<string> list)
        {
            return CombineStringList(list, ",");
        }

        public static string CombineStringList(List<string> list, string separator)
        {
            if (list != null)
            {
                return string.Join(separator, list.ToArray());
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<string> SplitString(string str)
        {
            return SplitString(str, new char[] { ',', ';' });
        }

        public static List<string> SplitString(string str, char[] separator)
        {
            List<string> list = new List<string>();
            if (str != null && str.Length > 0)
            {
                list = str.Split(separator).ToList();
            }
            return list;
        }

        public static string GetFixedLengthString(string str, int length, bool addEllipsis)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length <= length)
                return str;

            return string.Format("{0}{1}", str.Substring(0, length), addEllipsis ? "..." : "");
        }

        #endregion

        #region SQL Methods

        /// <summary>
        /// Get filter string for Sql IN Clause: 'item1', 'item2', 'item3'
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GetSqlInClause(IEnumerable<string> items)
        {
            string result = "";
            foreach (var item in items)
            {
                result += string.Format("'{0}', ", item.Replace("'", "''"));
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 2);
            }
            return result;
        }

        public static string GetSqlInClause(IEnumerable<int> items)
        {
            string result = "";
            foreach (var item in items)
            {
                result += string.Format("{0}, ", item);
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 2);
            }
            return result;
        }

        #endregion

        #region Deep Clone Functions

        public static T DeepClone<T>(T obj) where T : class
        {
            var ds = new DataContractSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            ds.WriteObject(stream, obj);
            stream.Position = 0;
            return (T)ds.ReadObject(stream);
        }

        public static T DeepCloneUsingBinaryWriter<T>(T obj) where T : class
        {
            var ds = new DataContractSerializer(obj.GetType());
            MemoryStream s = new MemoryStream();
            using (XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter(s))
            {
                ds.WriteObject(w, obj);
                var length = s.Length;
            }

            var s2 = new MemoryStream(s.ToArray());
            using (XmlDictionaryReader r = XmlDictionaryReader.CreateBinaryReader(s2, XmlDictionaryReaderQuotas.Max))
            {
                return (T)ds.ReadObject(r);
            }
        }

        #endregion

        #region Serialization and Deserialization

        public static byte[] Serialization<T>(T obj) where T : class
        {
            var ds = new DataContractSerializer(typeof(T));
            var s = new MemoryStream();
            using (XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter(s))
            {
                ds.WriteObject(w, obj);
            }
            return s.ToArray();
        }

        public static T Deserialization<T>(byte[] bytes) where T : class, new()
        {
            T obj = new T();
            var ds = new DataContractSerializer(typeof(T));
            var s = new MemoryStream(bytes);
            using (XmlDictionaryReader r = XmlDictionaryReader.CreateBinaryReader(s, XmlDictionaryReaderQuotas.Max))
            {
                obj = (T)ds.ReadObject(r);
                return obj;
            }
        }

        public static string SerializeToXml(object obj)
        {
            string str = string.Empty;
            if (obj != null)
            {
                XmlSerializer ser = new XmlSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                ser.Serialize(stream, obj);
                str = UTF8Encoding.UTF8.GetString(stream.ToArray())
                     .Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "")
                     .Replace(" xsi:", " ")
                     .Replace(" xsd:", " ")
                     .Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"GB2312\"?>"); ;
                stream.Close();
            }
            return str;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Regular expression, which is used to validate an E-Mail address.
        /// </summary>
        public const string MatchEmailPattern =
                  @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        /// <summary>
        /// Checks whether the given Email-Parameter is a valid E-Mail address.
        /// </summary>
        /// <param name="email">Parameter-string that contains an E-Mail address.</param>
        /// <returns>True, when Parameter-string is not null and 
        /// contains a valid E-Mail address;
        /// otherwise false.</returns>
        public static bool IsEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }

        #endregion
    }
}
