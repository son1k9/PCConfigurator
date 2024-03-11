#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class PowerSupply
    {
        public int PowerSupplyId { get; set; }
        public string Model { get; set; }
        public int Wattage { get; set; }

        public List<Configuration> Configurations { get; set; } = [];
    }
}
