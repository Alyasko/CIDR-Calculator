using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRCalculator
{
    public class NetworkAddressInfo
    {

        public IPv4Address NetworkAddress { get; set; }
        public IPv4Address BroadcastAddress { get; set; }
        public IPv4Address FirstNodeAddress { get; set; }
        public IPv4Address LastNodeAddress { get; set; }

    }
}
