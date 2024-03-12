using System.Collections.ObjectModel;

#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Motherboard
    {
        public int MotherboardId { get; set; }
        public required string Model { get; set; }
        public required int RamSlots { get; set; }
        public required int MaxRamCapacity { get; set; }
        public required int Sata3Ports { get; set; }
        public required int PCIex16Slots { get; set; }
        public required RamType RamType { get; set; }

        public ICollection<M2Slot> M2Slots { get; } = new ObservableCollection<M2Slot>();
        public List<Configuration> Configurations { get; } = [];

        public int SocketId { get; set; }
        public required Socket Socket { get; set; }
        public int ChipsetId { get; set; }
        public required Chipset Chipset { get; set; }
    }
}
