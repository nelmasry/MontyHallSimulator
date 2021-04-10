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
        public async Task<ActionResult> Index(MontyHallSimulationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response;
                    using (var _client = new HttpClient())
                    {
                        _client.DefaultRequestHeaders.Accept.Clear();
                        _client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MontyHallApiAddress"]);
                        _client.DefaultRequestHeaders.Add("Accept", "application/json");

                        response = await _client.GetAsync($"{ConfigurationManager.AppSettings["MontyHallApiPath"]}" +
                            $"/{model.TotalGames}/{model.StickToSameDoor}");

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var motyHallResult = JsonConvert.DeserializeObject<MontyHallSimulationResponse>(
                                await response.Content.ReadAsStringAsync());
                            model.TotalWins = motyHallResult.TotalWins;
                            model.TotalLoss = motyHallResult.TotalLoss;
                            return View(model);
                        }
                        ModelState.AddModelError("SimulateError", $"Error while simulating games, ex: {response.Content.ReadAsStringAsync()}");
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("SimulateError", $"Error while simulating games, ex: {ex.Message}");
            }
            return View();
        }
    }
}