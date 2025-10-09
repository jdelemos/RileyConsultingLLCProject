using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quick_Application2.Server.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesWithoutPrefixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCells_AppJails_JailId",
                table: "AppCells");

            migrationBuilder.DropForeignKey(
                name: "FK_AppCells_AppUnits_UnitId",
                table: "AppCells");

            migrationBuilder.DropForeignKey(
                name: "FK_AppInmates_AppCells_CellId",
                table: "AppInmates");

            migrationBuilder.DropForeignKey(
                name: "FK_AppInmates_AppJails_JailId",
                table: "AppInmates");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderDetails_AppOrders_OrderId",
                table: "AppOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderDetails_AppProducts_ProductId",
                table: "AppOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppCustomers_CustomerId",
                table: "AppOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AspNetUsers_CashierId",
                table: "AppOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppProductCategories_ProductCategoryId",
                table: "AppProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppProducts_ParentId",
                table: "AppProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTransfers_AppInmates_InmateId",
                table: "AppTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTransfers_AppJails_FromJailId",
                table: "AppTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTransfers_AppJails_ToJailId",
                table: "AppTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUnits_AppJails_JailId",
                table: "AppUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AppInmates_InmateId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AppJails_JailId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUnits",
                table: "AppUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppTransfers",
                table: "AppTransfers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProducts",
                table: "AppProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProductCategories",
                table: "AppProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppOrders",
                table: "AppOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppOrderDetails",
                table: "AppOrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppJails",
                table: "AppJails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppInmates",
                table: "AppInmates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCustomers",
                table: "AppCustomers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCells",
                table: "AppCells");

            migrationBuilder.RenameTable(
                name: "AppUnits",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "AppTransfers",
                newName: "Transfers");

            migrationBuilder.RenameTable(
                name: "AppProducts",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "AppProductCategories",
                newName: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "AppOrders",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "AppOrderDetails",
                newName: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "AppJails",
                newName: "Jails");

            migrationBuilder.RenameTable(
                name: "AppInmates",
                newName: "Inmates");

            migrationBuilder.RenameTable(
                name: "AppCustomers",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "AppCells",
                newName: "Cells");

            migrationBuilder.RenameIndex(
                name: "IX_AppUnits_Name_JailId",
                table: "Units",
                newName: "IX_Units_Name_JailId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUnits_JailId",
                table: "Units",
                newName: "IX_Units_JailId");

            migrationBuilder.RenameIndex(
                name: "IX_AppTransfers_ToJailId",
                table: "Transfers",
                newName: "IX_Transfers_ToJailId");

            migrationBuilder.RenameIndex(
                name: "IX_AppTransfers_InmateId",
                table: "Transfers",
                newName: "IX_Transfers_InmateId");

            migrationBuilder.RenameIndex(
                name: "IX_AppTransfers_FromJailId",
                table: "Transfers",
                newName: "IX_Transfers_FromJailId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProducts_ProductCategoryId",
                table: "Products",
                newName: "IX_Products_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProducts_ParentId",
                table: "Products",
                newName: "IX_Products_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProducts_Name",
                table: "Products",
                newName: "IX_Products_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AppOrders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_AppOrders_CashierId",
                table: "Orders",
                newName: "IX_Orders_CashierId");

            migrationBuilder.RenameIndex(
                name: "IX_AppOrderDetails_ProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AppOrderDetails_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_AppJails_Name_City",
                table: "Jails",
                newName: "IX_Jails_Name_City");

            migrationBuilder.RenameIndex(
                name: "IX_AppInmates_LastName_FirstName",
                table: "Inmates",
                newName: "IX_Inmates_LastName_FirstName");

            migrationBuilder.RenameIndex(
                name: "IX_AppInmates_JailId",
                table: "Inmates",
                newName: "IX_Inmates_JailId");

            migrationBuilder.RenameIndex(
                name: "IX_AppInmates_ExternalId",
                table: "Inmates",
                newName: "IX_Inmates_ExternalId");

            migrationBuilder.RenameIndex(
                name: "IX_AppInmates_CellId",
                table: "Inmates",
                newName: "IX_Inmates_CellId");

            migrationBuilder.RenameIndex(
                name: "IX_AppInmates_BookingDate",
                table: "Inmates",
                newName: "IX_Inmates_BookingDate");

            migrationBuilder.RenameIndex(
                name: "IX_AppCustomers_Name",
                table: "Customers",
                newName: "IX_Customers_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AppCells_UnitId",
                table: "Cells",
                newName: "IX_Cells_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AppCells_JailId",
                table: "Cells",
                newName: "IX_Cells_JailId");

            migrationBuilder.RenameIndex(
                name: "IX_AppCells_CellNumber_JailId",
                table: "Cells",
                newName: "IX_Cells_CellNumber_JailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transfers",
                table: "Transfers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jails",
                table: "Jails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inmates",
                table: "Inmates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cells",
                table: "Cells",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Inmates_InmateId",
                table: "Bookings",
                column: "InmateId",
                principalTable: "Inmates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Jails_JailId",
                table: "Bookings",
                column: "JailId",
                principalTable: "Jails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Jails_JailId",
                table: "Cells",
                column: "JailId",
                principalTable: "Jails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Units_UnitId",
                table: "Cells",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inmates_Cells_CellId",
                table: "Inmates",
                column: "CellId",
                principalTable: "Cells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inmates_Jails_JailId",
                table: "Inmates",
                column: "JailId",
                principalTable: "Jails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CashierId",
                table: "Orders",
                column: "CashierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ParentId",
                table: "Products",
                column: "ParentId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Inmates_InmateId",
                table: "Transfers",
                column: "InmateId",
                principalTable: "Inmates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Jails_FromJailId",
                table: "Transfers",
                column: "FromJailId",
                principalTable: "Jails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Jails_ToJailId",
                table: "Transfers",
                column: "ToJailId",
                principalTable: "Jails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Jails_JailId",
                table: "Units",
                column: "JailId",
                principalTable: "Jails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Inmates_InmateId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Jails_JailId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Jails_JailId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Units_UnitId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Inmates_Cells_CellId",
                table: "Inmates");

            migrationBuilder.DropForeignKey(
                name: "FK_Inmates_Jails_JailId",
                table: "Inmates");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CashierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ParentId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Inmates_InmateId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Jails_FromJailId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Jails_ToJailId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Jails_JailId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transfers",
                table: "Transfers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jails",
                table: "Jails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inmates",
                table: "Inmates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cells",
                table: "Cells");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "AppUnits");

            migrationBuilder.RenameTable(
                name: "Transfers",
                newName: "AppTransfers");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "AppProducts");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "AppProductCategories");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "AppOrders");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "AppOrderDetails");

            migrationBuilder.RenameTable(
                name: "Jails",
                newName: "AppJails");

            migrationBuilder.RenameTable(
                name: "Inmates",
                newName: "AppInmates");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "AppCustomers");

            migrationBuilder.RenameTable(
                name: "Cells",
                newName: "AppCells");

            migrationBuilder.RenameIndex(
                name: "IX_Units_Name_JailId",
                table: "AppUnits",
                newName: "IX_AppUnits_Name_JailId");

            migrationBuilder.RenameIndex(
                name: "IX_Units_JailId",
                table: "AppUnits",
                newName: "IX_AppUnits_JailId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_ToJailId",
                table: "AppTransfers",
                newName: "IX_AppTransfers_ToJailId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_InmateId",
                table: "AppTransfers",
                newName: "IX_AppTransfers_InmateId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_FromJailId",
                table: "AppTransfers",
                newName: "IX_AppTransfers_FromJailId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategoryId",
                table: "AppProducts",
                newName: "IX_AppProducts_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ParentId",
                table: "AppProducts",
                newName: "IX_AppProducts_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Name",
                table: "AppProducts",
                newName: "IX_AppProducts_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "AppOrders",
                newName: "IX_AppOrders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CashierId",
                table: "AppOrders",
                newName: "IX_AppOrders_CashierId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "AppOrderDetails",
                newName: "IX_AppOrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "AppOrderDetails",
                newName: "IX_AppOrderDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Jails_Name_City",
                table: "AppJails",
                newName: "IX_AppJails_Name_City");

            migrationBuilder.RenameIndex(
                name: "IX_Inmates_LastName_FirstName",
                table: "AppInmates",
                newName: "IX_AppInmates_LastName_FirstName");

            migrationBuilder.RenameIndex(
                name: "IX_Inmates_JailId",
                table: "AppInmates",
                newName: "IX_AppInmates_JailId");

            migrationBuilder.RenameIndex(
                name: "IX_Inmates_ExternalId",
                table: "AppInmates",
                newName: "IX_AppInmates_ExternalId");

            migrationBuilder.RenameIndex(
                name: "IX_Inmates_CellId",
                table: "AppInmates",
                newName: "IX_AppInmates_CellId");

            migrationBuilder.RenameIndex(
                name: "IX_Inmates_BookingDate",
                table: "AppInmates",
                newName: "IX_AppInmates_BookingDate");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Name",
                table: "AppCustomers",
                newName: "IX_AppCustomers_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Cells_UnitId",
                table: "AppCells",
                newName: "IX_AppCells_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Cells_JailId",
                table: "AppCells",
                newName: "IX_AppCells_JailId");

            migrationBuilder.RenameIndex(
                name: "IX_Cells_CellNumber_JailId",
                table: "AppCells",
                newName: "IX_AppCells_CellNumber_JailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUnits",
                table: "AppUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppTransfers",
                table: "AppTransfers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProducts",
                table: "AppProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProductCategories",
                table: "AppProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppOrders",
                table: "AppOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppOrderDetails",
                table: "AppOrderDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppJails",
                table: "AppJails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppInmates",
                table: "AppInmates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCustomers",
                table: "AppCustomers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCells",
                table: "AppCells",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCells_AppJails_JailId",
                table: "AppCells",
                column: "JailId",
                principalTable: "AppJails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppCells_AppUnits_UnitId",
                table: "AppCells",
                column: "UnitId",
                principalTable: "AppUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppInmates_AppCells_CellId",
                table: "AppInmates",
                column: "CellId",
                principalTable: "AppCells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInmates_AppJails_JailId",
                table: "AppInmates",
                column: "JailId",
                principalTable: "AppJails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderDetails_AppOrders_OrderId",
                table: "AppOrderDetails",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderDetails_AppProducts_ProductId",
                table: "AppOrderDetails",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppCustomers_CustomerId",
                table: "AppOrders",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AspNetUsers_CashierId",
                table: "AppOrders",
                column: "CashierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppProductCategories_ProductCategoryId",
                table: "AppProducts",
                column: "ProductCategoryId",
                principalTable: "AppProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppProducts_ParentId",
                table: "AppProducts",
                column: "ParentId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTransfers_AppInmates_InmateId",
                table: "AppTransfers",
                column: "InmateId",
                principalTable: "AppInmates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTransfers_AppJails_FromJailId",
                table: "AppTransfers",
                column: "FromJailId",
                principalTable: "AppJails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTransfers_AppJails_ToJailId",
                table: "AppTransfers",
                column: "ToJailId",
                principalTable: "AppJails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUnits_AppJails_JailId",
                table: "AppUnits",
                column: "JailId",
                principalTable: "AppJails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AppInmates_InmateId",
                table: "Bookings",
                column: "InmateId",
                principalTable: "AppInmates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AppJails_JailId",
                table: "Bookings",
                column: "JailId",
                principalTable: "AppJails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
