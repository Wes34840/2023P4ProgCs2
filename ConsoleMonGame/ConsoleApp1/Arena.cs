using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Arena
    {

        internal void Fight(ConsoleMon fighterA, ConsoleMon fighterB)
        {
            ConsoleMon Winner;
            while (true)
            { 

                DisplayConsoleMonStats(fighterA, fighterB);

                UseSkill(fighterA, fighterB);
                if (CheckIfFainted(fighterB))
                {
                    Winner = fighterA;
                    Console.WriteLine(fighterB.name + " fucking died");
                    break;
                }
                UseSkill(fighterB, fighterA);
                if (CheckIfFainted(fighterA))
                {
                    Winner = fighterB;
                    Console.WriteLine(fighterA.name + " fucking died");
                    break;
                }
            }
            Console.WriteLine(string.Empty);
            Console.WriteLine(Winner.name + " has won the battle");
        }

        internal void UseSkill(ConsoleMon fighter, ConsoleMon target)
        {
            Random rand = new Random();
        //    fighter.skills.Where(i => i.energyCost <= fighter.energy).ToList();
            Skill usedMove = fighter.skills[rand.Next( fighter.skills.Count)];
            while (usedMove.energyCost >= fighter.energy)
            {
                usedMove = fighter.skills[rand.Next(fighter.skills.Count)];
            }
            usedMove.UseOn(target, fighter);
            Console.WriteLine(fighter.name + " used " + usedMove.name);
            Console.WriteLine(string.Empty);

            if (usedMove.name == "Rest")
            {
                Console.WriteLine(fighter.name + " regained a bit of energy");
            }
            else
            {
                Console.WriteLine(target.name + " took " + usedMove.damage + " damage");
            }

            Console.WriteLine(string.Empty);

        }

        internal void DisplayConsoleMonStats(ConsoleMon fighterA, ConsoleMon fighterB)
        {
            Console.WriteLine(fighterA.name);
            Console.WriteLine("Health: " + fighterA.health);
            Console.WriteLine("Energy: " + fighterA.energy);
            Console.WriteLine(string.Empty);
            Console.WriteLine(fighterB.name);
            Console.WriteLine("Health: " + fighterB.health);
            Console.WriteLine("Energy: " + fighterB.energy);
            Console.WriteLine(string.Empty);
        }
        internal bool CheckIfFainted(ConsoleMon fighter)
        {
            return fighter.health <= 0;
        }
    }
}
