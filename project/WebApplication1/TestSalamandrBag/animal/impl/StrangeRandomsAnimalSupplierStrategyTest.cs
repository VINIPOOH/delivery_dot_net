using System;
using Castle.Core.Internal;
using Moq;
using NUnit.Framework;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;

namespace TestSalamandrBag.animal.impl
{
    public class StrangeRandomsAnimalSupplierStrategyTest
    {
        private IAnimalFactory _stabCargoFactoryStrategy;
        private string _animalStateFieldName;

        private StrangeRandomsAnimalSupplierStrategy _animalSupplierStrategy;

        [SetUp]
        public void Setup()
        {
            _stabCargoFactoryStrategy = new StabFactory();
            _animalSupplierStrategy = new StrangeRandomsAnimalSupplierStrategy(_stabCargoFactoryStrategy);
        }

        [Test]
        public void ShouldCreateAnimalInCorrectProportion()
        {
            double expectedOkkamKoff = 0.2;
            double expectedComuflorKoff = 0.2;
            double expectedLichurkaKoff = 0.2;
            double maxErrorDelta = 0.001;
            int amountCreatedAnimals = 1000000;
            IAnimal[] animals = new IAnimal[amountCreatedAnimals];

            for (int i = 0; i < amountCreatedAnimals; i++)
            {
                animals[i] = _animalSupplierStrategy.GetAnimal();
            }
            
            double actuaOkkamKoff = (double) animals.FindAll(a => a.GetAnimalType() == AnimalType.OKKAM).Length /
                                    animals.Length;
            double actuaComuflorKoff = (double) animals.FindAll(a => a.GetAnimalType() == AnimalType.OKKAM).Length /
                                       animals.Length;
            double actuaLichurkaKoff = (double) animals.FindAll(a => a.GetAnimalType() == AnimalType.OKKAM).Length /
                                       animals.Length;
            Assert.True(Math.Abs(expectedOkkamKoff - actuaOkkamKoff) < maxErrorDelta);
            Assert.True(Math.Abs(expectedComuflorKoff - actuaComuflorKoff) < maxErrorDelta);
            Assert.True(Math.Abs(expectedLichurkaKoff - actuaLichurkaKoff) < maxErrorDelta);
        }
        
        private class StabFactory: IAnimalFactory
        {
            private Animal _animalOKKAM;
            private Animal _animalCOMUFLOR;
            private Animal _animalLICHURKA;
            private string _name = "Name";
            private int _weightOfFoodPerDay = 5;
            private AnimalState _animalStateOKKAM = new AnimalState(AnimalType.OKKAM, null);
            private AnimalState _animalStateCOMUFLOR = new AnimalState(AnimalType.COMUFLOR, null);
            private AnimalState _animalStateLICHURKA = new AnimalState(AnimalType.LICHURKA, null);

            public StabFactory()
            {
                _animalOKKAM = new Animal(_name, _weightOfFoodPerDay, _animalStateOKKAM);
                _animalCOMUFLOR = new Animal(_name, _weightOfFoodPerDay, _animalStateCOMUFLOR);
                _animalLICHURKA = new Animal(_name, _weightOfFoodPerDay, _animalStateLICHURKA);
            }

            public IAnimal CreateAnimal(string animalName, int foodWeightPerDay, AnimalType type)
            {
                
                switch (type)
                {
                    case AnimalType.OKKAM:
                        return _animalOKKAM;
                    case AnimalType.LICHURKA:
                        return _animalLICHURKA;
                    case AnimalType.COMUFLOR:
                        return _animalCOMUFLOR;
                }
                throw new ArgumentException();
            }
        }
    }
}