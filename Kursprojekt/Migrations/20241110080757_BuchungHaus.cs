using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursprojekt.Migrations
{
    public partial class BuchungHaus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Häuser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bild = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AuftragID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZimmerAnzahl = table.Column<int>(type: "int", nullable: false),
                    Preis = table.Column<double>(type: "float", nullable: false),
                    Anschrift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IstReserviert = table.Column<bool>(type: "bit", nullable: false),
                    IstVerkauft = table.Column<bool>(type: "bit", nullable: false),
                    VerkaufDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Häuser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Buchungen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bearbeiter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KundenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KundenTelefonNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuchungDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BesichtigungDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BesichtigungZeit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HausID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buchungen", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Buchungen_Häuser_HausID",
                        column: x => x.HausID,
                        principalTable: "Häuser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_HausID",
                table: "Buchungen",
                column: "HausID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buchungen");

            migrationBuilder.DropTable(
                name: "Häuser");
        }
    }
}
