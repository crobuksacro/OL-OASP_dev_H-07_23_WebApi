using Microsoft.AspNetCore.Mvc;
using OL_OASP_dev_H_07_23_MVC.Models;
using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_Shared.Services.Interfaces;
using System.Diagnostics;

namespace OL_OASP_dev_H_07_23_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebApiMovieServiceClient webApiMovieServiceClient;


        public HomeController(ILogger<HomeController> logger, IWebApiMovieServiceClient webApiMovieServiceClient)
        {
            _logger = logger;
            this.webApiMovieServiceClient = webApiMovieServiceClient;
        }

        public IActionResult Index()
        {
            var movies =  webApiMovieServiceClient.GetMovies();
            return View(movies);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieBinding model)
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }


        public IActionResult Delete(int id)
        {
           return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(MovieUpdateBinding model)
        {
            return View();
        }
    }
}
