using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public class DefaultPinyinEncoding : IPinyinEncoding
    {
        private IDictionary<string, CharacterPinyinMappingItem> _characterPinyins;

        public DefaultPinyinEncoding(string encodingResouceFile, IEncodingFileReader reader)
        {
            _characterPinyins = reader.ReadCharacterPinyin(encodingResouceFile);
        }

        #region ICharacterPinyinEncoding

        public IDictionary<string, CharacterPinyinMappingItem> CharacterPinyins
        {
            get { return _characterPinyins; }
        }

        public DataTable GetCharacterPinyinTable()
        {
            DataTable tb = new DataTable();
            DataColumn col = new DataColumn("Id", Type.GetType("System.Int32"));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 1;
            col.Unique = true;
            tb.Columns.Add(col);
            tb.Columns.Add(new DataColumn("Character", Type.GetType("System.String")));
            tb.Columns.Add(new DataColumn("Pinyin", Type.GetType("System.String")));
            tb.Columns.Add(new DataColumn("Pinyins", Type.GetType("System.String")));
            if (_characterPinyins != null)
            {
                tb.BeginLoadData();
                foreach (var item in _characterPinyins.Values)
                {
                    DataRow row = tb.NewRow();
                    row["Character"] = item.Character;
                    row["Pinyin"] = item.Pinyin;
                    row["Pinyins"] = String.Join(",", item.Pinyins);
                    tb.Rows.Add(row);
                }
                tb.EndLoadData();
            }
            return tb;
        }

        public string GetPinyin(string characters)
        {
            return GetPinyin(characters, " ");
        }

        public string GetPinyin(string characters, string splitter)
        {
            return GetPinyin(characters, splitter, false);
        }

        public string GetPinyinFirstChar(string characters)
        {
            return GetPinyinFirstChar(characters, " ");
        }

        public string GetPinyinFirstChar(string characters, string splitter)
        {
            return GetPinyin(characters, splitter, true);
        }

        #endregion

        #region Private helper methods

        private string GetPinyin(string characters, string splitter, bool firstPinyinChar)
        {
            if (characters == null || characters == "") return "";
            string pinyin = "";

            for (int i = 0; i < characters.Length; i++)
            {
                if (pinyin.Length > 0)
                    pinyin += splitter;

                string key = characters.Substring(i, 1);
                string py = "";
                bool find = false;
                if (_characterPinyins.ContainsKey(key))
                {
                    py = _characterPinyins[key].Pinyin;
                    find = true;
                }

                if (firstPinyinChar == false)	// full pinyin
                {
                    if (find)
                        pinyin += py;
                    else
                        pinyin += key;
                }
                else			                // first char of pinyin
                {
                    if (find)
                    {
                        if (py != null && py != "" && py.Length > 0)
                            pinyin += py.Substring(0, 1);
                    }
                    else
                        pinyin += key;
                }
            }
            return pinyin;
        }

        #endregion
    }
}
