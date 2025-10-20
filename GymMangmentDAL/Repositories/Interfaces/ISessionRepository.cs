using GymMangmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Repositories.Interfaces
{
    public interface ISessionRepository: IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategories();

        Session? GetSessionByIdWithTrainerAndCategories(int id);

        int GetCountOfBookedSlots(int sessionId);
    }
}
