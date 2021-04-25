using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShopProject.Infra;


namespace PetShopProject.Infra
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity Create(TEntity entityToCreate);
        bool Delete(int id);
        TEntity Update(int id, TEntity entityToUpdate);
    }
}
