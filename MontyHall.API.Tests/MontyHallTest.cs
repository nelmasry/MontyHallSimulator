using MontyHall.API.Service;
using System;
using Xunit;

namespace MontyHall.API.Tests
{
    public class MontyHallTest : IClassFixture<ClassFixture>
    {
        private readonly IMontyHallService _MontyHallService;

        public MontyHallTest(ClassFixture fixture)
        {
            _MontyHallService = fixture.MontyHallService;
        }
        [Fact]
        public async void RETURN_ZERO_IF_TOTALGAMES_ZERO()
        {
            var wins = await _MontyHallService.SimulateMontyHall(0, false);
            Assert.Equal(0, wins);
        }

        [Fact]
        public async void RETURN_MORE_LOSS_IF_STICKTOSAMEDOOR()
        {
            int totalGames = 100;
            var wins = await _MontyHallService.SimulateMontyHall(totalGames, true);
            int loss = totalGames - wins;
            Assert.True(wins < loss);
        }

        [Fact]
        public async void RETURN_MORE_WINS_IF_CHANGEDOOR()
        {
            int totalGames = 100;
            var wins = await _MontyHallService.SimulateMontyHall(totalGames, false);
            int loss = totalGames - wins;
            Assert.True(wins > loss);
        }
    }
}
