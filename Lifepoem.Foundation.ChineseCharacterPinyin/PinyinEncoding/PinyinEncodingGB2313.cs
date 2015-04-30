using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public class PinyinEncodingGB2313 : DefaultPinyinEncoding
    {
        public PinyinEncodingGB2313()
            : base("Lifepoem.Foundation.ChineseCharacterPinyin.EncodingFile.hzbm_gb2312.txt", new GBEncodingFileReader())
        {
        }
    }
}
