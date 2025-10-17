using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Entities
{
    public class Category: BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        #region Relationships
        #region Category- Session (1-M)
        public ICollection<Session> Sessions { get; set; } = null!;
        #endregion
        #endregion

    }
}
