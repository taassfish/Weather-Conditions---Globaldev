using Microsoft.EntityFrameworkCore.Migrations;

namespace Weather_Conditions___Globaldev.Migrations
{
    public partial class Without_The_Statical_Information : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageT",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "MaxT",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "MinT",
                table: "WeatherForecasts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageT",
                table: "WeatherForecasts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxT",
                table: "WeatherForecasts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinT",
                table: "WeatherForecasts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
