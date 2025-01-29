using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magalog.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => new { x.Order_id, x.Date });
                    table.ForeignKey(
                        name: "FK_Order_User_User_id",
                        column: x => x.User_id,
                        principalTable: "User",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Order_Order_id_Date",
                        columns: x => new { x.Order_id, x.Date },
                        principalTable: "Order",
                        principalColumns: new[] { "Order_id", "Date" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_User_id",
                table: "Order",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Order_id_Date",
                table: "OrderItems",
                columns: new[] { "Order_id", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
