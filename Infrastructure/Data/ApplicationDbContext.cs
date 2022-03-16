using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Quote> Quotes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var authorBuilder = modelBuilder.Entity<Author>();
        authorBuilder.ToTable(nameof(Author))
            .HasKey(a => a.Id);
        authorBuilder.Property(a => a.Name)
            .HasMaxLength(100)
            .IsRequired();
        authorBuilder.HasIndex(a => a.Name)
            .IsUnique();

        var quoteBuilder = modelBuilder.Entity<Quote>();
        quoteBuilder.ToTable(nameof(Quote))
            .HasKey(q => q.Id);
        quoteBuilder.Property(q => q.Text)
            .HasMaxLength(300)
            .IsRequired();
        quoteBuilder.HasOne(q => q.Author)
            .WithMany(a => a.Quotes)
            .IsRequired();
    }
}
