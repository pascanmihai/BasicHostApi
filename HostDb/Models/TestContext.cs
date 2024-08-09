using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HostDb.Models;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<User> Users { get; set; }
   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-7G8BTB9\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasColumnType("decimal(10, 0)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
