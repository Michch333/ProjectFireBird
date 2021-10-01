using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallYourShot.Models
{
    public class Player
    {
        public Player()
        {

        }
        public Player(string _name, int _roll)
        {
            Name = _name;
            Roll = _roll;
        }
        public string Name { get; set; }
        //public string Initals { get; set; }
        //public bool DabCheck { get; set; }
        public int GameScore { get; set; }
        public int Roll  { get; set; }
        //public int Wins { get; set; }
    }
}
