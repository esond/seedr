using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Seedr.Controller.Interface;
using Seedr.Shared;

namespace Seedr.Monitor.Infrastructure;

public interface IControllerClient
{
    Task<SeederSettings> SetFanSpeed(int fanSpeed);

    Task<SeederSettings> SetSeedRate(double seedRate);
}

public class ControllerClient : IControllerClient
{
    public async Task<SeederSettings> SetFanSpeed(int fanSpeed)
    {
        using var http = GrpcChannel.ForAddress("http://localhost:5000");
        var controllerService = http.CreateGrpcService<IControllerService>();

        var settings = await controllerService.SetFanSpeed(new SetFanSpeedCommand // todo: records?
        {
            FanSpeed = fanSpeed
        });

        return settings;
    }

    public async Task<SeederSettings> SetSeedRate(double seedRate)
    {
        using var http = GrpcChannel.ForAddress("http://localhost:5000");
        var controllerService = http.CreateGrpcService<IControllerService>();

        var settings = await controllerService.SetSeedRate(new SetSeedRateCommand // todo: records?
        {
            SeedRate = seedRate
        });

        return settings;
    }
}
