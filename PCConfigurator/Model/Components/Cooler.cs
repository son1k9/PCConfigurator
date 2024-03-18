using System.Collections.ObjectModel;

namespace PCConfigurator.Model.Components;

public class Cooler
{
    public int CoolerId { get; set; }
    public string Model { get; set; }
    public int Tdp { get; set; } = 35;

    public virtual ICollection<Socket> Sockets { get; set; } = new ObservableCollection<Socket>();
    public virtual List<Configuration> Configurations { get; set; } = [];

    public void AddSockets(params Socket[] sockets)
    {
        foreach (var socket in sockets)
        {
            Sockets.Add(socket);
        }
    }

    public Cooler Clone()
    {
        ObservableCollection<Socket> sockets = [];
        foreach (Socket socket in Sockets)
        {
            sockets.Add(socket);
        }

        return new Cooler()
        {
            CoolerId = CoolerId,
            Model = Model,
            Tdp = Tdp,
            Sockets = sockets,
            Configurations = Configurations
        };
    }
}
