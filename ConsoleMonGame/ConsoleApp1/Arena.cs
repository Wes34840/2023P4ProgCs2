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
        ConsoleMon fighterPL, fighterAI;
        internal void Fight(Roster RosterPL, Roster RosterAI)
        {
            fighterPL = RosterPL.lineUp[0];
            fighterAI = RosterAI.lineUp[0];
            Console.WriteLine("\n\n");
            while (!Array.TrueForAll(RosterPL.lineUp, c => c.health < 0) && !Array.TrueForAll(RosterAI.lineUp, c => c.health < 0))
            {
                while (fighterPL.health >= 0 && fighterAI.health >= 0)
                {
                    DisplayConsoleMonStats(fighterPL, fighterAI);
                    PlayTurn(fighterPL, fighterAI);
                }
                ReplaceDeadConsoleMon(RosterPL.lineUp, fighterPL, RosterAI.lineUp, fighterAI);
            }
            Roster[] rosters = new Roster[] { RosterPL, RosterAI };
            
            Roster Winner = Array.Find(rosters, r => Array.Exists(r.lineUp, c => c.health > 0)); // I did a thing

            Console.WriteLine($"{Winner.name} has won the battle");
        }

        internal void PlayTurn(ConsoleMon fighterA, ConsoleMon fighterB)
        {
            ShowUI();
            PlayerTurn();

            if (CheckIfFainted(fighterB))
            {
                Console.WriteLine($"{fighterB.name} fucking died\n");
                return;
            }

            UseSkill(fighterB, fighterA);
            if (CheckIfFainted(fighterA))
            {
                Console.WriteLine($"{fighterA.name} fucking died\n");
                return;
            }
        }
        internal void ShowUI()
        {
            Console.WriteLine("Choose your move\n");

            foreach (Skill skill in fighterPL.skills)
            {
                Console.WriteLine($"Name: {skill.name} \nDamage: {skill.damage} \t Energy Cost: {skill.energyCost} \t Element: {skill.element}\n");
            }
            Console.WriteLine($"Rest: \nSkip your turn and regain energy \n");
        }

        internal void PlayerTurn()
        {
            
            string input = Console.ReadLine().ToLower();
            if (fighterPL.skills.Exists(i => i.name.ToLower() == input))
            {
                List<Skill> useMove = fighterPL.skills.Where(i => i.name.ToLower() == input).ToList();
                
                if (useMove[0].energyCost > fighterPL.energy)
                {
                    Console.WriteLine($"{fighterPL.name} has too little energy for this move, try another or rest");
                    PlayerTurn();
                    return;
                }

                Console.Clear();
                Console.WriteLine("\n\n");

                Console.WriteLine($"{fighterPL.name} used {useMove[0].name} \n");
                useMove[0].UseOn(fighterAI, fighterPL);
            }
            else if (input.ToLower() == "rest")
            {
                Console.Clear();
                Console.WriteLine("\n\n");
                fighterPL.Rest();
            }
            else
            {
                Console.WriteLine("Invalid skill name, try again");
                PlayerTurn();
            }
        }
        
        

        internal void UseSkill(ConsoleMon fighter, ConsoleMon target)
        {
            Random rand = new Random();
            List<Skill> availableSkills = fighter.skills.Where(i => i.energyCost <= fighter.energy).ToList();

            if (availableSkills.Count == 0)
            {
                fighter.Rest();
                return;
            }

            Skill usedMove = availableSkills[rand.Next(availableSkills.Count)];
            Console.WriteLine($"{fighter.name} used {usedMove.name} \n");

            usedMove.UseOn(target, fighter);
        }

        internal void DisplayConsoleMonStats(ConsoleMon fighterA, ConsoleMon fighterB)
        {
            Console.WriteLine($"{fighterA.name} \nHealth: {fighterA.health} \nEnergy: {fighterA.energy} \n");
            Console.WriteLine($"{fighterB.name} \nHealth: {fighterB.health} \nEnergy: {fighterB.energy} \n");
        }
        internal bool CheckIfFainted(ConsoleMon fighter)
        {
            return fighter.health <= 0;
        }
        internal void ReplaceDeadConsoleMon(ConsoleMon[] RosterA, ConsoleMon fighterA, ConsoleMon[] RosterB, ConsoleMon fighterB)
        {
            if (fighterA.health < 0)
            {
                ConsoleMon[] AvailableConsoleMon = RosterA.Where(c => c.health > 0).ToArray();
                if (AvailableConsoleMon.Length > 0)
                {
                    fighterPL = AvailableConsoleMon[0];
                    Console.WriteLine($"Player sent out {fighterPL.name} \n");
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
                    fighterAI = AvailableConsoleMon[0];
                    Console.WriteLine($"AI sent out {fighterAI.name} \n");
                }
                else 
                { 
                    return; 
                }
            }
        }

    }
}
