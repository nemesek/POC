using System;
using System.Collections.Generic;
using System.Linq;

namespace CompositionAndDelegation.Better
{
    public class Radio
    {
        private int _currentVolumeLevel = 0;
        private readonly IEnumerable<int> _volumeRange = Enumerable.Range(0, 30);
        private bool _isRadioOn = false;

        public virtual void ToggleRadio()
        {
            _isRadioOn = !_isRadioOn;
        }
        public virtual void ChangeStation(string station)
        {
            if (!_isRadioOn) return;
            Console.WriteLine($"Tuned to station {station}");
        }

        public virtual void ChangeRadioVolume(int offset)
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
