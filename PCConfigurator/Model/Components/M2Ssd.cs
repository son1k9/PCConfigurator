using PCConfigurator.Model.Components.M2;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Model.Components;

public class M2Ssd : Component
{
    public int M2SsdId { get; set; }

    public int Capacity { get; set; } = 60;

    public int ReadSpeed { get; set; } = 350;

    public int WriteSpeed { get; set; } = 350;

    public int Tbw { get; set; } = 30;

    public NandType NandType { get; set; }

    private M2Interface _m2Interface = M2Interface.Nvme;
    public M2Interface M2Interface { get => _m2Interface; set => _m2Interface = value; }

    private M2Size _m2Size = M2Size._2260;
    public M2Size M2Size { get => _m2Size; set => _m2Size = value; }

    public virtual List<ConfigurationM2Ssd> ConfigurationM2Ssds { get; set; } = [];


    [NotMapped]
    public bool IsNvme
    {
        get => _m2Interface.HasFlag(M2Interface.Nvme);
        set => _m2Interface.SetInterface(M2Interface.Nvme, value);
    }

    [NotMapped]
    public bool IsSata
    {
        get => _m2Interface.HasFlag(M2Interface.Sata);
        set => _m2Interface.SetInterface(M2Interface.Sata, value);
    }



    [NotMapped]
    public bool Is2230
    {
        get => _m2Size.HasFlag(M2Size._2230);
        set => _m2Size.SetSize(M2Size._2230, value);
    }

    [NotMapped]
    public bool Is2242
    {
        get => _m2Size.HasFlag(M2Size._2242);
        set => _m2Size.SetSize(M2Size._2242, value);
    }

    [NotMapped]
    public bool Is2260
    {
        get => _m2Size.HasFlag(M2Size._2260);
        set => _m2Size.SetSize(M2Size._2260, value);
    }

    [NotMapped]
    public bool Is2280
    {
        get => _m2Size.HasFlag(M2Size._2280);
        set => _m2Size.SetSize(M2Size._2280, value);
    }

    [NotMapped]
    public bool Is22110
    {
        get => _m2Size.HasFlag(M2Size._22110);
        set => _m2Size.SetSize(M2Size._22110, value);
    }
}
