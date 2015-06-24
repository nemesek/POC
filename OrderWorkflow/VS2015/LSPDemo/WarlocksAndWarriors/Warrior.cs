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
            // we don't allow warriors to carry staffs since they can't perform magic
            // it would be utterly useless in their hands
            // So we are going to switch things up if need be
            var staffWeapon = weapon as Staff;
            if (staffWeapon == null) base.Weapon = weapon;
            //base.Weapon = weapon;
        }

        public override int Attack()
        {
            return base.Weapon.GetDamagePoints() * base.Level;
        }
    }
}
