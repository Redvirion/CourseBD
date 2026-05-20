using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseBD.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintsAndMakeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TechProcess_MaterialQuantity",
                table: "TechProcesses",
                sql: "\"MaterialQuantity\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Request_Quantity",
                table: "Requests",
                sql: "\"Quantity\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductComponent_Quantity",
                table: "ProductComponents",
                sql: "\"Quantity\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Operation_Cost",
                table: "Operations",
                sql: "\"Cost\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Material_Cost",
                table: "Materials",
                sql: "\"Cost\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Component_Cost",
                table: "Components",
                sql: "\"Cost\" >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TechProcess_MaterialQuantity",
                table: "TechProcesses");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Request_Quantity",
                table: "Requests");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductComponent_Quantity",
                table: "ProductComponents");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Operation_Cost",
                table: "Operations");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Material_Cost",
                table: "Materials");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Component_Cost",
                table: "Components");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
