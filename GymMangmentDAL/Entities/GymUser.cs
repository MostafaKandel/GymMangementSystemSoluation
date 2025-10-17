using GymMangmentDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Entities
{
    public class GymUser: BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; }= null!;
        
        public string Phone { get; set; }= null!;

        public DateOnly DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;

    }


    // owned: it is used when you have a class that is used only in one entity and you want to store it in the same table as the entity. like address is
    // apart of the Gymuser entity
    [Owned]
    // here for clean code it is better to create a new class for address in another file, but the address class
    // is used only in the GymUser class so I put it here
   public  class Address
    {
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}
