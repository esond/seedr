using System.Runtime.Serialization;

namespace Seedr.Controller.Interface.Contracts;

[DataContract]
public record SetFanSpeedCommand
{
    [DataMember(Order = 1)]
    public required int FanSpeed { get; set; }
}
