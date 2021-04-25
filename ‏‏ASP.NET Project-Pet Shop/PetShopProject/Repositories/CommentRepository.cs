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
    public class CommentRepository : IRepository<Comment>
    {
        private CatalogContext _context;

        public CommentRepository(CatalogContext context)
        {
            _context = context;
        }
        public Comment Create(Comment entityToCreate)
        {
            _context.Comments.Add(entityToCreate);
            _context.SaveChanges();
            return entityToCreate;
        }
        public bool Delete(int id)
        {
            Comment commentToDelete = _context.Comments.SingleOrDefault(comment => comment.Id == id);

            if (commentToDelete != null)
            {
                _context.Comments.Remove(commentToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments.ToList();
        }
        public Comment GetById(int id)
        {
            return _context.Comments.SingleOrDefault(comment => comment.Id == id);
        }       
        public Comment Update(int id, Comment entityToUpdate)
        {
            Comment commentInDb = _context.Comments.SingleOrDefault(comment => comment.Id == id);

            if (commentInDb != null)
            {
                commentInDb.AnimalId = entityToUpdate.AnimalId;
                commentInDb.CommentText = entityToUpdate.CommentText;
            }
            _context.SaveChanges();
            return commentInDb;
        }
    }
}

