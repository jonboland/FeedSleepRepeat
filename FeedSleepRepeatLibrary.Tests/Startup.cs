using Microsoft.Extensions.DependencyInjection;

namespace FeedSleepRepeatLibrary.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFeedSleepRepeatLogic, FeedSleepRepeatLogic>();
        }
    }
}
