using GymMangmentBLL.Services.Classes;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.MemeberViewModel;
using GymMangmentBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymMangementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController( ITrainerService trainerService) {
            _trainerService = trainerService;
        }
        #region Get all Trainers
        public IActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            
            return View(trainers);
        }
        #endregion

        #region Create Trainer
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data and Missing Fields");
                return View(nameof(Create), model);
            }
            bool Result = _trainerService.CreateTrainer(model);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed to Create, Check Phone and Mail  ";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Details
        public IActionResult Details(int id) {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

            var Trainer = _trainerService.GetTrainerDetails(id);
            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Trainer);

        }
        #endregion

        #region Edit Trainer
        public IActionResult Edit(int id) {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var trainer= _trainerService.GetTrainerToUpdate(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var Result = _trainerService.UpdateTrainerDetails(viewModel, id);
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
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = _trainerService.GetTrainerToUpdate(id);
            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {

            var Result = _trainerService.RemoveTrainer(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer is deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed to delete ";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
