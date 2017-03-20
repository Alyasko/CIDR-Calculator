using CIDRCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIDRConsoleView.Implementation;
using CIDRConsoleView.Interfaces;

namespace CIDRConsoleView
{
    class Worker
    {
        public void Run()
        {
            Console.WriteLine("Reading input data...");
            IList<Subnetwork> subnetworks = LoadSubnetworks("input.txt");

            Console.WriteLine("Calculating...");
            CidrIpCalculator calculator = new CidrIpCalculator();
            calculator.InputSubnetworks = subnetworks;
            calculator.IspDedicatedAddress = new IPv4Address("10.0.0.0/16");
            IList<Subnetwork> sortedProcessedSubnetworks = calculator.Calculate();

            Console.WriteLine("Exporting...");
            ICsvCidrExporter exporter = new CsvCidrExporter();
            exporter.Subnetworks = sortedProcessedSubnetworks;
            exporter.OutputPath = "output.csv";
            exporter.Export();
            Console.WriteLine("Complete.");
            Console.WriteLine("Press any key...");
        }

        public IList<Subnetwork> LoadSubnetworks(String path)
        {
            InputConfigReader reader = new InputConfigReader();
            reader.FilePath = path;
            return reader.LoadConfig();
        }
    }
}
