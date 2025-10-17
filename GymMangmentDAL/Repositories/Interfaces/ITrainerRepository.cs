using GymMangmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
         
        IEnumerable<Trainer> GetAllTrainers();
        Trainer? GetById(int id);
        int Add(Trainer trainer);
        int Update(Trainer trainer);
        int Delete(Trainer trainer);
    }
}
