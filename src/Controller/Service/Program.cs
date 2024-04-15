using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProtoBuf.Grpc.Server;
using Seedr.Controller.Interface;
using Seedr.Controller.Service.Data;
using Seedr.Controller.Service.Services;

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
