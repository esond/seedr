using System.Runtime.Serialization;

namespace Seedr.Controller.Interface.Contracts;

[DataContract]
public record SetSeedRateCommand
{
    [DataMember(Order = 1)]
    public required double SeedRate { get; set; }
}
