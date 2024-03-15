#nullable disable

namespace PCConfigurator.Model.Components
{
    public class Socket()
    {
        public int SocketId { get; set; }
        public required string Name { get; set; }

        public virtual List<Chipset> Chipsets { get; } = [];
        public virtual List<Cooler> Coolers { get; } = [];
        public virtual List<Cpu> Cpus { get; } = [];
        public virtual List<Motherboard> Motherboards { get; } = [];

        public static Socket FromString(string s) 
        {
            ApplicationContext dbContext = new ApplicationContext();
            try
            {
                Socket socket = dbContext.Socket.First(x => x.Name == s);
                return socket;
            }
            catch (InvalidOperationException)
            {
                return new Socket() { Name = s };
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
