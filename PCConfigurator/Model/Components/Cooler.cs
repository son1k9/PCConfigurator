using System.Collections.ObjectModel;

namespace PCConfigurator.Model.Components;

public class Cooler
{
    public int CoolerId { get; set; }
    public required string Model { get; set; }
    public required int Tdp { get; set; }
    public required int MinRpm { get; set; }
    public required int MaxRpm { get; set; }
    public required int Size { get; set; }

    public virtual ICollection<Socket> Sockets { get; } = new ObservableCollection<Socket>();
    public virtual List<Configuration> Configurations { get; } = [];

    public void AddSockets(params Socket[] sockets)
    {
        foreach (var socket in sockets) 
        {
            Sockets.Add(socket);
        }
    }
}
