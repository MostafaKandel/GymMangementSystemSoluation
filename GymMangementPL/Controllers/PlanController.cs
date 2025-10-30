using GymMangmentBLL.Services.Classes;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.PlanViewModels;
using GymMangmentBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMangementPL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }

        public IActionResult Details(int id) {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Plan can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

           var plan = _planService.GetPlanDetails(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);

        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Plan can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var planToUpdate = _planService.GetPlanToUpdate(id);
            if (planToUpdate == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found or Cannot be Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(planToUpdate);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdatePlanViewModel updatePlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data");
                return View(updatePlan);
            }
            var Result = _planService.UpdatePlan(id, updatePlan);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan is Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed to Updated  ";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Activate(int id) { 
        
            var Result =_planService.ToggleStatus(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan status is Changed Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan status Failed to Change";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
