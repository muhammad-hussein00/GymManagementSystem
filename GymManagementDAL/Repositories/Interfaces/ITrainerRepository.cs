using GymManagementDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ITrainerRepository : IGenericRepository<Trainer>
    {
        Trainer? GetTrainerDetailsWithAddress(int trainerId);
        Trainer? GetTrainerWithSessions(int trainerId);
    }
}
