using System;

namespace WarlocksAndWarriors
{
    internal class BattleField
    {
        // using the <fill in the blank> pattern here so I can be loosely coupled
        public void GoIntoBattle(Player player)
        {
            //player.Equip(new Sword());
            //player.Equip(new Dagger());
            player.Equip(new Staff());
            var damage = player.Attack();
            player.Weapon
            Console.WriteLine($"Player inflicted {damage} damage points");
        }
    }
}
