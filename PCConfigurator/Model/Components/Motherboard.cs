using System.Collections.ObjectModel;

#nullable disable

namespace PCConfigurator.Model.Components
{
    public class Motherboard
    {
        public int MotherboardId { get; set; }

        public string Model { get; set; }

        public int SocketId { get; set; }
        public virtual Socket Socket { get; set; }

        public int ChipsetId { get; set; }
        public virtual Chipset Chipset { get; set; }

        public RamType RamType { get; set; }

        public int RamSlots { get; set; }

        public int MaxRamCapacity { get; set; }

        public virtual ICollection<M2Slot> M2Slots { get; set; } = new ObservableCollection<M2Slot>();

        public int Sata3Ports { get; set; }

        public int PCIex16Slots { get; set; }

        public virtual List<Configuration> Configurations { get; } = [];
    }
}
