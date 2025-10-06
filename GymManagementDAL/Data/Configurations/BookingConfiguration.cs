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
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => new { x.MemberId, x.SessionId, x.CreatedAt });
            builder.Ignore(x => x.Id);
            builder.Property(x => x.CreatedAt).HasColumnName("BookingDate").HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.IsAttended).HasDefaultValue(false);
        }
    }
}
