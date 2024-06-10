using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TasksToDo_AspNetUsers_UsersId",
                table: "TasksToDo");

            migrationBuilder.DropIndex(
                name: "IX_TasksToDo_UsersId",
                table: "TasksToDo");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "TasksToDo");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TasksToDo",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TasksToDo_UserId",
                table: "TasksToDo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TasksToDo_AspNetUsers_UserId",
                table: "TasksToDo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TasksToDo_AspNetUsers_UserId",
                table: "TasksToDo");

            migrationBuilder.DropIndex(
                name: "IX_TasksToDo_UserId",
                table: "TasksToDo");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TasksToDo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "TasksToDo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TasksToDo_UsersId",
                table: "TasksToDo",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TasksToDo_AspNetUsers_UsersId",
                table: "TasksToDo",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
