using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRConsoleView.Interfaces
{
    public interface ICsvCidrExporter : ICidrExporter
    {
        String Separator { get; set; }
        String OutputPath { get; set; }
    }
}
