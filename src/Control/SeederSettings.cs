namespace Seedr.Control;

public record SeederSettings
{
    /// <summary>
    /// The speed of the implement in kilometers per hour.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// The target seed rate in kilograms per hectare.
    /// </summary>
    public double SeedRate { get; set; }
}
