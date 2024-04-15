using Seeder.Controller.Service.Data;
using Seedr.Controller.Interface;
using Seedr.Controller.Interface.Contracts;
using Seedr.Shared.Contracts;

namespace Seeder.Controller.Service.Services;

public class ControllerService(ISettingsStore settings) : IControllerService
{
    public Task<SeederSettings> SetFanSpeed(SetFanSpeedCommand command)
    {
        return settings.SetFanSpeed(command.FanSpeed);
    }

    public Task<SeederSettings> SetSeedRate(SetSeedRateCommand command)
    {
        return settings.SetSeedRate(command.SeedRate);
    }
}
