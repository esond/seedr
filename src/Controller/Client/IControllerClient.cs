using Seedr.Controller.Interface.Contracts;

namespace Seedr.Controller.Client;

public interface IControllerClient
{
    Task<SeederSettings> SetFanSpeed(int fanSpeed);

    Task<SeederSettings> SetSeedRate(double seedRate);
}
