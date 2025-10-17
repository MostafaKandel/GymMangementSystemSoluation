using GymMangmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Repositories.Interfaces
{
    internal interface ISessionRepository
    {
       IEnumerable<Session> GetAll();
        Session? GetById(int id);
        int Add(Session session);
        int Update(Session session);
        int Delete(Session session);
    }
}
