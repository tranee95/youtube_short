using Microsoft.EntityFrameworkCore.Migrations;

namespace vitouch.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Favorite",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Guid",
                table: "Favorite",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Guid",
                table: "Favorite",
                column: "Guid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorite_Guid",
                table: "Favorite");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Favorite",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Guid",
                table: "Favorite",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
