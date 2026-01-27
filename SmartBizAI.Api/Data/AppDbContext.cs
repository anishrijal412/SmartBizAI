using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Data;

public sealed class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Income> Incomes => Set<Income>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Invoice> Invoices => Set<Invoice>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
            entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(100);
        });

        builder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.SKU).IsRequired().HasMaxLength(50);
        });

        builder.Entity<Income>(entity =>
        {
            entity.Property(e => e.Source).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Notes).HasMaxLength(500);
        });

        builder.Entity<Expense>(entity =>
        {
            entity.Property(e => e.Category).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Notes).HasMaxLength(500);
        });

        builder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(150);
        });
    }
}
