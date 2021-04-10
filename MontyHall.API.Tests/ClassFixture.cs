using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MontyHall.API.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MontyHall.API.Tests
{
    public class ClassFixture
    {
        public IMontyHallService MontyHallService;
        public ClassFixture()
        {
            var services = new ServiceCollection();
            services.AddScoped<IMontyHallService, MontyHallService>();

            var provider = services.BuildServiceProvider();
            MontyHallService = provider.GetService<IMontyHallService>();
        }
    }
}
