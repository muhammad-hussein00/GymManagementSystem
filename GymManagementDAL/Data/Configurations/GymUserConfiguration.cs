using GymManagementDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(G => G.Name).HasMaxLength(50).IsUnicode(false);
            builder.Property(G => G.Email).HasMaxLength(100).IsUnicode(false);
            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("GymUserValidEmail", "Email LIKE '_%@_%._%'");
                Tb.HasCheckConstraint("GymUserValidPhone", "Phone LIKE '01%' AND Phone LIKE '%[^0-9]%'");
            });
            builder.HasIndex(G => G.Email).IsUnique();
            builder.HasIndex(G => G.Phone).IsUnique();

            builder.OwnsOne(G => G.Address, addressBuilder =>
            {
                addressBuilder.Property(A => A.Street)
                              .HasColumnName("Street")
                              .HasColumnType("varchar")
                              .HasMaxLength(30);
                addressBuilder.Property(A => A.City)
                              .HasColumnName("City")
                              .HasColumnType("varchar")
                              .HasMaxLength(30);
                addressBuilder.Property(A => A.BuildingNumber)
                              .HasColumnName("BuildingNumber");
            });

        }
    }
}
