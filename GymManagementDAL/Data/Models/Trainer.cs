using GymManagementDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    public class Trainer : GymUser
    {
        // HireDate == CreatedAt of basemodel
        public Specialty Specialty { get; set; }

        #region Relationships
        #region Trainer - Session
        public ICollection<Session> Sessions { get; set; } = null!;
        #endregion
        #endregion
    }
}
