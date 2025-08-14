using FoodDelivery.Protos;
using Grpc.Core;
using Grpc.Net.Client;


// Nota: assicurati che il server giri su https://localhost:7272
using var channel = GrpcChannel.ForAddress("https://localhost:7272");
var client = new FoodDeliveryService.FoodDeliveryServiceClient(channel);

// 1) Chiamata unary
var confirmation = await client.PlaceOrderAsync(new OrderRequest
{
    CustomerId = "C123",
    RestaurantId = "R456",
    Items = { "Pizza Margherita", "Coca Cola" }
});
Console.WriteLine($"[CLIENT] Ordine creato: {confirmation.OrderId} — Stato: {confirmation.Status}");

// 2) Server streaming
using var call = client.TrackOrder(confirmation);
await foreach (var update in call.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"[CLIENT] Update: {update.OrderId} — {update.Status}");
}