using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XSquareCalculationsApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ATHENTICATES",
                columns: table => new
                {
                    AUTHENTICATE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    USER_ID = table.Column<int>(type: "integer", nullable: false),
                    ID_TOKEN = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EXPIRED_DATETIME = table.Column<DateTime>(type: "datetime", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATHENTICATES", x => x.AUTHENTICATE_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TEMPLATES",
                columns: table => new
                {
                    TEMPLATE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TEMPLATE_NAME = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TEMPLATE_BLOB = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    THUMBNAIL = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LIKE_COUNT = table.Column<int>(type: "int", nullable: false),
                    DOWNLOAD_COUNT = table.Column<int>(type: "int", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    DEL_FLG = table.Column<string>(type: "char", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_TIME = table.Column<DateTime>(type: "Datetime", nullable: false),
                    UPDATED_TIME = table.Column<DateTime>(type: "Datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATES", x => x.TEMPLATE_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    USER_NAME = table.Column<string>(type: "varchar(35)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_PASSWORD = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PASSWORD_SALT = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LIKE_NUMBER_SUM = table.Column<int>(type: "int", nullable: false),
                    DEL_FLG = table.Column<string>(type: "char", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime", nullable: false),
                    UPDATED_TIME = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USER_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATHENTICATES");

            migrationBuilder.DropTable(
                name: "TEMPLATES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
