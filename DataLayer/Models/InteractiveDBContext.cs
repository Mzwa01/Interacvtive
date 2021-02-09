using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.Models
{
    public partial class InteractiveDBContext : DbContext
    {
        public InteractiveDBContext()
        {
        }

        public InteractiveDBContext(DbContextOptions<InteractiveDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=InteractiveDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.HasIndex(e => e.AccountNumber, "IX_Account_num")
                    .IsUnique();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_number");

                entity.Property(e => e.OutstandingBalance)
                    .HasColumnType("money")
                    .HasColumnName("outstanding_balance");

                entity.Property(e => e.PersonCode).HasColumnName("person_code");

                entity.HasOne(d => d.PersonCodeNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.PersonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.HasIndex(e => e.IdNumber, "IX_Person_id")
                    .IsUnique();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id_number");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.AccountCode).HasColumnName("account_code");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");

                entity.Property(e => e.CaptureDate)
                    .HasColumnType("datetime")
                    .HasColumnName("capture_date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");

                entity.HasOne(d => d.AccountCodeNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.AccountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
