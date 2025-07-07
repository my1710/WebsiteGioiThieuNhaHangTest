using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GioiThieuNhaHang.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminLog_AdminUser_IdAD",
                table: "AdminLog");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminRole_AdminUser_IdAD",
                table: "AdminRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUser",
                table: "AdminUser");

            migrationBuilder.RenameTable(
                name: "AdminUser",
                newName: "AdminUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers",
                column: "IdAD");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminLog_AdminUsers_IdAD",
                table: "AdminLog",
                column: "IdAD",
                principalTable: "AdminUsers",
                principalColumn: "IdAD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminRole_AdminUsers_IdAD",
                table: "AdminRole",
                column: "IdAD",
                principalTable: "AdminUsers",
                principalColumn: "IdAD",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminLog_AdminUsers_IdAD",
                table: "AdminLog");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminRole_AdminUsers_IdAD",
                table: "AdminRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.RenameTable(
                name: "AdminUsers",
                newName: "AdminUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUser",
                table: "AdminUser",
                column: "IdAD");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminLog_AdminUser_IdAD",
                table: "AdminLog",
                column: "IdAD",
                principalTable: "AdminUser",
                principalColumn: "IdAD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminRole_AdminUser_IdAD",
                table: "AdminRole",
                column: "IdAD",
                principalTable: "AdminUser",
                principalColumn: "IdAD",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
