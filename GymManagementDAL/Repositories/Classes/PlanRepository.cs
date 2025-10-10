using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Models;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymContext _context; 
        public PlanRepository(GymContext context)
        {
            _context = context;
        }


        public IEnumerable<Plan> GetAll()
        {
            return _context.Plans.ToList();
        }

        public Plan? GetById(int id)
        {
            var plan = _context.Plans.Find(id);
            return plan;
        }

        public int Update(Plan plan)
        {
            _context.Plans.Update(plan);
            return _context.SaveChanges();
        }
    }
}
