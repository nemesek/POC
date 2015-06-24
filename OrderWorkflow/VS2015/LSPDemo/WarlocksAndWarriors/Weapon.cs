namespace WarlocksAndWarriors
{
    internal abstract class Weapon
    {
        public abstract string Name { get; }
        public abstract int GetDamagePoints();
    }
}
