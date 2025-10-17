using GymMangmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Entities
{
   public class Trainer: GymUser
    {
        // HireDate in this class is the same as the CreatedAt in the BaseEntity

        public Specialties Specialties { get; set; }

        #region Relationships
        #region Trainer- Session (1-M)
        public ICollection<Session> TrainerSessions { get; set; } = null!;
        #endregion
        #endregion

    }
}
