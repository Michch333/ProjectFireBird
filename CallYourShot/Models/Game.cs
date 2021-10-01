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
        public string Type { get; set; }
        public List<Player> Players { get; set; }
        public bool HasBeenPlayed { get; set; }
        public string Winner { get; set; }
        public int WinnerScore { get; set; }
    }
}
