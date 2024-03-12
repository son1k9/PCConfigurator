using System.Collections.ObjectModel;

namespace PCConfigurator.Model.Components;

internal class Cooler
{
    public int CoolerId { get; set; }
    public required string Model { get; set; }
    public required int Tdp { get; set; }
    public required int MinRpm { get; set; }
    public required int MaxRpm { get; set; }
    public required int Size { get; set; }

    public ICollection<Socket> Sockets { get; } = new ObservableCollection<Socket>();
    public List<Configuration> Configurations { get; } = [];
}
