namespace WarlocksAndWarriors
{
    internal class Staff : Weapon
    {
        public override string Name => "Staff";

        public override int GetDamagePoints()
        {
            return 7;
        }
    }
}
