namespace PCConfigurator.Model.Components;

#nullable disable

internal class M2Slot
{
    public int M2SlotId { get; set; }
    public required M2.M2Interface M2Interface { get; set; }
    public required int SlotNumber { get; set; }
    public M2.M2Size M2Size { get; set; }

    public int MotherboardId { get; set; }
    public Motherboard Motherboard { get; set; }
}
