namespace PCConfigurator.Model.Components;

public class Configuration
{
    public int ConfigurationId { get; set; }

    public string Name { get; set; } = "";

    public int? MotherboardId { get; set; }
    public virtual Motherboard? Motherboard { get; set; }

    public int? CpuId { get; set; }
    public virtual Cpu? Cpu { get; set; }

    public int? CoolerId { get; set; }
    public virtual Cooler? Cooler { get; set; }

    public int? PowerSupplyId { get; set; }
    public virtual PowerSupply? PowerSupply { get; set; }

    public virtual List<Ram> Rams { get; set; } = [];
    public virtual List<ConfigurationRam> ConfigurationRams { get; set; } = [];

    public virtual List<Gpu> Gpus { get; set; } = [];
    public virtual List<ConfigurationGpu> ConfigurationGpus { get; set; } = [];

    public virtual List<Ssd> Ssds { get; set; } = [];
    public virtual List<ConfigurationSsd> ConfigurationSsds { get; set; } = [];

    public virtual List<Hdd> Hdds { get; set; } = [];
    public virtual List<ConfigurationHdd> ConfigurationHdds { get; set; } = [];

    public virtual List<ConfigurationM2Ssd> ConfigurationM2Ssds { get; set; } = [];

    public string[] CheckCompatibility()
    {
        List<string> result = [];

        if (Motherboard is null)
            result.Add("Отсутствует материнская плата.");

        if (Cpu is null)
            result.Add("Отсутствует процессор.");

        if (Cooler is null)
            result.Add("Отсутствует кулер.");

        if (PowerSupply is null)
            result.Add("Отсутствует блок питания.");

        if (ConfigurationRams.Count < 1)
            result.Add("Отсутствует оперативная память.");

        if (ConfigurationGpus.Count < 1 && !(Cpu?.HaveGraphics == true))
            result.Add("Отсутствует видеокарта и процессор не имеет встроенной графики.");

        if (ConfigurationSsds.Count < 1 && ConfigurationHdds.Count < 1 && ConfigurationM2Ssds.Count < 1)
            result.Add("Отсутствует устройство хранения данных.");

        int powerConsumption = 0;

        if (Motherboard is not null)
        {
            powerConsumption += 35;

            foreach (ConfigurationRam ram in ConfigurationRams)
            {
                switch (ram.Ram.RamType)
                {
                    case RamType.DDR3:
                        powerConsumption += 9;
                        break;

                    case RamType.DDR4:
                        powerConsumption += 6;
                        break;

                    case RamType.DDR5:
                        powerConsumption += 5;
                        break;

                    default:
                        break;
                }

                
                if (Motherboard.RamType != ram.Ram.RamType)
                    result.Add($"Тип оперативной памяти {ram.Ram.Model} отличается от типа оперативной памяти материнской платы.");
            }

            int ramCapacitySum = 0;
            foreach (ConfigurationRam ram in ConfigurationRams)
                ramCapacitySum += ram.Ram.Capacity;

            if (ramCapacitySum > Motherboard.MaxRamCapacity)
                result.Add("Максимальный объем оперативной памяти материнской платы меньше, чем сумарный объём всех ОЗУ конфигурации.");

            foreach(ConfigurationM2Ssd m2Ssd in ConfigurationM2Ssds)
            {
                powerConsumption += 20;

                if (!m2Ssd.M2Slot.M2Size.HasFlag(m2Ssd.M2Ssd.M2Size))
                    result.Add($"Размер M2 SSD {m2Ssd.M2Ssd.Model}, неподдерживается слотом, в который он установлен.");

                if ((m2Ssd.M2Slot.M2Interface & m2Ssd.M2Ssd.M2Interface) == 0)
                    result.Add($"Интерфейс работы M2 SSD {m2Ssd.M2Ssd.Model}, неподдерживается слотом, в который он установлен.");
            }

            if (Cpu is not null)
            {
                powerConsumption += Cpu.Tdp;

                if (Motherboard.Socket != Cpu.Socket)
                    result.Add("Сокет процессора отличается от сокета материнской платы.");

                if (!Cpu.RamTypes.HasFlag(Motherboard.RamType))
                    result.Add("Тип оперативной памяти процесора отличается от типа оперативной памяти материнской платы.");

                if (ramCapacitySum > Cpu.MaxRamCapacity)
                    result.Add("Максимальный объем оперативной памяти процессора меньше, чем сумарный объём всех ОЗУ конфигурации.");

                if (Cooler is not null)
                    if (Cooler.Tdp < Cpu.Tdp)
                       result.Add("TDP кулера меньше, чем TDP процессора.");
            }

            if (Cooler is not null)
            {
                if (!(Cooler?.Sockets.Any(socket => socket == Motherboard?.Socket) == true))
                    result.Add("Сокет кулера отличается от сокета материнской платы.");

                powerConsumption += 5;
            }
        }

        powerConsumption = (int)(powerConsumption * 1.35);

        foreach (ConfigurationGpu gpu in ConfigurationGpus)
            powerConsumption += gpu.Gpu.PowerConsumption;
        foreach (ConfigurationHdd hdd in ConfigurationHdds)
            powerConsumption += 12;
        foreach (ConfigurationSsd ssd in ConfigurationSsds)
            powerConsumption += 3;

        if (PowerSupply?.Wattage < powerConsumption)
            result.Add($"Мощность блока питания ({PowerSupply.Wattage}W) меньше, чем энергопотребление конфигурации ({powerConsumption}W).");

        return [.. result];
    }
}
