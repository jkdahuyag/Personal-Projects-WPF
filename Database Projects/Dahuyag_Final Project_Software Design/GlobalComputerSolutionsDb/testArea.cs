using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using GlobalComputerSolutionsDb.Core;
using NUnit.Framework;

namespace GlobalComputerSolutionsDb
{
    public class testArea
    {
        [Test]
        public void METHOD()
        {
            for (int i = 1; i <= 300; i++)
            {
                Console.WriteLine((i % 100) + 1);
                Console.WriteLine(((i + (int)Math.Floor(i / 100f)) % 20) + 1);
            }
        }
    }
}
