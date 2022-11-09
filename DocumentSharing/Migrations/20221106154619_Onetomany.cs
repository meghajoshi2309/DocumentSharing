using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentSharing.Migrations
{
    public partial class Onetomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    ClassroomId = table.Column<int>(nullable: false),
                    FileClassId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileClass_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileClass_FileClass_FileClassId",
                        column: x => x.FileClassId,
                        principalTable: "FileClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileClass_ClassroomId",
                table: "FileClass",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_FileClass_FileClassId",
                table: "FileClass",
                column: "FileClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileClass");
        }
    }
}
