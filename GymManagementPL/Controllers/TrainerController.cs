using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        #region Get all trainers
        public ActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        } 
        #endregion

        #region Get trainer details
        public ActionResult Details(int id)
        {
            // Validate id
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero";
                return RedirectToAction(nameof(Index));
            }

            // Get trainer
            var trainer = _trainerService.GetTrainerDetails(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }
        #endregion

        #region Edit trainer
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerToUpdate(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel trainerToUpdateView)
        {
            // Validate id
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id must be greater than zero.";
                return RedirectToAction(nameof(Index));
            }

            // Check trainerToUpdateView
            if (!ModelState.IsValid)
            {
                return View(trainerToUpdateView);
            }

            // Update trainer
            var isUpdated = _trainerService.UpdateTrainer(id, trainerToUpdateView);
            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Trainer updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer failed to update.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Create trainer
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createTrainerView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("wrongData", "Please correct the errors and try again.");
                return View(nameof(Create), createTrainerView);
            }
            var isCreated = _trainerService.CreateTrainer(createTrainerView);
            if (isCreated)
                TempData["SuccessMessage"] = "Trainer created successfully.";
            else
                TempData["ErrorMessage"] = "Trainer failed to create.";
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
