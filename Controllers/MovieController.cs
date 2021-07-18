using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieStore.DB;
using MovieStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MovieStore.Controllers
{
    public class MovieController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Movie m = new Movie();

            return View(m);
        }




        [HttpPost]
        public IActionResult MovieSearch(string searchString)
        {
            if (searchString != null)
            {
                string jsonStringMovieSearch = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/search/movie?api_key=5933922b6587d2d506362381025ef410&query="
                    + searchString.Replace(' ', '+'));

                MoviesSearchListJson list = JsonConvert.DeserializeObject<MoviesSearchListJson>(jsonStringMovieSearch);

                return View(list);
            }
            else
                return View();
        }

        public IActionResult MovieReserve()
        {
            string movieId = HttpContext.Request.QueryString.Value.Substring(HttpContext.Request.QueryString.Value.LastIndexOf('=') + 1);
            //string test = str.Substring(str.LastIndexOf('-') + 1)


            return View(JSONMethods.GetMovie(movieId));
        }

        [HttpPost]
        public IActionResult MovieReserve(DateTime startDate, DateTime endDate, string movieId)
        {

            Movie movie = JSONMethods.GetMovie(movieId);

            return View(movie);
        }

        public IActionResult MoviePrint()
        {
            string movieId = HttpContext.Request.RouteValues["id"].ToString();

            Movie movie = JSONMethods.GetMovie(movieId);

            return View(movie);
        }

        
    }
}
