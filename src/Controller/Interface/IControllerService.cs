using System.ServiceModel;
using Seedr.Shared;

namespace Seedr.Controller.Interface;

[ServiceContract]
public interface IControllerService
{
    [OperationContract]
    ValueTask<SeederSettings> SetFanSpeed(SetFanSpeedCommand command);

    [OperationContract]
    ValueTask<SeederSettings> SetSeedRate(SetSeedRateCommand command);
}
