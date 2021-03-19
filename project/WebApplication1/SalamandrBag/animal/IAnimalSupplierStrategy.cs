using SalamandrBag.animal.impl;

namespace SalamandrBag.animal
{
    public interface IAnimalSupplierStrategy
    {
        IAnimal GetAnimal();
    }
}