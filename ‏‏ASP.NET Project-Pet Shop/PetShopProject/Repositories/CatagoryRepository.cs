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
    public class CatagoryRepository : IRepository<Catagory>
    {
        private CatalogContext _context;

        public CatagoryRepository(CatalogContext context)
        {
            _context = context;
        }
        public Catagory Create(Catagory entityToCreate)
        {
            _context.Catagories.Add(entityToCreate);
            _context.SaveChanges();
            return entityToCreate;
        }
        public bool Delete(int id)
        {
            Catagory catagoryToDelete = _context.Catagories.SingleOrDefault(catagory => catagory.Id == id);

            if (catagoryToDelete != null)
            {                
                _context.Catagories.Remove(catagoryToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<Catagory> GetAll()
        {
            return _context.Catagories.ToList();
        }
        public Catagory GetById(int id)
        {
            return _context.Catagories.SingleOrDefault(catagory => catagory.Id == id);
        }
        public Catagory Update(int id, Catagory entityToUpdate)
        {
            Catagory catagoryInDb = _context.Catagories.SingleOrDefault(catagory => catagory.Id == id);

            if (catagoryInDb != null)
            {
                catagoryInDb.Name = entityToUpdate.Name;                
            }
            _context.SaveChanges();
            return catagoryInDb;
        }
    }
}
