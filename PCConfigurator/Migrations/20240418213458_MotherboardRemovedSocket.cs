using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCConfigurator.Migrations
{
    /// <inheritdoc />
    public partial class MotherboardRemovedSocket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motherboard_Socket_SocketId",
                table: "Motherboard");

            migrationBuilder.AlterColumn<int>(
                name: "SocketId",
                table: "Motherboard",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "L3Cache",
                table: "Cpu",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "L2Cache",
                table: "Cpu",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboard_Socket_SocketId",
                table: "Motherboard",
                column: "SocketId",
                principalTable: "Socket",
                principalColumn: "SocketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motherboard_Socket_SocketId",
                table: "Motherboard");

            migrationBuilder.AlterColumn<int>(
                name: "SocketId",
                table: "Motherboard",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "L3Cache",
                table: "Cpu",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "L2Cache",
                table: "Cpu",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboard_Socket_SocketId",
                table: "Motherboard",
                column: "SocketId",
                principalTable: "Socket",
                principalColumn: "SocketId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
