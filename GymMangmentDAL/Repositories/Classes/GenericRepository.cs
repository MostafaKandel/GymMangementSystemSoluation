using GymMangmentDAL.Data.Context;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext _dbContext;
        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();

        }

        public int Delete( TEntity entity)
        {
          
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();

        }

        public IEnumerable<TEntity> GetAll(Func<TEntity,bool>? condition=null)
        {
            if (condition == null)
            {
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            }
            else
            {
                return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
            }

        }

        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);

        }

        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();

        }
    }
}
