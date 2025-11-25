using GymManagementDAL.Data.Models;
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
    internal class HealthrecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members")
                   .HasKey(X => X.Id);

            builder.HasOne<Member>()
                   .WithOne(X => X.HealthRecord)
                   .HasForeignKey<HealthRecord>(X => X.Id);

        }
    }
}
