using ReactiveUI;
using Seedr.Shared.Contracts;

namespace Seedr.Monitor;

public class SeederSettingsViewModel(SeederSettings settings) : ReactiveObject
{
    public int FanSpeed { get; set; } = settings.FanSpeed;

    public double SeedRate { get; set; } = settings.SeedRate;
}
