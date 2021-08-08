using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStore.DB;
using MovieStore.Models.Actor;
using MovieStore.Models.Cast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Controllers
{
    public class ActorController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        public ActorController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }



        
        public IActionResult Index()
        {

            string actorId = HttpContext.Request.RouteValues.Values.Last().ToString();
            PersonView actor = new PersonView(actorId);
            return View(actor);
        }
    }
}
