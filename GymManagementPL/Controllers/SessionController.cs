using GymManagementBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }
        public ActionResult Details(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }

            var sessionDetails = _sessionService.GetSessionDetails(id);

            if(sessionDetails is null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                RedirectToAction(nameof(Index));
            }

            return View(sessionDetails);
        }
        public ActionResult Create()
        {
            LoadDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createSessionView)
        {
            if(!ModelState.IsValid)
            {
                LoadDropDown();
                return View(createSessionView);
            }

            var isCreated = _sessionService.CreateSession(createSessionView);

            if (isCreated)
            {
                TempData["SuccessMessage"] = "Session created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Session failed to create.";
                LoadDropDown();
                return View(createSessionView);
            }
        }


        #region Helper methods
        private void LoadDropDown()
        {
            // Get categories and trainers to use it in dropdownlist.
            var trainers = _sessionService.GetTrainersForDropdown();
            var categories = _sessionService.GetAllCategoriesForDropdown();

            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
        #endregion
    }
}
