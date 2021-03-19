using System;
using System.Collections.Generic;
using System.Text;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;

namespace SalamandrBag.place.impl
{
    public class ConcretePlaceOfLiving:IPlace
    {
        private List<IAnimal> _animals = new List<IAnimal>();
        private List<AnimalType> _animalTypesWhichCanLiveHere;

        public ConcretePlaceOfLiving(List<AnimalType> animalTypesWhichCanLiveHere)
        {
            _animalTypesWhichCanLiveHere = animalTypesWhichCanLiveHere;
        }

        public bool AddAnimal(IAnimal animal)
        {
            if (_animalTypesWhichCanLiveHere.Contains(animal.GetAnimalType()))
            {
                _animals.Add(animal);
                return true;
            }
            return false;
        }

        public string VoiceToConcreteAnimal(string animalName)
        {
            String animalSays = null;
            foreach (var animal in _animals)
            {
                if (animal.GetName()==animalName)
                {
                    animalSays = animal.CommandVoice();
                    break;
                }
            }
            return animalSays;
        }

        public StringBuilder VoiceToAllAnimals()
        {
            StringBuilder animalSays = new StringBuilder();
            foreach (var animal in _animals)
            {
                animalSays.Append(animal.CommandVoice());
            }
            return animalSays;
        }

        public int GetTotalFoodWeightPerDay()
        {
            int totalFoodWeight = 0;
            foreach (var animal in _animals)
            {
                totalFoodWeight += animal.GetWeightOfFoodPerDay();
            }
            return totalFoodWeight;
        }

        public float GetAverageFoodWeightPerAnimal()
        {
            int totalFoodWeight = 0;
            foreach (var animal in _animals)
            {
                totalFoodWeight += animal.GetWeightOfFoodPerDay();
            }
            return totalFoodWeight/_animals.Count;
        }

        public int CountAnimals()
        {
            return _animals.Count;
        }
    }
}