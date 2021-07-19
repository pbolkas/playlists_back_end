using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class verificationTokenLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "verification_token",
                table: "users",
                type: "varchar(90)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "verification_token",
                table: "users",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(90)",
                oldNullable: true);
        }
    }
}
