using Seedr.Shared.Contracts;

namespace Seeder.Controller.Service.Data;

public interface ISettingsStore
{
    Task<SeederSettings> GetCurrentSettings();

    Task<SeederSettings> SetFanSpeed(int fanSpeed);

    Task<SeederSettings> SetSeedRate(double seedRate);
}
