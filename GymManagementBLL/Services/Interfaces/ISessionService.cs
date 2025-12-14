using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionDetails(int sessionId);
        bool CreateSession(CreateSessionViewModel createSessionViewModel);
        bool UpdateSession(UpdateSessionViewModel updateSessionViewModel);
        bool DeleteSession(int sessionId);  
        IEnumerable<TrainerToSelectViewModel> GetTrainersForDropdown();
        IEnumerable<CategoryToSelectViewModel> GetAllCategoriesForDropdown();
    }
}
