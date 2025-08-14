# Proto3 gRPC PoC — C# Guide

Contiene un piccolo servizio gRPC scritto in C# (.NET 9), un client console e un file `.proto` condiviso. È un PoC minimale ma completo, pensato come *template personale*.

---

## 📦 Requisiti

* **.NET 9+** (va bene anche .NET 8)
* **SDK gRPC** (incluso nei pacchetti NuGet che installiamo)

``` xml
<ItemGroup>
	<PackageReference Include="Grpc.AspNetCore" Version="2.71.0" />
	<PackageReference Include="Google.Protobuf" Version="3.30.2" />
	<PackageReference Include="Grpc.Net.Client" Version="2.71.0" />
	<PackageReference Include="Grpc.Tools" Version="2.72.0">
		<PrivateAssets>all</PrivateAssets>
		<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
	</PackageReference>
</ItemGroup>
```

---

## 🗂️ Struttura del repository

```
food-delivery-poc/
├─ proto/
│  └─ food_delivery.proto
├─ server/
│  ├─ FoodDelivery.Server.csproj
│  ├─ Program.cs
│  └─ Services/
│     └─ FoodDeliveryServiceImpl.cs
├─ client/
│  ├─ FoodDelivery.Client.csproj
│  └─ Program.cs
└─ ...
```



## 🧩 Aggiungere metodi lato modello (partial) — opzionale

Vuoi un helper direttamente sul `message` generato? Le classi generate sono `partial`. Crea, ad esempio, `client/PersonExtensions.cs` (o in un progetto condiviso) con:

```csharp
namespace FoodDelivery.Protos
{
    public sealed partial class OrderConfirmation
    {
        public bool IsDelivered() => string.Equals(Status, "Consegnato", StringComparison.OrdinalIgnoreCase);
    }
}
```

> Ricorda: **stesso namespace, stesso progetto** del tipo generato.

---

## ⚙️ Opzioni utili nel `.proto`

```proto
syntax = "proto3";
option csharp_namespace = "FoodDelivery.Protos"; // imposta il namespace C# generato

message OrderStatus {
  option deprecated = false; // esempio di option a livello di message
  string order_id = 1;
  string status = 2 [deprecated = false]; // esempio a livello di field
}
```

Le *options* aggiungono metadati (non logica). Per gRPC non devi settare nulla di speciale oltre al namespace.

## ✅ Cosa Abbiamo:

* Un **servizio gRPC** con 1 RPC unary e 1 RPC server‑streaming.
* Un **client console** che invoca entrambi.
* Un **file `.proto`** condiviso.


