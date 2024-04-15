using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProtoBuf.Grpc.Server;
using Seeder.Controller.Service.Data;
using Seeder.Controller.Service.Services;
using Seedr.Controller.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISettingsStore, InMemorySettingsStore>();

builder.Services.AddCodeFirstGrpc();

builder.Services.AddTransient<IControllerService, ControllerService>()
    .Decorate<IControllerService, LoggingControllerService>();

// Force HTTP2 to enable gRPC communication
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var host = builder.Build();

host.MapGrpcService<IControllerService>();

host.Run();
