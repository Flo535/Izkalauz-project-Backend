using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IzKalauzBackend.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // SQLite használata
            optionsBuilder.UseSqlite("Data Source=IzKalauz.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
