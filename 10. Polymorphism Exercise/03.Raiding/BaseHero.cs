using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public abstract class BaseHero
    {
        public abstract string Name { get; protected set; }
        public abstract int Power { get; }
        public abstract string CastAbility();
    }
}
