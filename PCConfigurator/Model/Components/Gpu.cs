#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Gpu
    {
        public int GpuId { get; set; }
        public string Model { get; set; }
        public int CoreClock { get; set; }
        public int BoostClock { get; set; }
        public int VramCapacity { get; set; }
        public int VramClock { get; set; }
        public int Tdp { get; set; }

        public List<Configuration> Configurations { get; set; } = [];
    }
}
