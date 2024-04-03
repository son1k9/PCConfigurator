namespace PCConfigurator.Model.Components;

public class PowerSupply
{
    public int PowerSupplyId { get; set; }

    public string Model { get; set; }

    public int Wattage { get; set; } = 200;

    public virtual List<Configuration> Configurations { get; set; } = [];


    public PowerSupply Clone()
    {
        return new PowerSupply
        {
            PowerSupplyId = PowerSupplyId,
            Model = Model,
            Wattage = Wattage,
            Configurations = Configurations
        };
    }
}
