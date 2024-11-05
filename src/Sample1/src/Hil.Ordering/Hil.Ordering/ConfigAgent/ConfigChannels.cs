using Hil.Ordering.Models.Models;
using System.Threading.Channels;

namespace Hil.Ordering.ConfigAgent
{
    public static class ConfigChannels
    {
        public static void RegistrationChannels(this IServiceCollection services)
        {

            services.AddSingleton(Channel.CreateBounded<GenerationZeroModel>(new BoundedChannelOptions(100000)
            {
                FullMode = BoundedChannelFullMode.Wait

            }));

            services.AddSingleton(Channel.CreateBounded<GenerationOneModel>(new BoundedChannelOptions(10000)
            {
                FullMode = BoundedChannelFullMode.Wait

            }));

            services.AddSingleton(Channel.CreateBounded<GenerationTwoModel>(new BoundedChannelOptions(5000)
            {
                FullMode = BoundedChannelFullMode.Wait

            }));

        }
    }
}
