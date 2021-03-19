using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using SalamandrBag.animal;
using SalamandrBag.place;
using SalamandrBag.place.impl;

namespace TestSalamandrBag.Util
{
    public class RoomTest
    {
        private Room _placeOfLiving;
//        private List<IAnimal> _stubListOfAAnimals;
        private List<AnimalType> _stubListOfAnimalPermittedTypes;
        private List<IPlace> _stubListOfPlacesLiving;
        private Mock<IPlace> _mockPlace;
        private Mock<IAnimal> _mockAnimalOkkam;
//        private Mock<IAnimal> _mockAnimalKomuflor;
        private Mock<IAnimal> _mockAnimalLichurka;

        [SetUp]
        public void Setup()
        {
            _mockAnimalOkkam = new Mock<IAnimal>();
            _mockAnimalOkkam.Setup(a => a.GetAnimalType()).Returns(AnimalType.OKKAM);
//            _mockAnimalKomuflor = new Mock<IAnimal>();
//            _mockAnimalKomuflor.Setup(a => a.GetAnimalType()).Returns(AnimalType.COMUFLOR);
            _mockAnimalLichurka = new Mock<IAnimal>();
            _mockAnimalLichurka.Setup(a => a.GetAnimalType()).Returns(AnimalType.LICHURKA);
            
            _mockPlace = new Mock<IPlace>();
            _stubListOfPlacesLiving = new List<IPlace>{_mockPlace.Object};
            
            _stubListOfAnimalPermittedTypes = new List<AnimalType>{AnimalType.OKKAM, AnimalType.COMUFLOR};
            
            _placeOfLiving = new Room(_stubListOfPlacesLiving,_stubListOfAnimalPermittedTypes);
        }

        [Test]
        public void ShouldSuccessfullyAddAnimal()
        {
            bool expectedResult = true;
            _mockPlace.Setup(a => a.AddAnimal(It.IsAny<IAnimal>())).Returns(expectedResult);

            bool actualResult = _placeOfLiving.AddAnimal(_mockAnimalOkkam.Object);
            
            Assert.AreEqual(expectedResult, actualResult);
            _mockPlace.Verify(a => a.AddAnimal(It.IsAny<IAnimal>()), Times.Once());
        }
        
        [Test]
        public void ShouldUnsuccessfullyAddAnimal()
        {
            bool expectedResult = false;
            _mockPlace.Setup(a => a.AddAnimal(It.IsAny<IAnimal>())).Returns(expectedResult);

            bool actualResult = _placeOfLiving.AddAnimal(_mockAnimalLichurka.Object);
            
            Assert.AreEqual(expectedResult, actualResult);
            _mockPlace.Verify(a => a.AddAnimal(It.IsAny<IAnimal>()), Times.Never);
        }
        
        [Test]
        public void ShouldCommandVoiceToConcreteExistAnimal()
        {
            String animalName = "name";
            String expectedResult = "say"+animalName;
            _mockPlace.Setup(a => a.VoiceToConcreteAnimal(animalName)).Returns(expectedResult);

            String actualResult = _placeOfLiving.VoiceToConcreteAnimal(animalName);

            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        public void ShouldCommandVoiceToConcreteNotExistAnimal()
        {
            String animalName = "name";
            String notExistingAnimalName = "notExistingAnimalName";
            String expectedResult = "say"+animalName;
            _mockPlace.Setup(a => a.VoiceToConcreteAnimal(animalName)).Returns(expectedResult);


            String actualResult = _placeOfLiving.VoiceToConcreteAnimal(notExistingAnimalName);

            Assert.Null(actualResult);
        }
        
                [Test]
        public void ShouldCommandVoiceToAllAnimal()
        {
            String expectedResult = "say";
            StringBuilder builder = new StringBuilder();
            builder.Append(expectedResult);
            _mockPlace.Setup(a => a.VoiceToAllAnimals()).Returns(builder);

            String actualResult = _placeOfLiving.VoiceToAllAnimals().ToString();

            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        public void ShouldCommandVoiceToAllAnimalWhenThereIsNoAnimalsInPlace()
        {
            StringBuilder builder = new StringBuilder();
            _mockPlace.Setup(a => a.VoiceToAllAnimals()).Returns(builder);

            String actualResult = _placeOfLiving.VoiceToAllAnimals().ToString();

            Assert.AreEqual(String.Empty, actualResult);
        }
        
                [Test]
        public void ShouldGetTotalFoodWeightPerDay()
        {
            int expectedAmountFood = 100;
            _mockPlace.Setup(a => a.GetTotalFoodWeightPerDay()).Returns(expectedAmountFood);

            int actualAmountFood = _placeOfLiving.GetTotalFoodWeightPerDay();

            Assert.AreEqual(expectedAmountFood, actualAmountFood);
        }
        
                [Test]
        public void ShouldGetAverageFoodWeightPerAnimal()
        {
            float expectedAmountFood = 100.0F;
            _mockPlace.Setup(a => a.GetAverageFoodWeightPerAnimal()).Returns(expectedAmountFood);

            float actualAmountFood = _placeOfLiving.GetAverageFoodWeightPerAnimal();

            Assert.AreEqual(expectedAmountFood, actualAmountFood);
        }

        [Test]
        public void ShouldCountAllAnimals()
        {
            int expectedAmountAnimals = 100;
            _mockPlace.Setup(a => a.CountAnimals()).Returns(expectedAmountAnimals);

            int actualAmountAnimals = _placeOfLiving.CountAnimals();

            Assert.AreEqual(expectedAmountAnimals, actualAmountAnimals);
        }
    }
}