using System;

namespace WarlocksAndWarriors
{
    public class Sword : Weapon
    {
        public override string Name => "Sword";
        
        public override int GetDamagePoints()
        {
            return 10;
        }
    }
}
