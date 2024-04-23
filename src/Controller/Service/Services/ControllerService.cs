using Seedr.Controller.Interface;
using Seedr.Controller.Interface.Contracts;
using Seedr.Controller.Service.Data;

namespace Seedr.Controller.Service.Services;

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
