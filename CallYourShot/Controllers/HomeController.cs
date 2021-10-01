using CallYourShot.Models;
using CallYourShot.Models.Enums;
using CallYourShot.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace CallYourShot.Controllers
{
    public class HomeController : Controller
    {   List<Player> currentGamePlayersWithScores = new List<Player>();          
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
            var playersWithRolls = new List<Player>().OrderByDescending(p => p.Roll).ToList<Player>();

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

            foreach (Player player in playersWithRolls)
            {
                if (player.Roll == playersWithRolls.FirstOrDefault().Roll)
                {
                    tiedPLayersList.Add(player);
                }

            }
            GameRoll(tiedPLayersList, numberToRollUnder);
        }
        //*end dice logic

        //*Start games logic
        
        public void GameChooser(List<Player> players,Player rollWinner)
        {
                 
        }
        public List<string> GameTypesListMaker(List<GameTypes> games)
        {
            var gamesListWithoutDelimiters = new List<string>();
            var gameTypeList = Enum.GetNames(typeof(GameTypes)).ToList();
            foreach(var gameType in gameTypeList)
            {
                string GameTypeWithSpaces = Regex.Replace(gameType, @"[_]"," ");
                string GameTypeWithDashes = Regex.Replace(GameTypeWithSpaces, @"[__]", "-");
                string GameTypeRemoveStar = Regex.Replace(GameTypeWithDashes, @"[*]", "");
                gamesListWithoutDelimiters.Add(GameTypeRemoveStar);
            }
            return gamesListWithoutDelimiters;
        }

        public List<string> ConsoleListMaker()
        {
            var consoleList = Enum.GetNames(typeof(GamingConsoles)).ToList();
            var consolelistWithoutDelimiters = new List<String>();
            foreach(var consoles in consoleList)
            {
                string consoleWithSpaces = Regex.Replace(consoles, @"[_]", " ");
                string consoleTypeWithDashes = Regex.Replace(consoleWithSpaces, @"[__]", "-");
                string consoleTypeRemoveStar = Regex.Replace(consoleTypeWithDashes, @"[*]", "");
                consolelistWithoutDelimiters.Add(consoleTypeRemoveStar);
            }
            return consolelistWithoutDelimiters;
        }
        //*will seperate out later
        public void GameApiCall()
        {

        }
        public void WriteGameInfoToDB()
        {

        }
        //*will seperate out later end
        public void GameScoreEntry(Player player) 
        {
            var score = 0;
            player.GameScore = score ;
            currentGamePlayersWithScores.Add(player);
            
        }

        public Player GameWinnerLogic(List<Player> players, string gameList,string consoleList)
        { 
            var SelectedGameType ="" ;
            var currentPlayersGameScores = new List<Player>().OrderByDescending(p => p.Roll).ToList<Player>();  
            if( SelectedGameType != "Racing Games")
            {
              var orderdedList = currentGamePlayersWithScores.OrderByDescending(p => p.Roll).ToList<Player>();
                return orderdedList.First();
            }
            //* gonna code individual game score logic later but the two active ones should fit our needs for testing
            //if (SelectedGameType == "Race")
            //{
            //    var orderdedList = currentGamePlayersWithScores.OrderByDescending(p => p.Roll).ToList<Player>();

            //    return orderdedList.First();
            //}
            else
            {
                var orderdedList = currentGamePlayersWithScores.OrderBy(p => p.Roll).ToList<Player>();
                return orderdedList.First();
            }
            
        }

        //*end game logic



    }
}
