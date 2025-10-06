using GymManagementDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    internal class Trainer : GymUser
    {
        public Specialty Specialty { get; set; }

        // HireDate == CreatedAt of basemodel

        #region Relationships
        #region Trainer - Session
        public ICollection<Session> Sessions { get; set; } = null!;
        #endregion
        #endregion
    }
}
