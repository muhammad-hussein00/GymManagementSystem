using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Data.Models;
using GymManagementDAL.Models;
using GymManagementDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class HomeAnalyticsService : IHomeAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeAnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public HomeAnalyticsViewModel GetHomeAnalyticsService()
        {
            var sessions = _unitOfWork.SessionRepository.GetAll();
            return new HomeAnalyticsViewModel()
            {
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                Trainers = _unitOfWork.TrainerRepository.GetAll().Count(),
                ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(X => X.IsActive == true).Count(),
                CompletedSessions = sessions.Count(X => X.EndDate < DateTime.Now),
                OngoingSessions = sessions.Count(X => X.StartDate <= DateTime.Now && X.EndDate >= DateTime.Now),
                UpcomingSessions = sessions.Count(X => X.StartDate > DateTime.Now)
            };
        }
    }
}
