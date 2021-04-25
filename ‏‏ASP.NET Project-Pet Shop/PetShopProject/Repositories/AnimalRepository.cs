using Microsoft.EntityFrameworkCore;
using PetShopProject.Data;
using PetShopProject.Infra;
using PetShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopProject.Repositories
{
    public class AnimalRepository : IRepository<Animal>
    {
        private CatalogContext _context;

        public AnimalRepository(CatalogContext catalogContext)
        {
            _context = catalogContext;
        }

        public Animal Create(Animal entityToCreate)
        {
            _context.Animals.Add(entityToCreate);
            _context.SaveChanges();
            return entityToCreate;
        }
        public bool Delete(int id)
        {
            Animal animalToDelete = _context.Animals.SingleOrDefault(animal => animal.Id == id);

            if (animalToDelete != null)
            {                
                _context.Animals.Remove(animalToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<Animal> GetAll()
        {
            return _context.Animals.Include(animal => animal.Comments);
        }
        public Animal GetById(int id)
        {
            Animal animal = _context.Animals.SingleOrDefault(animal => animal.Id == id);
            if(animal != null)
            {
                _context.Entry(animal).Collection(animal => animal.Comments).Load();
                _context.Entry(animal).Reference(animal => animal.Catagory).Load();
            }    
            return animal;
        }
        public Animal Update(int id, Animal entityToUpdate)
        {
            Animal animalInDb = _context.Animals.SingleOrDefault(animal => animal.Id == id);
            
            if(animalInDb != null)
            {
                animalInDb.Name = entityToUpdate.Name;
                animalInDb.PictureUrl = entityToUpdate.PictureUrl;
                animalInDb.Description = entityToUpdate.Description;
                animalInDb.Age = entityToUpdate.Age;
                animalInDb.CatagoryId = entityToUpdate.CatagoryId;
            }
            _context.SaveChanges();
            return animalInDb;
        }
    }
}
