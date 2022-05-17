using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_service_api.Migrations
{
    public partial class addedmediaType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mediaType",
                table: "Messages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mediaType",
                table: "Messages");
        }
    }
}
