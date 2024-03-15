namespace PCConfigurator.Model.Components;

#nullable disable

public class M2Slot
{
    public int M2SlotId { get; set; }
    public required M2.M2Interface M2Interface { get; set; }
    public required int SlotNumber { get; set; }
    public required M2.M2Size M2Size { get; set; }

    public int MotherboardId { get; set; }
    public virtual Motherboard Motherboard { get; set; }

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
            SlotNumber = SlotNumber,
            M2Size = M2Size,
            MotherboardId = MotherboardId,
            Motherboard = Motherboard
        };
        return m2Slot;
    }
}
