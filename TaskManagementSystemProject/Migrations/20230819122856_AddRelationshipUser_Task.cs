using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystemProject.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipUser_Task : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignId",
                table: "TaskItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "TaskItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TaskCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskComments_TaskComments_TaskCommentId",
                        column: x => x.TaskCommentId,
                        principalTable: "TaskComments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_IdentityUserId",
                table: "TaskItems",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_TaskCommentId",
                table: "TaskComments",
                column: "TaskCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_AspNetUsers_IdentityUserId",
                table: "TaskItems",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_AspNetUsers_IdentityUserId",
                table: "TaskItems");

            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_IdentityUserId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "AssignId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "TaskItems");
        }
    }
}
