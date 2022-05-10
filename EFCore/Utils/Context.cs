using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Utils;

public class Context : DbContext
{
    // sqladmin password:
    // 7DaM5vEa!1q550H8#pFtH
    public Context()
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if MIGRATE_STAGING
        optionsBuilder.UseSqlServer(@"Server=tcp:sep6-sql-server.database.windows.net,1433;Initial Catalog=movie-db;Persist Security Info=False;User ID=sqladmin;Password=7DaM5vEa!1q550H8#pFtH;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
#elif MIGRATE_PROD
        optionsBuilder.UseSqlServer(@"Server=tcp:sep6-sql-server.database.windows.net,1433;Initial Catalog=movie-db_prod;Persist Security Info=False;User ID=sqladmin;Password=7DaM5vEa!1q550H8#pFtH;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
#endif
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT ") is not null)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:sep6-sql-server.database.windows.net,1433;Initial Catalog=movie-db;Persist Security Info=False;User ID=sqladmin;Password=7DaM5vEa!1q550H8#pFtH;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
            var e = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT ");
        Console.WriteLine(e);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Configure<Movie>(
            m => m.HasKey(m => m.Id)
        );
        modelBuilder.Configure<User>(
            u => u.HasKey(user => user.Id)
        );

        modelBuilder.Configure<UserToplists>(
            tp => tp.HasKey(
                tp => new
                {
                    tp.MovieId,
                    tp.UserId
                }),
            tp => tp
                .HasOne(tp => tp.Movie)
                .WithMany(m => m.TopLists)
                .HasForeignKey(tp => tp.MovieId),
            tp => tp
                .HasOne(tp => tp.User)
                .WithMany(u => u.TopLists)
                .HasForeignKey(tp => tp.UserId),
            tp => tp
                .Property(tp => tp.TopListIndex)
                .IsRequired()
        );

        
    }
}