using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    public class Member : GymUser
    {
        //JoinDate == CreatedAt of basemodel
        public string? Photo { get; set; }

        #region Relationship
        public HealthRecord HealthRecord { get; set; } = null!;

        public ICollection<MemberPlan> MemberPlans { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = null!;
        #endregion

    }
}
