using System;
using System.Text;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;

namespace SalamandrBag.place
{
    public interface IPlace
    {
        bool AddAnimal(IAnimal animal);
        String VoiceToConcreteAnimal(String animalName);
        StringBuilder VoiceToAllAnimals();
        int GetTotalFoodWeightPerDay();
        float GetAverageFoodWeightPerAnimal();
        int CountAnimals();
    }
}