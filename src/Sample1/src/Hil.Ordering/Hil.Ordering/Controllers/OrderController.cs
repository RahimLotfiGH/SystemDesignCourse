using System.Threading.Channels;
using Hil.Ordering.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hil.Ordering.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly Channel<GenerationZeroModel> _channelZero;

        public OrderController(Channel<GenerationZeroModel> channelZero)
        {
            _channelZero = channelZero;
        }

        [HttpPost]
        public async Task SendOrders()
        {

            await Task.Run(async () =>
            {
                SendOrderToQueue(1, 1000);

                SendOrderToQueue(2, 1000);

            });

        }

        private async Task SendOrderToQueue(int batchId, int count)
        {
            for (var i = 0; i < count; ++i)
            {
                await Task.Delay(1);
                await _channelZero.Writer.WriteAsync(new GenerationZeroModel
                {

                    Data = $" BatchId [{batchId}]- Order id is {i}",
                    Identity = i
                });

            }

        }
    }
}
