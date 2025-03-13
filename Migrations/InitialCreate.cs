using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoObrigacoes.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(maxLength: 200, nullable: false),
                    cnpj = table.Column<string>(maxLength: 18, nullable: false),
                    endereco = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    telefone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "obrigacoes_acessorias",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(maxLength: 200, nullable: false),
                    periodicidade = table.Column<int>(nullable: false),
                    empresa_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_obrigacoes_acessorias", x => x.id);
                    table.ForeignKey(
                        name: "FK_obrigacoes_acessorias_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_empresas_cnpj",
                table: "empresas",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_obrigacoes_acessorias_empresa_id",
                table: "obrigacoes_acessorias",
                column: "empresa_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "obrigacoes_acessorias");

            migrationBuilder.DropTable(
                name: "empresas");
        }
    }
}