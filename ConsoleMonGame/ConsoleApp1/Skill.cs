using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Skill
    {
        internal int damage, energyCost;
        internal string name;
        internal Element element;

        internal void UseOn(ConsoleMon target, ConsoleMon caster)
        {

            caster.DepleteEnergy(energyCost);
            target.TakeDamage(damage);

            if (target.weakness == element)
            {
                target.TakeDamage(damage / 2);
            }

        }
    }
}
