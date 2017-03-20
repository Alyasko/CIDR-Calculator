using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRCalculator
{
    public class Subnetwork
    {
        public Subnetwork(String name, int nodesCount)
        {
            Name = name;
            NodesCount = nodesCount;
            NetworkAddressInfo = new NetworkAddressInfo();
        }

        public override string ToString()
        {
            return $"{Name}: {NodesCount}";
        }

        public String Name { get; set; }
        public int NodesCount { get; set; }
        public NetworkAddressInfo NetworkAddressInfo { get; set; }
    }
}
