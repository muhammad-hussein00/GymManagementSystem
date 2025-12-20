using GymManagementDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Models
{
    public class Member : GymUser
    {
        // JoinDate == CreatedAt
        public string Photo { get; set; } = null!;

        #region Relationships

        #region Member - Health record
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion

        #region Member - Membership
        public ICollection<Membership> Memberships { get; set; } = null!;
        #endregion

        #region Member - MemberSession
        public ICollection<MemberSession> MemberSessions { get; set; } = null!;
        #endregion

        #endregion
    }
}
