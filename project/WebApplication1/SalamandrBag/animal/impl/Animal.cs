using System;

namespace SalamandrBag.animal.impl
{
    public class Animal:IAnimal
    {
        private String name;
        private int weightOfFoodPerDay;
        private AnimalState animalState;

        public Animal(string name, int weightOfFoodPerDay, AnimalState animalState)
        {
            this.name = name;
            this.weightOfFoodPerDay = weightOfFoodPerDay;
            this.animalState = animalState;
        }

        public AnimalType GetAnimalType()
        {
            return animalState.GetType();
        }

        public int GetWeightOfFoodPerDay()
        {
            return weightOfFoodPerDay;
        }

        public String GetName()
        {
            return name;
        }

        public String CommandVoice()
        {
            return animalState.GetSpeech() + name;
        }
    }
}