using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Entities
{
    internal class Category: BaseEntity
    {
        public string CategoryName { get; set; } = null!;
    }
}
