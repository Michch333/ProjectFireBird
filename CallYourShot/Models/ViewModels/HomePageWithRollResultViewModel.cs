using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallYourShot.Models.ViewModels
{
    public class   HomePageWithRollResultViewModel
    {
        public int NumberToRollUnder { get; set; }
        public int CurrentRoll { get; set; }
        public string CurrentRollName { get; set; }
        public string NextRollerName { get; set; }
    }
}
