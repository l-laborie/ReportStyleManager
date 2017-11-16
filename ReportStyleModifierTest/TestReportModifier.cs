using Microsoft.XmlDiffPatch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Xml;
using ReportStyleModifier;

namespace ReportStyleModifierTest
{
    [TestClass]
    public class TestReportModifier
    {
        public bool CompareXml(XmlNode originalNode, XmlNode finalNode, XmlWriter diffGramWriter)
        {
            XmlDiff xmldiff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder |
                XmlDiffOptions.IgnoreNamespaces | XmlDiffOptions.IgnorePrefixes);

            bool bIdentical = xmldiff.Compare(originalNode, finalNode, diffGramWriter);
            diffGramWriter.Close();
            return bIdentical;
        }

        [TestMethod]
        public void TestPass()
        {
            XmlDocument original_report = new XmlDocument();
            original_report.LoadXml(Properties.Resources.report_rdl);

            XmlDocument expected_report = new XmlDocument();
            expected_report.LoadXml(Properties.Resources.report_expected_rdl);

            ReportModifier report_modifier;
            report_modifier = new ReportModifier(original_report);
            report_modifier.SetManagedStyle("dsReportStyle");

            StringBuilder output = new StringBuilder();
            XmlWriter differences = XmlWriter.Create(output);

            bool are_same = this.CompareXml(
                original_report.DocumentElement,
                expected_report.DocumentElement,
                differences);

            Assert.IsTrue(are_same, output.ToString());
        }
    }
}
