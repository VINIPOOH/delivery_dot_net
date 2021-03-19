using System;
using System.Runtime.CompilerServices;

namespace SalamandrBag.animal.impl
{
    public class StrangeRandomsAnimalSupplierStrategy:IAnimalSupplierStrategy
    {
        private const int DEFAULTS_AMOUNT_FOOD_PER_DAY = 5;
        private const String DEFAULTS_ANIMAL_NAME = "animal";
        private IAnimalFactory AnimalFactory;
        private int AnimalCounter;

        public StrangeRandomsAnimalSupplierStrategy(IAnimalFactory animalFactory)
        {
            AnimalFactory = animalFactory;
        }

        public IAnimal 
            GetAnimal()
        {
            Random rnd = new Random();
            int value = rnd.Next(0,5);
            String generatedAnimalName = DEFAULTS_ANIMAL_NAME + AnimalCounter++;
            switch (value)
            {
                case 0:
                case 1:
                    return AnimalFactory.CreateAnimal(generatedAnimalName, DEFAULTS_AMOUNT_FOOD_PER_DAY,
                        AnimalType.LICHURKA);
                case 2:
                case 3:
                    return AnimalFactory.CreateAnimal(generatedAnimalName, DEFAULTS_AMOUNT_FOOD_PER_DAY,
                        AnimalType.COMUFLOR);
                case 4:
                    return AnimalFactory.CreateAnimal(generatedAnimalName, DEFAULTS_AMOUNT_FOOD_PER_DAY,
                        AnimalType.OKKAM);
            }
            throw new RuntimeWrappedException("this will newer happened");
        }
    }
}