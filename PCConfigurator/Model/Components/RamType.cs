namespace PCConfigurator.Model.Components;

[Flags]
public enum RamType
{
    DDR3 = 0b00001,
    DDR4 = 0b00010,
    DDR5 = 0b00100
}
