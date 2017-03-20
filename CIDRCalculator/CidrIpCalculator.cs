using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRCalculator
{
    public class CidrIpCalculator
    {
        public CidrIpCalculator()
        {
            InputSubnetworks = null;
        }

        public IEnumerable<Subnetwork> Calculate()
        {
            IEnumerable<Subnetwork> sortedSubnetworks = SortInputSubnetworks();

            foreach (Subnetwork subnetwork in sortedSubnetworks)
            {
                // Nodes + broadcast + network.
                Int32 requiredNumber = subnetwork.NodesCount + 2;
                Byte bitsNumber = (Byte)Math.Ceiling(Math.Log(requiredNumber, 2));
                Byte maskBitsCount = (Byte)(32 - bitsNumber);

                IPv4Address networkAddress = IspDedicatedAddress.Clone();
                networkAddress.Mask = maskBitsCount;

                IncrementIpAddress(networkAddress);
            }

            return null;
        }

        private void IncrementIpAddress(IPv4Address address)
        {
            BitArray array = new BitArray(address.Address);
            
        }


        private IEnumerable<Subnetwork> SortInputSubnetworks()
        {
            return InputSubnetworks.OrderByDescending(x => x.NodesCount).ToList();
        }

        public IEnumerable<Subnetwork> InputSubnetworks { get; set; }
        public IPv4Address IspDedicatedAddress { get; set; }
    }
}
