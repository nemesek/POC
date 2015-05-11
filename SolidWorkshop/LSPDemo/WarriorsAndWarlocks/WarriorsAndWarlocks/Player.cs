using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarriorsAndWarlocks
{
    public abstract class Player
    {
        public Weapon Weapon{ get; protected set; }
        public int Level { get; protected set; }
        public abstract void Equip(Weapon weapon);
        public abstract int Attack();
    }
}
