using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using mvc_angela_trifunoska.Models;

namespace mvc_angela_trifunoska.Data
{
    public class MVCUniversityContext : DbContext
    {
        public MVCUniversityContext(DbContextOptions<MVCUniversityContext> options)
            : base(options) { }

        public DbSet<Student> Student { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "MVCUniversityDb.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasOne<Professor>(p => p.FirstProfessor);

            modelBuilder.Entity<Subject>().HasOne<Professor>(p => p.SecondProfessor);

            modelBuilder.Entity<Professor>().HasMany<Subject>(prop => prop.Subject);

            modelBuilder.Entity<StudentSubject>().HasOne<Subject>(prop => prop.Subject);

            modelBuilder.Entity<StudentSubject>().HasOne<Student>(prop => prop.Student);

        }
    }

}
