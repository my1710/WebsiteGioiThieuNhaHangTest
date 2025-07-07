using GioiThieuNhaHang.Models;
using Microsoft.EntityFrameworkCore;

namespace GioiThieuNhaHang.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet cho từng bảng
        public DbSet<LoaiMon> LoaiMon { get; set; }
        public DbSet<MonAn> MonAn { get; set; }
        public DbSet<DatBan> DatBan { get; set; }
        public DbSet<TinTuc> TinTuc { get; set; }
        public DbSet<LienHe> LienHe { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AdminLogs> AdminLog { get; set; }
        public DbSet<Roles> Role { get; set; }
        public DbSet<AdminRoles> AdminRole { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình khóa chính tổng hợp cho AdminRoles
            modelBuilder.Entity<AdminRoles>()
                .HasKey(ar => new { ar.IdAD, ar.RoleID });

            // Cấu hình quan hệ AdminUser - AdminRoles
            modelBuilder.Entity<AdminRoles>()
                .HasOne(ar => ar.AdminUser)
                .WithMany(u => u.AdminRoles)
                .HasForeignKey(ar => ar.IdAD);

            modelBuilder.Entity<AdminRoles>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.AdminRoles)
                .HasForeignKey(ar => ar.RoleID);

            // Cấu hình quan hệ AdminUser - AdminLogs
            modelBuilder.Entity<AdminLogs>()
                .HasKey(al => al.LogID); // 👈 Khóa chính

            modelBuilder.Entity<AdminLogs>()
                .HasOne(log => log.AdminUser)
                .WithMany(user => user.Logs)
                .HasForeignKey(log => log.IdAD);

            // Cấu hình quan hệ MonAn - LoaiMon
            modelBuilder.Entity<MonAn>()
                .HasOne(m => m.LoaiMon)
                .WithMany(l => l.MonAns)
                .HasForeignKey(m => m.IdLoai);

            base.OnModelCreating(modelBuilder);
        }
    }
}
