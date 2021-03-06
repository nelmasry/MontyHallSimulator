using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHall.API.Service
{
    
    public interface IMontyHallService
    {
        Task<int> SimulateMontyHall(int totalGamesCount, bool stickToSameDoor);
    }
    public class MontyHallService : IMontyHallService
    {

        public MontyHallService()
        {
        }

        /// <summary>
        /// Simulate Monty hall games based on number of games and decision
        /// </summary>
        /// <param name="totalGamesCount">Total games to be simulated</param>
        /// <param name="stickToSameDoor">Whether guest decided to stick to same door or change his selection</param>
        /// <returns></returns>
        public Task<int> SimulateMontyHall(int totalGamesCount, bool stickToSameDoor)
        {
            int wins = 0;
            Random rand = new Random();
            if (totalGamesCount > 0)
            {
                for (int i = 0; i < totalGamesCount; i++)
                {
                    List<int> doors = new List<int>() { 0, 0, 0 };

                    int carDoor = rand.Next(3);
                    doors[carDoor] = 1;
                    int selectedDoor = rand.Next(3);

                    if (stickToSameDoor)
                    {
                        if (carDoor == selectedDoor)
                            wins++;
                    }
                    else
                    {
                        int shownDoor = GetShownDoor(doors, selectedDoor);
                        int switchedDoor = GetSwitchedDoor(selectedDoor, shownDoor);

                        if (switchedDoor == carDoor)
                            wins++;
                    }
                }
            }
            return Task.FromResult(wins);
        }

        /// <summary>
        /// Get teh door that will be switched when guest decide to change selected door
        /// </summary>
        /// <param name="selectedDoor">Selected door by the guest</param>
        /// <param name="shownDoor">The door shown by the game owner</param>
        /// <returns></returns>
        private int GetSwitchedDoor(int selectedDoor, int shownDoor)
        {
            return new List<int>() { 0, 1, 2 }.Where(l => l != selectedDoor && l != shownDoor).FirstOrDefault();
        }

        /// <summary>
        /// Pick the door that will be shown to guest which has the goat
        /// </summary>
        /// <param name="doors">List of the three door having two goats and a car</param>
        /// <param name="selectedDoor">The door selected by the guest</param>
        /// <returns></returns>
        private int GetShownDoor(List<int> doors, int selectedDoor)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i] == 0 && i != selectedDoor)
                    return i;
            }
            return -1;
        }
    }
}
