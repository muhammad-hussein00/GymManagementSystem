using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    internal class MemberPlan : BaseModel
    {
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public bool IsActive { get
            {
                if(EndDate >= DateTime.Now) return true;
                else return false;
            } 
        }

    }
}
