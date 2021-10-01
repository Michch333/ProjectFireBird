using CallYourShot.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CallYourShot.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public int DiceRoll() 
        {
                Random dice = new Random();
                int roll = dice.Next(1, 60);
                return roll;
        }
        public IActionResult GameRoll(List<Player> players)
        {
            int numberToRollUnder = 0;
            string rollWinner = null; 
            foreach(var player in players)
            {
                int playerRoll =DiceRoll();
                if(playerRoll <= numberToRollUnder)
                {
                     player.roll = playerRoll;
                    rollWinner = player.Name;

                }
            }


            return View();
        }
        
    }
}
