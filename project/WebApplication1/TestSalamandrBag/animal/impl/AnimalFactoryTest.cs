using System;
using NUnit.Framework;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;
using TestSalamandrBag.Util;

namespace TestSalamandrBag.animal.impl
{
    public class AnimalFactoryTest
    {
        private AnimalFactory _animalFactory;
        private string _name = "Name";
        private int _weightOfFoodPerDay = 5;
        private AnimalType _animalType = AnimalType.OKKAM;
        private string _animalStateFieldName;

        [SetUp]
        public void Setup()
        {
            _animalFactory = new AnimalFactory();
        }

        [Test]
        public void ShouldCreateAnimalWhichIsOccamType()
        {
            AnimalState animalState = new AnimalState(_animalType, null);
            IAnimal expectedAnimal = new Animal(_name, _weightOfFoodPerDay, animalState);

            IAnimal actualAnimal = _animalFactory.CreateAnimal(_name,_weightOfFoodPerDay, _animalType);
            
            Assert.AreEqual(expectedAnimal.GetAnimalType(), actualAnimal.GetAnimalType());
            Assert.AreEqual(expectedAnimal.GetName(), actualAnimal.GetName());
            Assert.AreEqual(expectedAnimal.GetWeightOfFoodPerDay(), actualAnimal.GetWeightOfFoodPerDay());
        }
        
        [Test]
        public void ShouldCreateOnlyOneAnimalStateObjectForTwoAnimalWithSameType()
        {
            IAnimal firsAnimal = _animalFactory.CreateAnimal(_name,_weightOfFoodPerDay, _animalType);
            IAnimal secondAnimal = _animalFactory.CreateAnimal(_name,_weightOfFoodPerDay, _animalType);
            _animalStateFieldName = "animalState";
            AnimalState firstAnimalState =
                (AnimalState) ReflectionUtil.GetFieldValue(firsAnimal, _animalStateFieldName);
            AnimalState secondAnimalState =
                (AnimalState) ReflectionUtil.GetFieldValue(firsAnimal, _animalStateFieldName);
            
            Assert.True(firstAnimalState==secondAnimalState);
        }
    }
}