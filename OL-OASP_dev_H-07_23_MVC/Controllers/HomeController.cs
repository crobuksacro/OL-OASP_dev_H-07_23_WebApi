using Microsoft.AspNetCore.Mvc;
using OL_OASP_dev_H_07_23_MVC.Models;
using System.Diagnostics;

namespace OL_OASP_dev_H_07_23_MVC.Controllers
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
            return View();
        }

 

    }
}
