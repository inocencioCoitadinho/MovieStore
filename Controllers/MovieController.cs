using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MovieStore.Data;
using MovieStore.DB;
using MovieStore.Models;
using MovieStore.Models.Genres;
using MovieStore.Models.Movie;
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

        private readonly UserManager<IdentityUser> _userManager;

        public MovieController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }



        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Language"] = new SelectList(MovieLanguage.GetMovieLanguages(), "NameJson", "Name");
            ViewData["Genres"] = new SelectList(Genre.GetGenres(), "GenreId", "Name");
            Genre m = new Genre();
            return View(m);
        }




        [HttpGet]
        public IActionResult MovieSearch(string searchString, string language, int page, int current_result, int page_number_language)
        {
            if (searchString != null)
            {
                string args = JSONMethods.BuildSearchString(searchString, null, page);
                string jsonStringMovieSearch = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/search/movie?api_key=5933922b6587d2d506362381025ef410"
                   + args);

                MoviesSearchListJson list = JsonConvert.DeserializeObject<MoviesSearchListJson>(jsonStringMovieSearch);
                list.searchString = searchString;
                list.PaginatedByLanguage(language, page_number_language, current_result);

                return View(list);
            }
            else
                return View();
        }

      

        public IActionResult MovieReserve()
        {
            string movieId = HttpContext.Request.QueryString.Value.Substring(HttpContext.Request.QueryString.Value.LastIndexOf('=') + 1);

            ViewData["Post"] = false;
            return View(JSONMethods.GetMovie(movieId));
        }


        [HttpPost]
        public IActionResult MovieReserve(DateTime startDate, DateTime endDate, string movieId)
        {
            Movie movie = JSONMethods.GetMovie(movieId);
            int status = DataManipulation.CheckMovieReservationStatus(startDate, endDate, movie);
            ViewData["Post"] = true;
            ViewData["Status"] = status;

            if (status == 1 )//movie can be reserved
            {
                string userId = _userManager.GetUserId(HttpContext.User);
                Movie.InsertMovie(movie, endDate, startDate, Guid.Parse(userId));
                return View(movie); 
            }
            else
            { 
                return View(movie);
            }
        }


        public IActionResult MoviePrint()
        {
            string movieId = HttpContext.Request.RouteValues["id"].ToString();

            Movie movie = JSONMethods.GetMovie(movieId);
            MovieView view = new MovieView(movie);

            return View(view);
        }

        
    }
}
