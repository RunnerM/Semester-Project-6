using Data;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Utils;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Could be a connection string in docker secrets or something.
        optionsBuilder.UseSqlServer(
            "Server=tcp:db-server-sep6.database.windows.net,1433;Initial Catalog=movei-db;Persist Security Info=False;User ID=sqladmin;Password=#6WNhfMG54UJ7kIr5LGQeqznCC8c*KxMf^VVVW6kA$cfStLm2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Movie

        modelBuilder.Entity<Movie>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Rating)
            .WithOne(r => r.Movie)
            .HasForeignKey<Rating>(r => r.MovieId);
        modelBuilder.Entity<Rating>().HasKey(r=>r.MovieId);

        #endregion

        #region People

        modelBuilder.Entity<Director>()
            .HasKey(dr=> new{dr.MovieId, dr.PersonId});

        modelBuilder.Entity<Director>()
            .HasOne(dr => dr.Movie)
            .WithMany(m => m.Directors)
            .HasForeignKey(dr=> dr.MovieId);
        
        modelBuilder.Entity<Director>()
            .HasOne(dr => dr.Person)
            .WithMany(p => p.Directors)
            .HasForeignKey(dr=> dr.PersonId);

        modelBuilder.Entity<Star>()
            .HasKey(dr=> new{dr.MovieId, dr.PersonId});

        modelBuilder.Entity<Star>()
            .HasOne(dr => dr.Movie)
            .WithMany(m => m.Stars)
            .HasForeignKey(dr=> dr.MovieId);
            
        modelBuilder.Entity<Star>()
            .HasOne(s => s.Person)
            .WithMany(p => p.Stars)
            .HasForeignKey(s=> s.PersonId);
            
        modelBuilder.Entity<Person>();

        #endregion
    }
}