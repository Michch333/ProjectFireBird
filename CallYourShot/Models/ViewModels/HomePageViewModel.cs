using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallYourShot.Models.ViewModels
{
    public class HomePageViewModel : HomePageWithRollResultViewModel
    {
        public HomePageViewModel()
        {
            ActivePlayers = new List<Player>();
            var Jason = new Player("Jason", 0);
            ActivePlayers.Add(Jason);

            var Mike = new Player("Mike", 0);
            ActivePlayers.Add(Mike);
            RollWinner = new Player();
            CurrentRoller = new Player();

        }
        public Player CurrentRoller { get; set; }
        public List<Player> ActivePlayers { get; set; }
        public Player RollWinner { get; set; }
    }
}
