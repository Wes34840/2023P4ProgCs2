using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace ConsoleApp1
{
    internal class ConsoleMonFactory
    {
        internal void Load(string datafile)
        {
            string[] lines = File.ReadAllLines(datafile);



            foreach (string line in lines)
            {
                string[] typeSplit = line.Split("|");
                string[] consoleMonData = typeSplit[0].Split(",");
                
                ConsoleMon dataMon = new ConsoleMon();

                //stats
                dataMon.name = consoleMonData[0];
                dataMon.health = int.Parse(consoleMonData[2]);
                dataMon.energy = int.Parse(consoleMonData[4]);
                dataMon.weakness = Enum.Parse<Element>(consoleMonData[6]); 
                Debug.Log(dataMon.name);
                Debug.Log("Health: " + dataMon.health);
                Debug.Log("Energy: " + dataMon.energy);
                Debug.Log("Weakness: " + dataMon.weakness);

                // skills
                
                string[] skillData = typeSplit[1].Split(";");

                string[] skill1Data = skillData[1].Split(",");
                string[] skill2Data = skillData[2].Split(",");

                List<Skill> skillList = new List<Skill>
                {
                    createSkill(skill1Data),
                    createSkill(skill2Data)
                };
                dataMon.skills = skillList;

                foreach (Skill skill in skillList)
                {
                    Debug.Log(skill.name);
                    Debug.Log("Damage: " + skill.damage);
                    Debug.Log("EnergyCost: " + skill.energyCost);
                    Debug.Log("Element: " + skill.element);
                }

            }

        }

        internal Skill createSkill(string[] skillData)
        {
            Skill skill = new Skill();

            skill.name = skillData[0];
            skill.damage = int.Parse(skillData[2]);
            skill.energyCost = int.Parse(skillData[4]);
            skill.element = Enum.Parse<Element>(skillData[6]);

            return skill;
        }

        internal List<ConsoleMon> LoadJson(string json)
        {
            DataFile templates = JsonUtility.FromJson<DataFile>(json);

            return templates.data;
        }

        internal Skill CopySkill(Skill copyFrom)
        {
            Skill copyResult = new Skill();

            copyResult.name= copyFrom.name;
            copyResult.damage = copyFrom.damage;
            copyResult.energyCost = copyFrom.energyCost;
            copyResult.element = copyFrom.element;

            return copyResult;
        }   

        internal ConsoleMon CopyConsoleMon(ConsoleMon copyFrom)
        {
            ConsoleMon copyResult = new ConsoleMon();

            copyResult.name = copyFrom.name;
            copyResult.health = copyFrom.health;
            copyResult.energy = copyFrom.energy;
            copyResult.weakness = copyFrom.weakness;
            copyResult.skills = new List<Skill>();
            foreach (Skill skill in copyFrom.skills)
            {
                copyResult.skills.Add(CopySkill(skill));
            }
            return copyResult;
        }
    }
}
