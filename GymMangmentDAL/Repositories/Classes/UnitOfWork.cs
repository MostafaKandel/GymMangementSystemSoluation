using GymMangmentDAL.Data.Context;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;

        public UnitOfWork(GymDbContext dbContext, ISessionRepository sessionRepository  )
        {
            _dbContext = dbContext;
            SessionRepository = sessionRepository;
        }
        // this for save all repository which i need before GetRepository called
        // key: Member, trainer,...    value: GenericRepository<Member>, GenericRepository<Trainer>, ....
        private readonly Dictionary<Type, object> _repositores= new();

        public ISessionRepository SessionRepository { get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType= typeof(TEntity);
            if (_repositores.ContainsKey(EntityType))
                return (IGenericRepository<TEntity>)_repositores[EntityType];
            var NewRepo = new GenericRepository<TEntity>(_dbContext);
            _repositores[EntityType]= NewRepo;
            return NewRepo;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

    }
}
