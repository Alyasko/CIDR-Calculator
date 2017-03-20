using System;
using System.Collections.Generic;
using System.Linq;
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

        public IPv4Address(byte firstOctet, byte secondOctet, byte thirdOctet, byte fourthOctet)
        {
            Address = new byte[4];

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

        public override string ToString()
        {
            return $"{Address[0]}.{Address[1]}.{Address[2]}.{Address[3]}/{Mask}";
        }

        public IPv4Address Clone()
        {
            return new IPv4Address(ToString());
        }

        public byte[] Address { get; set; }
        public byte Mask { get; set; }
    }
}
