using CIDRCalculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CIDRConsoleView
{
    class InputConfigReader
    {
        public InputConfigReader()
        {
            FilePath = null;
        }

        public String FilePath { get; set; }

        public IEnumerable<Subnetwork> LoadConfig()
        {
            List<Subnetwork> subnetworks = new List<Subnetwork>();

            if(File.Exists(FilePath))
            {
                String[] fileLines = File.ReadAllLines(FilePath);
                foreach (String line in fileLines)
                {
                    Match match = Regex.Match(line, @"(?<name>\w+)\s+(?<hosts>\d+)");
                    if(match.Success)
                    {
                        String name = match.Groups["name"].Value;
                        Int32 hostsCount = Convert.ToInt32(match.Groups["hosts"].Value);

                        subnetworks.Add(new Subnetwork(name, hostsCount));
                    }
                }
            }

            return subnetworks;
        }
    }
}
