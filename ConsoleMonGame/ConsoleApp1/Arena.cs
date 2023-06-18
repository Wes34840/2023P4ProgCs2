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
        Roster RosterPL, RosterAI;
        
        internal void Fight(Roster RosterPL, Roster RosterAI)
        {
            this.RosterPL = RosterPL;
            this.RosterAI = RosterAI;
            fighterPL = RosterPL.lineUp[0];
            fighterAI = RosterAI.lineUp[0];
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
            Console.WriteLine($"Rest: \nSkip your turn and regain energy \n\nType switch if you want to switch ConsoleMon\n");
        }

        internal void PlayerTurn()
        {
            
            string input = Console.ReadLine().ToLower();

            if (input == "switch")
            {
                InitiatePlayerSwitch();
            }
            else
            {
                PlayerAttack(input);
            }

            
        }
        internal void InitiatePlayerSwitch()
        {
            Console.Clear();
            DisplayPlayerConsoleMon();
            Console.WriteLine("\nWhich consolemon do you wish to send out? \n");
            ConsoleMon chosen = PlayerSwitchInput();
            while (chosen == null)
            {
                chosen = PlayerSwitchInput();
            }
            SwitchConsoleMon(chosen);
        }
        
        internal ConsoleMon PlayerSwitchInput()
        {
            string input = Console.ReadLine();
            if (input.ToLower() == fighterPL.name.ToLower())
            {
                Console.WriteLine($"{fighterPL.name} is already on the field, try again");
                return null;
            }

            ConsoleMon[] chosenConsoleMon = RosterPL.lineUp.Where(c => c.name.ToLower() == input.ToLower()).ToArray();

            if (chosenConsoleMon.Length == 0)
            {
                Console.WriteLine($"{input} is not a valid ConsoleMon, try again \n");
                return null;
            }

            return chosenConsoleMon[0];
            
        }
        internal void DisplayPlayerConsoleMon()
        {
            foreach (ConsoleMon consolemon in RosterPL.lineUp)
            {
                string noteCurrent = "";
                string noteFainted = "";
                if (consolemon.name ==  fighterPL.name) 
                {
                    noteCurrent = "(Currently out)";
                }
                if (consolemon.health <= 0)
                {
                    noteFainted = "Fainted";
                }
                Console.WriteLine($"\n{consolemon.name} \t {noteCurrent}");
                
                if (noteFainted == "") 
                {
                    Console.WriteLine($"Health: {consolemon.health} \nEnergy: {consolemon.energy} ");
                }
                else
                {
                    Console.WriteLine(noteFainted);
                }
            }
        }
        internal void SwitchConsoleMon(ConsoleMon switchTo)
        {
            Console.Clear();
            Console.WriteLine($"{RosterPL.name} returned {fighterPL.name}. \n");
            fighterPL = switchTo;
            Console.WriteLine($"{RosterPL.name} sent out {fighterPL.name}. \n");
        }
        internal void PlayerAttack(string input)
        {
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

                Console.WriteLine($"{fighterPL.name} used {useMove[0].name} \n");
                useMove[0].UseOn(fighterAI, fighterPL);
            }
            else if (input.ToLower() == "rest")
            {
                Console.Clear();
                fighterPL.Rest();
            }
            else
            {
                Console.WriteLine("Invalid input, try again");
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
                if (RosterA.Where(c => c.health > 0).ToArray().Length != 0)
                {
                    Console.WriteLine($"{fighterPL.name} has fainted");
                    InitiatePlayerSwitch();
                }
                return;
                
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
