using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public interface IEncodingFileReader
    {
        IDictionary<string, CharacterPinyinMappingItem> ReadCharacterPinyin(string encodingResourceFile);
    }




}
