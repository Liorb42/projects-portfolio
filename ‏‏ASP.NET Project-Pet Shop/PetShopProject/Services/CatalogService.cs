using PetShopProject.Infra;
using PetShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopProject.Services
{
    public class CatalogService : ICatalogService
    {
        IRepository<Animal> _animalRepository;
        IRepository<Catagory> _catagoryRepository;
        IRepository<Comment> _commnetRepository;

        public CatalogService(IRepository<Animal> animalRepository, IRepository<Catagory> catagoryRepository, IRepository<Comment> commnetRepository)
        {
            _animalRepository = animalRepository;
            _catagoryRepository = catagoryRepository;
            _commnetRepository = commnetRepository;
        }
        public IEnumerable<Animal> GetAllAnimalsSorted()
        {
             List<Animal> animals = _animalRepository.GetAll().ToList();   
            return animals.OrderByDescending(animal => animal , new AnimalComparerByComments()).ToList();
        }
        public Animal GetAnimalById(int id)
        {
            return _animalRepository.GetById(id);            
        }
        public Animal UpdateAnimal (int id, Animal updatedAnimal)
        {
            return _animalRepository.Update(id, updatedAnimal);
        }
        public Animal AddAnimal(Animal animal)
        {
            return _animalRepository.Create(animal);
        }
        public bool DeleteAnimalById(int id)
        {
            return _animalRepository.Delete(id);
        }
        public IEnumerable<Catagory> GetAllCatagories()
        {
            return _catagoryRepository.GetAll();
        }
        public Catagory GetCatagoryById(int id)
        {
            return _catagoryRepository.GetById(id);
        }
        public bool AddCommnet(Comment comment)
        {
            Animal animal = _animalRepository.GetById(comment.AnimalId);
            if (animal != null)
            {
                comment = _commnetRepository.Create(comment);
                animal.Comments.Add(comment);
                return true;
            }
            else
                return false;
        }      

    }
}
