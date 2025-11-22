using GymManagementDAL.Models.Owned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    internal class MemberDetailsViewModel : MemberViewModel
    {
        public string DateOfBirth { get; set; } = null!;
        public string? MembershipStartDate { get; set; } = null!;    
        public string? MembershipEndDate { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? PlanName { get; set; } = null!;
    }
}
