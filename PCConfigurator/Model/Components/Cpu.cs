namespace PCConfigurator.Model.Components;

internal class Cpu
{
    public int CpuId { get; set; }
    public required string Model { get; set; }
    public required int Cores { get; set; }
    public required int ECores { get; set; }
    public required int Treads { get; set; }
    public required float L2Cache { get; set; }
    public required float L3Cache { get; set; }
    public required int Tdp { get; set; }
    public required int MaxRamCapacity { get; set; }
    public required float CoreClock { get; set; }
    public required float BoostClock { get; set; }
    public required bool HaveGraphics { get; set; }
    public required RamType RamTypes { get; set; }

    public int SocketId { get; set; }
    public required Socket Socket { get; set; }

    //public Cpu(string model, int cores, int ecores, int threads, int l2Cache, int l3Cache, 
    //    int tdp, int maxRamCapacity, int coreClock, int boostClock, bool haveGraphics, Socket socket, params RamType[] ramTypes)
    //{
    //    Model = model;
    //    Cores = cores;
    //    ECores = ecores;
    //    Treads = threads;
    //    L2Cache = l2Cache;
    //    L3Cache = l3Cache;
    //    Tdp = tdp;
    //    MaxRamCapacity = maxRamCapacity;
    //    CoreClock = coreClock;
    //    BoostClock = boostClock;
    //    HaveGraphics = haveGraphics;
    //    Socket = socket;

    //    foreach (RamType ramType in ramTypes) 
    //    {
    //        RamTypes.Add(ramType);
    //    }
    //}

    //public Cpu(string model, int cores, int ecores, int threads, int l2Cache, int l3Cache,
    //    int tdp, int maxRamCapacity, int coreClock, int boostClock, bool haveGraphics, string socket, params RamType[] ramTypes)
    //{
    //    ApplicationContext dbContext = new ApplicationContext();
    //    try
    //    {
    //        Socket _socket = dbContext.Sockets.First(s => s.Name == socket);
    //        Socket = _socket;
    //    }
    //    catch (InvalidOperationException) 
    //    {
    //        Socket = new Socket(socket);
    //    }
    //    Model = model;
    //    Cores = cores;
    //    ECores = ecores;
    //    Treads = threads;
    //    L2Cache = l2Cache;
    //    L3Cache = l3Cache;
    //    Tdp = tdp;
    //    MaxRamCapacity = maxRamCapacity;
    //    CoreClock = coreClock;
    //    BoostClock = boostClock;
    //    HaveGraphics = haveGraphics;
    //    RamTypes = ramTypes;
    //}

    public List<Configuration> Configurations { get; } = [];
}
