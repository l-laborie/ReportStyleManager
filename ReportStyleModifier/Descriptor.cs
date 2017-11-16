using System;
using System.Collections.Generic;

namespace ReportStyleModifier
{
    public class DescriptionException : Exception
    {
        public DescriptionException(string message):
            base(message)
        { }
    }

    public class Descriptor
    {
        private IDictionary<
            string,
            IDictionary<
                string,
                IDictionary<
                    string,
                    IList<string>
                    >
                >
            > data;

        private string separator;

        public string Separator
        {
            get { return this.separator; }
        }
        
        public Descriptor(IList<string> input, string separator = null)
        {
            this.separator = separator ?? "_";
            this.data = new Dictionary<
                string,
                IDictionary<
                    string,
                    IDictionary<
                        string,
                        IList<string>>>>();

            foreach (string row in input)
            {
                string[] row_parts = row.Split(
                    new string[] { this.separator }, StringSplitOptions.None);

                switch (row_parts.Length)
                {
                    case 3:
                        row_parts = new string[]
                        {
                            "_MAIN_",
                            row_parts[0],
                            row_parts[1],
                            row_parts[2]
                        };
                        break;

                    case 4:
                        break;

                    default:
                        throw new DescriptionException(
                            string.Format(
                                "The style declaration {0} haven't a good format.",
                                row));
                }

                if (!this.data.ContainsKey(row_parts[0]))
                    this.data.Add(row_parts[0],
                        new Dictionary<string, IDictionary<string, IList<string>>>());
                if (!this.data[row_parts[0]].ContainsKey(row_parts[1]))
                    this.data[row_parts[0]].Add(row_parts[1], 
                        new Dictionary<string, IList<string>>());
                if (!this.data[row_parts[0]][row_parts[1]].ContainsKey(row_parts[2]))
                    this.data[row_parts[0]][row_parts[1]].Add(
                        row_parts[2],
                        new List<string>());
                this.data[row_parts[0]][row_parts[1]][row_parts[2]].Add(row_parts[3]);
            }
        }

        public ICollection<string> GetLevel1Values()
        {
            return this.data.Keys;
        }

        public ICollection<string> GetLevel2Values(string level1)
        {
            return this.data[level1].Keys;
        }

        public ICollection<string> GetLevel3Values(string level1, string level2)
        {
            return this.data[level1][level2].Keys;
        }
        public ICollection<string> GetLevel4Values(string level1, string level2, string level3)
        {
            return this.data[level1][level2][level3];
        }

        public string RebuildValue(string level1, string level2, string level3, string level4)
        {
            if (level1 == "_MAIN_")
                return string.Join(this.Separator, level2, level3, level4);

            return string.Join(this.Separator, level1, level2, level3, level4);
        }
    }
}
