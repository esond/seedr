using Seedr.Shared.Contracts;

namespace Seedr.Controller.Service.Data;

public class InMemorySettingsStore : ISettingsStore
{
    private readonly SeederSettings _settings = new();

    public Task<SeederSettings> GetCurrentSettings()
    {
        return Task.FromResult(_settings);
    }

    public Task<SeederSettings> SetFanSpeed(int fanSpeed)
    {
        _settings.FanSpeed = fanSpeed;

        return Task.FromResult(_settings);
    }

    public Task<SeederSettings> SetSeedRate(double seedRate)
    {
        _settings.SeedRate = seedRate;

        return Task.FromResult(_settings);
    }
}
