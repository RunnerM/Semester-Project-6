using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Utils;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #if MIGRATE_STAGING
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=Staging;User Id=sa;Password=123456;");
        #elif MIGRATE_PROD
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=Prod;User Id=sa;Password=123456;");
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