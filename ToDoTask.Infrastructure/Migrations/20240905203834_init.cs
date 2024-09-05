using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "User" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", "$2a$11$b.3A6am7nKQOUPf1Qwy5Uu71qz9EwouS36TYWqHzJBv6f1Rn5md2q", 1 },
                    { 2, "Bob Smith", "$2a$11$Cjem2z0sOM3FzXSdBMV/KOoIlDjkxSzeOvUsm141nybeaDVPJkcF.", 1 },
                    { 3, "Carol Davis", "$2a$11$oO.w2v0l9uMB1slci35nvu0zX7/lSX4AADmZwz1bp/DKffO3tMlnq", 1 },
                    { 4, "David Wilson", "$2a$11$m0PnpFLTg6zU6Sa9a/fgA.wypVRAJA/d1povil.OhiEnO4sE6oVqG", 1 },
                    { 5, "Emily Brown", "$2a$11$cOgBXK73oYmkjahCkcMDwO/YoN7ikrkXDCHbxFWsumJ38WaYMn1Ey", 1 },
                    { 6, "Frank Harris", "$2a$11$hdYNFXohq.VfT1l5l33uW.ejDeKCwb4UlRKVkRgPTvCFKFaoODgUa", 1 },
                    { 7, "Grace Martin", "$2a$11$ZzMws6wLvl2aTJqh69j0l.EF9U0DmD.3ulXGbGTHoovseAjogPC7S", 1 },
                    { 8, "Henry Thompson", "$2a$11$T1zTLfw2Zn4k.RVJ0Zv8POPyql6Jjv.8bJ/X96GSdXVjtqG.SkbTy", 1 },
                    { 9, "Ivy Garcia", "$2a$11$R39zUKOe1RwiWOV/Q5Rn4.bCcRHXlDPaCS2bS.OEjL4/QAvWpIaNO", 1 },
                    { 10, "Jack Lee", "$2a$11$jfppudvKA/08QzWnzdzw0O5fqtKN6H4C3vF1688xGlvX0wIpyDV8m", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_UserId",
                table: "ToDos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
