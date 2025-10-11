using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Entities
{
    internal class Member: GymUser
    {
        // JoinDate in this class is the same as the CreatedAt in the BaseEntity

        public string? Photo  { get; set; }
    }
}
