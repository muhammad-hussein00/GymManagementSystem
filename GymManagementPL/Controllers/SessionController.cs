using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
