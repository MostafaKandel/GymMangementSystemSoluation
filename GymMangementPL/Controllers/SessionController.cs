using GymMangmentBLL.Services.Classes;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.SessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymMangementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var Sessions = _sessionService.GetAllSessions();
            return View(Sessions);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of session can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

            var Session = _sessionService.GetSessionById(id);
            if (Session == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Session);

        }

        public IActionResult Create()
        {
            LoadCategiesDropDowns();
            LoadTrainersDropDowns();

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateSessionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadCategiesDropDowns();
                LoadTrainersDropDowns();

                return View(viewModel);
            }
            var Result = _sessionService.CreateSession(viewModel);
            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed to create session. Please check the details and try again.";
                LoadCategiesDropDowns();
                LoadTrainersDropDowns();
                return RedirectToAction(nameof(Create));
            }
            TempData["SuccessMessage"] = "Session created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of session can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

            var Session = _sessionService.GetSessionToUpdate(id);
            if (Session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            LoadTrainersDropDowns();
            return View(Session);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel updateSession)
        {
            if (!ModelState.IsValid)
            {
                
                LoadTrainersDropDowns();

                return View(updateSession);
            }
            var Result = _sessionService.UpdateSession(updateSession,id);
            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed to create session. Please check the details and try again.";
              
                LoadTrainersDropDowns();
                return RedirectToAction(nameof(Edit));
            }
            TempData["SuccessMessage"] = "Session created successfully.";
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of session can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Session = _sessionService.GetSessionById(id);
            if (Session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            return View(Session);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of session can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Result = _sessionService.RemoveSession(id);
            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed to delete session.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Session deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        #region Helpers

        private void LoadTrainersDropDowns() {
           
            var Trainers = _sessionService.GetAllTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }

        private void LoadCategiesDropDowns() {
            var Categories = _sessionService.GetAllCategoriesForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        }

        #endregion
    }
}
