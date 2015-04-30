using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public interface IPinyinEncoding
    {
        IDictionary<string, CharacterPinyinMappingItem> CharacterPinyins { get; }
        DataTable GetCharacterPinyinTable();
        string GetPinyin(string characters);
        string GetPinyin(string characters, string splitter);
        string GetPinyinFirstChar(string characters);
        string GetPinyinFirstChar(string characters, string splitter);
    }







}
