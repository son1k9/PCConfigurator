using System.Collections.ObjectModel;

#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Cooler
    {
        public int CoolerId { get; set; }
        public string Model { get; set; }
        public int Tdp { get; set; }
        public int Rpm { get; set; }

        public ICollection<Socket> Sockets { get; set; } = new ObservableCollection<Socket>();
        public List<Configuration> Configurations { get; set; } = [];
    }
}
