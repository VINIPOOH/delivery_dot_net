using System;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;
using SalamandrBag.exeption;
using SalamandrBag.impl;

namespace SalamandrBag
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IBagFactory bagFactory = new DefaultBagFactory();
            IBagService bagService = bagFactory.GetBagService();
            IAnimalFactory factory = new AnimalFactory();
            bagService.AddAnimal(factory.CreateAnimal("mops", 3, AnimalType.OKKAM));
            bagService.AddAnimal(factory.CreateAnimal("sharpey", 3, AnimalType.OKKAM));
            bagService.SetDay();
            Console.WriteLine("NovDay");
            Console.WriteLine(bagService.CommandVoiceToConcreteAnimal("mops"));
            Console.WriteLine(bagService.CommandVoiceToConcreteAnimal("sharpey"));
            Console.WriteLine(bagService.CommandVoiceToAllAnimals());
            bagService.SetNight();
            Console.WriteLine("NovNight");
            Console.WriteLine(bagService.CommandVoiceToConcreteAnimal("mops"));
            Console.WriteLine(bagService.CommandVoiceToConcreteAnimal("sharpey"));
            try
            {
                Console.WriteLine(bagService.CommandVoiceToAllAnimals());
            }
            catch (CallAllAnimalsAtNightException e)
            {
                Console.WriteLine(e.GetType());
            }
            Console.WriteLine("GetTotalFoodWeightPerDay");
            Console.WriteLine(bagService.GetTotalFoodWeightPerDay());
            Console.WriteLine("CountAllAnimals");
            Console.WriteLine(bagService.CountAllAnimals());
            Console.WriteLine("GetAverageFoodWeightPerAnimal");
            Console.WriteLine(bagService.GetAverageFoodWeightPerAnimal());
        }
    }
}