using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EFCore.Utils;

public static class ContextExtensions
{
    public static void Configure<TModel>(this ModelBuilder builder, params Action<EntityTypeBuilder<TModel>>[] actions)
        where TModel : class
    {
        builder.Entity<TModel>();
        foreach (var action in actions)
            action(builder.Entity<TModel>());
        var dateProperties = typeof(TModel).GetProperties()
            .Where(p => p.PropertyType == typeof(DateTime));
        foreach (var prop in dateProperties)
            builder.Entity<TModel>()
                .Property(prop.PropertyType, prop.Name)
                .HasDefaultValue(DateTime.MinValue);
    }
}