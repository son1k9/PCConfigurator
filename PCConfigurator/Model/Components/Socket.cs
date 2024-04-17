using System.Collections.ObjectModel;

namespace PCConfigurator.Model.Components;

public class Socket()
{
    public int SocketId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Chipset> Chipsets { get; set; } = new ObservableCollection<Chipset>();

    public virtual List<Cooler> Coolers { get; set; } = [];

    public virtual List<Cpu> Cpus { get; set; } = [];

    public virtual List<Motherboard> Motherboards { get; set; } = [];


    public override string ToString()
    {
        return Name;
    }
}
