using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MiniStore.Helper;

namespace MiniStore.Context
{
    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

            var connectionString = EnvirenmentVariablesHelper.GetEnvironmentVariable("POSTGRESQL_CONNECTION_STRING");
            optionsBuilder.UseNpgsql(connectionString);

            return new StoreContext(optionsBuilder.Options);
        }
    }
}
