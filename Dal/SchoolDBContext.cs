using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public class SchoolDBContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=SchoolAdmin;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(a => a.StudentId);
                e.HasMany(d => d.Tests).WithOne().HasForeignKey(a => a.StudentId);
            });

            modelBuilder.Entity<Test>(e =>
            {
                e.HasKey(a => a.TestId);
               // e.HasOne(d => d.Student).WithMany().HasForeignKey(a => a.StudentId);
            });
        }
    }
}
