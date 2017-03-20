using CIDRCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRConsoleView
{
    class Worker
    {
        public void Run()
        {
            IEnumerable<Subnetwork> subnetworks = LoadSubnetworks("input.txt");

            CidrIpCalculator calculator = new CidrIpCalculator();
            calculator.InputSubnetworks = subnetworks;
            calculator.IspDedicatedAddress = new IPv4Address("10.0.0.0/16");
            calculator.Calculate();
        }

        public IEnumerable<Subnetwork> LoadSubnetworks(String path)
        {
            InputConfigReader reader = new InputConfigReader();
            reader.FilePath = path;
            return reader.LoadConfig();
        }
    }
}
