using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MontyHall_Game.Models
{
    public class MontyHallSimulationViewModel
    {
        [Required]
        [Display(Name = "Total Games")]
        public int TotalGames { get; set; }

        [Display(Name = "Stick to same door")]
        public bool StickToSameDoor { get; set; }

        [Display(Name = "Total Wins")]
        public int TotalWins { get; set; }

        [Display(Name = "Total Loss")]
        public int TotalLoss { get; set; }
    }
}