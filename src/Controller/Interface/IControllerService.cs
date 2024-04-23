using System.ServiceModel;
using Seedr.Controller.Interface.Contracts;

namespace Seedr.Controller.Interface;

[ServiceContract]
public interface IControllerService
{
    [OperationContract]
    Task<SeederSettings> SetFanSpeed(SetFanSpeedCommand command);

    [OperationContract]
    Task<SeederSettings> SetSeedRate(SetSeedRateCommand command);
}
