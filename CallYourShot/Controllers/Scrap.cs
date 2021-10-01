using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallYourShot.Controllers
{
    public class Scrap : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
