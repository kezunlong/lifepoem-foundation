using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Foundation.ChineseCharacterPinyin
{
    public class PinyinEncodingFactory
    {
        /// <summary>
        /// Create an instance of Character Pinyin encoding instance
        /// </summary>
        /// <param name="encodingType"></param>
        /// <returns></returns>
        public static IPinyinEncoding CreateCharacterPinyinEncodingInstance(PinyinEncodingType encodingType)
        {
            switch (encodingType)
            {
                case PinyinEncodingType.GB2312:
                    return new PinyinEncodingGB2313();
                case PinyinEncodingType.GBK:
                    return new PinyinEncodingGBK();
                case PinyinEncodingType.Notune:
                    return new PinyinEncodingNoTune();
                default:
                    return new PinyinEncodingGB2313();
            }
        }
    }

    /// <summary>
    /// 
    /// GBK:
    /// 
    /// </summary>
    public enum PinyinEncodingType
    {
        // GB2312:
        // The "GB" stands for "guojia biaozhun", or "national standard". The encoding standard adopted in mainland China in 1981, GB2312-1980 includes 6,763 simplified characters. 
        GB2312,
        // GBK:
        // The "K" in GBK stands for "kuozhan", meaning "extension". Adopted in 1993, GBK retained the code positions of the original GB set while packing in the rest of the 21,886 characters required for compatibility with Unicode 2.1 (ISO 10646-1). 
        GBK,
        // Notune:
        // No tune for the pinyin
        Notune
    }
}
