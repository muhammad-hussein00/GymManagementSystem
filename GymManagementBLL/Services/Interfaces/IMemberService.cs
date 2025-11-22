using GymManagementBLL.ViewModels.HealthRecordViewModels;
using GymManagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMemberViewModel);
        MemberDetailsViewModel? GetMemberDetails(int id);
        HealthRecordViewModel? GetMemberHealthDetails(int memberId);
        MemberToUpdateViewModel? GetMemberToUpdate(int memberId);
        bool UpdateMember(MemberToUpdateViewModel memberToUpdateView, int memberId);
        bool TryDeleteMember(int memberId);
    }
}
