using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Models;
using GymManagementDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionsWithTrainerAndCategory = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (!sessionsWithTrainerAndCategory.Any())
                return [];
            return sessionsWithTrainerAndCategory.Select( s => new SessionViewModel()
            {
                Id = s.Id,
                Description = s.Description,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Capacity = s.Capacity,
                CategoryName = s.Category.CategoryName,
                AvailableSlots = s.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(s.Id),
                TrainerName = s.Trainer.Name
            });
        }

        public SessionViewModel? GetSessionDetails(int sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
