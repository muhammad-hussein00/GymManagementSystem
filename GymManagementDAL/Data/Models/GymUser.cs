using GymManagementDAL.Models.Enums;
using GymManagementDAL.Models.Owned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models
{
    internal abstract class GymUser : BaseModel
    {
        public string Name { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;
    }
}
