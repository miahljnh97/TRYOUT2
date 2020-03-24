using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PaymentService.Migrations
{
    public partial class PaymentServiceMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Update_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_id = table.Column<int>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Update_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_userModels_User_id",
                        column: x => x.User_id,
                        principalTable: "userModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Product_id = table.Column<int>(nullable: false),
                    Order_id = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Update_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_Details_orders_Order_id",
                        column: x => x.Order_id,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_Details_products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order_id = table.Column<int>(nullable: false),
                    Transaction_id = table.Column<int>(nullable: false),
                    Payment_type = table.Column<string>(nullable: true),
                    Gross_amount = table.Column<string>(nullable: true),
                    Bank = table.Column<string>(nullable: true),
                    Transaction_time = table.Column<string>(nullable: true),
                    Transaction_status = table.Column<string>(nullable: true),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Update_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payment_orders_Order_id",
                        column: x => x.Order_id,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_Details_Order_id",
                table: "order_Details",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_Details_Product_id",
                table: "order_Details",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_User_id",
                table: "orders",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_Order_id",
                table: "payment",
                column: "Order_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_Details");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "userModels");
        }
    }
}
