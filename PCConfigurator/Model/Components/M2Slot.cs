#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class M2Slot
    {
        public int M2SlotId { get; set; }
        public M2.M2Interface M2Interface { get; set; }
        public string Sizes { get; set; }
        public int SlotNumber { get; set; }

        public M2.M2Size M2Size { get; set; }

        public int MotherboardId { get; set; }
        public Motherboard Motherboard { get; set; }
    }
}
