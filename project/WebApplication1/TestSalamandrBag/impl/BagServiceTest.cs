using System;
using System.Text;
using Moq;
using NUnit.Framework;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;
using SalamandrBag.exeption;
using SalamandrBag.impl;
using SalamandrBag.place;
using TestSalamandrBag.Util;

namespace TestSalamandrBag.impl
{
    public class BagServiceTest
    {
        private BagService _bagService;
        private Mock<IPlace> _placeMock;
        private Mock<IAnimalSupplierStrategy> _mockAnimalSupplierStrategy;
        private Mock<IAnimal> animalOkkam = new Mock<IAnimal>();
        private String isDayBagServiceFieldName = "_isDay";

        [SetUp]
        public void Setup()
        {
            _placeMock = new Mock<IPlace>();
            _mockAnimalSupplierStrategy = new Mock<IAnimalSupplierStrategy>();
            _bagService = new BagService(_placeMock.Object, _mockAnimalSupplierStrategy.Object);
        }

        [Test]
        public void ShouldCountAllAnimals()
        {
            int expectedAmountAnimals = 200;
            _placeMock.Setup(a => a.CountAnimals()).Returns(expectedAmountAnimals);

            int actualAmountAnimals = _bagService.CountAllAnimals();

            Assert.AreEqual(expectedAmountAnimals, actualAmountAnimals);
        }

        [Test]
        public void ShouldSuccessfullyAddAnimal()
        {
            bool expectedResult = true;
            _placeMock.Setup(a => a.AddAnimal(It.IsAny<IAnimal>())).Returns(expectedResult);

            bool actualResult = _bagService.AddAnimal(animalOkkam.Object);

            Assert.AreEqual(expectedResult, actualResult);
            _placeMock.Verify(place => place.AddAnimal(It.IsAny<IAnimal>()), Times.Once());
        }

        [Test]
        public void ShouldUnsuccessfullyAddAnimal()
        {
            bool expectedResult = false;
            _placeMock.Setup(a => a.AddAnimal(It.IsAny<IAnimal>())).Returns(expectedResult);

            bool actualResult = _bagService.AddAnimal(animalOkkam.Object);

            Assert.AreEqual(expectedResult, actualResult);
            _placeMock.Verify(place => place.AddAnimal(It.IsAny<IAnimal>()), Times.Once());
        }

        [Test]
        public void ShouldCommandVoiceToConcreteAnimal()
        {
            String expectedResult = "say";
            String animalName = "name";
            _placeMock.Setup(a => a.VoiceToConcreteAnimal(It.IsAny<String>())).Returns(expectedResult);

            String actualResult = _bagService.CommandVoiceToConcreteAnimal(animalName);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ShouldCommandVoiceToAllAnimalWhenDay()
        {
            String expectedResult = "say";
            StringBuilder builder = new StringBuilder();
            builder.Append(expectedResult);
            _placeMock.Setup(a => a.VoiceToAllAnimals()).Returns(builder);
            _bagService.SetDay();

            String actualResult = _bagService.CommandVoiceToAllAnimals();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ShouldThrowCallAllAnimalsAtNightExceptionWhenCommandVoiceToAllAnimalWhenNight()
        {
            String expectedResult = "say";
            StringBuilder builder = new StringBuilder();
            builder.Append(expectedResult);
            _placeMock.Setup(a => a.VoiceToAllAnimals()).Returns(builder);
            _bagService.SetNight();

            var actualResult =
                Assert.Throws<CallAllAnimalsAtNightException>(() => _bagService.CommandVoiceToAllAnimals());

            Assert.AreEqual(typeof(CallAllAnimalsAtNightException), actualResult.GetType());
        }

        [Test]
        public void ShouldGetTotalFoodWeightPerDay()
        {
            int expectedAmountFood = 200;
            _placeMock.Setup(a => a.GetTotalFoodWeightPerDay()).Returns(expectedAmountFood);

            int actualAmountFood = _bagService.GetTotalFoodWeightPerDay();

            Assert.AreEqual(expectedAmountFood, actualAmountFood);
        }

        [Test]
        public void ShouldGetAverageFoodWeightPerAnimal()
        {
            float expectedAmountAnimals = 200.0F;
            _placeMock.Setup(a => a.GetAverageFoodWeightPerAnimal()).Returns(expectedAmountAnimals);

            float actualAmountAnimals = _bagService.GetAverageFoodWeightPerAnimal();

            Assert.AreEqual(expectedAmountAnimals, actualAmountAnimals);
        }

        [Test]
        public void ShouldSucceedInAnimalTryJumpIntoBag()
        {
            bool expectedResult = true;
            _placeMock.Setup(a => a.AddAnimal(It.IsAny<IAnimal>())).Returns(expectedResult);
            _mockAnimalSupplierStrategy.Setup(a => a.GetAnimal()).Returns(animalOkkam.Object);

            bool actualResult = _bagService.AnimalTryJumpIntoBag();

            Assert.AreEqual(expectedResult, actualResult);
            _placeMock.Verify(place => place.AddAnimal(It.IsAny<IAnimal>()), Times.Once());
            _mockAnimalSupplierStrategy.Verify(a => a.GetAnimal(), Times.Once());
        }

        [Test]
        public void ShouldUnsucceedInAnimalTryJumpIntoBag()
        {
            bool expectedResult = false;
            _placeMock.Setup(a => a.AddAnimal(It.IsAny<IAnimal>())).Returns(expectedResult);
            _mockAnimalSupplierStrategy.Setup(a => a.GetAnimal()).Returns(animalOkkam.Object);

            bool actualResult = _bagService.AnimalTryJumpIntoBag();

            Assert.AreEqual(expectedResult, actualResult);
            _placeMock.Verify(place => place.AddAnimal(It.IsAny<IAnimal>()), Times.Once());
            _mockAnimalSupplierStrategy.Verify(a => a.GetAnimal(), Times.Once());
        }

        [Test]
        public void ShouldSetDayVariableAsTrue()
        {
            bool expectedResult = true;
            ReflectionUtil.SetFieldValue(_bagService, isDayBagServiceFieldName, !expectedResult);
           
            _bagService.SetDay();
            bool actualResult = (bool) ReflectionUtil.GetFieldValue(_bagService, isDayBagServiceFieldName);

            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        public void ShouldSetDayVariableAsFalse()
        {
            bool expectedResult = false;
            ReflectionUtil.SetFieldValue(_bagService, isDayBagServiceFieldName, !expectedResult);
           
            _bagService.SetNight();
            bool actualResult = (bool) ReflectionUtil.GetFieldValue(_bagService, isDayBagServiceFieldName);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}