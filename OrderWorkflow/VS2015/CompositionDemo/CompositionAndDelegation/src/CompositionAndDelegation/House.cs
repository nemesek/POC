using System;

namespace CompositionAndDelegation
{
    public class House
    {
        public void WatchTv()
        {
            Console.WriteLine("Watching the ball game.");
        }

        public void Clean()
        {
            Console.WriteLine("Someone is cleaning me up.");
        }

        public virtual void PlayMusic(string genre)
        {
            var radio = new CarWithNoDelegation();
            radio.ToggleRadio();
            radio.ChangeStation(genre);
            radio.ChangeRadioVolume(100);
        }
    }
}
