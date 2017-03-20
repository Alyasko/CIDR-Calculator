using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace CIDRCalculator
{
    public class IPv4Address
    {
        public IPv4Address()
        {
            Address = new byte[4];
            Mask = 0;
        }

        public IPv4Address(byte[] address, byte mask): this()
        {
            for (int i = 0; i < address.Length; i++)
            {
                Address[i] = address[i];
            }

            Mask = mask;
        }

        public IPv4Address(String ipAddressString) : this()
        {
            this.Parse(ipAddressString);
        }

        public IPv4Address(UInt32 mergedAddress, Byte mask) : this()
        {
            this.Parse(mergedAddress, mask);
        }

        public void Parse(String ipAddressString)
        {
            String[] parts = ipAddressString.Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            byte mask = 0;
            if (byte.TryParse(parts[1], out mask))
            {
                Mask = mask;
            }

            String[] octets = parts[0].Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < octets.Length; i++)
            {
                String octet = octets[i];
                byte byteValue = 0;
                if (byte.TryParse(octet, out byteValue))
                {
                    Address[i] = byteValue;
                }
            }
        }

        public void Parse(UInt32 ipAddressValue, Byte mask)
        {
            for (int i = 0; i < Address.Length; i++)
            {
                Address[i] = (Byte) (ipAddressValue >> (Address.Length - 1 - i) * 8);
            }

            Mask = mask;
        }

        public UInt32 ConvertToUInt32()
        {
            UInt32 mergedIpAddress = 0;

            // Create IP 32-bit address.
            for (int i = 0; i < Address.Length; i++)
            {
                mergedIpAddress |= Address[i];
                if (i != Address.Length - 1)
                {
                    mergedIpAddress <<= 8;
                }
            }

            return mergedIpAddress;
        }

        public override string ToString()
        {
            return $"{Address[0]}.{Address[1]}.{Address[2]}.{Address[3]}/{Mask}";
        }

        /// <summary>
        /// Creates instance of ip address with current parameters.
        /// </summary>
        /// <returns></returns>
        public IPv4Address Clone()
        {
            return new IPv4Address(ToString());
        }

        public byte[] Address { get; set; }
        public byte Mask { get; set; }
    }
}
