using Hil.Ordering.Engine.Engines;

namespace Hil.Ordering.ConfigAgent
{
    public static class ConfigEngines
    {
        public static void RegistrationEngines(this IServiceCollection services)
        {

            services.AddHostedService<GenerationZeroEngine>();

            services.AddHostedService<GenerationOneEngine>();

            services.AddHostedService<GenerationTwoEngine>();
        }
    }
}
