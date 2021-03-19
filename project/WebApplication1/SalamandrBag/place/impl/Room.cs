using System;
using System.Collections.Generic;
using System.Text;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;

namespace SalamandrBag.place.impl
{
    public class Room:IPlace
    {
        private List<IPlace> Places;
        private List<AnimalType> AnimalTypesWhichCanPathHere;

        public Room(List<IPlace> places, List<AnimalType> animalTypesWhichCanPathHere)
        {
            Places = places;
            AnimalTypesWhichCanPathHere = animalTypesWhichCanPathHere;
        }

        public bool AddAnimal(IAnimal animal)
        {
            if (AnimalTypesWhichCanPathHere.Contains(animal.GetAnimalType()))
            {
                foreach (var place in Places)
                {
                    if (place.AddAnimal(animal))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public String VoiceToConcreteAnimal(string animalName)
        {
            String animalSays = null;
            foreach (var place in Places)
            {
                animalSays = place.VoiceToConcreteAnimal(animalName);
                if (animalSays!=null)
                {
                    break;
                }
            }
            return animalSays;
        }

        public StringBuilder VoiceToAllAnimals()
        {
            StringBuilder animalSays = new StringBuilder();
            foreach (var place in Places)
            {
                animalSays.Append(place.VoiceToAllAnimals());
            }
            return animalSays;
        }

        public int GetTotalFoodWeightPerDay()
        {
            int totalFoodWeight = 0;
            foreach (var place in Places)
            {
                totalFoodWeight += place.GetTotalFoodWeightPerDay();
            }
            return totalFoodWeight;
        }

        public float GetAverageFoodWeightPerAnimal()
        {
            float totalFoodWeight = 0.0f;
            foreach (var place in Places)
            {
                totalFoodWeight += place.GetAverageFoodWeightPerAnimal();
            }
            return totalFoodWeight/Places.Count;
        }
        public int CountAnimals()
        {
            int totalAnimalAmount = 0;
            foreach (var place in Places)
            {
                totalAnimalAmount += place.CountAnimals();
            }
            return totalAnimalAmount;
        }

        public void AddPlace(IPlace place)
        {
            Places.Add(place);
        }
    }
}