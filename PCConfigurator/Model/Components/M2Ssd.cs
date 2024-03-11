#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class M2Ssd
    {
        public int M2SsdId { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public int ReadSpeed { get; set; }
        public int WriteSpeed { get; set; }
        public int Tbw { get; set; }
        public NandType NandType { get; set; }
        public int Size { get; set; }
        public M2.M2Interface M2Interface { get; set; }
        public M2.M2Size M2Size { get; set; }

        public List<Configuration> Configurations { get; set; } = [];
    }
}
