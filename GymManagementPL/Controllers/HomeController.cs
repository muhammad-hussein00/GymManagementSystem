using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeAnalyticsService _homeAnalyticsService;

        public HomeController(IHomeAnalyticsService homeAnalyticsService)
        {
            _homeAnalyticsService = homeAnalyticsService;
        }
        public ActionResult Index()
        {
            var data = _homeAnalyticsService.GetHomeAnalyticsService();
            return View(data);
        }
    }
}
