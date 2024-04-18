﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PCConfigurator.Model;

#nullable disable

namespace PCConfigurator.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240418213458_MotherboardRemovedSocket")]
    partial class MotherboardRemovedSocket
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("CoolerSocket", b =>
                {
                    b.Property<int>("CoolersCoolerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SocketsSocketId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CoolersCoolerId", "SocketsSocketId");

                    b.HasIndex("SocketsSocketId");

                    b.ToTable("CoolerSocket");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Chipset", b =>
                {
                    b.Property<int>("ChipsetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SocketId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ChipsetId");

                    b.HasIndex("SocketId");

                    b.ToTable("Chipset");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Configuration", b =>
                {
                    b.Property<int>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CoolerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CpuId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MotherboardId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PowerSupplyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConfigurationId");

                    b.HasIndex("CoolerId");

                    b.HasIndex("CpuId");

                    b.HasIndex("MotherboardId");

                    b.HasIndex("PowerSupplyId");

                    b.ToTable("Configuration");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationGpu", b =>
                {
                    b.Property<int>("ConfigurationGpuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigurationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GpuId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConfigurationGpuId");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("GpuId");

                    b.ToTable("ConfigurationGpu");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationHdd", b =>
                {
                    b.Property<int>("ConfigurationHddId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigurationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HddId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConfigurationHddId");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("HddId");

                    b.ToTable("ConfigurationHdd");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationM2Ssd", b =>
                {
                    b.Property<int>("M2SlotId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigurationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("M2SsdId")
                        .HasColumnType("INTEGER");

                    b.HasKey("M2SlotId", "ConfigurationId");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("M2SsdId");

                    b.ToTable("ConfigurationM2Ssd");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationRam", b =>
                {
                    b.Property<int>("ConfigurationRamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigurationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConfigurationRamId");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("RamId");

                    b.ToTable("ConfigurationRam");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationSsd", b =>
                {
                    b.Property<int>("ConfigurationSsdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigurationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SsdId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConfigurationSsdId");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("SsdId");

                    b.ToTable("ConfigurationSsd");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Cooler", b =>
                {
                    b.Property<int>("CoolerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Tdp")
                        .HasColumnType("INTEGER");

                    b.HasKey("CoolerId");

                    b.ToTable("Cooler");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Cpu", b =>
                {
                    b.Property<int>("CpuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("BoostClock")
                        .HasColumnType("REAL");

                    b.Property<float>("CoreClock")
                        .HasColumnType("REAL");

                    b.Property<int>("Cores")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ECores")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HaveGraphics")
                        .HasColumnType("INTEGER");

                    b.Property<int>("L2Cache")
                        .HasColumnType("INTEGER");

                    b.Property<int>("L3Cache")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxRamCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RamTypes")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Smt")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SocketId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tdp")
                        .HasColumnType("INTEGER");

                    b.HasKey("CpuId");

                    b.HasIndex("SocketId");

                    b.ToTable("Cpu");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Gpu", b =>
                {
                    b.Property<int>("GpuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoostClock")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoreClock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PowerConsumption")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VramCapacity")
                        .HasColumnType("INTEGER");

                    b.HasKey("GpuId");

                    b.ToTable("Gpu");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Hdd", b =>
                {
                    b.Property<int>("HddId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SpindelSpeed")
                        .HasColumnType("INTEGER");

                    b.HasKey("HddId");

                    b.ToTable("Hdd");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.M2Slot", b =>
                {
                    b.Property<int>("M2SlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("M2Interface")
                        .HasColumnType("INTEGER");

                    b.Property<int>("M2Size")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MotherboardId")
                        .HasColumnType("INTEGER");

                    b.HasKey("M2SlotId");

                    b.HasIndex("MotherboardId");

                    b.ToTable("M2Slot");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.M2Ssd", b =>
                {
                    b.Property<int>("M2SsdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("M2Interface")
                        .HasColumnType("INTEGER");

                    b.Property<int>("M2Size")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NandType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReadSpeed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tbw")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WriteSpeed")
                        .HasColumnType("INTEGER");

                    b.HasKey("M2SsdId");

                    b.ToTable("M2Ssd");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Motherboard", b =>
                {
                    b.Property<int>("MotherboardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChipsetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxRamCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PCIex16Slots")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RamSlots")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RamType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sata3Ports")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SocketId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MotherboardId");

                    b.HasIndex("ChipsetId");

                    b.HasIndex("SocketId");

                    b.ToTable("Motherboard");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.PowerSupply", b =>
                {
                    b.Property<int>("PowerSupplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Wattage")
                        .HasColumnType("INTEGER");

                    b.HasKey("PowerSupplyId");

                    b.ToTable("PowerSupply");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Ram", b =>
                {
                    b.Property<int>("RamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cl")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Clock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RamType")
                        .HasColumnType("INTEGER");

                    b.HasKey("RamID");

                    b.ToTable("Ram");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Socket", b =>
                {
                    b.Property<int>("SocketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SocketId");

                    b.ToTable("Socket");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Ssd", b =>
                {
                    b.Property<int>("SsdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NandType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReadSpeed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tbw")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WriteSpeed")
                        .HasColumnType("INTEGER");

                    b.HasKey("SsdId");

                    b.ToTable("Ssd");
                });

            modelBuilder.Entity("CoolerSocket", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Cooler", null)
                        .WithMany()
                        .HasForeignKey("CoolersCoolerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCConfigurator.Model.Components.Socket", null)
                        .WithMany()
                        .HasForeignKey("SocketsSocketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Chipset", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Socket", "Socket")
                        .WithMany("Chipsets")
                        .HasForeignKey("SocketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Socket");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Configuration", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Cooler", "Cooler")
                        .WithMany("Configurations")
                        .HasForeignKey("CoolerId");

                    b.HasOne("PCConfigurator.Model.Components.Cpu", "Cpu")
                        .WithMany("Configurations")
                        .HasForeignKey("CpuId");

                    b.HasOne("PCConfigurator.Model.Components.Motherboard", "Motherboard")
                        .WithMany("Configurations")
                        .HasForeignKey("MotherboardId");

                    b.HasOne("PCConfigurator.Model.Components.PowerSupply", "PowerSupply")
                        .WithMany("Configurations")
                        .HasForeignKey("PowerSupplyId");

                    b.Navigation("Cooler");

                    b.Navigation("Cpu");

                    b.Navigation("Motherboard");

                    b.Navigation("PowerSupply");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationGpu", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Configuration", "Configuration")
                        .WithMany("ConfigurationGpus")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCConfigurator.Model.Components.Gpu", "Gpu")
                        .WithMany("ConfigurationGpus")
                        .HasForeignKey("GpuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");

                    b.Navigation("Gpu");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationHdd", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Configuration", "Configuration")
                        .WithMany("ConfigurationHdds")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCConfigurator.Model.Components.Hdd", "Hdd")
                        .WithMany("ConfigurationHdds")
                        .HasForeignKey("HddId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");

                    b.Navigation("Hdd");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationM2Ssd", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Configuration", "Configuration")
                        .WithMany("ConfigurationM2Ssds")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCConfigurator.Model.Components.M2Slot", "M2Slot")
                        .WithMany("ConfigurationM2Ssds")
                        .HasForeignKey("M2SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCConfigurator.Model.Components.M2Ssd", "M2Ssd")
                        .WithMany("ConfigurationM2Ssds")
                        .HasForeignKey("M2SsdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");

                    b.Navigation("M2Slot");

                    b.Navigation("M2Ssd");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationRam", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Configuration", "Configuration")
                        .WithMany("ConfigurationRams")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCConfigurator.Model.Components.Ram", "Ram")
                        .WithMany("ConfigurationRams")
                        .HasForeignKey("RamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");

                    b.Navigation("Ram");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.ConfigurationSsd", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Configuration", "Configuration")
                        .WithMany("ConfigurationSsds")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCConfigurator.Model.Components.Ssd", "Ssd")
                        .WithMany("ConfigurationSsds")
                        .HasForeignKey("SsdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");

                    b.Navigation("Ssd");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Cpu", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Socket", "Socket")
                        .WithMany("Cpus")
                        .HasForeignKey("SocketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Socket");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.M2Slot", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Motherboard", "Motherboard")
                        .WithMany("M2Slots")
                        .HasForeignKey("MotherboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motherboard");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Motherboard", b =>
                {
                    b.HasOne("PCConfigurator.Model.Components.Chipset", "Chipset")
                        .WithMany("Motherboards")
                        .HasForeignKey("ChipsetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCConfigurator.Model.Components.Socket", null)
                        .WithMany("Motherboards")
                        .HasForeignKey("SocketId");

                    b.Navigation("Chipset");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Chipset", b =>
                {
                    b.Navigation("Motherboards");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Configuration", b =>
                {
                    b.Navigation("ConfigurationGpus");

                    b.Navigation("ConfigurationHdds");

                    b.Navigation("ConfigurationM2Ssds");

                    b.Navigation("ConfigurationRams");

                    b.Navigation("ConfigurationSsds");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Cooler", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Cpu", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Gpu", b =>
                {
                    b.Navigation("ConfigurationGpus");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Hdd", b =>
                {
                    b.Navigation("ConfigurationHdds");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.M2Slot", b =>
                {
                    b.Navigation("ConfigurationM2Ssds");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.M2Ssd", b =>
                {
                    b.Navigation("ConfigurationM2Ssds");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Motherboard", b =>
                {
                    b.Navigation("Configurations");

                    b.Navigation("M2Slots");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.PowerSupply", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Ram", b =>
                {
                    b.Navigation("ConfigurationRams");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Socket", b =>
                {
                    b.Navigation("Chipsets");

                    b.Navigation("Cpus");

                    b.Navigation("Motherboards");
                });

            modelBuilder.Entity("PCConfigurator.Model.Components.Ssd", b =>
                {
                    b.Navigation("ConfigurationSsds");
                });
#pragma warning restore 612, 618
        }
    }
}
