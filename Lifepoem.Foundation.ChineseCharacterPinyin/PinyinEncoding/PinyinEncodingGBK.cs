using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public class PinyinEncodingGBK : DefaultPinyinEncoding
    {
        public PinyinEncodingGBK()
            : base("Lifepoem.Foundation.ChineseCharacterPinyin.EncodingFile.hzbm_gbk.txt", new GBEncodingFileReader())
        {
        }
    }
}
