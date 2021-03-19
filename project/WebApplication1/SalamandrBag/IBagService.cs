using System;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;

namespace SalamandrBag
{
    public interface IBagService
    {
        int CountAllAnimals();
        bool AddAnimal(IAnimal animal);
        String CommandVoiceToConcreteAnimal(String animalName);
        String CommandVoiceToAllAnimals();
        int GetTotalFoodWeightPerDay();
        float GetAverageFoodWeightPerAnimal();
        bool AnimalTryJumpIntoBag();

        void SetDay();

        void SetNight();
    }
}