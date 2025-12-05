using GymManagementDAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    public class Membership : BaseModel
    {
        public DateTime EndDate { get; set; }
        public bool IsActive { get
            {
                if(EndDate >= DateTime.Now) return true;
                else return false;
            } 
        }
        #region Relationships

        #region Membership - Member
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        #endregion

        #region Membership - Plan
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        #endregion

        #endregion
    }
}
