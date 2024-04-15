using System.Runtime.Serialization;

namespace Seedr.Controller.Interface;

[DataContract]
public class SetFanSpeedCommand
{
    [DataMember(Order = 1)]
    public int FanSpeed { get; set; }
}


[DataContract]
public class SetSeedRateCommand
{
    [DataMember(Order = 1)]
    public double SeedRate { get; set; }
}
