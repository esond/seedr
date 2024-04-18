using Seedr.Shared.Contracts;

namespace Seedr.Monitor.Infrastructure;

public interface IControllerClient
{
    Task<SeederSettings> SetFanSpeed(int fanSpeed);

    Task<SeederSettings> SetSeedRate(double seedRate);
}
