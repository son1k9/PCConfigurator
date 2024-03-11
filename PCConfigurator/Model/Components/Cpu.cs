using System.Collections.ObjectModel;

#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Cpu
    {
        public int CpuId { get; set; }
        public string Model { get; set; }
        public int Cores { get; set; }
        public int ECores { get; set; }
        public int Treads { get; set; }
        public int L2Cache { get; set; }
        public int L3Cache { get; set; }
        public int Tdp { get; set; }
        public int MaxRamCapacity { get; set; }
        public int CoreClock { get; set; }
        public int BoostClock { get; set; }
        public bool HaveGraphics { get; set; }

        public int SocketId { get; set; }
        public Socket Socket { get; set; }

        public ICollection<RamType> RamTypes { get; set; } = new ObservableCollection<RamType>();
        public List<Configuration> Configurations { get; set; } = [];
    }
}
