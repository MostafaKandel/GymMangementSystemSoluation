
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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext dbContext;

        public TrainerRepository(GymDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public int Add(Trainer trainer)
        {
            dbContext.Trainers.Add(trainer);
            return dbContext.SaveChanges();

        }

        public int Delete(Trainer trainer)
        {
            dbContext.Trainers.Remove(trainer);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAllTrainers()
        {
            return dbContext.Trainers.ToList();

        }

        public Trainer? GetById(int id)
        {
            return dbContext.Trainers.Find(id);

        }

        public int Update(Trainer trainer)
        {
            dbContext.Trainers.Update(trainer);
            return dbContext.SaveChanges();

        }
    }
}
