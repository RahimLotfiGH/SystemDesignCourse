using System.Threading.Channels;
using Hil.Ordering.Models.Mapping;
using Hil.Ordering.Models.Models;
using Microsoft.Extensions.Hosting;

namespace Hil.Ordering.Engine.Engines
{
    public class GenerationZeroEngine : BackgroundService
    {
        private readonly Channel<GenerationZeroModel> _channelZero;
        private readonly Channel<GenerationOneModel> _channelOne;

        public GenerationZeroEngine(Channel<GenerationZeroModel> channelZero, Channel<GenerationOneModel> channelOne)
        {
            _channelZero = channelZero;
            _channelOne = channelOne;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var model in _channelZero.Reader.ReadAllAsync(stoppingToken))
                {
                    await DoProcess(model);
                }
            }
        }

        private async Task DoProcess(GenerationZeroModel model)
        {
            if (model.Identity % 2 == 0)
                await SendToGenerationOne(model);
            else
                await WriteToFile(model);

        }

        private async Task SendToGenerationOne(GenerationZeroModel model)
        {
            await Task.Delay(100);

            await _channelOne.Writer.WriteAsync(model.ToGenerationOne());
        }

        private static async Task WriteToFile(GenerationZeroModel model)
        {
            await Task.Delay(100);

            await File.AppendAllTextAsync("GenerationZero.Engine.txt", model.ToString());
        }
    }
}
