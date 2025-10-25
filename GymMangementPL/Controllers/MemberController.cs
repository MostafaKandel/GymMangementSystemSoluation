using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.MemeberViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymMangementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        #region get All Members
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
          
            return View(members);
        }
        #endregion

        #region GetMember Data
        public ActionResult MemberDetails(int id) { 
        
            if(id<= 0)
            {
                TempData["ErrorMessage"] = "Id of Member can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

            var Member= _memberService.GetMemberDetails(id);
            if (Member == null) {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);
        }
        #endregion


        #region Member Health Record
        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var HealthRecord = _memberService.GetMemberHealthRecordDetails(id);
            if (HealthRecord == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(HealthRecord);
        }
        #endregion

        #region add member

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel createMember)
        {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check Data and Missing Fields");
                return View(nameof(Create), createMember);
            }
            bool Result= _memberService.CreateMember(createMember);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member is Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed to Create, Check Phone and Mail  ";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Member
        public ActionResult MemberEdit(int id) {
            if (id <= 0) {
                TempData["ErrorMessage"] = "Id of Member can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Member= _memberService.GetMemberToUpdate(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);
        }

        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id, MemberToUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var Result = _memberService.UpdateMemberDetails(id, viewModel);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member is Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed to Updated, Check Phone and Mail  ";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id) {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberToUpdate(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId= id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id) { 
        
            var Result= _memberService.RemoveMember(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member is deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed to delete ";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
