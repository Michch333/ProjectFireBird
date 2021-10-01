using CallYourShot.Models;
using CallYourShot.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        //*start dice logic
        public int DiceRoll()
        {
            Random dice = new Random();
            int roll = dice.Next(1, 60);
            return roll;
        }
        public Player DiceRoll(Player player)
        {
            Random dice = new Random();
            player.Roll = dice.Next(1, 60);
            return player;
        }
        public IActionResult GameRoll(List<Player> players, int numberToRollUnder)
        {
            var playersWithRolls = new List<Player>();

            foreach (var player in players)
            {
                DiceRoll(player);

                if (player.Roll <= numberToRollUnder)
                {
                    playersWithRolls.Add(player);
                }
            }
            var viewModel = new RollInfoViewModel();
            viewModel.RollWinner = CheckforWinner(players, numberToRollUnder, playersWithRolls);
            return View("Index", viewModel);
        }

        private Player CheckforWinner(List<Player> players, int numberToRollUnder, List<Player> playersWithRolls)
        {
            if (playersWithRolls.Count() == 0)
            {
                GameRoll(players, numberToRollUnder);
            }

            if (playersWithRolls.Count() >= 2)
            {
                CheckForTie(playersWithRolls, numberToRollUnder);
            }
            return players.FirstOrDefault();
        }

        public void CheckForTie(List<Player> playersWithRolls,int numberToRollUnder)
        {
            var tiedPLayersList = new List<Player>();
            var orderdedList = playersWithRolls.OrderBy(p => p.Roll).ToList<Player>();

            foreach (Player player in orderdedList)
            {
                if (player.Roll == orderdedList.FirstOrDefault().Roll)
                {
                    tiedPLayersList.Add(player);
                }

            }
            GameRoll(tiedPLayersList, numberToRollUnder);
        }
        //*end dice logic

        //*Start game logic
        public void GameChooser()
        {
                    
        }

        //*end game logic



    }
}
