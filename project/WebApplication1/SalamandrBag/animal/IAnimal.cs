using System;

namespace SalamandrBag.animal
{
    public interface IAnimal
    {
        AnimalType GetAnimalType();
        int GetWeightOfFoodPerDay();
        String GetName();
        String CommandVoice();
    }
}