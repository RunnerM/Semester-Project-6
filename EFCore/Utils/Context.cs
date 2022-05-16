using Data.Domain;
using EFCore.Config;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Utils;

public class Context : DbContext
{
    // sqladmin password:
    // 7DaM5vEa!1q550H8#pFtH

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if MIGRATE_DB
              Config.Config.Init(ConfigVariables.Variables("Default"));
#endif

        if (Config.Config.UnitTests)
        {
            optionsBuilder.UseInMemoryDatabase("db");
        }
        else
        {
            optionsBuilder.UseSqlServer(Config.Config.ConnectionString);
        }
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