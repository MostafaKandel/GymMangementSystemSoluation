using GymMangmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMangementPL.Controllers
{
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
