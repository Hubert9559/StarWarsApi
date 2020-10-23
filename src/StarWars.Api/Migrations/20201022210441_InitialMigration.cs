using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Characters");

            migrationBuilder.CreateSequence(
                name: "charactersseq",
                schema: "Characters",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Characters",
                schema: "Characters",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Episodes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                schema: "Characters",
                columns: table => new
                {
                    Friend1Id = table.Column<long>(nullable: false),
                    Friend2Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.Friend1Id, x.Friend2Id });
                    table.ForeignKey(
                        name: "FK_Friendships_Characters_Friend1Id",
                        column: x => x.Friend1Id,
                        principalSchema: "Characters",
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_Characters_Friend2Id",
                        column: x => x.Friend2Id,
                        principalSchema: "Characters",
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Name",
                schema: "Characters",
                table: "Characters",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_Friend2Id",
                schema: "Characters",
                table: "Friendships",
                column: "Friend2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships",
                schema: "Characters");

            migrationBuilder.DropTable(
                name: "Characters",
                schema: "Characters");

            migrationBuilder.DropSequence(
                name: "charactersseq",
                schema: "Characters");
        }
    }
}
