using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using XSquareCalculationsApi.Persistance;

namespace XSquareCalculationsApiTest
{
    internal sealed class XSquareCalculationsAPITestDbContextFactory
    {
        internal XSquareCalculationContext CreateDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<XSquareCalculationContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;
            var context = new XSquareCalculationContext(dbContextOptions);
            context.Database.EnsureCreated();
            return context;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }
    }
}
