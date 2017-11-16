using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportStyleModifier;

namespace ReportModifierTest
{
    [TestClass]
    public class StyleDescriptor
    {
        private string[] pass_inputs = new string[]
        {
            "CHART_TITLE_Font_Family",
            "CHART_SUBTITLE_Font_Color",
            "MAIN_SUBTITLE_Font_Color",
            "TITLE_Font_Family",
            "TABLE_TITLE_Font_Family",
            "TABLE_SUBTITLE_Font_Color",
        };

        [TestMethod]
        public void TestPass()
        {
            Descriptor descriptor;

            descriptor = new Descriptor(
                this.pass_inputs, "_");

            List<String> level1 = new List<String>(
                descriptor.GetLevel1Values());
            Assert.AreEqual(4, level1.Count);
            Assert.IsTrue(level1.Contains("MAIN"));
            Assert.IsTrue(level1.Contains("_MAIN_"));
            Assert.IsTrue(level1.Contains("TABLE"));
            Assert.IsTrue(level1.Contains("CHART"));

            foreach(string key in level1)
            {
                List<String> level2 = new List<String>(
                descriptor.GetLevel2Values(key));
                if (key.Contains("MAIN"))
                    Assert.AreEqual(1, level2.Count);
                else
                    Assert.AreEqual(2, level2.Count);
                if (key != "MAIN")
                    Assert.IsTrue(level2.Contains("TITLE"));
                if (key != "_MAIN_")
                    Assert.IsTrue(level2.Contains("SUBTITLE"));

                foreach(string key2 in level2)
                {
                    List<string> values = new List<string>(
                        descriptor.GetLevel3Values(key, key2));
                    Assert.AreEqual(1, values.Count);
                    Assert.IsTrue(values.Contains("Font"));
                    values = new List<string>(
                        descriptor.GetLevel4Values(key, key2, "Font"));
                    if (key2 == "TITLE")
                        Assert.IsTrue(values.Contains("Family"));
                    else
                        Assert.IsTrue(values.Contains("Color"));
                }
            }
        }
    }
}
