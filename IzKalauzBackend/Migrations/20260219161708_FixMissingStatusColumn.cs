using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IzKalauzBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixMissingStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ÜRESRE HAGYJUK: Az oszlop már létezik az adatbázisban, 
            // csak azért kell ez a fájl, hogy az EF elkönyvelje: ez a migráció is "kész".
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Szintén üres
        }
    }
}