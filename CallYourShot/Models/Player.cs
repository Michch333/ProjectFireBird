using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallYourShot.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string Initals { get; set; }
        public bool DabCheck { get; set; }
        public int GameScore { get; set; }
        public int roll  { get; set; }
        public int Wins { get; set; }
    }
}
