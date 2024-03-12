#nullable disable

namespace PCConfigurator.Model.Components
{
    internal class Socket()
    {
        public int SocketId { get; set; }
        public required string Name { get; set; }

        public List<Chipset> Chipsets { get; } = [];
        public List<Cooler> Coolers { get; } = [];
        public List<Cpu> Cpus { get; } = [];
        public List<Motherboard> Motherboards { get; } = [];

        public static Socket FromString(string s) 
        {
            ApplicationContext dbContext = new ApplicationContext();
            try
            {
                Socket socket = dbContext.Sockets.First(x => x.Name == s);
                return socket;
            }
            catch (InvalidOperationException)
            {
                return new Socket() { Name = s };
            }
        }
    }
}
