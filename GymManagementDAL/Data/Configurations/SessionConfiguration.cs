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
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_On_Capacity", "Capacity between 1 and 25");
                tb.HasCheckConstraint("CK_On_Date", "EndDate > StartDate ");
            });

            #region Session - Category
            builder.HasOne(x => x.Category)
                       .WithMany(x => x.Sessions)
                       .HasForeignKey(x => x.CategoryId);
            #endregion

            #region Session - Trainer
            builder.HasOne(x => x.Trainer)
                   .WithMany(x => x.Sessions)
                   .HasForeignKey(x => x.TrainerId);
            #endregion
        }
    }
}
