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
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        private readonly GymContext _context;

        public TrainerRepository(GymContext context) : base(context)
        {
            this._context = context;
        }
        public Trainer? GetTrainerDetailsWithAddress(int trainerId)
        {
            var trainer = _context.Trainers.Include(x => x.Address).FirstOrDefault(x => x.Id == trainerId);
            if (trainer == null)
                return null;

            return trainer;
        }

        public Trainer? GetTrainerWithSessions(int trainerId)
        {
            var trainer = _context.Trainers.Include(x => x.Sessions).FirstOrDefault(x => x.Id == trainerId);
            if (trainer == null)
                return null;

            return trainer;
        }
    }
}
