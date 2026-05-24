using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Linguagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linguagens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidades_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Desenvolvedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Senioridade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CidadeId = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Deletado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desenvolvedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Desenvolvedores_Cidades_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesenvolvedorLinguagens",
                columns: table => new
                {
                    DesenvolvedorId = table.Column<int>(type: "int", nullable: false),
                    LinguagemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenvolvedorLinguagens", x => new { x.DesenvolvedorId, x.LinguagemId });
                    table.ForeignKey(
                        name: "FK_DesenvolvedorLinguagens_Desenvolvedores_DesenvolvedorId",
                        column: x => x.DesenvolvedorId,
                        principalTable: "Desenvolvedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesenvolvedorLinguagens_Linguagens_LinguagemId",
                        column: x => x.LinguagemId,
                        principalTable: "Linguagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_EstadoId",
                table: "Cidades",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Desenvolvedores_CidadeId",
                table: "Desenvolvedores",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Desenvolvedores_Email",
                table: "Desenvolvedores",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesenvolvedorLinguagens_LinguagemId",
                table: "DesenvolvedorLinguagens",
                column: "LinguagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenvolvedorLinguagens");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Desenvolvedores");

            migrationBuilder.DropTable(
                name: "Linguagens");

            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Estados");
        }
    }
}
