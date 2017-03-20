using System;
using System.Collections.Generic;
using CIDRCalculator;

namespace CIDRConsoleView.Interfaces
{
    public interface ICidrExporter
    {
        void Export();
        IList<Subnetwork> Subnetworks { get; set; }
    }
}
