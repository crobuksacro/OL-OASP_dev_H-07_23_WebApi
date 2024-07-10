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
            webApiMovieServiceClient.AddMovie(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var response =  webApiMovieServiceClient.GetMovie(id);

            return View(response);
        }


        public IActionResult Delete(int id)
        {
            webApiMovieServiceClient.GetMovie(id);
           return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var response = webApiMovieServiceClient.GetMovie<MovieUpdateBinding>(id);
            return View(response);
        }

        [HttpPost]
        public IActionResult Edit(MovieUpdateBinding model)
        {
            webApiMovieServiceClient.Update(model);
            return RedirectToAction("Index");
        }
    }
}
