using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace ConsoleApp1
{
    internal class Arena : MonoBehaviour
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
                    GameCycle();
                }
                ReplaceDeadConsoleMon();
            }
            Roster[] rosters = new Roster[] { RosterPL, RosterAI };
            
            Roster Winner = Array.Find(rosters, r => Array.Exists(r.lineUp, c => c.health > 0)); // I did a thing

            Debug.Log($"{Winner.name} has won the battle");
        }

        internal void GameCycle()
        {
            ShowUI();
            PlayerTurn();

            if (CheckIfFainted(fighterAI))
            {
                Debug.Log($"{fighterPL.name} fucking died\n");
                return;
            }

            UseSkill(fighterAI, fighterPL);
            if (CheckIfFainted(fighterAI))
            {
                Debug.Log($"{fighterPL.name} fucking died\n");
                return;
            }
        }

        internal void ShowUI()
        {
            Debug.Log("Choose your move\n");

            foreach (Skill skill in fighterPL.skills)
            {
                Debug.Log($"Name: {skill.name} \nDamage: {skill.damage} \t Energy Cost: {skill.energyCost} \t Element: {skill.element}\n");
            }
            Debug.Log($"Rest: \nSkip your turn and regain energy \n\nType switch if you want to switch ConsoleMon\n");
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
            Debug.Log("\nWhich consolemon do you wish to send out? \n");
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
                Debug.Log($"{fighterPL.name} is already on the field, try again");
                return null;
            }

            ConsoleMon[] chosenConsoleMon = RosterPL.lineUp.Where(c => c.name.ToLower() == input.ToLower()).ToArray();

            if (chosenConsoleMon.Length == 0)
            {
                Debug.Log($"{input} is not a valid ConsoleMon, try again \n");
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
                Debug.Log($"\n{consolemon.name} \t {noteCurrent}");
                
                if (noteFainted == "") 
                {
                    Debug.Log($"Health: {consolemon.health} \nEnergy: {consolemon.energy} ");
                }
                else
                {
                    Debug.Log(noteFainted);
                }
            }
        }

        internal void SwitchConsoleMon(ConsoleMon switchTo)
        {
            Console.Clear();
            Debug.Log($"{RosterPL.name} returned {fighterPL.name}. \n");
            fighterPL = switchTo;
            Debug.Log($"{RosterPL.name} sent out {fighterPL.name}. \n");
        }

        internal void PlayerAttack(string input)
        {
            if (fighterPL.skills.Exists(i => i.name.ToLower() == input))
            {
                List<Skill> useMove = fighterPL.skills.Where(i => i.name.ToLower() == input).ToList();

                if (useMove[0].energyCost > fighterPL.energy)
                {
                    Debug.Log($"{fighterPL.name} has too little energy for this move, try another or rest");
                    PlayerTurn();
                    return;
                }

                Console.Clear();

                Debug.Log($"{fighterPL.name} used {useMove[0].name} \n");
                useMove[0].UseOn(fighterAI, fighterPL);
            }
            else if (input.ToLower() == "rest")
            {
                Console.Clear();
                fighterPL.Rest();
            }
            else
            {
                Debug.Log("Invalid input, try again");
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
            Debug.Log($"{fighter.name} used {usedMove.name} \n");

            usedMove.UseOn(target, fighter);
        }

        internal void DisplayConsoleMonStats(ConsoleMon fighterA, ConsoleMon fighterB)
        {
            Debug.Log($"{fighterA.name} \nHealth: {fighterA.health} \nEnergy: {fighterA.energy} \n");
            Debug.Log($"{fighterB.name} \nHealth: {fighterB.health} \nEnergy: {fighterB.energy} \n");
        }

        internal bool CheckIfFainted(ConsoleMon fighter)
        {
            return fighter.health <= 0;
        }

        internal void ReplaceDeadConsoleMon()
        {
            if (fighterPL.health < 0)
            {
                if (RosterPL.lineUp.Where(c => c.health > 0).ToArray().Length != 0)
                {
                    Debug.Log($"{fighterPL.name} has fainted");
                    InitiatePlayerSwitch();
                }
                return;
                
            }
            else if (fighterAI.health < 0)
            {
                ConsoleMon[] AvailableConsoleMon = RosterAI.lineUp.Where(c => c.health > 0).ToArray();
                if (AvailableConsoleMon.Length > 0)
                {
                    fighterAI = AvailableConsoleMon[0];
                    Debug.Log($"AI sent out {fighterAI.name} \n");
                }
                else 
                { 
                    return; 
                }
            }
        }

    }
}
