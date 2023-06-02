using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Arena
    {
        bool FightActive = true;
        ConsoleMon fighterPL, fighterAI;
        internal void Fight(Roster RosterPL, Roster RosterAI)
        {
            fighterPL = RosterPL.lineUp[0];
            fighterAI = RosterAI.lineUp[0];
            while (!Array.TrueForAll(RosterPL.lineUp, c => c.health < 0) && !Array.TrueForAll(RosterAI.lineUp, c => c.health < 0))
            {
                while (fighterPL.health >= 0 && fighterAI.health >= 0)
                {
                    Console.Clear();
                    DisplayConsoleMonStats(fighterPL, fighterAI);
                    PlayTurn(fighterPL, fighterAI);
                }
                ReplaceDeadConsoleMon(RosterPL.lineUp, fighterPL, RosterAI.lineUp, fighterAI);
            }
            Roster[] rosters = new Roster[] { RosterPL, RosterAI };
            
            Roster Winner = Array.Find(rosters, r => Array.Exists(r.lineUp, c => c.health > 0)); // I did a thing
            Console.WriteLine(string.Empty);
            Console.WriteLine($"{Winner.name} has won the battle");
        }

        internal void PlayTurn(ConsoleMon fighterA, ConsoleMon fighterB)
        {
            UseSkill(fighterA, fighterB);
            if (CheckIfFainted(fighterB))
            {
                Console.WriteLine(fighterB.name + " fucking died");
                return;
            }
            UseSkill(fighterB, fighterA);
            if (CheckIfFainted(fighterA))
            {
                Console.WriteLine(fighterA.name + " fucking died");
                return;
            }
        }

        internal void UseSkill(ConsoleMon fighter, ConsoleMon target)
        {
            Random rand = new Random();
            List<Skill> availableSkills = fighter.skills.Where(i => i.energyCost <= fighter.energy).ToList();

            if (availableSkills.Count == 0)
            {
                fighter.Rest();
                Console.WriteLine(fighter.name + " used Rest and regained some energy");
                return;
            }

            Skill usedMove = availableSkills[rand.Next(availableSkills.Count)];
            Console.WriteLine(fighter.name + " used " + usedMove.name);

            Console.WriteLine(string.Empty);

            Console.WriteLine(target.name + " took " + usedMove.damage + " damage");

            Console.WriteLine(string.Empty);

            usedMove.UseOn(target, fighter);
        }

        internal void DisplayConsoleMonStats(ConsoleMon fighterA, ConsoleMon fighterB)
        {
            Console.WriteLine(fighterA.name);
            Console.WriteLine($"Health: {fighterA.health}");
            Console.WriteLine($"Energy: {fighterA.energy}");
            Console.WriteLine(string.Empty);
            Console.WriteLine(fighterB.name);
            Console.WriteLine($"Health: {fighterB.health}");
            Console.WriteLine($"Energy: {fighterB.energy}");
            Console.WriteLine(string.Empty);
        }
        internal bool CheckIfFainted(ConsoleMon fighter)
        {
            return fighter.health <= 0;
        }
        internal void ReplaceDeadConsoleMon(ConsoleMon[] RosterA, ConsoleMon fighterA, ConsoleMon[] RosterB, ConsoleMon fighterB)
        {
            if (fighterA.health < 0)
            {
                Console.WriteLine("at least you tried");
                ConsoleMon[] AvailableConsoleMon = RosterA.Where(c => c.health > 0).ToArray();
                if (AvailableConsoleMon.Length > 0)
                {
                    Console.WriteLine("Replaced A");
                    fighterPL = AvailableConsoleMon[0];
                }
                else
                {
                    return;
                }
            }
            else if (fighterB.health < 0)
            {
                ConsoleMon[] AvailableConsoleMon = RosterB.Where(c => c.health > 0).ToArray();
                if (AvailableConsoleMon.Length > 0)
                {
                    Console.WriteLine("Replaced B");
                    fighterAI = AvailableConsoleMon[0];
                    Console.WriteLine(fighterB.name);
                    Console.WriteLine(fighterB.health);
                }
                else 
                { 
                    return; 
                }
            }
        }
    }
}
