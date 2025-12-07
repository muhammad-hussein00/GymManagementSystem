using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModel;
using GymManagementDAL.Models;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if(plans is null || !plans.Any())
                return [];

            return plans.Select(p => new PlanViewModel()
            {
                PlanId = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive,
            });
        }
        public PlanViewModel? GetPlanById(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if(plan is null) return null;

            return new PlanViewModel()
            {
                PlanId = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive,
            };
        }
        public UpdatePlanViewModel? GetPlanToUpdate(int planId)////////////////////////////////////////////////////////////
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if(plan is null || HasActiveMemberPlan(planId)) return null;

            return new UpdatePlanViewModel()
            {
                PlanName = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays
            };
        }
        public bool UpdatePlan(int planId, UpdatePlanViewModel updatedPlan)
        {
            var planRepo = _unitOfWork.GetRepository<Plan>();
            var plan = planRepo.GetById(planId);
            if(plan is null || HasActiveMemberPlan(planId)) return false;

            (plan.Description, plan.DurationDays, plan.Price)
                = (updatedPlan.Description,  updatedPlan.DurationDays, updatedPlan.Price);
            try
            {
                planRepo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool TogglePlan(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if(plan is null || HasActiveMemberPlan(planId))
                return false;
            plan.IsActive = plan.IsActive == true ? false : true;
            plan.CreatedAt = DateTime.Now;
            try
            {
                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper methods
        private bool HasActiveMemberPlan(int planId)
        {
            return _unitOfWork.GetRepository<Membership>().GetAll(x => x.Id == planId && x.IsActive == true).Any();
        }
        #endregion
    }
}
