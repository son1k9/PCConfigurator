using PCConfigurator.Model.Components.M2;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Model.Components;

public class M2Slot
{
    public int M2SlotId { get; set; }
    public M2.M2Interface M2Interface { get; set; }
    public M2.M2Size M2Size { get; set; }

    public int MotherboardId { get; set; }
    public virtual Motherboard Motherboard { get; set; }

    public virtual List<ConfigurationM2Ssd> ConfigurationM2Ssds { get; set; } = [];

    public override string ToString()
    {
        string result = M2Interface.ToString() + " " + M2Size.ToString();
        const string value = "_";
        int index = result.IndexOf(value);
        while (index != -1)
        {
            result = result.Remove(index, 1);
            index = result.IndexOf(value);
        }
        return result;
    }

    public M2Slot Clone()
    {
        M2Slot m2Slot = new M2Slot()
        {
            M2SlotId = M2SlotId,
            M2Interface = M2Interface,
            M2Size = M2Size,
            MotherboardId = MotherboardId,
            Motherboard = Motherboard,
            ConfigurationM2Ssds = ConfigurationM2Ssds
        };
        return m2Slot;
    }


    [NotMapped]
    public bool IsNvme
    {
        get => M2Interface.HasFlag(M2Interface.Nvme);
        set => SetInterface(M2Interface.Nvme, value);
    }

    [NotMapped]
    public bool IsSata
    {
        get => M2Interface.HasFlag(M2Interface.Sata);
        set => SetInterface(M2Interface.Sata, value);
    }

    public void SetInterface(M2Interface m2Interface, bool value)
    {
        if (value)
            M2Interface |= m2Interface;
        else if ((M2Interface & ~m2Interface) != 0)
            M2Interface &= ~m2Interface;
    }


    [NotMapped]
    public bool Is2230
    {
        get => M2Size.HasFlag(M2Size._2230);
        set => SetSize(M2Size._2230, value);
    }

    [NotMapped]
    public bool Is2242
    {
        get => M2Size.HasFlag(M2Size._2242);
        set => SetSize(M2Size._2242, value);
    }

    [NotMapped]
    public bool Is2260
    {
        get => M2Size.HasFlag(M2Size._2260);
        set => SetSize(M2Size._2260, value);
    }

    [NotMapped]
    public bool Is2280
    {
        get => M2Size.HasFlag(M2Size._2280);
        set => SetSize(M2Size._2280, value);
    }

    [NotMapped]
    public bool Is22110
    {
        get => M2Size.HasFlag(M2Size._22110);
        set => SetSize(M2Size._22110, value);
    }


    public void SetSize(M2Size size, bool value)
    {
        if (value)
            M2Size |= size;
        else if ((M2Size & ~size) != 0)
            M2Size &= ~size;
    }
}
