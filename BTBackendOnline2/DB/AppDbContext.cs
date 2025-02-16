using BTBackendOnline2.Models;
using Microsoft.EntityFrameworkCore;

namespace BTBackendOnline2.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<AllowAccess>().HasKey( x => x.Id);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Role>().HasKey(r =>  r.RoleId);
            modelBuilder.Entity<Intern>().HasKey(i => i.Id);

            modelBuilder.Entity<User>().Property(u => u.UserId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Role>().Property(u => u.RoleId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Intern>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AllowAccess>().Property(u => u.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<AllowAccess>()
                .HasOne(a => a.Role)
                .WithMany(a => a.AllowAccesses)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(r => r.Role)
                .WithOne(r => r.User)
                .HasForeignKey<User>(r => r.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            //Properties
            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Address)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<AllowAccess>()
                .Property(a => a.TableName)
                .HasMaxLength(100)
                .IsRequired();
        }

        public DbSet<Intern> Interns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AllowAccess> AllowAccess { get; set; }

    }
}
