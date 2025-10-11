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
    internal class GymUserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        /*
         Model-level validation (like [EmailAddress]) checks data in the applicatio
        before saving, ensuring users enter valid input. if the user try to enter the data
        outside the application it will not be validated, it will be inserted as is.
HasCheckConstraint() enforces validation in the database,
        preventing invalid data from being inserted or updated even outside the application.
         */
        public void Configure(EntityTypeBuilder<T> builder)
        {
             builder.Property(X=> X.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(X => X.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(X => X.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            /*
              HasCheckConstraint() adds a SQL-level constraint directly to the database table.
This means that the validation happens in the database, not just in your C# model.
            and this constrains will be add to the table
             */

            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("GymUserValidEmailCheck", "Email Like '_%@_%._%'");
                Tb.HasCheckConstraint("GymUserValidPhoneCheck", "Phone Like '01' and Phone Not Like '%[^0-9]%' ");
            });

            // unique Non clustered index
            builder.HasIndex(X => X.Email).IsUnique();
            builder.HasIndex(X => X.Phone).IsUnique();

            // it used to set the configuration of the owned type 'Address class'
            builder.OwnsOne(X => X.Address, address =>
            {
                address.Property(a => a.BuildingNumber)
                .HasColumnName("BuildingNumber");
                

                address.Property(a => a.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                address.Property(a => a.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(50);
            });

        }
    }
}
