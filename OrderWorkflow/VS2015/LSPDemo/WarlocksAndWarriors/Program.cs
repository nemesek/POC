using System;

namespace WarlocksAndWarriors
{
    class Program
    {
        static void Main(string[] args)
        {
            var warrior = new Warrior();
            var warlock = new Warlock();
            var battleField = new BattleField();
            battleField.GoIntoBattle(warrior);
            //battleField.GoIntoBattle(warlock);

        }
    }
}
