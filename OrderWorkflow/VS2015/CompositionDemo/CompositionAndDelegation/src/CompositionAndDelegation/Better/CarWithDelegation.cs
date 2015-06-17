using System;

namespace CompositionAndDelegation.Better
{
    public class CarWithDelegation
    {
        private readonly Radio _radio = new Radio();

        public void Drive(string destination)
        {
            Console.WriteLine($"Arrived at destination {destination}");
        }

        public void ToggleRadio()
        {
            _radio.ToggleRadio();
        }
        public void ChangeStation(string station)
        {
            _radio.ChangeStation(station);
        }

        public void ChangeRadioVolume(int offset)
        {
            _radio.ChangeRadioVolume(offset);
        }
    }
}
