using System;

namespace WarlocksAndWarriors
{
    class Program
    {
        static void Main(string[] args)
        {
            var warrior = new Warrior();
            var warlock = new Warlock();
            GoIntoBattle(warrior);
            //GoIntoBattle(warlock);

        }

        static void GoIntoBattle(Player player)
        {
            //player.Equip(new Sword());
            player.Equip(new Dagger());
            //player.Equip(new Staff());
            var damage = player.Attack();
            Console.WriteLine("Damage is {0}", damage);
        }
    }
}
