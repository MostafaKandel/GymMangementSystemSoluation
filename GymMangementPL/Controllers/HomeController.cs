using GymMangmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMangementPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public HomeController( IAnalyticsService analyticsService)
        {
           _analyticsService = analyticsService;
        }

        public ViewResult Index()
        {
            var Data= _analyticsService.GetAnalyticsData();
            return View(Data);
        }
    }
}
