using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PCConfigurator.Helper
{
    /// <summary>
    /// Class to import and export pc configurations.
    /// </summary>
    /*!
    * \section faq FAQ
    * 
    * \subsection q1 What are possible file formats for import and export of configuration? 
    * 
    * File name can have any extension, but at the moment only json format is supported.
    * 
    * \subsection q2 How can I export incompatible configuration?
    * 
    * Method for export was written around compatible configuration so theres is no garantee 
    * that it will work with incompatible configurations.
    * So there is no way to export incompatible configuration.
    * 
    */
    /// <remarks>
    /// Подробное руководство [Руководство 1](../guides/guide1.html)
    /// </remarks>
    public static class ConfigurationIE
    {
        /// <summary>
        /// Prepares configuration for export.
        /// </summary>
        /// <param name="configuration">Configuration to prepare</param>
        /// <returns>Configuration prepared for export</returns>
        private static Configuration PrepareConfigurationForExport(Configuration configuration)
        {
            Cooler cooler = new Cooler()
            {
                Model = configuration.Cooler!.Model,
                Tdp = configuration.Cooler.Tdp,
            };

            Socket socket = new Socket()
            {
                Name = configuration.Motherboard!.Chipset.Socket.Name,
            };

            Cpu cpu = new Cpu()
            {
                Model = configuration.Cpu!.Model,
                Socket = socket,
                Cores = configuration.Cpu.Cores,
                ECores = configuration.Cpu.ECores,
                Smt = configuration.Cpu.Smt,
                CoreClock = configuration.Cpu.CoreClock,
                BoostClock = configuration.Cpu.BoostClock,
                L2Cache = configuration.Cpu.L2Cache,
                L3Cache = configuration.Cpu.L3Cache,
                Tdp = configuration.Cpu.Tdp,
                RamTypes = configuration.Cpu.RamTypes,
                MaxRamCapacity = configuration.Cpu.MaxRamCapacity,
                HaveGraphics = configuration.Cpu.HaveGraphics
            };

            Chipset chipset = new Chipset()
            {
                Name = configuration.Motherboard.Chipset.Name,
                Socket = socket
            };

            Motherboard motherboard = new Motherboard()
            {
                Model = configuration.Motherboard.Model,
                Chipset = chipset,
                RamType = configuration.Motherboard.RamType,
                RamSlots = configuration.Motherboard.RamSlots,
                MaxRamCapacity = configuration.Motherboard.MaxRamCapacity,
                Sata3Ports = configuration.Motherboard.Sata3Ports,
                PCIex16Slots = configuration.Motherboard.PCIex16Slots
            };

            List<Gpu> gpus = [];
            foreach (var gpu in configuration.ConfigurationGpus)
            {
                gpus.Add(new Gpu()
                {
                    Model = gpu.Gpu.Model,
                    CoreClock = gpu.Gpu.CoreClock,
                    BoostClock = gpu.Gpu.BoostClock,
                    VramCapacity = gpu.Gpu.VramCapacity,
                    PowerConsumption = gpu.Gpu.PowerConsumption
                });
            }

            List<Hdd> hdds = [];
            foreach (var hdd in configuration.ConfigurationHdds)
            {
                hdds.Add(new Hdd()
                {
                    Model = hdd.Hdd.Model,
                    Capacity = hdd.Hdd.Capacity,
                    SpindelSpeed = hdd.Hdd.SpindelSpeed
                });
            }

            List<ConfigurationM2Ssd> m2ssds = [];
            foreach (var slot in configuration.Motherboard.M2Slots)
            {
                ConfigurationM2Ssd? m2Ssd = configuration.ConfigurationM2Ssds.FirstOrDefault(c => c.M2SlotId == slot.M2SlotId);
                M2Ssd? m2Ssd1 = null;
                if (m2Ssd != null)
                {
                    m2Ssd1 = new M2Ssd()
                    {
                        Model = m2Ssd.M2Ssd.Model,
                        Capacity = m2Ssd.M2Ssd.Capacity,
                        ReadSpeed = m2Ssd.M2Ssd.ReadSpeed,
                        WriteSpeed = m2Ssd.M2Ssd.WriteSpeed,
                        Tbw = m2Ssd.M2Ssd.Tbw,
                        NandType = m2Ssd.M2Ssd.NandType,
                        M2Interface = m2Ssd.M2Ssd.M2Interface,
                        M2Size = m2Ssd.M2Ssd.M2Size
                    };
                }
                m2ssds.Add(new ConfigurationM2Ssd()
                {
                    M2Slot = new M2Slot()
                    {
                        M2Interface = slot.M2Interface,
                        M2Size = slot.M2Size
                    },
                    M2Ssd = m2Ssd1
                });
            }

            PowerSupply powerSupply = new PowerSupply()
            {
                Model = configuration.PowerSupply!.Model,
                Wattage = configuration.PowerSupply.Wattage
            };

            List<Ram> rams = [];
            foreach (var ram in configuration.ConfigurationRams)
            {
                rams.Add(new Ram()
                {
                    Model = ram.Ram.Model,
                    RamType = ram.Ram.RamType,
                    Capacity = ram.Ram.Capacity,
                    Clock = ram.Ram.Clock,
                    Cl = ram.Ram.Cl
                });
            }

            List<Ssd> ssds = [];
            foreach (var ssd in configuration.ConfigurationSsds)
            {
                ssds.Add(new Ssd()
                {
                    Model = ssd.Ssd.Model,
                    Capacity = ssd.Ssd.Capacity,
                    ReadSpeed = ssd.Ssd.ReadSpeed,
                    WriteSpeed = ssd.Ssd.WriteSpeed,
                    Tbw = ssd.Ssd.Tbw,
                    NandType = ssd.Ssd.NandType
                });
            }

            Configuration configuration1 = new Configuration()
            {
                Name = configuration.Name,
                Motherboard = motherboard,
                Cpu = cpu,
                Cooler = cooler,
                PowerSupply = powerSupply,
                Rams = rams,
                Gpus = gpus,
                Ssds = ssds,
                Hdds = hdds,
                ConfigurationM2Ssds = m2ssds
            };

            return configuration1;
        }

        /// <summary>
        /// Export pc configuration to a file. If file does not exist creates it.
        /// </summary>
        /// <param name="path">Path to file to export in.</param>
        /// <param name="configuration">Configuration to export.</param>
        /// <returns>True if succesfull, False if configuration is not compatible.</returns>
        /// <example>
        /// Usage example:
        /// <code>
        /// var configuration = new Configuration();
        /// // Add components to a configuration here
        /// var path = "conf.json";
        /// if (ConfigurationIE.Export(path, configuration))
        ///     Console.WriteLine($"Configuration {configuration.Name} exported successfuly.");
        /// else
        ///     Console.WriteLine($"Error while exporting configuration {configuration.Name}.");
        /// </code>
        /// </example>
        public static bool Export(string path, Configuration configuration)
        {
            if (configuration.CheckCompatibility().Length > 0)
                return false;

            Configuration configuration1 = PrepareConfigurationForExport(configuration);
            string jsonString = JsonConvert.SerializeObject(configuration1, Formatting.Indented);
            using StreamWriter file = File.CreateText(path);
            file.Write(jsonString);
            return true;
        }

        /// <summary>
        /// Constructs pc configuration with components from database. If components dont exist they are created.
        /// </summary>
        /// <param name="configuration">Configuration to construct from</param>
        /// <param name="dbContext">Database context</param>
        /// <returns>Constructed configuration if succesfull, null otherwise.</returns>
        private static Configuration? ConstructConfigurationFromImport(Configuration configuration, ApplicationContext dbContext)
        {
            Configuration configuration1 = new Configuration() { Name = configuration.Name, };
            Socket? socket = dbContext.Socket.FirstOrDefault(s => s.Name == configuration.Motherboard.Chipset.Socket.Name);
            if (socket == null)
                socket = configuration.Motherboard.Chipset.Socket;

            Chipset? chipset = dbContext.Chipset.FirstOrDefault(c => c.Name == configuration.Motherboard.Chipset.Name);
            if (chipset == null)
            {
                chipset = configuration.Motherboard.Chipset;
                chipset.Socket = socket;
            }

            Cpu? cpu = dbContext.Cpu.FirstOrDefault(c => c.Model == configuration.Cpu.Model);
            if (cpu == null)
            {
                cpu = configuration.Cpu;
                cpu.Socket = socket;
            }

            PowerSupply? powerSupply = dbContext.PowerSupply.FirstOrDefault(p => p.Model == configuration.PowerSupply.Model);
            if (powerSupply == null)
                powerSupply = configuration.PowerSupply;

            Cooler? cooler = dbContext.Cooler.FirstOrDefault(c => c.Model == configuration.Cooler.Model);
            if (cooler == null)
            {
                cooler = configuration.Cooler;
                cooler.Sockets = [socket];
            }

            List<ConfigurationRam> rams = [];
            foreach (var ram in configuration.Rams)
            {
                Ram? ramFromDb = dbContext.Ram.FirstOrDefault(r => r.Model == ram.Model);
                if (ramFromDb == null)
                {
                    var ram1 = rams.FirstOrDefault(r => r.Ram.Model == ram.Model);
                    if (ram1 == null)
                        ramFromDb = ram;
                    else
                        ramFromDb = ram1.Ram;
                }
                rams.Add(new ConfigurationRam() { Configuration = configuration1, Ram = ramFromDb });
            }

            List<ConfigurationGpu> gpus = [];
            foreach (var gpu in configuration.Gpus)
            {
                Gpu? gpuFromDb = dbContext.Gpu.FirstOrDefault(g => g.Model == gpu.Model);
                if (gpuFromDb == null)
                    gpuFromDb = gpu;
                gpus.Add(new ConfigurationGpu() { Configuration = configuration1, Gpu = gpuFromDb });
            }

            List<ConfigurationHdd> hdds = [];
            foreach (var hdd in configuration.Hdds)
            {
                Hdd? hddFromDb = dbContext.Hdd.FirstOrDefault(h => h.Model == hdd.Model);
                if (hddFromDb == null)
                    hddFromDb = hdd;
                hdds.Add(new ConfigurationHdd() { Configuration = configuration1, Hdd = hddFromDb });
            }

            List<ConfigurationSsd> ssds = [];
            foreach (var ssd in configuration.Ssds)
            {
                Ssd? ssdFromDb = dbContext.Ssd.FirstOrDefault(s => s.Model == ssd.Model);
                if (ssdFromDb == null)
                    ssdFromDb = ssd;
                ssds.Add(new ConfigurationSsd() { Configuration = configuration1, Ssd = ssdFromDb });
            }

            List<ConfigurationM2Ssd> m2Ssds = [];
            Motherboard? motherboard = dbContext.Motherboard.FirstOrDefault(m => m.Model == configuration.Motherboard.Model);
            if (motherboard == null)
            {
                motherboard = configuration.Motherboard;
                motherboard.Chipset = chipset;

                List<M2Slot> m2Slots = [];
                foreach (var m2Ssd in configuration.ConfigurationM2Ssds)
                {
                    m2Slots.Add(m2Ssd.M2Slot);
                    if (m2Ssd.M2Ssd is null)
                        continue;

                    M2Ssd? m2SsdFromDb = dbContext.M2Ssd.FirstOrDefault(m => m.Model == m2Ssd.M2Ssd.Model);
                    if (m2SsdFromDb == null)
                        m2SsdFromDb = m2Ssd.M2Ssd;

                    m2Ssds.Add(new ConfigurationM2Ssd() { Configuration = configuration1, M2Slot = m2Ssd.M2Slot, M2Ssd = m2SsdFromDb });
                }

                motherboard.M2Slots = m2Slots;
            }
            else
            {
                HashSet<M2Slot> m2Slots = [];
                foreach (var m2Ssd in configuration.ConfigurationM2Ssds)
                {
                    if (m2Ssd.M2Ssd is null)
                        continue;

                    M2Ssd? m2SsdFromDb = dbContext.M2Ssd.FirstOrDefault(m => m.Model == m2Ssd.M2Ssd.Model);
                    if (m2SsdFromDb == null)
                        m2SsdFromDb = m2Ssd.M2Ssd;
                    M2Slot? m2Slot = motherboard.M2Slots.Except(m2Slots).FirstOrDefault(m => m.M2Interface == m2Ssd.M2Slot.M2Interface && m.M2Size == m2Ssd.M2Slot.M2Size);
                    if (m2Slot == null)
                    {
                        break;
                    }
                    m2Slots.Add(m2Slot);
                    m2Ssds.Add(new ConfigurationM2Ssd() { Configuration = configuration1, M2Slot = m2Slot, M2Ssd = m2SsdFromDb });
                }
            }

            configuration1.Cpu = cpu;
            configuration1.Cooler = cooler;
            configuration1.Motherboard = motherboard;
            configuration1.PowerSupply = powerSupply;
            configuration1.ConfigurationRams = rams;
            configuration1.ConfigurationGpus = gpus;
            configuration1.ConfigurationSsds = ssds;
            configuration1.ConfigurationHdds = hdds;
            configuration1.ConfigurationM2Ssds = m2Ssds;


            var errors = configuration1.CheckCompatibility();
            if (errors.Length > 0)
                return null;
            else
                return configuration1;
        }

        /// <summary>
        /// Imports pc configuration from file.
        /// </summary>
        /// <param name="path">Path to a file</param>
        /// <param name="dbContext">Database context to use for constraction of configuration</param>
        /// <returns>Imported configuration</returns>
        /*!
        * \section faq2 FAQ
        * 
        * \subsection q3 Why do i need to pass a dbContext to import configuration?
        * 
        * When configuration is imported it is constructed with components 
        * that are already exist in db instead of creating new once, 
        * this prevents duplicate components in db.
        * 
        */
        public static Configuration? Import(string path, ApplicationContext dbContext)
        {
            string jsonString = File.ReadAllText(path);
            Configuration? configuration = JsonConvert.DeserializeObject<Configuration>(jsonString);
            if (configuration != null)
            {
                try
                {
                    Configuration? configuration1 = ConstructConfigurationFromImport(configuration, dbContext);
                    return configuration1;
                }
                catch
                {
                    Console.WriteLine(1);
                    return null;
                }
            }
            return null;
        }
    }
}

/*!
 * \page faq_page FAQ
 *
 *  There can be some text here.
 * 
 */