using Microsoft.AspNetCore.Mvc;

namespace GymMangementPL.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
