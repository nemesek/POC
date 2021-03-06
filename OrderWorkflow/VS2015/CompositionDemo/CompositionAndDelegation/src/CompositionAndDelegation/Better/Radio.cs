﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CompositionAndDelegation.Better
{
    public class Radio
    {
        private int _currentVolumeLevel = 0;
        private readonly IEnumerable<int> _volumeRange = Enumerable.Range(0, 30);
        protected bool IsRadioOn;

        public virtual void ToggleRadio()
        {
            IsRadioOn = !IsRadioOn;
        }
        public virtual void ChangeStation(string station)
        {
            if (!IsRadioOn) return;
            Console.WriteLine($"Tuned to station {station}");
        }

        public virtual void ChangeRadioVolume(int offset)
        {
            if (!IsRadioOn) return;

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
