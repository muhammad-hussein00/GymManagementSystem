using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Models;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository :  GenericRepository<Session>,ISessionRepository
    {
        private readonly GymContext _context;

        public SessionRepository(GymContext context) : base(context) 
        {
            this._context = context;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _context.Sessions.Include(x => x.Trainer)
                                    .Include(x => x.Category)
                                    .ToList();
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _context.MemberSessions.Count(x => x.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _context.Sessions.Include(x => x.Trainer)
                                    .Include(x => x.Category)
                                    .FirstOrDefault();
        }
    }
}
 