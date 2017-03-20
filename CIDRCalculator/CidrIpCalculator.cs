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

        /// <summary>
        /// Goes through input subnetworks and calculates details of each subnetwork according to ISP IP address.
        /// </summary>
        /// <returns>New instance of subnetworks list. Subnetworks instances are the same.</returns>
        public IList<Subnetwork> Calculate()
        {
            IList<Subnetwork> sortedSubnetworks = SortInputSubnetworks();

            IPv4Address rootAddress = IspDedicatedAddress;

            foreach (Subnetwork subnetwork in sortedSubnetworks)
            {
                // Nodes + broadcast + network.
                Int32 requiredNumber = subnetwork.NodesCount + 2;
                Byte bitsNumber = (Byte)Math.Ceiling(Math.Log(requiredNumber, 2));
                Byte maskBitsCount = (Byte)(32 - bitsNumber);

                IPv4Address networkAddress = rootAddress;
                networkAddress.Mask = maskBitsCount;

                subnetwork.NetworkAddressInfo.NetworkAddress = networkAddress;
                subnetwork.NetworkAddressInfo.BroadcastAddress = GetBroadcastAddress(networkAddress);
                subnetwork.NetworkAddressInfo.FirstNodeAddress = GetFirstNodeAddress(networkAddress);
                subnetwork.NetworkAddressInfo.LastNodeAddress = GetLastNodeAddress(networkAddress);

                rootAddress = networkAddress.Clone();

                IncrementIpAddress(rootAddress);
            }

            return sortedSubnetworks;
        }

        /// <summary>
        /// Returns the last node address of specified network address.
        /// </summary>
        /// <param name="networkAddress">Input network address.</param>
        /// <returns></returns>
        private IPv4Address GetLastNodeAddress(IPv4Address networkAddress)
        {
            UInt32 mergedBroadcastAddress = GetBroadcastAddress(networkAddress).ConvertToUInt32();

            UInt32 hostsMask = UInt32.MaxValue >> networkAddress.Mask;
            UInt32 maxHostsNumber = hostsMask - 2;

            if (maxHostsNumber > 0)
            {
                mergedBroadcastAddress--;
            }
            else
            {
                throw new InvalidOperationException("Host part of address is not wide enough to have one host.");
            }

            return new IPv4Address(mergedBroadcastAddress, networkAddress.Mask);
        }


        /// <summary>
        /// Returns the first node address of specified network address.
        /// </summary>
        /// <param name="networkAddress">Input network address.</param>
        /// <returns></returns>
        private IPv4Address GetFirstNodeAddress(IPv4Address networkAddress)
        {
            UInt32 mergedAddress = networkAddress.ConvertToUInt32();

            UInt32 hostsMask = UInt32.MaxValue >> networkAddress.Mask;
            UInt32 maxHostsNumber = hostsMask - 2;

            if (maxHostsNumber > 0)
            {
                mergedAddress++;
            }
            else
            {
                throw new InvalidOperationException("Host part of address is not wide enough to have one host.");
            }

            return new IPv4Address(mergedAddress, networkAddress.Mask);
        }

        /// <summary>
        /// Returns the broadcast address of specified network address.
        /// </summary>
        /// <param name="networkAddress">Input network address.</param>
        /// <returns></returns>
        private IPv4Address GetBroadcastAddress(IPv4Address networkAddress)
        {
            UInt32 mergedAddress = networkAddress.ConvertToUInt32();
            mergedAddress |= UInt32.MaxValue >> networkAddress.Mask;

            return new IPv4Address(mergedAddress, networkAddress.Mask);
        }

        /// <summary>
        /// Increments network part of IP address.
        /// </summary>
        /// <param name="address">IP address to be incremented.</param>
        private void IncrementIpAddress(IPv4Address address)
        {
            UInt32 mergedIpAddress = address.ConvertToUInt32();

            Byte shiftBitsCount = (Byte) (32 - address.Mask);
            UInt32 shiftedAddress = mergedIpAddress >> (shiftBitsCount);

            shiftedAddress++;
            mergedIpAddress = (shiftedAddress << shiftBitsCount) | (mergedIpAddress & (UInt32.MaxValue >> address.Mask));

            address.Parse(mergedIpAddress, address.Mask);
        }

        /// <summary>
        /// Sorts subnetworks by hosts count.
        /// </summary>
        /// <returns></returns>
        private IList<Subnetwork> SortInputSubnetworks()
        {
            return InputSubnetworks.OrderByDescending(x => x.NodesCount).ToList();
        }

        public IEnumerable<Subnetwork> InputSubnetworks { get; set; }
        public IPv4Address IspDedicatedAddress { get; set; }
    }
}
