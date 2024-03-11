using System.Collections.ObjectModel;

#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Motherboard
    {
        public int MotherboardId { get; set; }
        public string Model { get; set; }
        public int RamSlots { get; set; }
        public int MaxRamCapacity { get; set; }
        public int Sata3Ports { get; set; }
        public int PCIex16Slots { get; set; }
        public RamType RamType { get; set; }

        public ICollection<M2Slot> M2Slots { get; set; } = new ObservableCollection<M2Slot>();
        public List<Configuration> Configurations { get; set; } = [];

        public int SocketId { get; set; }
        public Socket Socket { get; set; }
        public int ChipsetId { get; set; }
        public Chipset Chipset { get; set; }
    }
}
