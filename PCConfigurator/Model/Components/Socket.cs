#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Socket
    {
        public int SocketId { get; set; }
        public string Name { get; set; }

        public List<Chipset> Chipsets { get; set; } = [];
        public List<Cooler> Coolers { get; set; } = [];
        public List<Cpu> Cpus { get; set; } = [];
        public List<Motherboard> Motherboards { get; set; } = [];
    }
}
