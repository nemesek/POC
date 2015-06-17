using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompositionAndDelegation.Better
{
    public class House : CompositionAndDelegation.House
    {
        private readonly Radio _radio;

        public House(Radio radio)
        {
            _radio = radio;
        }

        public override void PlayMusic(string genre)
        {
            _radio.ToggleRadio();
            _radio.ChangeStation(genre);
            _radio.ChangeRadioVolume(100);
        }
    }
}
