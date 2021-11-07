using Microsoft.EntityFrameworkCore.Migrations;

namespace vitouch.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateIndex(
                name: "IX_Dislikes_VideoId_UserId",
                table: "Dislikes",
                columns: new[] { "VideoId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_VideoId_UserId",
                table: "Likes",
                columns: new[] { "VideoId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Themes_Name",
                table: "Themes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ThemesId_BlogerId_Name",
                table: "Videos",
                columns: new[] { "ThemesId", "BlogerId", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
