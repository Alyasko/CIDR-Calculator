using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIDRCalculator;
using CIDRConsoleView.Interfaces;

namespace CIDRConsoleView.Implementation
{
    public class CsvCidrExporter : ICsvCidrExporter
    {
        public CsvCidrExporter()
        {
            Separator = ",";
        }

        public void Export()
        {
            String outputCsv = FormatHeader();
            int number = 1;

            foreach (Subnetwork subnetwork in Subnetworks)
            {
                outputCsv += $"{number}{Separator}{subnetwork.Name}{Separator}{subnetwork.NodesCount}{Separator}{subnetwork.NetworkAddressInfo.NetworkAddress}{Separator}" +
                             $"{subnetwork.NetworkAddressInfo.FirstNodeAddress}{Separator}{subnetwork.NetworkAddressInfo.LastNodeAddress}{Separator}" +
                             $"{subnetwork.NetworkAddressInfo.BroadcastAddress}{Separator}";

                outputCsv += "\n";

                number++;
            }

            File.WriteAllText(OutputPath, outputCsv);
        }

        private String FormatHeader()
        {
            return $"N{Separator}Network name{Separator}Nodes count{Separator}Network address{Separator}First node{Separator}Last node{Separator}Broadcast{Separator}\n";
        }

        public IList<Subnetwork> Subnetworks { get; set; }
        public string OutputPath { get; set; }
        public string Separator { get; set; }
    }
}
