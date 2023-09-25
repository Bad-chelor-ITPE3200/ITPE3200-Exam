using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamITPE3200.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cityName = table.Column<string>(type: "TEXT", nullable: true),
                    country = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityModelId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ContryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Contryname = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ContryId);
                });

            migrationBuilder.CreateTable(
                name: "Landlord",
                columns: table => new
                {
                    landlordModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rating = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landlord", x => x.landlordModelId);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    listingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    listingName = table.Column<string>(type: "TEXT", nullable: true),
                    username = table.Column<string>(type: "TEXT", nullable: true),
                    noOfBeds = table.Column<int>(type: "INTEGER", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: true),
                    area = table.Column<int>(type: "INTEGER", nullable: false),
                    rating = table.Column<float>(type: "REAL", nullable: false),
                    available = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.listingId);
                });

            migrationBuilder.CreateTable(
                name: "Renters",
                columns: table => new
                {
                    renterModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renters", x => x.renterModelId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: true),
                    password = table.Column<string>(type: "TEXT", nullable: true),
                    firstName = table.Column<string>(type: "TEXT", nullable: true),
                    lastName = table.Column<string>(type: "TEXT", nullable: true),
                    phone = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    profilePicture = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserModelId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    bookingModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    dates = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    bookingName = table.Column<string>(type: "TEXT", nullable: true),
                    ListingModellistingId = table.Column<int>(type: "INTEGER", nullable: true),
                    renterModelId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.bookingModelId);
                    table.ForeignKey(
                        name: "FK_Bookings_Listings_ListingModellistingId",
                        column: x => x.ListingModellistingId,
                        principalTable: "Listings",
                        principalColumn: "listingId");
                    table.ForeignKey(
                        name: "FK_Bookings_Renters_renterModelId",
                        column: x => x.renterModelId,
                        principalTable: "Renters",
                        principalColumn: "renterModelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ListingModellistingId",
                table: "Bookings",
                column: "ListingModellistingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_renterModelId",
                table: "Bookings",
                column: "renterModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Landlord");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Renters");
        }
    }
}
