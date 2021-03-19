using SalamandrBag.animal;
using SalamandrBag.animal.impl;
using SalamandrBag.exeption;
using SalamandrBag.place;

namespace SalamandrBag.impl
{
    public class BagService: IBagService
    {
        private IPlace Place;
        private bool _isDay = true;
        private IAnimalSupplierStrategy _animalSupplierStrategy;

        public BagService(IPlace place, IAnimalSupplierStrategy animalSupplierStrategy)
        {
            Place = place;
            _animalSupplierStrategy = animalSupplierStrategy;
        }

        public int CountAllAnimals()
        {
            return Place.CountAnimals();
        }

        public bool AddAnimal(IAnimal animal)
        {
            return Place.AddAnimal(animal);
        }

        public string CommandVoiceToConcreteAnimal(string animalName)
        {
            return Place.VoiceToConcreteAnimal(animalName);
        }

        public string CommandVoiceToAllAnimals()
        {
            if (!_isDay)
            {
                throw new CallAllAnimalsAtNightException();
            }
            return Place.VoiceToAllAnimals().ToString();
        }

        public int GetTotalFoodWeightPerDay()
        {
            return Place.GetTotalFoodWeightPerDay();
        }

        public float GetAverageFoodWeightPerAnimal()
        {
            return Place.GetAverageFoodWeightPerAnimal();
        }

        public bool AnimalTryJumpIntoBag()
        {
            return Place.AddAnimal(_animalSupplierStrategy.GetAnimal());;
        }

        public void SetDay()
        {
            _isDay = true;
        }

        public void SetNight()
        {
            _isDay = false;
        }
    }
}