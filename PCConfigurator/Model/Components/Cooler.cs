using System.Collections.ObjectModel;

namespace PCConfigurator.Model.Components;

public class Cooler : Component
{
    public int CoolerId { get; set; }

    public int Tdp { get; set; } = 35;

    public virtual ICollection<Socket> Sockets { get; set; } = new ObservableCollection<Socket>();

    public virtual List<Configuration> Configurations { get; set; } = [];
}
