using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseBD.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    TechProcessRef = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentId);
                    table.CheckConstraint("CK_Component_Cost", "\"Cost\" >= 0");
                    table.ForeignKey(
                        name: "FK_Components_Components_TechProcessRef",
                        column: x => x.TechProcessRef,
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                    table.CheckConstraint("CK_Material_Price", "\"Price\" >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HourlyRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Hours = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationId);
                    table.CheckConstraint("CK_Operation_HourlyRate", "\"HourlyRate\" >= 0");
                    table.CheckConstraint("CK_Operation_Hours", "\"Hours\" > 0");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LaborHours = table.Column<decimal>(type: "numeric", nullable: false),
                    Composition = table.Column<string>(type: "text", nullable: true),
                    MaterialRef = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.CheckConstraint("CK_Product_LaborHours", "\"LaborHours\" >= 0");
                    table.ForeignKey(
                        name: "FK_Products_Materials_MaterialRef",
                        column: x => x.MaterialRef,
                        principalTable: "Materials",
                        principalColumn: "MaterialId");
                });

            migrationBuilder.CreateTable(
                name: "TechProcesses",
                columns: table => new
                {
                    TechProcessId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComponentId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    OperationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechProcesses", x => x.TechProcessId);
                    table.CheckConstraint("CK_TechProcess_Quantity", "\"Quantity\" > 0");
                    table.ForeignKey(
                        name: "FK_TechProcesses_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechProcesses_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechProcesses_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCompositions",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ComponentId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCompositions", x => new { x.ProductId, x.ComponentId });
                    table.CheckConstraint("CK_ProductComposition_Quantity", "\"Quantity\" > 0");
                    table.ForeignKey(
                        name: "FK_ProductCompositions_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCompositions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_Name",
                table: "Components",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Components_TechProcessRef",
                table: "Components",
                column: "TechProcessRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComposition_ProductId",
                table: "ProductCompositions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCompositions_ComponentId",
                table: "ProductCompositions",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MaterialRef",
                table: "Products",
                column: "MaterialRef");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TechProcess_ComponentId",
                table: "TechProcesses",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_TechProcess_MaterialId",
                table: "TechProcesses",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TechProcess_OperationId",
                table: "TechProcesses",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_TechProcess_Unique",
                table: "TechProcesses",
                columns: new[] { "ComponentId", "MaterialId", "OperationId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCompositions");

            migrationBuilder.DropTable(
                name: "TechProcesses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
