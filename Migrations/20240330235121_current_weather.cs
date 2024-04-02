using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adad.Migrations
{
    /// <inheritdoc />
    public partial class current_weather : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "curr_gusts",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "curr_precip",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "curr_temperature",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "curr_time",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "curr_weather_code",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "curr_wind_dir",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "curr_windspeed",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tomorrow_gusts",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tomorrow_high_temp",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tomorrow_low_temp",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tomorrow_precip",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tomorrow_weather_code",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tomorrow_wind_dir",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tomorrow_windspeed",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "curr_gusts",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "curr_precip",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "curr_temperature",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "curr_time",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "curr_weather_code",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "curr_wind_dir",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "curr_windspeed",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "tomorrow_gusts",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "tomorrow_high_temp",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "tomorrow_low_temp",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "tomorrow_precip",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "tomorrow_weather_code",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "tomorrow_wind_dir",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "tomorrow_windspeed",
                schema: "Identity",
                table: "SiteModel");
        }
    }
}
