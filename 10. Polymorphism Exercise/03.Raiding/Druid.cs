using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public class Druid : BaseHero
    {
        public Druid(string name)
        {
            Name = name;
            Power = 80;
        }

        public override string Name { get; protected set; }

        public override int Power { get; }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {Name} healed for {Power}";
        }
    }
}
