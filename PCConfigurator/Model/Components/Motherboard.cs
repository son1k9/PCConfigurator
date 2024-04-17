using System.Collections.ObjectModel;

namespace PCConfigurator.Model.Components;

public class Motherboard : Component
{
    public int MotherboardId { get; set; }

    public int SocketId { get; set; }
    public virtual Socket Socket { get; set; }

    public int ChipsetId { get; set; }
    public virtual Chipset Chipset { get; set; }

    public RamType RamType { get; set; }

    public int RamSlots { get; set; } = 2;

    public int MaxRamCapacity { get; set; } = 4;

    public virtual ICollection<M2Slot> M2Slots { get; set; } = new ObservableCollection<M2Slot>();

    public int Sata3Ports { get; set; } = 1;

    public int PCIex16Slots { get; set; } = 1;

    public virtual List<Configuration> Configurations { get; set; } = [];
}
