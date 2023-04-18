using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dahuyag_AS2
{
    public class Occurances
    {
        public string Data { get; private set; }
        public int Count { get; private set; }

        public Occurances(string data, int count)
        {
            Data = data;
            Count = count;
        }
    }
}
