using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ConsoleMon
    {
        public int health { get; set; }
        public int energy { get; set; }
        public string name { get; set; }
        public Element weakness { get; set; }
        public List<Skill> skills { get; set; }

        public ConsoleMon()
        {

        }

        internal ConsoleMon(int health, int energy, string name, Element weakness)
        {
            this.health = health;
            this.energy = energy;
            this.name = name;
            this.weakness = weakness;
        }


        internal void TakeDamage(int damage)
        {
            health -= damage;
        }

        internal void DepleteEnergy(int energy)
        {
            this.energy -= energy;
        }

        internal void Rest()
        {
            Random rand = new Random();
            int energyRecovery = rand.Next(20, 30);
            if (energy < 50)
            {
                Console.WriteLine($"{name} used Rest and regained some energy");
                energy += energyRecovery;
                if (energy > 50)
                {
                    energy = 50;
                }
            }
            else
            {
                Console.WriteLine($"{name} already has max energy");
            }
            Console.WriteLine(string.Empty);
        }

    }
}
