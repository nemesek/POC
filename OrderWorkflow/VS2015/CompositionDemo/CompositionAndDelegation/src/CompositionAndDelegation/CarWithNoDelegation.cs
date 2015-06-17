using System;
using System.Collections.Generic;
using System.Linq;

namespace CompositionAndDelegation
{
    public class CarWithNoDelegation
    {
        private int _currentVolumeLevel = 0;
        private readonly IEnumerable<int> _volumeRange = Enumerable.Range(0, 30);
        private bool _isRadioOn = false;

        public void Drive(string destination)
        {
            Console.WriteLine($"Arrived at destination {destination}");
        }

        public void ToggleRadio()
        {
            _isRadioOn = !_isRadioOn;
        }
        public void ChangeStation(string station)
        {
            if (!_isRadioOn) return;
            Console.WriteLine($"Tuned to station {station}");
        }

        public void ChangeRadioVolume(int offset)
        {
            if (!_isRadioOn) return;

            var desiredVolume = _currentVolumeLevel + offset;
            if (_volumeRange.Contains(desiredVolume))
            {
                _currentVolumeLevel = desiredVolume;
                return;
            }

            _currentVolumeLevel = _volumeRange.Max();
        }
    }
}
