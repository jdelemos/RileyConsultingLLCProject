using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quick_Application2.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitCellTransferTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CellId",
                table: "AppInmates",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AppInmates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "AppInmates",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InmateId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromJailId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToJailId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTransfers_AppInmates_InmateId",
                        column: x => x.InmateId,
                        principalTable: "AppInmates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppTransfers_AppJails_FromJailId",
                        column: x => x.FromJailId,
                        principalTable: "AppJails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppTransfers_AppJails_ToJailId",
                        column: x => x.ToJailId,
                        principalTable: "AppJails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    JailId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUnits_AppJails_JailId",
                        column: x => x.JailId,
                        principalTable: "AppJails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CellNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsOccupied = table.Column<bool>(type: "boolean", nullable: false),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    JailId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCells_AppJails_JailId",
                        column: x => x.JailId,
                        principalTable: "AppJails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCells_AppUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "AppUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInmates_CellId",
                table: "AppInmates",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCells_CellNumber_JailId",
                table: "AppCells",
                columns: new[] { "CellNumber", "JailId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCells_JailId",
                table: "AppCells",
                column: "JailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCells_UnitId",
                table: "AppCells",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTransfers_FromJailId",
                table: "AppTransfers",
                column: "FromJailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTransfers_InmateId",
                table: "AppTransfers",
                column: "InmateId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTransfers_ToJailId",
                table: "AppTransfers",
                column: "ToJailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUnits_JailId",
                table: "AppUnits",
                column: "JailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUnits_Name_JailId",
                table: "AppUnits",
                columns: new[] { "Name", "JailId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppInmates_AppCells_CellId",
                table: "AppInmates",
                column: "CellId",
                principalTable: "AppCells",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInmates_AppCells_CellId",
                table: "AppInmates");

            migrationBuilder.DropTable(
                name: "AppCells");

            migrationBuilder.DropTable(
                name: "AppTransfers");

            migrationBuilder.DropTable(
                name: "AppUnits");

            migrationBuilder.DropIndex(
                name: "IX_AppInmates_CellId",
                table: "AppInmates");

            migrationBuilder.DropColumn(
                name: "CellId",
                table: "AppInmates");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AppInmates");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "AppInmates");
        }
    }
}
