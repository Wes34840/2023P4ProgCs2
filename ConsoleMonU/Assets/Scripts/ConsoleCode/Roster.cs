using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Roster
    {
        internal string name { get; set; }
        internal ConsoleMon[] lineUp { get; set; }


        public Roster()
        {

        }

        internal Roster(string name, ConsoleMon[] lineUp)
        {
            this.name = name;
            this.lineUp = lineUp;
        }
    }
}
