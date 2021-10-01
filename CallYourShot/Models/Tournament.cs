using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallYourShot.Models
{
    public class Tournament
    {
        public List<Game> Games { get; set;}
        public Player Winner { get; set; }
        public Player CurrentLeader { get; set; }
    }
}
