using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieStore.DB;
using MovieStore.Models;
using MovieStore.Models.Movie;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Controllers
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
            string jsonStringMovieSearch = JSONMethods.JsonApiRequest
                ("https://api.themoviedb.org/3/trending/movie/week?api_key=5933922b6587d2d506362381025ef410");

            MoviesSearchListJson list = JsonConvert.DeserializeObject<MoviesSearchListJson>(jsonStringMovieSearch);
            List<MovieSearchJsonView> view = MovieSearchJsonView.Create(list);

            return View(view);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult GetTMDBLogo()
        {
            using (MovieStoreContext db = new MovieStoreContext())
            {
                byte[] photo = db
                .FileDepot
                .Where(p => p.FileId == Guid.Parse("9447D858-E174-46D0-991C-2EFDF0EF5DBD"))
                .Select(img => img.File)
                .FirstOrDefault();
                return File(photo, "image/jpeg");
                
            }
            
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
