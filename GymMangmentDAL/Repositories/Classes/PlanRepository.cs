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
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;
        public PlanRepository( GymDbContext dbContext) {
            _dbContext = dbContext;
        }
        public IEnumerable<Plan> GetAll()
        {
            return _dbContext.Plans.ToList();

        }

        public Plan? GetById(int id)
        {
            return _dbContext.Plans.Find(id);
            
        }

        public int Update(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            return _dbContext.SaveChanges();
             
        }
    }
}
