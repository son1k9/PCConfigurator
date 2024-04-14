using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.Stores;
using PCConfigurator.ViewModel;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.Intrinsics.Arm;
using System.Windows;

namespace PCConfigurator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public void Fill()
    {
        ApplicationContext dbContext = new ApplicationContext();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        //Socket 
        Socket socket1 = new Socket() { Name = "AM4" };
        Socket socket2 = new Socket() { Name = "AM5" };
        Socket socket3 = new Socket() { Name = "AM3+" };
        Socket socket5 = new Socket() { Name = "LGA 1200" };

        //CPU

        Cpu cpu1 = new Cpu()
        {
            Model = "AMD Ryzen 5 5600",
            Cores = 6,
            ECores = 0,
            Smt = true,
            Tdp = 65,
            L2Cache = 8,
            L3Cache = 16,
            MaxRamCapacity = 64,
            CoreClock = 4.0f,
            BoostClock = 4.5f,
            HaveGraphics = false,
            Socket = socket1,
            RamTypes = RamType.DDR4
        };

        dbContext.Cpu.Add(cpu1);

        Cpu cpu2 = new Cpu() {
            Model = "AMD Ryzen 5 5600x",
            Cores = 6,
            ECores = 0,
            Smt = true,
            Tdp = 65,
            L2Cache = 8,
            L3Cache = 16,
            MaxRamCapacity = 64,
            CoreClock = 4.2f,
            BoostClock = 4.7f,
            HaveGraphics = false,
            Socket = socket1,
            RamTypes = RamType.DDR4
        };

    dbContext.Cpu.Add(cpu2);

        dbContext.Cpu.Add(new Cpu()
        {
            Model = "AMD Ryzen 3 1200",
            Cores = 4,
            ECores = 0,
            Smt = false,
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

        dbContext.Cpu.Add(new Cpu()
        {
            Model = "AMD Ryzen 5 1600",
            Cores = 6,
            ECores = 0,
            Smt = true,
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

        Cpu cpu3 = new Cpu()
        {
            Model = "AMD FX-6300",
            Cores = 8,
            ECores = 0,
            Smt = false,
            Tdp = 220,
            L2Cache = 8,
            L3Cache = 8,
            MaxRamCapacity = 32,
            CoreClock = 3.5f,
            BoostClock = 5.0f,
            HaveGraphics = false,
            Socket = socket3,
            RamTypes = RamType.DDR3
        };

        dbContext.Cpu.Add(cpu3);

        dbContext.Cpu.Add(new Cpu()
        {
            Model = "AMD Ryzen 7 7800X3D",
            Cores = 8,
            ECores = 0,
            Smt = true,
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

        dbContext.Cpu.Add(new Cpu()
        {
            Model = "Intel Core i5-10400F",
            Cores = 6,
            ECores = 0,
            Smt = true,
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


        //GPU

        Gpu gpu1 = new Gpu()
        {
            Model = "GIGABYTE GeForce RTX 4090 AERO OC",
            CoreClock = 2235,
            BoostClock = 2535,
            VramCapacity = 24,
            PowerConsumption = 450
        };

        dbContext.Gpu.Add(gpu1);

        dbContext.Gpu.Add(new Gpu()
        {
            Model = "MSI GeForce RTX 4070 VENTUS 3X E OC",
            CoreClock = 1920,
            BoostClock = 2520,
            VramCapacity = 12,
            PowerConsumption = 200
        });

        dbContext.Gpu.Add(new Gpu()
        {
            Model = "MSI GeForce RTX 4060 VENTUS 2X BLACK OC",
            CoreClock = 1830,
            BoostClock = 2505,
            VramCapacity = 8,
            PowerConsumption = 115
        });

        dbContext.Gpu.Add(new Gpu()
        {
            Model = "Sapphire AMD Radeon RX 7600 PULSE GAMING OC",
            CoreClock = 1720,
            BoostClock = 2755,
            VramCapacity = 8,
            PowerConsumption = 185
        });

        dbContext.Gpu.Add(new Gpu()
        {
            Model = "Sapphire AMD Radeon RX 6600 PULSE",
            CoreClock = 1626,
            BoostClock = 2491,
            VramCapacity = 8,
            PowerConsumption = 140
        });


        //Cooler
        Cooler cooler1 = new Cooler()
        {
            Model = "DEEPCOOL AG300",
            Tdp = 150
        };
        cooler1.AddSockets([socket1, socket2, socket5]);

        Cooler cooler2 = new Cooler()
        {
            Model = "DEEPCOOL GAMMAXX 400 EX",
            Tdp = 180
        };
        cooler2.AddSockets([socket1, socket2, socket5]);

        Cooler cooler3 = new Cooler()
        {
            Model = "ID-COOLING SE-903-XT",
            Tdp = 130
        };
        cooler3.AddSockets([socket1, socket2]);

        Cooler cooler4 = new Cooler()
        {
            Model = "IDEEPCOOL AG400",
            Tdp = 220
        };
        cooler4.AddSockets([socket3]);

        dbContext.Cooler.Add(cooler1);
        dbContext.Cooler.Add(cooler2);
        dbContext.Cooler.Add(cooler3);
        dbContext.Cooler.Add(cooler4);


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
            Name = "760G",
            Socket = socket3
        };

        dbContext.Chipset.Add(chipset1);
        dbContext.Chipset.Add(chipset2);
        dbContext.Chipset.Add(chipset3);
        dbContext.Chipset.Add(chipset4);
        dbContext.Chipset.Add(chipset5);


        //RAM

        Ram ram1 = new Ram()
        {
            Model = "Kingston FURY Beast Black",
            Capacity = 8,
            Clock = 3200,
            Cl = 16,
            RamType = RamType.DDR4
        };

        dbContext.Ram.Add(ram1);
        dbContext.Ram.Add(new Ram()
        {
            Model = "ADATA XPG Lancer Blade",
            Capacity = 16,
            Clock = 5600,
            Cl = 46,
            RamType = RamType.DDR5
        });

        Ram ram3 = new Ram()
        {
            Model = "Patriot Viper 3",
            Capacity = 8,
            Clock = 1600,
            Cl = 9,
            RamType = RamType.DDR3
        };
        dbContext.Ram.Add(ram3);


        //Power Supply

        PowerSupply powerSupply1 = new PowerSupply()
        {
            Model = "DEEPCOOL DQ750",
            Wattage = 750
        };

        dbContext.PowerSupply.Add(powerSupply1);

        dbContext.PowerSupply.Add(new PowerSupply()
        {
            Model = "Cougar STX 700W",
            Wattage = 700
        });

        dbContext.PowerSupply.Add(new PowerSupply()
        {
            Model = "Cougar GX 1050W",
            Wattage = 1050
        });


        //HDD
        dbContext.Hdd.Add(new Hdd()
        {
            Model = "WD Blue",
            Capacity = 1000,
            SpindelSpeed = 7200
        });

        dbContext.Hdd.Add(new Hdd()
        {
            Model = "Toshiba P300",
            Capacity = 1000,
            SpindelSpeed = 7200
        });

        dbContext.Hdd.Add(new Hdd()
        {
            Model = "Seagate BarraCuda",
            Capacity = 2000,
            SpindelSpeed = 7200
        });


        //SSD

        Ssd ssd1 = new Ssd()
        {
            Model = "Kingston A400",
            Capacity = 480,
            ReadSpeed = 500,
            WriteSpeed = 450,
            Tbw = 160,
            NandType = NandType.TLC
        };

        dbContext.Ssd.Add(ssd1);

        dbContext.Ssd.Add(new Ssd()
        {
            Model = "Samsung 870 EVO",
            Capacity = 1000,
            ReadSpeed = 560,
            WriteSpeed = 530,
            Tbw = 600,
            NandType = NandType.TLC
        });

        dbContext.Ssd.Add(new Ssd()
        {
            Model = "Samsung 870 QVO",
            Capacity = 1000,
            ReadSpeed = 560,
            WriteSpeed = 530,
            Tbw = 360,
            NandType = NandType.QLC
        });


        //M2 SSD

        M2Ssd m2ssd1 = new M2Ssd()
        {
            Model = "ARDOR GAMING Ally AL1288",
            Capacity = 1024,
            ReadSpeed = 3300,
            WriteSpeed = 3100,
            Tbw = 750,
            NandType = NandType.TLC,
            M2Interface = Model.Components.M2.M2Interface.Nvme | Model.Components.M2.M2Interface.Sata,
            M2Size = Model.Components.M2.M2Size._2280 | Model.Components.M2.M2Size._2260
        };

        dbContext.M2Ssd.Add(m2ssd1);

        dbContext.M2Ssd.Add(new M2Ssd()
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

        dbContext.M2Ssd.Add(new M2Ssd()
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

        //Motherboards
        Motherboard motherboard = new Motherboard()
        {
            Model = "ASUS PRIME A320M-K",
            RamSlots = 2,
            MaxRamCapacity = 64,
            Sata3Ports = 6,
            PCIex16Slots = 2,
            RamType = RamType.DDR4,
            Socket = socket1,
            Chipset = chipset3
        };

        ObservableCollection<M2Slot> motherboardSlots =
        [
            new M2Slot()
            {
                M2Interface = Model.Components.M2.M2Interface.Nvme | Model.Components.M2.M2Interface.Sata,
                M2Size = Model.Components.M2.M2Size._2280 | Model.Components.M2.M2Size._2260
            },
            new M2Slot()
            {
                M2Interface = Model.Components.M2.M2Interface.Sata,
                M2Size = Model.Components.M2.M2Size._22110
            },
        ];

        motherboard.M2Slots = motherboardSlots;
        dbContext.Add(motherboard);

        Motherboard motherboard1 = new Motherboard()
        {
            Model = "ASRock 760GM-HDV",
            RamSlots = 2,
            MaxRamCapacity = 64,
            Sata3Ports = 4,
            PCIex16Slots = 1,
            RamType = RamType.DDR3,
            Socket = socket3,
            Chipset = chipset5
        };

        dbContext.Add(motherboard1);

        //Configurations
        Configuration configuration1 = new Configuration()
        {
            Name = "Test Configuration1",
            Motherboard = motherboard,
            Cpu = cpu1,
            Cooler = cooler1,
            PowerSupply = powerSupply1,
            Gpus = [gpu1]
        };

        configuration1.ConfigurationRams.Add(new ConfigurationRam()
        {
            Configuration = configuration1,
            Ram = ram1
        });

        configuration1.ConfigurationRams.Add(new ConfigurationRam()
        {
            Configuration = configuration1,
            Ram = ram1
        });

        configuration1.ConfigurationM2Ssds.Add(new ConfigurationM2Ssd()
        {
            Configuration = configuration1,
            M2Ssd = m2ssd1,
            M2Slot = motherboardSlots[1]

        });

        configuration1.ConfigurationM2Ssds.Add(new ConfigurationM2Ssd()
        {
            Configuration = configuration1,
            M2Ssd = m2ssd1,
            M2Slot = motherboardSlots[0]

        });

        Configuration configuration2 = new Configuration()
        {
            Name = "Test Configuration2",
            Motherboard = motherboard1,
            Cpu = cpu3,
            Cooler = cooler4,
            PowerSupply = powerSupply1,
            Gpus = [gpu1],
            Ssds = [ssd1]
        };

        configuration2.ConfigurationRams = [new ConfigurationRam()
        {
            Configuration = configuration2,
            Ram = ram3
        },

        new ConfigurationRam()
        {
            Configuration = configuration2,
            Ram = ram3
        }];

        dbContext.Configuration.Add(configuration1);
        dbContext.Configuration.Add(configuration2);

        dbContext.SaveChanges();

    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        Fill();
        MainViewModel mainViewModel = new MainViewModel();
        MainWindow = new MainWindow() { DataContext = mainViewModel };
        MainWindow.Show();
    }
}
