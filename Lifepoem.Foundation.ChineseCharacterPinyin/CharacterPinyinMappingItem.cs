using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public class CharacterPinyinMappingItem
    {
        public string Character { get; set; }

        public string Pinyin
        {
            get
            {
                if (Pinyins.Count > 0)
                    return Pinyins[0];
                return string.Empty;
            }
        }

        public List<string> Pinyins = new List<string>();

        public CharacterPinyinMappingItem(string character, string pinyin)
        {
            Character = character;
            Pinyins.Add(pinyin);
        }
    }
}
