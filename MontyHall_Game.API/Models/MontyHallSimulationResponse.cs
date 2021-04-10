using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MontyHall_Game.API.Models
{
    public class MontyHallSimulationResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "total_wins")]
        public int TotalWins { get; set; }

        [JsonProperty(PropertyName = "total_loss")]
        public int TotalLoss { get; set; }

        [JsonProperty(PropertyName = "total_games")]
        public int TotalGames { get; set; }
    }
}