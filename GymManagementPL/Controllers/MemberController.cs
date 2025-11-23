using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        public ActionResult MemberDetails(int id)
        {
            // If the Id is not valid
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            // If member not found in database
            var members = _memberService.GetMemberDetails(id);
            if (members == null)
                return RedirectToAction(nameof(Index));

            return View(members);
        }
        public ActionResult HealthRecordDetails(int id)
        {

            // If the Id is not valid
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            // If member not found in database
            var memberHealthRecordDetails = _memberService.GetMemberHealthDetails(id);
            if (memberHealthRecordDetails == null)
                return RedirectToAction(nameof(Index));

            return View(memberHealthRecordDetails);
        }
    }
}
