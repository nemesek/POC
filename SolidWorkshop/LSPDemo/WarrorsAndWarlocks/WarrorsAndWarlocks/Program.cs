﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarrorsAndWarlocks
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
            player.Equip(new Staff());
            var damage = player.Attack();
            Console.WriteLine("Damage is {0}", damage);
        }
    }
}
