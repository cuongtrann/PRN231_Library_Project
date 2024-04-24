using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN231_Library_Project.DataAccess.Models
{
    public partial class PRN231_Library_ProjectContext : DbContext
    {
        public PRN231_Library_ProjectContext()
        {
        }

        public PRN231_Library_ProjectContext(DbContextOptions<PRN231_Library_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Checkout> Checkouts { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("author");

                entity.Property(e => e.BookContent)
                    .IsUnicode(false)
                    .HasColumnName("book_content");

                entity.Property(e => e.Category)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Copies).HasColumnName("copies");

                entity.Property(e => e.CopiesAvailable).HasColumnName("copies_available");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Img)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.Property(e => e.Title)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Checkout>(entity =>
            {
                entity.ToTable("checkout");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CheckoutDate)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("checkout_date");

                entity.Property(e => e.ReturnDate)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("return_date");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Checkouts)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_checkout_book");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("author");

                entity.Property(e => e.CheckoutDate)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("checkout_date");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Img)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.Property(e => e.ReturnedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("returned_date");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("user_email");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminEmail)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("admin_email");

                entity.Property(e => e.Closed).HasColumnName("closed");

                entity.Property(e => e.Question)
                    .IsUnicode(false)
                    .HasColumnName("question");

                entity.Property(e => e.Response)
                    .IsUnicode(false)
                    .HasColumnName("response");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("user_email");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(150)
                    .HasColumnName("user_email");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReviewDescription)
                    .IsUnicode(false)
                    .HasColumnName("review_description");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_review_book");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
