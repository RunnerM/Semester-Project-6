using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Utils;

public class Context : DbContext
{
    // sqladmin password:
    // 7DaM5vEa!1q550H8#pFtH
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #if MIGRATE_STAGING
        optionsBuilder.UseSqlServer(@"Server=tcp:sep6-sql-server.database.windows.net,1433;Initial Catalog=movie-db;Persist Security Info=False;User ID=sqladmin;Password=7DaM5vEa!1q550H8#pFtH;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        #elif MIGRATE_PROD
        optionsBuilder.UseSqlServer(@"Server=tcp:sep6-sql-server.database.windows.net,1433;Initial Catalog=movie-db_prod;Persist Security Info=False;User ID=sqladmin;Password=7DaM5vEa!1q550H8#pFtH;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        #endif
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Configure<Movie>(
            m=>m.HasKey(m=>m.Id),
            m=>m.HasMany<UserToplists>().WithOne()
            );
        modelBuilder.Configure<UserToplists>();
        modelBuilder.Configure<User>();
    }
}