using Seedr.Controller.Interface.Contracts;

namespace Seedr.Controller.Service.Data;

public interface ISettingsStore
{
    Task<SeederSettings> GetCurrentSettings();

    Task<SeederSettings> SetFanSpeed(int fanSpeed);

    Task<SeederSettings> SetSeedRate(double seedRate);
}
