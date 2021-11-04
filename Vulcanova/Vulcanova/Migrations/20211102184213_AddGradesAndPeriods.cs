using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vulcanova.Migrations
{
    public partial class AddGradesAndPeriods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Column",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<Guid>(type: "TEXT", nullable: false),
                    PeriodId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Group = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Column", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Current = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Period_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatorName = table.Column<string>(type: "TEXT", nullable: true),
                    PupilId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContentRaw = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated_Timestamp = table.Column<long>(type: "INTEGER", nullable: true),
                    DateCreated_DateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateCreated_DateDisplay = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated_Time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModify_Timestamp = table.Column<long>(type: "INTEGER", nullable: true),
                    DateModify_DateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModify_DateDisplay = table.Column<string>(type: "TEXT", nullable: true),
                    DateModify_Time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Value = table.Column<int>(type: "INTEGER", nullable: true),
                    ColumnId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_Column_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Column",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_ColumnId",
                table: "Grade",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_Period_AccountId",
                table: "Period",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropTable(
                name: "Column");
        }
    }
}
