using System;
using System.Collections;
using System.Collections.Generic;
using SalamandrBag.animal.exeption;

namespace SalamandrBag.animal.impl
{
    public class AnimalFactory:IAnimalFactory
    {
        private const String LICHURKA_SPEECH = "I am lichurka said ";
        private const String COMUFLOR_SPEECH = "I am comuflor said ";
        private const String OKKAM_SPEECH = "I am okkam said ";
        private Dictionary<AnimalType,AnimalState>  AnimalStates = new Dictionary<AnimalType,AnimalState>();  
        
        public IAnimal CreateAnimal(String animalName, int foodWeightPerDay, AnimalType type)
        {
            if (AnimalStates.ContainsKey(type))
            {
                return new Animal(animalName,foodWeightPerDay,AnimalStates[type]);
            }
            switch (type)
            {
                case AnimalType.OKKAM:
                    AnimalStates[type] = new AnimalState(type,OKKAM_SPEECH);
                    break;
                case AnimalType.LICHURKA:
                    AnimalStates[type] = new AnimalState(type,LICHURKA_SPEECH);
                    break;
                case AnimalType.COMUFLOR:
                    AnimalStates[type] = new AnimalState(type,COMUFLOR_SPEECH);
                    break;
            }
            if (AnimalStates.ContainsKey(type))
            {
                return new Animal(animalName,foodWeightPerDay,AnimalStates[type]);
            }
            throw new NoSuchAnimalTypeException();
        }
    }
}