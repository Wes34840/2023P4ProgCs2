using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConsoleApp1
{
    [Serializable]
    internal class Skill
    {
        public int damage;
        public int energyCost;
        public string name;
        public Element element;

        public class MyClass
        {

        }


        internal Skill(int damage, int energyCost, string name, Element element)
        {
            this.damage = damage;
            this.energyCost = energyCost;
            this.name = name;
            this.element = element;
        }

        public Skill()
        {
        }

        internal void UseOn(ConsoleMon target, ConsoleMon caster)
        {

            caster.DepleteEnergy(energyCost);
            target.TakeDamage(damage);

            if (target.weakness == element)
            {
                target.TakeDamage(damage / 2);
                Debug.Log("It's super effective! \n");
            }

        }
    }
}
