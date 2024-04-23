using ReactiveUI;
using Seedr.Controller.Interface.Contracts;

namespace Seedr.Monitor.ViewModels;

public class SeederSettingsViewModel(SeederSettings settings) : ReactiveObject
{
    public int FanSpeed { get; set; } = settings.FanSpeed;

    public double SeedRate { get; set; } = settings.SeedRate;
}
