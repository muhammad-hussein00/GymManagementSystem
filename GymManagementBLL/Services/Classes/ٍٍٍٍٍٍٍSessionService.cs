using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Models;
using GymManagementDAL.UnitOfWork;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
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
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public bool CreateSession(CreateSessionViewModel createSessionViewModel)
        {
            try
            {
                // Check if valid to create session
                if (createSessionViewModel == null ||
                        !IsTrainerExists(createSessionViewModel.TrainerId) ||
                        !IsCategoryExists(createSessionViewModel.CategoryId) ||
                        !IsValidDateTime(createSessionViewModel.StartDate, createSessionViewModel.EndDate))
                    return false;

                // Mapping and adding it
                var mappedSession = _mapper.Map<Session>(createSessionViewModel);
                _unitOfWork.GetRepository<Session>().Add(mappedSession);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create session failed : {ex}");
                return false;
            }
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (!sessions.Any())
                return [];

            var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in mappedSessions)
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            
            return mappedSessions;
        }
        public SessionViewModel? GetSessionDetails(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId);
            if (session is null)
                return null;

            // Mapping session to sessionViewModel 
            var mappedSession = _mapper.Map<Session, SessionViewModel>(session);
            mappedSession.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);

            return mappedSession;
        }
        public bool UpdateSession(UpdateSessionViewModel updateSessionViewModel)
        {
            if(updateSessionViewModel == null ||
                !IsTrainerExists(updateSessionViewModel.TrainerId) ||
                !IsValidDateTime(updateSessionViewModel.StartDate, updateSessionViewModel.EndDate))
            {
                return false;
            }

            var UpdatedSession = _mapper.Map<Session>(updateSessionViewModel);
            _unitOfWork.SessionRepository.Update(UpdatedSession);
            return _unitOfWork.SaveChanges() > 0;
        }
        public bool DeleteSession(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetById(sessionId);

            // Check if session exists.
            if (session == null)
                return false;

            // Check if has bookings
            bool hasBookings = _unitOfWork.SessionRepository.GetCountOfBookedSlots(sessionId) > 0;
            if (hasBookings)
                return false;

            // Check if is ongoing or upcoming.
            bool isOnGoing = session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now;
            bool isUpComing = session.StartDate > DateTime.Now;
            if (isOnGoing || isUpComing)
                return false;

            // Safe to delete.
            _unitOfWork.SessionRepository.Delete(session);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region Helper methods
        private bool IsTrainerExists(int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerId) != null;
        }
        private bool IsCategoryExists(int categoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(categoryId) != null;
        }
        private bool IsValidDateTime(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }
        #endregion
    }
}
