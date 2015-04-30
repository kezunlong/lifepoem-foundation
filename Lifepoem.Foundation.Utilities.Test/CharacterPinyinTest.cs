using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lifepoem.Foundation.ChineseCharacterPinyin;

namespace Lifepoem.Foundation.Utilities.Test
{
    [TestClass]
    public class CharacterPinyinTest
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void TestCharacterPinyinNotone()
        {
            IPinyinEncoding pinyinRep = PinyinEncodingFactory.CreateCharacterPinyinEncodingInstance(PinyinEncodingType.Notune);
            string chinese = "中华人民共和国";
            string pinyin = "ZHONG HUA REN MIN GONG HE GUO";
            string pinyinFirstChar = "ZHRMGHG";
            string pinyinNotuneEncoding = pinyinRep.GetPinyin(chinese, " ");
            string pinyinFirstCharEncoding = pinyinRep.GetPinyinFirstChar(chinese, "");

            Assert.AreEqual(pinyin, pinyinNotuneEncoding);
            Assert.AreEqual(pinyinFirstChar, pinyinFirstCharEncoding);



        }

        [TestMethod]
        public void TestMultiPinyin()
        {
            IPinyinEncoding pinyinRep = PinyinEncodingFactory.CreateCharacterPinyinEncodingInstance(PinyinEncodingType.GB2312);

            string str = "好";
            var mappingItem = pinyinRep.CharacterPinyins[str];
            Assert.IsTrue(mappingItem.Pinyins.Count > 1);
        }
    }
}
