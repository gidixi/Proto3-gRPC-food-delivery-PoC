using FoodDelivery.Protos;
using Grpc.Core;

namespace FoodDeliveryPoC.Server.Services;

public class FoodDeliveryServiceImpl : FoodDeliveryService.FoodDeliveryServiceBase
{
    public override Task<OrderConfirmation> PlaceOrder(OrderRequest request, ServerCallContext context)
    {
        var confirmation = new OrderConfirmation
        {
            OrderId = Guid.NewGuid().ToString(),
            Status = "In preparazione"
        };
        Console.WriteLine($"[SERVER] Nuovo ordine di {request.CustomerId}: {string.Join(", ", request.Items)}");
        return Task.FromResult(confirmation);
    }

    public override async Task TrackOrder(OrderConfirmation request, IServerStreamWriter<OrderStatus> responseStream, ServerCallContext context)
    {
        var steps = new[] { "In preparazione", "In consegna", "Consegnato" };
        foreach (var step in steps)
        {
            await responseStream.WriteAsync(new OrderStatus { OrderId = request.OrderId, Status = step });
            await Task.Delay(1200);
        }
    }
}
