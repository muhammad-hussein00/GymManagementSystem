using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        #region Get all plans
        // Get all plans
        // BaseURL/Plan/Index()
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }

        #endregion

        #region Get plan details

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cann't Be Negative Or Zero";
                return RedirectToAction(nameof(Index));
            }
            var planDetails = _planService.GetPlanById(id);
            if (planDetails == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
            }
            return View(planDetails);
        }

        #endregion

        #region Edit plan
        public IActionResult Edit(int id)
        {
            // Validate id
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id: value cannot be zero or negative.";
                return RedirectToAction(nameof(Index));
            }

            // Retrieve plan for editing
            var planToEdit = _planService.GetPlanToUpdate(id);

            if (planToEdit == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(planToEdit);
        }
        [HttpPost]
        public IActionResult Edit(UpdatePlanViewModel updatePlanView, int id)
        {
            // Validate id
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id: value cannot be zero or negative.";
                return RedirectToAction(nameof(Index));
            }

            // Validate the view
            if (!ModelState.IsValid)
                return View(updatePlanView);


            // Update plan
            var isUpdated = _planService.UpdatePlan(id, updatePlanView);
            if (isUpdated)
                TempData["SuccessMessage"] = "Plan updated successfully.";
            else
                TempData["ErrorMessage"] = "Invalid data recieved.";

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Toggle status

        [HttpPost]
        public ActionResult ToggleStatus(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }
            var statusToggled = _planService.TogglePlan(id);
            if (statusToggled)
            {
                TempData["SuccessMessage"] = "Plan status toggled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to toggle plan status.";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
