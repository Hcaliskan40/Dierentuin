using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dierentuin.Migrations
{
    /// <inheritdoc />
    public partial class AddZooIdToEnclosure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enclosure_Zoos_ZooId",
                table: "Enclosure");

            migrationBuilder.AlterColumn<int>(
                name: "ZooId",
                table: "Enclosure",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enclosure_Zoos_ZooId",
                table: "Enclosure",
                column: "ZooId",
                principalTable: "Zoos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enclosure_Zoos_ZooId",
                table: "Enclosure");

            migrationBuilder.AlterColumn<int>(
                name: "ZooId",
                table: "Enclosure",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Enclosure_Zoos_ZooId",
                table: "Enclosure",
                column: "ZooId",
                principalTable: "Zoos",
                principalColumn: "Id");
        }
    }
}
