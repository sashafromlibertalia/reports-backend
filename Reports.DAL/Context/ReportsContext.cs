using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;

namespace Reports.DAL.Context
{
    public sealed class ReportsContext : DbContext
    {
        public ReportsContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<SprintEntity> Sprints { get; set; }
        public DbSet<ReportEntity> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentEntity>()
                .Property(item => item.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<TaskEntity>()
                .HasMany(item => item.Comments)
                .WithOne();

            modelBuilder.Entity<EmployeeEntity>()
                .HasMany(item => item.Tasks)
                .WithOne();
            modelBuilder.Entity<EmployeeEntity>()
                .HasMany(item => item.Staff)
                .WithOne();

            modelBuilder.Entity<SprintEntity>()
                .HasMany(item => item.ApprovedReports)
                .WithOne();

            modelBuilder.Entity<ReportEntity>()
                .HasMany(item => item.Tasks)
                .WithOne();
            modelBuilder.Entity<ReportEntity>()
                .HasMany(item => item.StaffReports)
                .WithOne();
        }
    }
}