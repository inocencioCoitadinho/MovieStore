using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Controllers
{
    public class MyMoviesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public MyMoviesController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            List<Movie> movies = Movie.GetMovieByUserId(userId);
            return View(movies);
        }
    }
}
