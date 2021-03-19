using System.Collections.Generic;
using SalamandrBag.animal;
using SalamandrBag.animal.impl;
using SalamandrBag.place;
using SalamandrBag.place.impl;

namespace SalamandrBag.impl
{
    public class DefaultBagFactory:IBagFactory
    {
        public IBagService GetBagService()
        {
            IAnimalSupplierStrategy supplierStrategy = new StrangeRandomsAnimalSupplierStrategy(new AnimalFactory());
            List<AnimalType> animalTypesWhichCanPathHere = new List<AnimalType>();
            animalTypesWhichCanPathHere.Add(AnimalType.OKKAM);
            IPlace concertRoom = new ConcretePlaceOfLiving(animalTypesWhichCanPathHere); 
            Room root = new Room(new List<IPlace>(), animalTypesWhichCanPathHere);
            root.AddPlace(concertRoom);
            IBagService bagService = new BagService(root, supplierStrategy);
            return bagService;
        }
    }
}