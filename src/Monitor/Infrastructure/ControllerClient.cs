using Seedr.Controller.Interface;
using Seedr.Controller.Interface.Contracts;
using Seedr.Shared.Contracts;

namespace Seedr.Monitor.Infrastructure;

public class ControllerClient(IControllerService proxy) : IControllerClient
{
    public Task<SeederSettings> SetFanSpeed(int fanSpeed)
    {
        return proxy.SetFanSpeed(new SetFanSpeedCommand
        {
            FanSpeed = fanSpeed
        });
    }

    public Task<SeederSettings> SetSeedRate(double seedRate)
    {
        return proxy.SetSeedRate(new SetSeedRateCommand
        {
            SeedRate = seedRate
        });
    }
}
