using System;

namespace WarlocksAndWarriors
{
    class Program
    {
        static void Main(string[] args)
        {
            var warrior = new Warrior();
            GoIntoBattle(warrior);
        }

        static void GoIntoBattle(Player player)
        {
            player.Equip(new Sword());
            var damage = player.Attack();
            Console.WriteLine("Damage is {0}", damage);
        }
    }
}
