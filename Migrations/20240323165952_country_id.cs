using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adad.Migrations
{
    /// <inheritdoc />
    public partial class country_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "country_id",
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
                name: "country_id",
                schema: "Identity",
                table: "SiteModel");
        }
    }
}
