using CallYourShot.Models;
using CallYourShot.Models.ViewModels;
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
        public int numberToRollUnder = 0;
        public IActionResult Index()
        {
            var viewModel = new HomePageViewModel();
            return View(viewModel);
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
        public IActionResult GameRoll(List<Player> players, int numberToRollUnder)
        {

            var playersWithRolls = new List<Player>();
            foreach(var player in players)
            {
                int playerRoll =DiceRoll();
                if(playerRoll <= numberToRollUnder)
                {
                    player.Roll = playerRoll;
                    playersWithRolls.Add(player);
                }
             }
            if (playersWithRolls.Count() == 0)
            {
                GameRoll( players, numberToRollUnder);
            }

            var orderdedList = playersWithRolls.OrderBy(p => p.Roll).ToList<Player>();
            var viewModel = new RollInfoViewModel();
            viewModel.RollWinner = orderdedList[0];
            return View("Index", viewModel);
        }
        
    }
}
