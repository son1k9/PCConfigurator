using Microsoft.EntityFrameworkCore.ChangeTracking;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.Stores;
using PCConfigurator.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace PCConfigurator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {
        Fill();
        NavigationStorage navigationStorage = new NavigationStorage();
        MainViewModel mainViewModel = new MainViewModel(navigationStorage);
        MainWindow = new MainWindow() { DataContext = mainViewModel };
        MainWindow.Show();
    }

    public void Fill()
    {
        ApplicationContext dbContext = new ApplicationContext();

        //Socket
        Socket socket1 = new Socket() { Name = "AM4" };
        Socket socket2 = new Socket() { Name = "AM5" };
        Socket socket3 = new Socket() { Name = "AM3+" };
        Socket socket5 = new Socket() { Name = "LGA 1200" };


        //CPU
        dbContext.Cpus.Add(new Cpu()
        {
            Model = "AMD Ryzen 5 5600",
            Cores = 6,
            ECores = 0,
            Treads = 12,
            Tdp = 65,
            L2Cache = 8,
            L3Cache = 16,
            MaxRamCapacity = 64,
            CoreClock = 4.0f,
            BoostClock = 4.5f,
            HaveGraphics = false,
            Socket = socket1,
            RamTypes = RamType.DDR4 | RamType.DDR5
        });

        dbContext.Cpus.Add(new Cpu()
        {
            Model = "AMD Ryzen 5 5600x",
            Cores = 6,
            ECores = 0,
            Treads = 12,
            Tdp = 65,
            L2Cache = 8,
            L3Cache = 16,
            MaxRamCapacity = 64,
            CoreClock = 4.2f,
            BoostClock = 4.7f,
            HaveGraphics = false,
            Socket = socket1,
            RamTypes = RamType.DDR4 | RamType.DDR5
        });

        dbContext.Cpus.Add(new Cpu()
        {
            Model = "AMD Ryzen 3 1200",
            Cores = 4,
            ECores = 0,
            Treads = 4,
            Tdp = 65,
            L2Cache = 4,
            L3Cache = 8,
            MaxRamCapacity = 32,
            CoreClock = 3.0f,
            BoostClock = 3.4f,
            HaveGraphics = false,
            Socket = socket1,
            RamTypes = RamType.DDR4
        });

        dbContext.Cpus.Add(new Cpu()
        {
            Model = "AMD Ryzen 5 1600",
            Cores = 6,
            ECores = 0,
            Treads = 12,
            Tdp = 65,
            L2Cache = 4,
            L3Cache = 12,
            MaxRamCapacity = 32,
            CoreClock = 3.2f,
            BoostClock = 3.6f,
            HaveGraphics = false,
            Socket = socket1,
            RamTypes = RamType.DDR4
        });

        dbContext.Cpus.Add(new Cpu()
        {
            Model = "AMD FX-6300",
            Cores = 8,
            ECores = 0,
            Treads = 8,
            Tdp = 220,
            L2Cache = 8,
            L3Cache = 8,
            MaxRamCapacity = 32,
            CoreClock = 3.5f,
            BoostClock = 5.0f,
            HaveGraphics = false,
            Socket = socket3,
            RamTypes = RamType.DDR3
        });

        dbContext.Cpus.Add(new Cpu()
        {
            Model = "AMD Ryzen 7 7800X3D",
            Cores = 8,
            ECores = 0,
            Treads = 16,
            Tdp = 120,
            L2Cache = 8,
            L3Cache = 96,
            MaxRamCapacity = 128,
            CoreClock = 4.2f,
            BoostClock = 5.0f,
            HaveGraphics = true,
            Socket = socket2,
            RamTypes = RamType.DDR5
        });

        dbContext.Cpus.Add(new Cpu()
        {
            Model = "Intel Core i5-10400F",
            Cores = 6,
            ECores = 0,
            Treads = 12,
            Tdp = 65,
            L2Cache = 1.5f,
            L3Cache = 12,
            MaxRamCapacity = 128,
            CoreClock = 2.9f,
            BoostClock = 4.3f,
            HaveGraphics = true,
            Socket = socket5,
            RamTypes = RamType.DDR4
        });


        ///GPU
        dbContext.Gpus.Add(new Gpu()
        {
            Model = "GIGABYTE GeForce RTX 4090 AERO OC",
            CoreClock = 2235,
            BoostClock = 2535,
            VramCapacity = 24,
            VramClock = 21000,
            PowerConsumption = 450
        });

        dbContext.Gpus.Add(new Gpu()
        {
            Model = "MSI GeForce RTX 4070 VENTUS 3X E OC",
            CoreClock = 1920,
            BoostClock = 2520,
            VramCapacity = 12,
            VramClock = 21000,
            PowerConsumption = 200
        });

        dbContext.Gpus.Add(new Gpu()
        {
            Model = "MSI GeForce RTX 4060 VENTUS 2X BLACK OC",
            CoreClock = 1830,
            BoostClock = 2505,
            VramCapacity = 8,
            VramClock = 17000,
            PowerConsumption = 115
        });

        dbContext.Gpus.Add(new Gpu()
        {
            Model = "Sapphire AMD Radeon RX 7600 PULSE GAMING OC",
            CoreClock = 1720,
            BoostClock = 2755,
            VramCapacity = 8,
            VramClock = 18000,
            PowerConsumption = 185
        });

        dbContext.Gpus.Add(new Gpu()
        {
            Model = "Sapphire AMD Radeon RX 6600 PULSE",
            CoreClock = 1626,
            BoostClock = 2491,
            VramCapacity = 8,
            VramClock = 14000,
            PowerConsumption = 140
        });


        //Cooler
        Cooler cooler1 = new Cooler()
        {
            Model = "DEEPCOOL AG300",
            Tdp = 150,
            MinRpm = 500,
            MaxRpm = 3050,
            Size = 92
        };
        cooler1.Sockets.ToList().AddRange([socket1, socket2, socket5]);

        Cooler cooler2 = new Cooler()
        {
            Model = "DEEPCOOL GAMMAXX 400 EX",
            Tdp = 180,
            MinRpm = 500,
            MaxRpm = 1500,
            Size = 120
        };
        cooler2.Sockets.ToList().AddRange([socket1, socket2, socket5]);

        Cooler cooler3 = new Cooler()
        {
            Model = "ID-COOLING SE-903-XT",
            Tdp = 130,
            MinRpm = 500,
            MaxRpm = 2200,
            Size = 92   
        };
        cooler3.Sockets.ToList().AddRange([socket1, socket2]);

        Cooler cooler4 = new Cooler()
        {
            Model = "IDEEPCOOL AG400",
            Tdp = 220,
            MinRpm = 500,
            MaxRpm = 2000,
            Size = 120
        };
        cooler4.Sockets.ToList().AddRange([socket3]);

        dbContext.Coolers.Add(cooler1);
        dbContext.Coolers.Add(cooler2);
        dbContext.Coolers.Add(cooler3);
        dbContext.Coolers.Add(cooler4);


        //Chipset
        Chipset chipset1 = new Chipset()
        {
            Name = "B650",
            Socket = socket2
        };

        Chipset chipset2 = new Chipset()
        {
            Name = "X670",
            Socket = socket2
        };

        Chipset chipset3 = new Chipset()
        {
            Name = "A320",
            Socket = socket1
        };

        Chipset chipset4 = new Chipset()
        {
            Name = "Z590",
            Socket = socket5
        };

        Chipset chipset5 = new Chipset()
        {
            Name = "980G",
            Socket = socket5
        };

        dbContext.Chipsets.Add(chipset1);
        dbContext.Chipsets.Add(chipset2);
        dbContext.Chipsets.Add(chipset3);
        dbContext.Chipsets.Add(chipset4);
        dbContext.Chipsets.Add(chipset5);


        //RAM
        dbContext.Rams.Add(new Ram()
        {
            Model = "Kingston FURY Beast Black",
            Capacity = 8,
            Clock = 3200,
            Cl = 16,
            RamType = RamType.DDR4
        });
        dbContext.Rams.Add(new Ram()
        {
            Model = "ADATA XPG Lancer Blade",
            Capacity = 16,
            Clock = 5600,
            Cl = 46,
            RamType = RamType.DDR5
        });
        dbContext.Rams.Add(new Ram()
        {
            Model = "Patriot Viper 3",
            Capacity = 8,
            Clock = 1600,
            Cl = 9,
            RamType = RamType.DDR3
        });


        //Power Supply
        dbContext.PowerSupplys.Add(new PowerSupply()
        {
            Model = "DEEPCOOL DQ750",
            Wattage = 750
        });

        dbContext.PowerSupplys.Add(new PowerSupply()
        {
            Model = "Cougar STX 700W",
            Wattage = 700
        });

        dbContext.PowerSupplys.Add(new PowerSupply()
        {
            Model = "Cougar GX 1050W",
            Wattage = 1050
        });


        //HDD
        dbContext.Hdds.Add(new Hdd()
        {
            Model = "WD Blue",
            Capacity = 1,
            SpindelSpeed = 7200
        });

        dbContext.Hdds.Add(new Hdd()
        {
            Model = "Toshiba P300",
            Capacity = 1,
            SpindelSpeed = 7200
        });

        dbContext.Hdds.Add(new Hdd()
        {
            Model = "Seagate BarraCuda",
            Capacity = 2,
            SpindelSpeed = 7200
        });


        //SSD
        dbContext.Ssds.Add(new Ssd()
        {
            Model = "Kingston A400",
            Capacity = 480,
            ReadSpeed = 500,
            WriteSpeed = 450,
            Tbw = 160,
            NandType = NandType.TLC
        });

        dbContext.Ssds.Add(new Ssd()
        {
            Model = "Samsung 870 EVO",
            Capacity = 1000,
            ReadSpeed = 560,
            WriteSpeed = 530,
            Tbw = 600,
            NandType = NandType.TLC
        });

        dbContext.Ssds.Add(new Ssd()
        {
            Model = "Samsung 870 QVO",
            Capacity = 1000,
            ReadSpeed = 560,
            WriteSpeed = 530,
            Tbw = 360,
            NandType = NandType.QLC
        });


        //M2 SSD
        dbContext.M2Ssds.Add(new M2Ssd()
        {
            Model = "ARDOR GAMING Ally AL1288",
            Capacity = 1024,
            ReadSpeed = 3300,
            WriteSpeed = 3100,
            Tbw = 750,
            NandType = NandType.TLC,
            M2Interface = Model.Components.M2.M2Interface.Nvme,
            M2Size = Model.Components.M2.M2Size._2280
        });

        dbContext.M2Ssds.Add(new M2Ssd()
        {
            Model = "Samsung 980 PRO",
            Capacity = 1000,
            ReadSpeed = 7000,
            WriteSpeed = 5000,
            Tbw = 600,
            NandType = NandType.TLC,
            M2Interface = Model.Components.M2.M2Interface.Nvme,
            M2Size = Model.Components.M2.M2Size._2280
        });

        dbContext.M2Ssds.Add(new M2Ssd()
        {
            Model = "Kingston NV2",
            Capacity = 500,
            ReadSpeed = 3500,
            WriteSpeed = 2100,
            Tbw = 160,
            NandType = NandType.TLC,
            M2Interface = Model.Components.M2.M2Interface.Nvme,
            M2Size = Model.Components.M2.M2Size._2280
        });

        dbContext.SaveChanges();

        //Motherboards
        Motherboard motherboard = new Motherboard()
        {
            Model = "ASUS PRIME A320M-K",
            RamSlots = 2,
            MaxRamCapacity = 64,
            Sata3Ports = 4,
            PCIex16Slots = 1,
            RamType = RamType.DDR4,
            Socket = socket1,
            Chipset = Chipset.FromString("A320")
        };

        dbContext.Add(motherboard);

        dbContext.SaveChanges();
    }
}
