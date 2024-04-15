using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCConfigurator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cooler",
                columns: table => new
                {
                    CoolerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Tdp = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cooler", x => x.CoolerId);
                });

            migrationBuilder.CreateTable(
                name: "Gpu",
                columns: table => new
                {
                    GpuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    CoreClock = table.Column<int>(type: "INTEGER", nullable: false),
                    BoostClock = table.Column<int>(type: "INTEGER", nullable: false),
                    VramCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerConsumption = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gpu", x => x.GpuId);
                });

            migrationBuilder.CreateTable(
                name: "Hdd",
                columns: table => new
                {
                    HddId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    SpindelSpeed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hdd", x => x.HddId);
                });

            migrationBuilder.CreateTable(
                name: "M2Ssd",
                columns: table => new
                {
                    M2SsdId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    ReadSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    WriteSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    Tbw = table.Column<int>(type: "INTEGER", nullable: false),
                    NandType = table.Column<int>(type: "INTEGER", nullable: false),
                    M2Interface = table.Column<int>(type: "INTEGER", nullable: false),
                    M2Size = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M2Ssd", x => x.M2SsdId);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupply",
                columns: table => new
                {
                    PowerSupplyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Wattage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupply", x => x.PowerSupplyId);
                });

            migrationBuilder.CreateTable(
                name: "Ram",
                columns: table => new
                {
                    RamID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    RamType = table.Column<int>(type: "INTEGER", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Clock = table.Column<int>(type: "INTEGER", nullable: false),
                    Cl = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ram", x => x.RamID);
                });

            migrationBuilder.CreateTable(
                name: "Socket",
                columns: table => new
                {
                    SocketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socket", x => x.SocketId);
                });

            migrationBuilder.CreateTable(
                name: "Ssd",
                columns: table => new
                {
                    SsdId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    ReadSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    WriteSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    Tbw = table.Column<int>(type: "INTEGER", nullable: false),
                    NandType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ssd", x => x.SsdId);
                });

            migrationBuilder.CreateTable(
                name: "Chipset",
                columns: table => new
                {
                    ChipsetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SocketId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chipset", x => x.ChipsetId);
                    table.ForeignKey(
                        name: "FK_Chipset_Socket_SocketId",
                        column: x => x.SocketId,
                        principalTable: "Socket",
                        principalColumn: "SocketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoolerSocket",
                columns: table => new
                {
                    CoolersCoolerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SocketsSocketId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoolerSocket", x => new { x.CoolersCoolerId, x.SocketsSocketId });
                    table.ForeignKey(
                        name: "FK_CoolerSocket_Cooler_CoolersCoolerId",
                        column: x => x.CoolersCoolerId,
                        principalTable: "Cooler",
                        principalColumn: "CoolerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoolerSocket_Socket_SocketsSocketId",
                        column: x => x.SocketsSocketId,
                        principalTable: "Socket",
                        principalColumn: "SocketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cpu",
                columns: table => new
                {
                    CpuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    SocketId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cores = table.Column<int>(type: "INTEGER", nullable: false),
                    ECores = table.Column<int>(type: "INTEGER", nullable: false),
                    Smt = table.Column<bool>(type: "INTEGER", nullable: false),
                    CoreClock = table.Column<float>(type: "REAL", nullable: false),
                    BoostClock = table.Column<float>(type: "REAL", nullable: false),
                    L2Cache = table.Column<float>(type: "REAL", nullable: false),
                    L3Cache = table.Column<float>(type: "REAL", nullable: false),
                    Tdp = table.Column<int>(type: "INTEGER", nullable: false),
                    RamTypes = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRamCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    HaveGraphics = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cpu", x => x.CpuId);
                    table.ForeignKey(
                        name: "FK_Cpu_Socket_SocketId",
                        column: x => x.SocketId,
                        principalTable: "Socket",
                        principalColumn: "SocketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motherboard",
                columns: table => new
                {
                    MotherboardId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    SocketId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChipsetId = table.Column<int>(type: "INTEGER", nullable: false),
                    RamType = table.Column<int>(type: "INTEGER", nullable: false),
                    RamSlots = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRamCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Sata3Ports = table.Column<int>(type: "INTEGER", nullable: false),
                    PCIex16Slots = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboard", x => x.MotherboardId);
                    table.ForeignKey(
                        name: "FK_Motherboard_Chipset_ChipsetId",
                        column: x => x.ChipsetId,
                        principalTable: "Chipset",
                        principalColumn: "ChipsetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motherboard_Socket_SocketId",
                        column: x => x.SocketId,
                        principalTable: "Socket",
                        principalColumn: "SocketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MotherboardId = table.Column<int>(type: "INTEGER", nullable: true),
                    CpuId = table.Column<int>(type: "INTEGER", nullable: true),
                    CoolerId = table.Column<int>(type: "INTEGER", nullable: true),
                    PowerSupplyId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.ConfigurationId);
                    table.ForeignKey(
                        name: "FK_Configuration_Cooler_CoolerId",
                        column: x => x.CoolerId,
                        principalTable: "Cooler",
                        principalColumn: "CoolerId");
                    table.ForeignKey(
                        name: "FK_Configuration_Cpu_CpuId",
                        column: x => x.CpuId,
                        principalTable: "Cpu",
                        principalColumn: "CpuId");
                    table.ForeignKey(
                        name: "FK_Configuration_Motherboard_MotherboardId",
                        column: x => x.MotherboardId,
                        principalTable: "Motherboard",
                        principalColumn: "MotherboardId");
                    table.ForeignKey(
                        name: "FK_Configuration_PowerSupply_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalTable: "PowerSupply",
                        principalColumn: "PowerSupplyId");
                });

            migrationBuilder.CreateTable(
                name: "M2Slot",
                columns: table => new
                {
                    M2SlotId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    M2Interface = table.Column<int>(type: "INTEGER", nullable: false),
                    M2Size = table.Column<int>(type: "INTEGER", nullable: false),
                    MotherboardId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M2Slot", x => x.M2SlotId);
                    table.ForeignKey(
                        name: "FK_M2Slot_Motherboard_MotherboardId",
                        column: x => x.MotherboardId,
                        principalTable: "Motherboard",
                        principalColumn: "MotherboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationGpu",
                columns: table => new
                {
                    ConfigurationGpuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    GpuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationGpu", x => x.ConfigurationGpuId);
                    table.ForeignKey(
                        name: "FK_ConfigurationGpu_Configuration_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configuration",
                        principalColumn: "ConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationGpu_Gpu_GpuId",
                        column: x => x.GpuId,
                        principalTable: "Gpu",
                        principalColumn: "GpuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationHdd",
                columns: table => new
                {
                    ConfigurationHddId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    HddId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationHdd", x => x.ConfigurationHddId);
                    table.ForeignKey(
                        name: "FK_ConfigurationHdd_Configuration_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configuration",
                        principalColumn: "ConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationHdd_Hdd_HddId",
                        column: x => x.HddId,
                        principalTable: "Hdd",
                        principalColumn: "HddId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationRam",
                columns: table => new
                {
                    ConfigurationRamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    RamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationRam", x => x.ConfigurationRamId);
                    table.ForeignKey(
                        name: "FK_ConfigurationRam_Configuration_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configuration",
                        principalColumn: "ConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationRam_Ram_RamId",
                        column: x => x.RamId,
                        principalTable: "Ram",
                        principalColumn: "RamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationSsd",
                columns: table => new
                {
                    ConfigurationSsdId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    SsdId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationSsd", x => x.ConfigurationSsdId);
                    table.ForeignKey(
                        name: "FK_ConfigurationSsd_Configuration_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configuration",
                        principalColumn: "ConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationSsd_Ssd_SsdId",
                        column: x => x.SsdId,
                        principalTable: "Ssd",
                        principalColumn: "SsdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationM2Ssd",
                columns: table => new
                {
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    M2SlotId = table.Column<int>(type: "INTEGER", nullable: false),
                    M2SsdId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationM2Ssd", x => new { x.M2SlotId, x.ConfigurationId });
                    table.ForeignKey(
                        name: "FK_ConfigurationM2Ssd_Configuration_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configuration",
                        principalColumn: "ConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationM2Ssd_M2Slot_M2SlotId",
                        column: x => x.M2SlotId,
                        principalTable: "M2Slot",
                        principalColumn: "M2SlotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationM2Ssd_M2Ssd_M2SsdId",
                        column: x => x.M2SsdId,
                        principalTable: "M2Ssd",
                        principalColumn: "M2SsdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chipset_SocketId",
                table: "Chipset",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_CoolerId",
                table: "Configuration",
                column: "CoolerId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_CpuId",
                table: "Configuration",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_MotherboardId",
                table: "Configuration",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_PowerSupplyId",
                table: "Configuration",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationGpu_ConfigurationId",
                table: "ConfigurationGpu",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationGpu_GpuId",
                table: "ConfigurationGpu",
                column: "GpuId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationHdd_ConfigurationId",
                table: "ConfigurationHdd",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationHdd_HddId",
                table: "ConfigurationHdd",
                column: "HddId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationM2Ssd_ConfigurationId",
                table: "ConfigurationM2Ssd",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationM2Ssd_M2SsdId",
                table: "ConfigurationM2Ssd",
                column: "M2SsdId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationRam_ConfigurationId",
                table: "ConfigurationRam",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationRam_RamId",
                table: "ConfigurationRam",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationSsd_ConfigurationId",
                table: "ConfigurationSsd",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationSsd_SsdId",
                table: "ConfigurationSsd",
                column: "SsdId");

            migrationBuilder.CreateIndex(
                name: "IX_CoolerSocket_SocketsSocketId",
                table: "CoolerSocket",
                column: "SocketsSocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Cpu_SocketId",
                table: "Cpu",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_M2Slot_MotherboardId",
                table: "M2Slot",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboard_ChipsetId",
                table: "Motherboard",
                column: "ChipsetId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboard_SocketId",
                table: "Motherboard",
                column: "SocketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationGpu");

            migrationBuilder.DropTable(
                name: "ConfigurationHdd");

            migrationBuilder.DropTable(
                name: "ConfigurationM2Ssd");

            migrationBuilder.DropTable(
                name: "ConfigurationRam");

            migrationBuilder.DropTable(
                name: "ConfigurationSsd");

            migrationBuilder.DropTable(
                name: "CoolerSocket");

            migrationBuilder.DropTable(
                name: "Gpu");

            migrationBuilder.DropTable(
                name: "Hdd");

            migrationBuilder.DropTable(
                name: "M2Slot");

            migrationBuilder.DropTable(
                name: "M2Ssd");

            migrationBuilder.DropTable(
                name: "Ram");

            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "Ssd");

            migrationBuilder.DropTable(
                name: "Cooler");

            migrationBuilder.DropTable(
                name: "Cpu");

            migrationBuilder.DropTable(
                name: "Motherboard");

            migrationBuilder.DropTable(
                name: "PowerSupply");

            migrationBuilder.DropTable(
                name: "Chipset");

            migrationBuilder.DropTable(
                name: "Socket");
        }
    }
}
