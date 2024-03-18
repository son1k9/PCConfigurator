using System.Collections.ObjectModel;

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

        public int RamSlots { get; set; } = 2;

        public int MaxRamCapacity { get; set; } = 4;

        public virtual ICollection<M2Slot> M2Slots { get; set; } = new ObservableCollection<M2Slot>();

        public int Sata3Ports { get; set; } = 1;

        public int PCIex16Slots { get; set; } = 1;

        public virtual List<Configuration> Configurations { get; set; } = [];

        public Motherboard()
        {
        }

        public Motherboard Clone()
        {
            ObservableCollection<M2Slot> m2Slots = [];
            foreach (M2Slot slot in M2Slots)
            {
                m2Slots.Add(slot.Clone());
            }

            Motherboard motherboard = new Motherboard
            {
                MotherboardId = MotherboardId,
                Model = Model,
                SocketId = SocketId,
                Socket = Socket,
                ChipsetId = ChipsetId,
                Chipset = Chipset,
                RamType = RamType,
                RamSlots = RamSlots,
                MaxRamCapacity = MaxRamCapacity,
                M2Slots = m2Slots,
                Sata3Ports = Sata3Ports,
                PCIex16Slots = PCIex16Slots,
                Configurations = Configurations
            };
            return motherboard;
        }
    }
}
