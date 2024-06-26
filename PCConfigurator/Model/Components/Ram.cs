﻿namespace PCConfigurator.Model.Components;

public class Ram : Component
{
    public int RamID { get; set; }

    public RamType RamType { get; set; }

    public int Capacity { get; set; } = 1;

    public int Clock { get; set; } = 800;

    public int Cl { get; set; } = 5;

    public virtual List<Configuration> Configurations { get; set; } = [];
    public virtual List<ConfigurationRam> ConfigurationRams { get; set; } = [];
}
