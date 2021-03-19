using System;
using NUnit.Framework;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;

namespace TestSalamandrBag.animal.impl
{
    public class AnimalTest
    {
        private Animal _animal;
        private string _name = "Name";
        private int _weightOfFoodPerDay = 5;
        private string _speech = "speech";

        [SetUp]
        public void Setup()
        {
            AnimalState animalState = new AnimalState(AnimalType.OKKAM, _speech);
            _animal = new Animal(_name, _weightOfFoodPerDay, animalState);
        }

        [Test]
        public void ShouldSaySpeechName()
        {
            String expectedPhrase = _speech + _name;

            String actualPhrase = _animal.CommandVoice();
            
            Assert.AreEqual(expectedPhrase, actualPhrase);
        }
    }
}