using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    public class Plan : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        #region Relationships

        #region Plan - Membership
        public ICollection<Membership> MemberPlans { get; set; } = null!;
        #endregion

        #endregion
    }
}
