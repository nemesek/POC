namespace WarlocksAndWarriors
{
    public class Warrior : Player
    {
        public Warrior()
        {
            base.Level = 10;
        }
        public override void Equip(Weapon weapon)
        {
            // we don't allow warriors to carry staffs
            var staffWeapon = weapon as Staff;
            if (staffWeapon == null) base.Weapon = weapon;
        }

        public override int Attack()
        {
            return base.Weapon.GetDamagePoints() * base.Level;
        }
    }
}
