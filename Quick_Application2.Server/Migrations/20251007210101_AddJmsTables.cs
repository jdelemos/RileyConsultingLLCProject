using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quick_Application2.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddJmsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppJails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Zip = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    OpenedYear = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Security = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FeaturesCsv = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppJails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppInmates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    JailId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInmates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInmates_AppJails_JailId",
                        column: x => x.JailId,
                        principalTable: "AppJails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInmates_BookingDate",
                table: "AppInmates",
                column: "BookingDate");

            migrationBuilder.CreateIndex(
                name: "IX_AppInmates_ExternalId",
                table: "AppInmates",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppInmates_JailId",
                table: "AppInmates",
                column: "JailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInmates_LastName_FirstName",
                table: "AppInmates",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_AppJails_Name_City",
                table: "AppJails",
                columns: new[] { "Name", "City" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInmates");

            migrationBuilder.DropTable(
                name: "AppJails");
        }
    }
}
