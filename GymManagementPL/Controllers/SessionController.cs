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

        #region Get all sessions
        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        } 
        #endregion

        #region Get session deteals
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
        #endregion

        #region Create session
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
        #endregion

        #region Edit session
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }
            var sessionToUpdate = _sessionService.GetSessionToUpdate(id);

            if (sessionToUpdate is null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }

            LoadDropDownForTrainers();
            return View(sessionToUpdate);
        }

        [HttpPost]
        public ActionResult Edit( int Id , UpdateSessionViewModel updatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownForTrainers();
                return View(updatedSession);
            }
            var isUpdated = _sessionService.UpdateSession(Id ,updatedSession);
            if(isUpdated)
            {
                TempData["SuccessMessage"] = "Session updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Session failed to update.";
                LoadDropDownForTrainers();
                return View(updatedSession);
            }
        }
        #endregion

        #region Delete session

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }

            // for check session is exist
            var session = _sessionService.GetSessionDetails(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.sessionId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var isDeleted = _sessionService.DeleteSession(id);

            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Session deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the session."
;
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper methods
        private void LoadDropDown()
        {
            // Get categories and trainers to use it in dropdownlist.
            var trainers = _sessionService.GetTrainersForDropdown();
            var categories = _sessionService.GetAllCategoriesForDropdown();

            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
        private void LoadDropDownForTrainers()
        {
            var trainers = _sessionService.GetTrainersForDropdown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
        #endregion
    }
}
