using CallYourShot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallYourShot.Models
{
    public class Game
    {
        public string Name { get; set; }
        public string Rules{ get; set; }
        public GameTypes Type { get; set; }
        public List<Player> Players { get; set; }
        public GamingConsoles Consoles { get; set; }
        public bool HasBeenPlayed { get; set; }
        public Player Winner { get; set; }
        public int WinnerScore { get; set; }
        public Player LastPlace { get; set; }
    }
}
