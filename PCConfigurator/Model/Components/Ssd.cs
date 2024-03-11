#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Ssd
    {
        public int SsdId { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public int ReadSpeed { get; set; }
        public int WriteSpeed { get; set; }
        public int Tbw { get; set; }
        public NandType NandType { get; set; }

        public List<Configuration> Configurations { get; set; } = [];
    }
}
