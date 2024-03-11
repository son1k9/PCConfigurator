#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Configuration
    {
        public int ConfigurationId { get; set; }

        public int MotherboardId { get; set; }
        public Motherboard Motherboard { get; set; }
        public int CpuId { get; set; }
        public Cpu Cpu { get; set; }
        public int CoolerId { get; set; }
        public Cooler Cooler { get; set; }
        public int PowerSupplyId { get; set; }
        public PowerSupply PowerSupply { get; set; }

        public List<Ram> Rams { get; set; } = [];
        public List<Gpu> Gpus { get; set; } = [];
        public List<Ssd> Ssds { get; set; } = [];
        public List<Hdd> Hdds { get; set; } = [];
        public List<M2Ssd> M2Ssds { get; set; } = [];
    }
}
