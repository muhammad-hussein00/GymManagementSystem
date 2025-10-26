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
        public string? Photo { get; set; }

        #region Member - Health record
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion
    }
}
