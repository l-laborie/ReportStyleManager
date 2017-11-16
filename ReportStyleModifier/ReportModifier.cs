using System;
using System.Collections.Generic;
using System.Xml;

namespace ReportStyleModifier
{
    public class ReportModifierException : Exception
    {
        public ReportModifierException(string message):
            base(message)
        { }
    }
    public class ReportModifier
    {
        #region Private arguments

        private XmlDocument report_document;
        private Dictionary<string, string> xpath_by_level1 = new Dictionary<string, string>()
        {
            {"MAIN", "Body/ReportItems/Textbox" },
            {"TABLE", "TablixCell/CellContents/Textbox" }
            // TODO add "CHART"
        };
        private Dictionary<string, string> xpath_by_style_entry = new Dictionary<string, string>()
        {
            {"Font", "Paragraphs/Paragraph/TextRuns/TextRun/Style"},
            {"Default", "Style"}
        };
        private string report_path = null;
        #endregion

        #region Contructors
        public ReportModifier(string report_path)
        {
            this.report_document = new XmlDocument();
            this.report_document.Load(report_path);
            this.report_path = report_path;
        }

        public ReportModifier(XmlDocument report)
        {
            this.report_document = report;
        }

        #endregion

        #region Public Methods

        public void SetManagedStyle(string dataset_name)
        {
            Descriptor descriptor = this.getDescriptor(dataset_name);
            foreach(string level1 in descriptor.GetLevel1Values())
            {
                XmlNodeList textbox_list = this.getNodes(level1);
                foreach (XmlNode textbox in textbox_list)
                {
                    foreach(string level2 in descriptor.GetLevel2Values(level1))
                    {
                        if (textbox.Attributes["Name"].Value.EndsWith(
                            string.Format("_{0}", level2)))
                        {
                            // this textbox need the managed style
                            Dictionary<string, XmlNode> named_styles = this.getStyle(textbox);
                            foreach(string level3 in descriptor.GetLevel3Values(level1, level2))
                            {
                                XmlNode style = named_styles[level3];
                                foreach(string level4 in descriptor.GetLevel4Values(level1, level2, level3))
                                {
                                    string entry_value = string.Format(
                                        "=First(Fields!{0}.Value, \"{1}\")",
                                        descriptor.RebuildValue(level1, level2, level3, level4),
                                        dataset_name);
                                    XmlNode entry = this.report_document.CreateNode(XmlNodeType.Element, level4, "");
                                    entry.InnerText = entry_value;
                                    
                                    XmlNode current_entry = style.SelectSingleNode(this.rewriteXPath(level4, false));
                                    if (current_entry != null)
                                        style.RemoveChild(current_entry);
                                    style.AppendChild(entry);
                                }
                            }
                        }
                    }                    
                }
            }

            if (!string.IsNullOrEmpty(this.report_path))
                this.report_document.Save(this.report_path);
        }

        #endregion

        #region Private Methods

        private XmlNamespaceManager getNamespaceManagerFromRoot()
        {
            XmlNode root = this.report_document.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(
                this.report_document.NameTable);
            nsmgr.AddNamespace(
                "df", root.Attributes.GetNamedItem("xmlns").Value);

            return nsmgr;
        }

        private string rewriteXPath(string xpath, bool everywhere = true)
        {
            string[] xpath_parts = xpath.Split('/');
            if (everywhere)
                xpath = "//";
            else
                xpath = "";

            for (int i=0; i < xpath_parts.Length; i++)
                if (!string.IsNullOrEmpty(xpath_parts[i]))
                    xpath_parts[i] = string.Format(
                        "*[local-name()='{0}']", xpath_parts[i]);

            xpath += string.Join("/", xpath_parts);

            return xpath;
        }

        private bool smartTryGetValue(
            IDictionary<string, string> dictionary, string key, out string xpath)
        {
            return dictionary.TryGetValue(key.Replace("_", ""), out xpath);
        }
        
        private XmlNodeList getNodes(string level1)
        {
            XmlNode root = this.report_document.DocumentElement;
            XmlNamespaceManager nsmgr = this.getNamespaceManagerFromRoot();

            string xpath;
            if (!this.smartTryGetValue(this.xpath_by_level1, level1, out xpath))
                return null;

            xpath = this.rewriteXPath(xpath);
            return root.SelectNodes(xpath, nsmgr);
        }

        private Dictionary<string, XmlNode> getStyle(XmlNode textbox)
        {
            Dictionary<string, XmlNode> result = new Dictionary<string, XmlNode>();

            foreach(string key in this.xpath_by_style_entry.Keys)
            {
                string xpath = this.xpath_by_style_entry[key];
                xpath = this.rewriteXPath(xpath, false);

                result.Add(key, textbox.SelectSingleNode(xpath));
            }

            return result;
        }

        private Descriptor getDescriptor(string dataset_name)
        {
            string xpath = string.Format("{0}[@Name='{1}']",
                this.rewriteXPath("DataSets/DataSet"),
                dataset_name);
            XmlNode dataset = this.report_document.SelectSingleNode(xpath);
            if (dataset == null)
                throw new ReportModifierException(
                    string.Format(
                        "The report haven't dataset named {0} where found the styles.",
                        dataset_name));

            List<string> fields = new List<string>();
            xpath = this.rewriteXPath("Fields/Field", false);
            foreach(XmlNode field in dataset.SelectNodes(xpath))
                fields.Add(field.Attributes["Name"].Value);

            return new Descriptor(fields);
        }
        
        #endregion
    }
}
