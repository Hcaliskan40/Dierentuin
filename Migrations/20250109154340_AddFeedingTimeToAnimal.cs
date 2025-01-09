using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dierentuin.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedingTimeToAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "FeedingTime",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PredatorId",
                table: "Animals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecurityRequirement",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SpaceRequirement",
                table: "Animals",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SunriseAction",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SunsetAction",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FeedingTime", "PredatorId", "SecurityRequirement", "SpaceRequirement", "SunriseAction", "SunsetAction" },
                values: new object[] { 0, null, 2, 12.5, 0, 1 });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FeedingTime", "PredatorId", "SecurityRequirement", "SpaceRequirement", "SunriseAction", "SunsetAction" },
                values: new object[] { 1, null, 1, 8.0, 1, 0 });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "ActivityPattern", "CategoryId", "DietaryClass", "FeedingTime", "Name", "PredatorId", "SecurityRequirement", "Size", "SpaceRequirement", "Species", "SunriseAction", "SunsetAction" },
                values: new object[,]
                {
                    { 3, 0, 1, 1, 0, "Deer", null, 0, 3, 10.0, "Cervidae", 0, 1 },
                    { 4, 1, 1, 0, 1, "Tiger", 1, 2, 4, 15.0, "Panthera tigris", 1, 0 }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_PredatorId",
                table: "Animals",
                column: "PredatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Animals_PredatorId",
                table: "Animals",
                column: "PredatorId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Animals_PredatorId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_PredatorId",
                table: "Animals");

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "FeedingTime",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "PredatorId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SecurityRequirement",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SpaceRequirement",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SunriseAction",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SunsetAction",
                table: "Animals");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
