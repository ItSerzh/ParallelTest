using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTest
{
    internal interface IOutput
    {
        void Write(string msg, ConsoleColor color = ConsoleColor.White);
    }
}
