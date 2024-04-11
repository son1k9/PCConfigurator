using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Model.Components;

public class Cpu : Component
{
    public int CpuId { get; set; }

    public override string Model { get; set; }

    public int SocketId { get; set; }
    public virtual Socket Socket { get; set; }

    public int Cores { get; set; } = 2;

    public int ECores { get; set; }

    public bool Smt { get; set; }

    public float CoreClock { get; set; } = 1.0f;

    public float BoostClock { get; set; } = 1.0f;

    public float L2Cache { get; set; } = 2;

    public float L3Cache { get; set; } = 2;

    public int Tdp { get; set; } = 35;

    private RamType _ramTypes;
    public RamType RamTypes { get => _ramTypes; set => _ramTypes = value; }

    public int MaxRamCapacity { get; set; } = 4;

    public bool HaveGraphics { get; set; }

    public virtual List<Configuration> Configurations { get; set; } = [];


    public Cpu Clone()
    {
        return new Cpu()
        {
            CpuId = CpuId,
            Model = Model,
            Socket = Socket,
            Cores = Cores,
            ECores = ECores,
            Smt = Smt,
            CoreClock = CoreClock,
            BoostClock = BoostClock,
            L2Cache = L2Cache,
            L3Cache = L3Cache,
            Tdp = Tdp,
            RamTypes = RamTypes,
            MaxRamCapacity = MaxRamCapacity,
            HaveGraphics = HaveGraphics,
            SocketId = SocketId,
            Configurations = Configurations
        };
    }

    [NotMapped]
    public bool IsDDR3
    {
        get => _ramTypes.HasFlag(RamType.DDR3);
        set => _ramTypes.SetRamType(RamType.DDR3, value);
    }

    [NotMapped]
    public bool IsDDR4
    {
        get => _ramTypes.HasFlag(RamType.DDR4);
        set => _ramTypes.SetRamType(RamType.DDR4, value);
    }

    [NotMapped]
    public bool IsDDR5
    {
        get => _ramTypes.HasFlag(RamType.DDR5);
        set => _ramTypes.SetRamType(RamType.DDR5, value);
    }
}
