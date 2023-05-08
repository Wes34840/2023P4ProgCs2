using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ConsoleMon
    {
        internal int health, energy;
        internal string name;
        private List<Skill> skills;
        internal Element weakness;

        internal void TakeDamage(int damage)
        {
            health -= damage;
        }

        internal void DepleteEnergy(int energy)
        {
            this.energy -= energy;
        }
    }
}
