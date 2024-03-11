#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Chipset
    {
        public int ChipsetId { get; set; }
        public string Name { get; set; }

        public int SocketId { get; set; }
        public Socket Socket { get; set; }

        public List<Motherboard> Motherboards { get; set; } = [];
    }
}
