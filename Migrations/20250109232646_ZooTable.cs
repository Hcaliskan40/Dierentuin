using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dierentuin.Migrations
{
    /// <inheritdoc />
    public partial class ZooTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZooId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnclosureId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZooId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Zoos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningHours = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zoos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enclosure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpaceRequirement = table.Column<double>(type: "float", nullable: false),
                    SecurityLevel = table.Column<int>(type: "int", nullable: false),
                    SpaceAvailable = table.Column<double>(type: "float", nullable: false),
                    ZooId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enclosure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enclosure_Zoos_ZooId",
                        column: x => x.ZooId,
                        principalTable: "Zoos",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EnclosureId", "ZooId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EnclosureId", "ZooId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EnclosureId", "ZooId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EnclosureId", "ZooId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ZooId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ZooId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Zoos",
                columns: new[] { "Id", "Location", "Name", "OpeningHours" },
                values: new object[,]
                {
                    { 1, null, "Safari Park", null },
                    { 2, null, "Wildlife Reserve", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ZooId",
                table: "Categories",
                column: "ZooId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_EnclosureId",
                table: "Animals",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ZooId",
                table: "Animals",
                column: "ZooId");

            migrationBuilder.CreateIndex(
                name: "IX_Enclosure_ZooId",
                table: "Enclosure",
                column: "ZooId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosure_EnclosureId",
                table: "Animals",
                column: "EnclosureId",
                principalTable: "Enclosure",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Zoos_ZooId",
                table: "Animals",
                column: "ZooId",
                principalTable: "Zoos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Zoos_ZooId",
                table: "Categories",
                column: "ZooId",
                principalTable: "Zoos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosure_EnclosureId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Zoos_ZooId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Zoos_ZooId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Enclosure");

            migrationBuilder.DropTable(
                name: "Zoos");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ZooId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Animals_EnclosureId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ZooId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ZooId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EnclosureId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ZooId",
                table: "Animals");
        }
    }
}
