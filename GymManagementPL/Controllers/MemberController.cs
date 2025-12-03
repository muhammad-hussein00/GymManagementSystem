using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
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

        #region Get All Members

        // BaseUrl/Member
        // BaseUrl/Member/Index
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        #endregion

        #region GetMemberDetails

        // BaseUrl/Member/MemberDetails/@id
        public ActionResult MemberDetails(int id)
        {
            // If the Id is not valid
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero";
                return RedirectToAction(nameof(Index));
            }

            // If member not found in database
            var members = _memberService.GetMemberDetails(id);
            if (members == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(members);
        }
        #endregion

        #region Get HealthRecord Details 

        // BaseUrl/Member/HealthRecordDetails/@id
        public ActionResult HealthRecordDetails(int id)
        {

            // If the Id is not valid
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero";
                return RedirectToAction(nameof(Index));

            }

            // If member not found in database
            var memberHealthRecordDetails = _memberService.GetMemberHealthDetails(id);
            if (memberHealthRecordDetails == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(memberHealthRecordDetails);
        }
        #endregion

        #region Create Member

        // BaseUrl/Member/CreateMember
        public ActionResult CreateMember()
        {
            return View();
        }

        // BaseUrl/Member
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel createMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "There are missing fields");
                return View(nameof(CreateMember), createMember);
            }
            var isCreated = _memberService.CreateMember(createMember);
            if (isCreated)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Faild To CreateMember";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Member

        // BaseUrl/Member/EditMember
        // GET
        public ActionResult EditMember(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id cann't be negative or zero";
                return RedirectToAction(nameof(Index));
            }
            var memberToEdit = _memberService.GetMemberToUpdate(id);
            if(memberToEdit == null)
            {
                TempData["ErrorMessage"] = "Member Not Found.";
                return RedirectToAction(nameof(Index));
            }
             return View(memberToEdit);
        } 
        [HttpPost]
        public ActionResult EditMember([FromRoute]int id ,MemberToUpdateViewModel memberUpdated)
        {
            if (!ModelState.IsValid)
                return View(memberUpdated);
            
            var isUpdated = _memberService.UpdateMember(memberUpdated, id);
            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Member Updated SuccessFully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Faild To Update";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion



    }
}
