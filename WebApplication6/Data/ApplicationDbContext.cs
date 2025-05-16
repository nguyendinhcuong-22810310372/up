using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<SanPham> SanPham { get; set; }
    public DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietSanPham>()
            .HasOne(ct => ct.SanPham)
            .WithMany(sp => sp.ChiTietSanPham)
            .HasForeignKey(ct => ct.MaSP);
    }
}