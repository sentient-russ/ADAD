using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adad.Migrations
{
    /// <inheritdoc />
    public partial class warning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "send_warning",
                schema: "Identity",
                table: "SiteModel",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "send_warning",
                schema: "Identity",
                table: "SiteModel");
        }
    }
}
