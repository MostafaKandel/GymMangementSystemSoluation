using GymMangmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Data.Configurations
{
    internal class HealthRecordConfigurations : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            // to mention that the member data and health record data will be stored in the same table
            builder.ToTable("Members");

            builder.HasOne<Member>()
                .WithOne(X=> X.HealthRecord)
                .HasForeignKey<Member>(X=> X.Id);
        }
    }
}
