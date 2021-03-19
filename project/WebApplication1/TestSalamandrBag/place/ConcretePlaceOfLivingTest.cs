using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;
using SalamandrBag.exeption;
using SalamandrBag.place.impl;
using TestSalamandrBag.Util;

namespace TestSalamandrBag.place
{
    public class ConcretePlaceOfLivingTest
    {
        private ConcretePlaceOfLiving _placeOfLiving;
        private List<IAnimal> _stubListOfAAnimals;
        private List<AnimalType> _stubListOfAnimalPermittedTypes;
        private Mock<IAnimal> _mockAnimalOkkam;
        private Mock<IAnimal> _mockAnimalKomuflor;
        private Mock<IAnimal> _mockAnimalLichurka;
        private String animalListConcretePlaceOfLivingFieldName = "_animals";

        [SetUp]
        public void Setup()
        {
            _mockAnimalOkkam = new Mock<IAnimal>();
            _mockAnimalOkkam.Setup(a => a.GetAnimalType()).Returns(AnimalType.OKKAM);
            _mockAnimalKomuflor = new Mock<IAnimal>();
            _mockAnimalKomuflor.Setup(a => a.GetAnimalType()).Returns(AnimalType.COMUFLOR);
            _mockAnimalLichurka = new Mock<IAnimal>();
            _mockAnimalLichurka.Setup(a => a.GetAnimalType()).Returns(AnimalType.LICHURKA);
            
            _stubListOfAnimalPermittedTypes = new List<AnimalType>{AnimalType.OKKAM, AnimalType.COMUFLOR};
            _stubListOfAAnimals = new List<IAnimal>
                {_mockAnimalOkkam.Object, _mockAnimalKomuflor.Object};
            
            _placeOfLiving = new ConcretePlaceOfLiving(_stubListOfAnimalPermittedTypes);
            ReflectionUtil.SetFieldValue(_placeOfLiving, animalListConcretePlaceOfLivingFieldName, _stubListOfAAnimals);
        }

        [Test]
        public void ShouldSuccessfullyAddAnimal()
        {
            bool actualResult = _placeOfLiving.AddAnimal(_mockAnimalOkkam.Object);
            IAnimal addedAnimal = _stubListOfAAnimals[_stubListOfAAnimals.Count-1];

            Assert.True(actualResult);
            Assert.AreEqual(_mockAnimalOkkam.Object, addedAnimal);
        }

        [Test]
        public void ShouldUnsuccessfullyAddAnimal()
        {
            bool actualResult = _placeOfLiving.AddAnimal(_mockAnimalLichurka.Object);
            IAnimal addedAnimal = _stubListOfAAnimals[_stubListOfAAnimals.Count-1];

            Assert.False(actualResult);
            Assert.AreNotEqual(_mockAnimalOkkam.Object, addedAnimal);
        }
        
        [Test]
        public void ShouldCommandVoiceToConcreteExistAnimal()
        {
            String animalName = "name";
            String expectedResult = "say"+animalName;
            _mockAnimalOkkam.Setup(a => a.GetName()).Returns(animalName);
            _mockAnimalOkkam.Setup(a => a.CommandVoice()).Returns(expectedResult);

            String actualResult = _placeOfLiving.VoiceToConcreteAnimal(animalName);

            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        public void ShouldCommandVoiceToConcreteNotExistAnimal()
        {
            String animalName = "name";
            String notExistingAnimalName = "notExistingAnimalName";
            String expectedResult = "say"+animalName;
            _mockAnimalOkkam.Setup(a => a.GetName()).Returns(animalName);
            _mockAnimalOkkam.Setup(a => a.CommandVoice()).Returns(expectedResult);

            String actualResult = _placeOfLiving.VoiceToConcreteAnimal(notExistingAnimalName);

            Assert.Null(actualResult);
        }
        
                [Test]
        public void ShouldCommandVoiceToAllAnimal()
        {
            String okamName = "Okam";
            String comuflorName =  " Comuflor";
            String expectedResult = okamName+comuflorName;
            _mockAnimalOkkam.Setup(a => a.GetName()).Returns(okamName);
            _mockAnimalOkkam.Setup(a => a.CommandVoice()).Returns(okamName);
            _mockAnimalKomuflor.Setup(a => a.GetName()).Returns(comuflorName);
            _mockAnimalKomuflor.Setup(a => a.CommandVoice()).Returns(comuflorName);
            
            String actualResult = _placeOfLiving.VoiceToAllAnimals().ToString();

            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        public void ShouldCommandVoiceToAllAnimalWhenThereIsNoAnimalsInPlace()
        {
            _stubListOfAAnimals.Clear();
            
            String actualResult = _placeOfLiving.VoiceToAllAnimals().ToString();

            Assert.AreEqual(String.Empty, actualResult);
        }
        
                [Test]
        public void ShouldGetTotalFoodWeightPerDay()
        {
            int amountOfFoodForOkkam = 100;
            int amountOfFoodForKomuflor = 150;
            int expectedAmountFood = amountOfFoodForOkkam+amountOfFoodForKomuflor;
            _mockAnimalOkkam.Setup(a => a.GetWeightOfFoodPerDay()).Returns(amountOfFoodForOkkam);
            _mockAnimalKomuflor.Setup(a => a.GetWeightOfFoodPerDay()).Returns(amountOfFoodForKomuflor);

            int actualAmountFood = _placeOfLiving.GetTotalFoodWeightPerDay();

            Assert.AreEqual(expectedAmountFood, actualAmountFood);
        }
        
                [Test]
        public void ShouldGetAverageFoodWeightPerAnimal()
        {
            int amountOfFoodForOkkam = 100;
            int amountOfFoodForKomuflor = 150;
            float expectedAmountFood = (float)(amountOfFoodForOkkam+amountOfFoodForKomuflor)/_stubListOfAAnimals.Count;
            _mockAnimalOkkam.Setup(a => a.GetWeightOfFoodPerDay()).Returns(amountOfFoodForOkkam);
            _mockAnimalKomuflor.Setup(a => a.GetWeightOfFoodPerDay()).Returns(amountOfFoodForKomuflor);

            float actualAmountFood = _placeOfLiving.GetAverageFoodWeightPerAnimal();

            Assert.AreEqual(expectedAmountFood, actualAmountFood);
        }

        [Test]
        public void ShouldCountAllAnimals()
        {
            int expectedAmountAnimals = _stubListOfAAnimals.Count;

            int actualAmountAnimals = _placeOfLiving.CountAnimals();

            Assert.AreEqual(expectedAmountAnimals, actualAmountAnimals);
        }
    }
}