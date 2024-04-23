using Seedr.Controller.Interface;
using Seedr.Controller.Interface.Contracts;

namespace Seedr.Controller.Client;

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
