using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public class GBEncodingFileReader : IEncodingFileReader
    {
        #region IReadHzbm Members

        public IDictionary<string, CharacterPinyinMappingItem> ReadCharacterPinyin(string encodingResourceFile)
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(encodingResourceFile))
            {
                IDictionary<string, CharacterPinyinMappingItem> hzpy = new Dictionary<string, CharacterPinyinMappingItem>();
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.Default);
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    line = line.Trim();
                    int index = line.IndexOf(" ");
                    if (index > 0)
                    {
                        string pinyin = line.Substring(0, index);
                        string hz = line.Substring(index).Trim();
                        if (pinyin != "" && hz != "")
                        {
                            for (int i = 0; i < hz.Length; i++)
                            {
                                string singleHz = hz.Substring(i, 1);
                                if (hzpy.ContainsKey(singleHz))
                                {
                                    (hzpy[singleHz]).Pinyins.Add(pinyin);
                                }
                                else
                                {
                                    hzpy.Add(singleHz, new CharacterPinyinMappingItem(singleHz, pinyin));
                                }
                            }
                        }
                    }
                }
                return hzpy;
            }
        }

        #endregion
    }
}
