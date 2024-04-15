using System.Runtime.Serialization;

namespace Seedr.Shared.Contracts;

[DataContract]
public record SeederSettings
{
    /// <summary>
    /// The speed of the air seeder in revolutions per minute (RPM)
    /// </summary>
    [DataMember(Order = 1)]
    public int FanSpeed { get; set; }

    /// <summary>
    /// The target seed rate in kilograms per hectare (kg/ha).
    /// </summary>
    [DataMember(Order = 2)]
    public double SeedRate { get; set; }
}
