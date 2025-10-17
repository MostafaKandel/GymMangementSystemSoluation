using GymMangmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        // Get all members
        IEnumerable<Member> GetAll();
        // Get member by ID
        Member? GetById(int id);
        // Add a new member
        int Add(Member member);
        // Update an existing member
        int Update(Member member);
        // Delete a member by ID
        int Delete(int id);
    }
}
