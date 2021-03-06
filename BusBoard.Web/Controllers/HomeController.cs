using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusBoard.Tfl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusBoard.Web.Models;

namespace BusBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var buses = TflApi.GetBusesForTwoNearestStops("NW5 1TL");
            return View(new HomeViewModel {buses = buses});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}

// form post
// path/query params
// controller method params

// 1. make a form
// 2. send data to somewhere
// 3. open a controller endpoint
// 4. perform api requests to tfl and postcodes.io
// 5. present in view
