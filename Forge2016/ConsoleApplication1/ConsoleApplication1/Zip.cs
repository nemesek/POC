﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class Zip
    {
        private readonly int _population;
        private readonly int _zipCode;

        public Zip(int zipCode)
        {
            _zipCode = zipCode;
            _population = Repository.GetZipPopulation(_zipCode);
        }
        public Zip(int zipCode, int population)
        {
            _zipCode = zipCode;
            _population = population;
        }

        public int Population => _population;
        public int ZipCode => _zipCode;

        public double CalculatePopulationDensityForTenMiles()
        {
            return (double)CalculatePopulationForListOfZips(LoadZipsWithinTenMiles)/10;
        }

        public double CalculatePopulationDensityForFiveMiles()
        {
            return (double)CalculatePopulationForListOfZips(LoadZipsWithinFiveMiles)/5;
        }

        public IEnumerable<Zip> LoadZipsWithinTenMiles()
        {
            return Repository.GetNeighborsForZipWithinNumberOfMiles(_zipCode, 10);
        }

        public IEnumerable<Zip> LoadZipsWithinFiveMiles()
        {
            return Repository.GetNeighborsForZipWithinNumberOfMiles(_zipCode, 5);
        }

        public int CalculatePopulationForListOfZips(Func<IEnumerable<Zip>> zipFunc)
        {
            var neighbors = zipFunc();
            return neighbors.Select(n => n.Population).Sum();
           
        }
    }
}
