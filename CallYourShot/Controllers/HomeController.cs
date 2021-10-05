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
    {
        public IActionResult Index()
        {
            var viewModel = new HomePageViewModel();
            return View(viewModel);
        } 
        public IActionResult DisplayUsersRoll(HomePageViewModel viewModel)
        {
            DiceRoll(viewModel.CurrentRoller);
            viewModel.CurrentRoll = viewModel.CurrentRoller.Roll;
            return View("Index", viewModel);
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
        public Player DiceRoll(Player player)
        {
            Random dice = new Random();
            player.Roll = dice.Next(1, 60);
            return player;

        }
        public IActionResult GameRoll(List<Player> players)
        {
            var playersWithRolls = players.OrderByDescending(p => p.Roll).ToList<Player>();
            var viewModel = new RollInfoViewModel();
            int numberToRollUnder = 60;
            DiceRoll(players[0]);
            //DisplayUsersRoll("Greg");
            if (playersWithRolls.Count() == players.Count())
            {
                CheckIfValid(players, playersWithRolls, numberToRollUnder);
                viewModel.RollWinner = CheckforWinner(players, numberToRollUnder, playersWithRolls);
                
            }
            return View("Index", viewModel);
        }

        private void CheckIfValid(List<Player> players, List<Player> playersWithRolls, int numberToRollUnder)
        {
            foreach (var player in players)
            {
                if (player.Roll > numberToRollUnder)
                {
                    playersWithRolls.Remove(player);
                }
            }
        }

        private Player CheckforWinner(List<Player> players, int numberToRollUnder, List<Player> playersWithRolls)
        {
            if (playersWithRolls.Count() == 0)
            {
                GameRoll(players);
            }

            if (playersWithRolls.Count() >= 2)
            {
                CheckForTie(playersWithRolls, numberToRollUnder);
            }
            return playersWithRolls.FirstOrDefault();
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
            GameRoll(tiedPLayersList);
        }
        //*end dice logic

        //*Start games logic
        
        public void GameChooser(List<Player> players,Player rollWinner)
        {
                 
        }
        public List<string> GameTypeToDisplayString(List<GameTypes> games)
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
            var currentPlayersGameScores = new List<Player>().OrderByDescending(p => p.Roll).ToList<Player>();
            var score = 0;
            player.GameScore = score;
            currentPlayersGameScores.Add(player);

        }

        public Player GameWinnerLogic(List<Player> players, string gameList, string consoleList, List<Player> currentPlayersGameScores)
        {
            
            GameTypes SelectedGameType = GameTypes.Battle_Royales;

            if (SelectedGameType != GameTypes.Racing_Games) // type of racing game instead of string?
            {
                var orderdedList = currentPlayersGameScores.OrderByDescending(p => p.GameScore).ToList<Player>();
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
                var orderdedList = currentPlayersGameScores.OrderBy(p => p.GameScore).ToList<Player>();
                return orderdedList.First();
            }

        }

        //*end game logic

        // state management
        public List<Player> ResetPlayers(List<Player> activePlayers)
        {
            foreach (Player player in activePlayers)
            {
                player.Roll = 0;
                player.GameScore = 0;
            }
            return activePlayers;
        }


    }
}
