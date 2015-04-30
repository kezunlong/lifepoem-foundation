using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public class PinyinEncodingNoTune : DefaultPinyinEncoding
    {
        public PinyinEncodingNoTune()
            : base("Lifepoem.Foundation.ChineseCharacterPinyin.EncodingFile.hzbm_notune.txt", new NoTuneEncodingFileReader())
        {
        }
    }
}
