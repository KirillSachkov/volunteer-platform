using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "owners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    profile_photo = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    credentials_login = table.Column<string>(type: "text", nullable: false),
                    credentials_password_hash = table.Column<string>(type: "text", nullable: false),
                    phone_number_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_owners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cat",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    animal_attitude = table.Column<string>(type: "text", nullable: true),
                    people_attitude = table.Column<string>(type: "text", nullable: true),
                    vaccine = table.Column<bool>(type: "boolean", nullable: true),
                    color = table.Column<string>(type: "text", nullable: true),
                    place = table.Column<string>(type: "text", nullable: true),
                    health = table.Column<string>(type: "text", nullable: true),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    age_months = table.Column<int>(type: "integer", nullable: false),
                    age_years = table.Column<int>(type: "integer", nullable: false),
                    gender_value = table.Column<string>(type: "text", nullable: false),
                    phone_number_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cat", x => x.id);
                    table.ForeignKey(
                        name: "fk_cat_owners_owner_id",
                        column: x => x.owner_id,
                        principalTable: "owners",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cat_tag",
                columns: table => new
                {
                    cat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cat_tag", x => new { x.cat_id, x.tags_id });
                    table.ForeignKey(
                        name: "fk_cat_tag_cat_cat_id",
                        column: x => x.cat_id,
                        principalTable: "cat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cat_tag_tag_tags_id",
                        column: x => x.tags_id,
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cat_owner_id",
                table: "cat",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_cat_tag_tags_id",
                table: "cat_tag",
                column: "tags_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cat_tag");

            migrationBuilder.DropTable(
                name: "cat");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "owners");
        }
    }
}
