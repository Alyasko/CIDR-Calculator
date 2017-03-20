using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRConsoleView
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.Run();
            Console.ReadKey();
        }
    }
}
