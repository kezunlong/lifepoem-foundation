using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{

    public class NoTuneEncodingFileReader : IEncodingFileReader
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
                    string[] arr = line.Split(',');
                    if (arr.Length > 1)
                    {
                        if (hzpy.ContainsKey(arr[1]))
                            hzpy[arr[1]].Pinyins.Add(arr[0]);
                        else
                            hzpy.Add(arr[1], new CharacterPinyinMappingItem(arr[1], arr[0]));
                    }
                }
                return hzpy;
            }
        }
        #endregion
    }
}
