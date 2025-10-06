using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    internal class Category : BaseModel
    {
        #region Properties
        public string CategoryName { get; set; } = null!;
        #endregion

        #region Relationships

        #region Category - Session
        public ICollection<Session> Sessions { get; set; } =null!;
        #endregion

        #endregion
    }
}
