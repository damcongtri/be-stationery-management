using System.Data;
using stationeryManagement.Data.Common.DbContext;
using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data.Common.DbContext;
public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    public void MarkAsModified(object o, string propertyName)
    {
        this.Entry(o).Property(propertyName).IsModified = true;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasIndex(c => c.RoleName)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(c => c.Email)
            .IsUnique();
        
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.CategoryName)
            .IsUnique();

        modelBuilder.Entity<Request>()
            .HasOne(r => r.Approver)
            .WithMany()
            .HasForeignKey(r => r.ApprovedId);
    }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Stationery>? Stationeries { get; set; }
    public DbSet<Import>? Imports { get; set; }
    public DbSet<ImportDetail>? ImportDetails { get; set; }
    public DbSet<Request>? Requests { get; set; }
    public DbSet<RequestDetail>? RequestDetails { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<Supplier>? Suppliers { get; set; }
    public DbSet<User>? Users { get; set; }
    // public DbSet<RefreshToken>? RefreshTokens { get; set; }
}