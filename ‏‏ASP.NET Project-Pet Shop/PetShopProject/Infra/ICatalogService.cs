using PetShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopProject.Infra
{
    public interface ICatalogService
    {
        public IEnumerable<Animal> GetAllAnimalsSorted();
        public Animal GetAnimalById(int id);
        public Animal UpdateAnimal(int id, Animal updatedAnimal);
        public bool DeleteAnimalById(int id);
        public IEnumerable<Catagory> GetAllCatagories();
        public Catagory GetCatagoryById(int id);
        public bool AddCommnet(Comment comment);
        public Animal AddAnimal(Animal animal);



    }
}
