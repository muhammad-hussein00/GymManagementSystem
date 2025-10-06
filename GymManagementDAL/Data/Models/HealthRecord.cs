using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    internal class HealthRecord : BaseModel
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }
    }
}
