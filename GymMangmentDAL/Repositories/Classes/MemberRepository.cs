using GymMangmentDAL.Data.Context;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Repositories.Classes
{
    internal class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _dbContext;

        public MemberRepository(GymDbContext dbContext) {
            _dbContext = dbContext;
        }
        public int Add(Member member)
        {
            _dbContext.Members.Add(member);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var Member = _dbContext.Members.Find(id);
            if (Member == null) return -1;
             _dbContext.Members.Remove(Member);
            return _dbContext.SaveChanges();

        }

        public IEnumerable<Member> GetAll()=> _dbContext.Members.ToList();
        

        public Member? GetById(int id)
        {
             return _dbContext.Members.Find(id);
        }

        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
