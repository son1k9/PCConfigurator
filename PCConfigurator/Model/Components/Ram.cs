#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Ram
    {
        public int RamID { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public int Clock { get; set; }
        public int Cl { get; set; }

        public int RamTypeId { get; set; }
        public RamType RamType { get; set; }

        public List<Configuration> Configurations { get; set; } = [];
    }
}
