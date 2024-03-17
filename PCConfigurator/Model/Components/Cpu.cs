﻿

using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Model.Components;

public class Cpu
{
    public int CpuId { get; set; }
    public string Model { get; set; }
    public virtual Socket Socket { get; set; }
    public int Cores { get; set; } = 2;
    public int ECores { get; set; }
    public bool Smt { get; set; }
    public float CoreClock { get; set; } = 1.0f;
    public float BoostClock { get; set; } = 1.0f;
    public float L2Cache { get; set; } = 2;
    public float L3Cache { get; set; } = 2;
    public int Tdp { get; set; } = 35;
    public RamType RamTypes { get; set; }
    public int MaxRamCapacity { get; set; } = 4;
    public bool HaveGraphics { get; set; }
    public int SocketId { get; set; }

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
    public bool IsDDR
    {
        get => RamTypes.HasFlag(RamType.DDR);
        set => SetRamType(RamType.DDR, value);
    }

    [NotMapped]
    public bool IsDDR2
    {
        get => RamTypes.HasFlag(RamType.DDR2);
        set => SetRamType(RamType.DDR2, value);
    }

    [NotMapped]
    public bool IsDDR3
    {
        get => RamTypes.HasFlag(RamType.DDR3);
        set => SetRamType(RamType.DDR3, value);
    }

    [NotMapped]
    public bool IsDDR4
    {
        get => RamTypes.HasFlag(RamType.DDR4);
        set => SetRamType(RamType.DDR4, value);
    }

    [NotMapped]
    public bool IsDDR5
    {
        get => RamTypes.HasFlag(RamType.DDR5);
        set => SetRamType(RamType.DDR5, value);
    }

    public void SetRamType(RamType type, bool value)
    {
        if (value)
            RamTypes |= type;
        else if ((RamTypes & ~type) !=0 )
            RamTypes &= ~type;
    }
}
