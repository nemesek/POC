using System;

namespace WarlocksAndWarriors
{
    public class Staff : Weapon
    {
        public override string Name => "Staff";

        public override int GetDamagePoints()
        {
            return 7;
        }
    }
}
