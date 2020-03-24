using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NotificationService.Migrations
{
    public partial class NotifServiceMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Update_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userModel",
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
                    table.PrimaryKey("PK_userModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Notification_id = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    From = table.Column<int>(nullable: false),
                    Target = table.Column<int>(nullable: false),
                    Email_destination = table.Column<string>(nullable: true),
                    Read_at = table.Column<DateTime>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Update_at = table.Column<DateTime>(nullable: false),
                    usersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notificationLogs_notification_Notification_id",
                        column: x => x.Notification_id,
                        principalTable: "notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notificationLogs_userModel_usersId",
                        column: x => x.usersId,
                        principalTable: "userModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notificationLogs_Notification_id",
                table: "notificationLogs",
                column: "Notification_id");

            migrationBuilder.CreateIndex(
                name: "IX_notificationLogs_usersId",
                table: "notificationLogs",
                column: "usersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notificationLogs");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "userModel");
        }
    }
}
