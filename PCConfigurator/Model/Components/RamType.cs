namespace PCConfigurator.Model.Components
{
    [Flags]
    public enum RamType
    {
        DDR  = 0b00001,
        DDR2 = 0b00010,
        DDR3 = 0b00100,
        DDR4 = 0b01000,
        DDR5 = 0b10000
    }
}
