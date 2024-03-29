using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adad.Migrations
{
    /// <inheritdoc />
    public partial class wind_direction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "sms",
                keyValue: null,
                column: "sms",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "sms",
                schema: "Identity",
                table: "SiteModel",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "site_name",
                keyValue: null,
                column: "site_name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "site_name",
                schema: "Identity",
                table: "SiteModel",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "phone",
                keyValue: null,
                column: "phone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                schema: "Identity",
                table: "SiteModel",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "longitude",
                keyValue: null,
                column: "longitude",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "longitude",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "latitude",
                keyValue: null,
                column: "latitude",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "latitude",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "email",
                keyValue: null,
                column: "email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "country_id",
                keyValue: null,
                column: "country_id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "country_id",
                schema: "Identity",
                table: "SiteModel",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "country_code",
                keyValue: null,
                column: "country_code",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "country_code",
                schema: "Identity",
                table: "SiteModel",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "country",
                keyValue: null,
                column: "country",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                schema: "Identity",
                table: "SiteModel",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "contact_name",
                keyValue: null,
                column: "contact_name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "contact_name",
                schema: "Identity",
                table: "SiteModel",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "SiteModel",
                keyColumn: "city",
                keyValue: null,
                column: "city",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                schema: "Identity",
                table: "SiteModel",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "wind_direction",
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
                name: "wind_direction",
                schema: "Identity",
                table: "SiteModel");

            migrationBuilder.AlterColumn<string>(
                name: "sms",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldMaxLength: 11)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "site_name",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldMaxLength: 11)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "longitude",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "latitude",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "country_id",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "country_code",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldMaxLength: 3)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "contact_name",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                schema: "Identity",
                table: "SiteModel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
