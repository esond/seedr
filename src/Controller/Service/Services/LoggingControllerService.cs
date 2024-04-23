using Seedr.Controller.Interface;
using Seedr.Controller.Interface.Contracts;

namespace Seedr.Controller.Service.Services;

public class LoggingControllerService(IControllerService innerService, ILogger<LoggingControllerService> logger)
    : IControllerService
{
    public async Task<SeederSettings> SetFanSpeed(SetFanSpeedCommand command)
    {
        var settings = await innerService.SetFanSpeed(command);

        logger.LogInformation("Fan speed set to {FanSpeed}!", settings.FanSpeed);

        return settings;
    }

    public async Task<SeederSettings> SetSeedRate(SetSeedRateCommand command)
    {
        var settings = await innerService.SetSeedRate(command);

        logger.LogInformation("Seed rate set to {SeedRate:N}!", settings.SeedRate);

        return settings;
    }
}
