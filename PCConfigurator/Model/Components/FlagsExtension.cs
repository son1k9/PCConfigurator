using PCConfigurator.Model.Components.M2;

namespace PCConfigurator.Model.Components;

public static class FlagsExtension
{
    public static void SetInterface(this ref M2Interface self, M2Interface m2Interface, bool value)
    {
        if (value)
            self |= m2Interface;
        else if ((self & ~m2Interface) != 0)
            self &= ~m2Interface;
    }

    public static void SetSize(this ref M2Size self, M2Size size, bool value)
    {
        if (value)
            self |= size;
        else if ((self & ~size) != 0)
            self &= ~size;
    }

    public static void SetRamType(this ref RamType self, RamType type, bool value)
    {
        if (value)
            self |= type;
        else if ((self & ~type) != 0)
            self &= ~type;
    }
}
