using System;
using SalamandrBag.animal.impl;

namespace SalamandrBag.animal
{
    public interface IAnimalFactory
    {
        IAnimal CreateAnimal(String animalName, int foodWeightPerDay, AnimalType type);
    }
}