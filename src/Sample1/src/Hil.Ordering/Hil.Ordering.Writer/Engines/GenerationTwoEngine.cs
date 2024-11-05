using System.Threading.Channels;
using Hil.Ordering.Models.Mapping;
using Hil.Ordering.Models.Models;
using Microsoft.Extensions.Hosting;

namespace Hil.Ordering.Engine.Engines
{
    public class GenerationTwoEngine : BackgroundService
    {
        private readonly Channel<GenerationZeroModel> _channelZero;
        private readonly Channel<GenerationTwoModel> _channelTwo;

        public GenerationTwoEngine(Channel<GenerationZeroModel> channelZero, Channel<GenerationTwoModel> channelTwo)
        {
            _channelZero = channelZero;
            _channelTwo = channelTwo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var model in _channelTwo.Reader.ReadAllAsync(stoppingToken))
                {
                    await DoProcess(model);
                }
            }
        }

        private async Task DoProcess(GenerationTwoModel model)
        {
            if (model.Identity % 4 == 0)
                await SendToGenerationZero(model);
            else
                await WriteToFile(model);

        }

        private async Task SendToGenerationZero(GenerationTwoModel model)
        {
            await Task.Delay(2000);

            model.ChangeIdentity();

            await _channelZero.Writer.WriteAsync(model.ToGenerationZero());
        }

        private static async Task WriteToFile(GenerationTwoModel model)
        {
            await Task.Delay(100);

            await File.AppendAllTextAsync("GenerationTwo.Engine.txt", model.ToString());
        }
    }
}
