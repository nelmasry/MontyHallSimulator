using MontyHall_Game.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MontyHall_Game.Controllers
{
    public class MontyHallController : Controller
    {
        public async Task<ActionResult> Index(int? totalGames, bool? sticktoSameDoor)
        {
            if (totalGames != null && totalGames > 0 && sticktoSameDoor != null)
            {
                HttpResponseMessage response;
                using (var _client = new HttpClient())
                {
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MontyHallApiAddress"]);
                    _client.DefaultRequestHeaders.Add("Accept", "application/json");

                    response = await _client.GetAsync($"{ConfigurationManager.AppSettings["MontyHallApiPath"]}/{totalGames}/{sticktoSameDoor}");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var motyHallResult = JsonConvert.DeserializeObject<MontyHallSimulationResponse>(
                            await response.Content.ReadAsStringAsync());
                        return View(new MontyHallSimulationViewModel()
                        {
                            TotalGames = totalGames.Value,
                            TotalWins = motyHallResult.TotalWins,
                            TotalLoss = motyHallResult.TotalLoss
                        });
                    }
                }
            }
            return View();
        }
    }
}