#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Hdd
    {
        public int HddId { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public int SpindelSpeed { get; set; }

        public List<Configuration> Configurations { get; set; } = [];
    }
}
