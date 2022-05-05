using Microsoft.EntityFrameworkCore.Migrations;

namespace QAHomework.Data.Migrations
{
    public partial class justtryingagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsTags_Users_UserId",
                table: "QuestionsTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsTags_UserId",
                table: "QuestionsTags");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuestionsTags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "QuestionsTags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsTags_UserId",
                table: "QuestionsTags",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsTags_Users_UserId",
                table: "QuestionsTags",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
