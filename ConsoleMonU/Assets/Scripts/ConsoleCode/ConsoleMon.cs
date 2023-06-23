using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

namespace ConsoleApp1
{
    [Serializable]
    internal class ConsoleMon : MonoBehaviour
    {
        public int health;
        public int energy;
        public string name;
        public Element weakness;
        public List<Skill> skills;
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
                Debug.Log($"{name} used Rest and regained some energy");
                energy += energyRecovery;
                if (energy > 50)
                {
                    energy = 50;
                }
            }
            else
            {
                Debug.Log($"{name} already has max energy");
            }
            Debug.Log(string.Empty);
        }

    }
}
