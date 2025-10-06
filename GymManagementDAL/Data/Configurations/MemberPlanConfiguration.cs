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
    internal class MemberPlanConfiguration : IEntityTypeConfiguration<MemberPlan>
    {
        public void Configure(EntityTypeBuilder<MemberPlan> builder)
        {
            builder.Property(x => x.CreatedAt).HasColumnName("StartDate");
            builder.HasKey( x => new {x.MemberId, x.PlanId, x.CreatedAt});
            builder.Ignore( x => x.Id);
        }
    }
}
