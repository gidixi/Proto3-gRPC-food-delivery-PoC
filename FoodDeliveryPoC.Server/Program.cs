using FoodDeliveryPoC.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGrpcService<FoodDeliveryServiceImpl>();
app.MapGet("/", () => "gRPC endpoint: usa un client gRPC per chiamare il servizio.");


app.UseHttpsRedirection();
app.Run();
