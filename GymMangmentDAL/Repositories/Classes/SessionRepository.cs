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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategories()
        {
           return _dbContext.Sessions.Include(s => s.SessionTrainer)
                                     .Include(s => s.SessionCategory)
                                     .ToList();
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(sb => sb.SessionId == sessionId);
        }

        public Session? GetSessionByIdWithTrainerAndCategories(int id)
        {
            return _dbContext.Sessions.Include(s => s.SessionTrainer)
                                      .Include(s => s.SessionCategory)
                                      .FirstOrDefault(s => s.Id == id)


                                      ;

        }
    }
}
