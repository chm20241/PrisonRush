using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.GammaShop.Infrastructure;

namespace Spg.GammaShop.DbExtentions
{
    public static class DatabaseExtentions
    {
        public static void ConfigureSQLite(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<GammaShopContext>(options =>
            {
                if (!options.IsConfigured)
                    options.UseLazyLoadingProxies();
                    options.UseSqlite(connectionString);
            });
                
        }
    }
}
