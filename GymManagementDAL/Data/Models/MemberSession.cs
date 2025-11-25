using GymManagementDAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    public class MemberSession : BaseModel
    {
        public int MemberId { get; set; }
        public int SessionId { get; set; }
        //bookingday == CreatedAt of basModel
        public bool IsAttended { get; set; }
        public Member Member { get; set; } = null!;
        public Session Session { get; set; } = null!;
        
    }
}
