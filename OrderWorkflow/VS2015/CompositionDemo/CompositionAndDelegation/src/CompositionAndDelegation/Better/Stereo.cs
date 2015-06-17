using System;
using System.Collections.Generic;
using System.Linq;

namespace CompositionAndDelegation.Better
{
    public class Stereo : Radio
    {
        private int _currentVolumeLevel = 0;
        private readonly IEnumerable<int> _volumeRange = Enumerable.Range(0, 101);
       

        public override void ChangeRadioVolume(int offset)
        {
            if (!IsRadioOn) return;

            var desiredVolume = _currentVolumeLevel + offset;
            if (_volumeRange.Contains(desiredVolume))
            {
                _currentVolumeLevel = desiredVolume;
                if(_currentVolumeLevel == 100) Console.WriteLine("If it's too loud you're too old.");
                return;
            }

            _currentVolumeLevel = _volumeRange.Max();

        }
    }
}
