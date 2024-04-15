using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Seedr.Controller.Interface;
using Seedr.Controller.Interface.Contracts;
using Seedr.Shared.Contracts;

namespace Seedr.Monitor.Infrastructure;

public interface IControllerClient
{
    Task<SeederSettings> SetFanSpeed(int fanSpeed);

    Task<SeederSettings> SetSeedRate(double seedRate);
}

public class ControllerClient(string controllerUrl) : IControllerClient
{
    public async Task<SeederSettings> SetFanSpeed(int fanSpeed)
    {
        using var http = GrpcChannel.ForAddress(controllerUrl);
        var controllerService = http.CreateGrpcService<IControllerService>();

        var settings = await controllerService.SetFanSpeed(new SetFanSpeedCommand
        {
            FanSpeed = fanSpeed
        });

        return settings;
    }

    public async Task<SeederSettings> SetSeedRate(double seedRate)
    {
        using var http = GrpcChannel.ForAddress(controllerUrl);
        var controllerService = http.CreateGrpcService<IControllerService>();

        var settings = await controllerService.SetSeedRate(new SetSeedRateCommand
        {
            SeedRate = seedRate
        });

        return settings;
    }
}
