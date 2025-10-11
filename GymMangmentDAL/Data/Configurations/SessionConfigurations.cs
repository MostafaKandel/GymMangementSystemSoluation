using GymMangmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Data.Configurations
{
    internal class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
             builder.ToTable(tb =>
             {
                 tb.HasCheckConstraint("SessionValidEndTimeCheck", "EndDate > StartDate");
        tb.HasCheckConstraint("SessionCapacityCheck", "Capacity Between 1 and 25");
             });

            builder.HasOne(X => X.SessionCategory)
                .WithMany(X => X.Sessions)
                .HasForeignKey(X => X.CategoryId);

            builder.HasOne(X => X.SessionTrainer)
                .WithMany(X => X.TrainerSessions)
                .HasForeignKey(X => X.TrainerId);


        }
    }
}
