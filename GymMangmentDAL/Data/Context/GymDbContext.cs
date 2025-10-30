using GymMangmentDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
 

namespace GymMangmentDAL.Data.Context
{
  public class GymDbContext : IdentityDbContext<ApplicationUser>
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymMangment;Trusted_Connection=True; TrustServerCertificate= true");
        //}

        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ApplicationUser>(Eb =>
            {
                Eb.Property(x => x.FirstName).HasColumnType("varchar").HasMaxLength(50);
                Eb.Property(x => x.LastName).HasColumnType("varchar").HasMaxLength(50);
            });
        }

        #region Db sets

        //public DbSet<ApplicationUser> Users { get; set; }

        //public DbSet<IdentityRole> Roles { get; set; }

        //public DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }

        #endregion

    }
}
