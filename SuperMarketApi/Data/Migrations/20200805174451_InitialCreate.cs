namespace SuperMarketApi.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));

            _ = migrationBuilder.CreateTable(
                name: "Products",
#pragma warning disable IDE0050 // Convert to tuple
                columns: table => new
#pragma warning restore IDE0050 // Convert to tuple
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));

            _ = migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
