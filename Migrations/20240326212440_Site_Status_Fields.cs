using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adad.Migrations
{
    /// <inheritdoc />
    public partial class Site_Status_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "severity",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "threat",
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
                name: "severity",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.DropColumn(
                name: "threat",
                schema: "Identity",
                table: "SiteModel");
        }
    }
}
