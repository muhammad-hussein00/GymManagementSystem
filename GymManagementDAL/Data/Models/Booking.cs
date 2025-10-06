using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    internal class Booking : BaseModel
    {
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        //bookingday == CreatedAt of basModel
        public bool IsAttended { get; set; }
        
    }
}
