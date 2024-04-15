using Seeder.Controller.Service.Data;
using Seedr.Controller.Interface;
using Seedr.Shared;

namespace Seeder.Controller.Service.Services;

public class ControllerService(ISettingsStore settings) : IControllerService
{
    public async ValueTask<SeederSettings> SetFanSpeed(SetFanSpeedCommand command)
    {
        return await settings.SetFanSpeed(command.FanSpeed);
    }

    public async ValueTask<SeederSettings> SetSeedRate(SetSeedRateCommand command)
    {
        return await settings.SetSeedRate(command.SeedRate);
    }
}
