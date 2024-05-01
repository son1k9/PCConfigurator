using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.Model.Components.M2;
using PCConfigurator.Helper;

namespace TestProject1
{

    public class ConfigurationTests
    {

        public Configuration CreateCorrectConfiguration()
        {
            var socket = new Socket() { Name = "AM4" };

            var cpu = new Cpu()
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
                Socket = socket,
                RamTypes = RamType.DDR4
            };

            var gpu = new Gpu()
            {
                Model = "GIGABYTE GeForce RTX 4090 AERO OC",
                CoreClock = 2235,
                BoostClock = 2535,
                VramCapacity = 24,
                PowerConsumption = 450
            };

            var cooler = new Cooler()
            {
                Model = "DEEPCOOL AG300",
                Tdp = 150,
                Sockets = [socket]
            };

            var chipset = new Chipset()
            {
                Name = "B650",
                Socket = socket
            };

            var ram = new Ram()
            {
                Model = "Kingston FURY Beast Black",
                Capacity = 8,
                Clock = 3200,
                Cl = 16,
                RamType = RamType.DDR4
            };

            var powerSupply = new PowerSupply()
            {
                Model = "DEEPCOOL DQ750",
                Wattage = 1000
            };

            var hdd = new Hdd()
            {
                Model = "WD Blue",
                Capacity = 1000,
                SpindelSpeed = 7200
            };

            var ssd = new Ssd()
            {
                Model = "Kingston A400",
                Capacity = 480,
                ReadSpeed = 500,
                WriteSpeed = 450,
                Tbw = 160,
                NandType = NandType.TLC
            };

            var m2ssd = new M2Ssd()
            {
                Model = "ARDOR GAMING Ally AL1288",
                Capacity = 1024,
                ReadSpeed = 3300,
                WriteSpeed = 3100,
                Tbw = 750,
                NandType = NandType.TLC,
                M2Interface = M2Interface.Nvme | M2Interface.Sata,
                M2Size = M2Size._2260
            };

            var m2ssd2 = new M2Ssd()
            {
                Model = "Samsung 980 PRO",
                Capacity = 1000,
                ReadSpeed = 7000,
                WriteSpeed = 5000,
                Tbw = 600,
                NandType = NandType.TLC,
                M2Interface = M2Interface.Nvme,
                M2Size = M2Size._2280
            };

            var motherboard = new Motherboard()
            {
                Model = "ASUS PRIME A320M-K",
                RamSlots = 2,
                MaxRamCapacity = 64,
                Sata3Ports = 6,
                PCIex16Slots = 2,
                RamType = RamType.DDR4,
                Chipset = chipset
            };

            List<M2Slot> motherboardSlots =
            [
                new M2Slot()
                    {
                        M2Interface = M2Interface.Nvme,
                        M2Size = M2Size._2280 | M2Size._2260
                    },
                new M2Slot()
                {
                    M2Interface = M2Interface.Nvme | M2Interface.Sata,
                    M2Size = M2Size._22110 | M2Size._2280
                }
            ];

            var configuration = new Configuration()
            {
                Name = "Test",
                Motherboard = motherboard,
                Cpu = cpu,
                Cooler = cooler,
                PowerSupply = powerSupply,
            };

            configuration.ConfigurationRams =
            [
                new ConfigurationRam
                {
                    Configuration = configuration,
                    Ram = ram
                }
            ];

            configuration.ConfigurationGpus =
            [
                new ConfigurationGpu
                {
                    Configuration = configuration,
                    Gpu = gpu
                }
            ];

            configuration.ConfigurationHdds =
            [
                new ConfigurationHdd
                {
                    Configuration = configuration,
                    Hdd = hdd
                }
            ];

            configuration.ConfigurationSsds =
            [
                new ConfigurationSsd
                {
                    Configuration = configuration,
                    Ssd = ssd
                }
            ];

            configuration.ConfigurationM2Ssds =
            [
                new ConfigurationM2Ssd
                {
                    Configuration = configuration,
                    M2Ssd = m2ssd,
                    M2Slot = motherboardSlots[0]
                },
                new ConfigurationM2Ssd
                {
                    Configuration = configuration,
                    M2Ssd = m2ssd2,
                    M2Slot = motherboardSlots[1]
                }
            ];

            return configuration;
        }

        [Fact]
        public void CheckCompatibilityTest1()
        {
            var configuration = CreateCorrectConfiguration();

            Assert.Empty(configuration.CheckCompatibility());
        }

        [Fact]
        public void CheckCompatibilityTest2()
        {
            var configuration = new Configuration()
            {
                Name = "Test"
            };

            Assert.Equal(7, configuration.CheckCompatibility().Length);
        }

        [Fact]
        public void CheckCompatibilityTest3() 
        {
            var socket = new Socket() { Name = "AM4" };

            var cpu = new Cpu()
            {
                Model = "AMD Ryzen 3 1200",
                Cores = 4,
                ECores = 0,
                Smt = false,
                Tdp = 200,
                L2Cache = 4,
                L3Cache = 8,
                MaxRamCapacity = 32,
                CoreClock = 3.0f,
                BoostClock = 3.4f,
                HaveGraphics = false,
                Socket = new Socket() { Name = "AM3"},
                RamTypes = RamType.DDR3
            };

            var gpu = new Gpu()
            {
                Model = "GIGABYTE GeForce RTX 4090 AERO OC",
                CoreClock = 2235,
                BoostClock = 2535,
                VramCapacity = 24,
                PowerConsumption = 450
            };

            var cooler = new Cooler()
            {
                Model = "DEEPCOOL AG300",
                Tdp = 150,
                Sockets = [new Socket { Name = "Test" }]
            };

            var chipset = new Chipset()
            {
                Name = "B650",
                Socket = socket
            };

            var ram = new Ram()
            {
                Model = "Kingston FURY Beast Black",
                Capacity = 128,
                Clock = 3200,
                Cl = 16,
                RamType = RamType.DDR3
            };

            var powerSupply = new PowerSupply()
            {
                Model = "DEEPCOOL DQ750",
                Wattage = 200
            };

            var hdd = new Hdd()
            {
                Model = "WD Blue",
                Capacity = 1000,
                SpindelSpeed = 7200
            };

            var ssd = new Ssd()
            {
                Model = "Kingston A400",
                Capacity = 480,
                ReadSpeed = 500,
                WriteSpeed = 450,
                Tbw = 160,
                NandType = NandType.TLC
            };

            var m2ssd = new M2Ssd()
            {
                Model = "ARDOR GAMING Ally AL1288",
                Capacity = 1024,
                ReadSpeed = 3300,
                WriteSpeed = 3100,
                Tbw = 750,
                NandType = NandType.TLC,
                M2Interface = M2Interface.Sata,
                M2Size = M2Size._2260
            };

            var m2ssd2 = new M2Ssd()
            {
                Model = "Samsung 980 PRO",
                Capacity = 1000,
                ReadSpeed = 7000,
                WriteSpeed = 5000,
                Tbw = 600,
                NandType = NandType.TLC,
                M2Interface = M2Interface.Nvme,
                M2Size = M2Size._2280
            };

            var motherboard = new Motherboard()
            {
                Model = "ASUS PRIME A320M-K",
                RamSlots = 2,
                MaxRamCapacity = 64,
                Sata3Ports = 6,
                PCIex16Slots = 2,
                RamType = RamType.DDR4,
                Chipset = chipset
            };

            List<M2Slot> motherboardSlots =
            [
                new M2Slot()
                    {
                        M2Interface = M2Interface.Nvme,
                        M2Size = M2Size._2280 | M2Size._2260
                    },
                new M2Slot()
                {
                    M2Interface = M2Interface.Nvme | M2Interface.Sata,
                    M2Size = M2Size._22110 | M2Size._2260
                }
            ];

            var configuration = new Configuration()
            {
                Name = "Test",
                Motherboard = motherboard,
                Cpu = cpu,
                Cooler = cooler,
                PowerSupply = powerSupply,
            };

            configuration.ConfigurationRams =
            [
                new ConfigurationRam
                {
                    Configuration = configuration,
                    Ram = ram
                }
            ];

            configuration.ConfigurationGpus =
            [
                new ConfigurationGpu
                {
                    Configuration = configuration,
                    Gpu = gpu
                }
            ];

            configuration.ConfigurationHdds =
            [
                new ConfigurationHdd
                {
                    Configuration = configuration,
                    Hdd = hdd
                }
            ];

            configuration.ConfigurationSsds =
            [
                new ConfigurationSsd
                {
                    Configuration = configuration,
                    Ssd = ssd
                }
            ];

            configuration.ConfigurationM2Ssds =
            [
                new ConfigurationM2Ssd
                {
                    Configuration = configuration,
                    M2Ssd = m2ssd,
                    M2Slot = motherboardSlots[0]
                },
                new ConfigurationM2Ssd
                {
                    Configuration = configuration,
                    M2Ssd = m2ssd2,
                    M2Slot = motherboardSlots[1]
                }
            ];


            Assert.Equal(10, configuration.CheckCompatibility().Length);
        }

        [Fact]
        public void ExportTest1()
        {
            var configuration = CreateCorrectConfiguration();
            Directory.CreateDirectory("Test");
            Assert.True(ConfigurationIE.Export("Test/v.json", configuration));
            Assert.True(File.Exists("Test/v.json"));
            File.Delete("Test/v.json");
            Directory.Delete("Test");
        }

        [Fact]
        public void ExportTest2()
        {
            var configuration = new Configuration() { Name = "Test" };
            Directory.CreateDirectory("Test");
            Assert.False(ConfigurationIE.Export("Test/v.json", configuration));
            var fileExists = File.Exists("Test/v.json");
            Assert.False(fileExists);
            if (fileExists)
            {
                File.Delete("Test/v.json");
            }
            Directory.Delete("Test");
        }
    }
}