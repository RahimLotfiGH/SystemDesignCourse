using System.Threading.Channels;
using Hil.Ordering.Models.Mapping;
using Hil.Ordering.Models.Models;
using Microsoft.Extensions.Hosting;

namespace Hil.Ordering.Engine.Engines
{
    public class GenerationOneEngine : BackgroundService
    {
        private readonly Channel<GenerationOneModel> _channelOne;
        private readonly Channel<GenerationTwoModel> _channelTwo;

        public GenerationOneEngine(Channel<GenerationOneModel> channelOne
                                   , Channel<GenerationTwoModel> channelTwo)
        {
            _channelOne = channelOne;
            _channelTwo = channelTwo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var model in _channelOne.Reader.ReadAllAsync(stoppingToken))
                {
                    await DoProcess(model);
                }
            }
        }

        private async Task DoProcess(GenerationOneModel model)
        {
            if (model.Identity % 3 == 0)
                await SendToGenerationOne(model);
            else
                await WriteToFile(model);

        }

        private async Task SendToGenerationOne(GenerationOneModel model)
        {
            await Task.Delay(2000);

            await _channelTwo.Writer.WriteAsync(model.ToGenerationTwo());
        }

        private static async Task WriteToFile(GenerationOneModel model)
        {
            await Task.Delay(100);

            await File.AppendAllTextAsync("GenerationOne.Engine.txt", model.ToString());
        }
    }
}
