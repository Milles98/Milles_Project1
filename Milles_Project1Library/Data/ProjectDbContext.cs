﻿using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Models;

namespace Milles_Project1Library.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
        {

        }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        public DbSet<Calculator> Calculator { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Shape> Shape { get; set; }
        public DbSet<GameHistory> GameHistory { get; set; }
        public DbSet<GameStatistics> GameStatistics { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calculator>()
                .Property(c => c.Number1)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Calculator>()
                .Property(c => c.Number2)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);
            modelBuilder.Entity<Calculator>()
                .Property(c => c.Result)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Shape>()
                .Property(s => s.Area)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Shape>()
                .Property(s => s.Perimeter)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Shape>()
                .Property(s => s.Base)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Shape>()
                .Property(s => s.Height)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Shape>()
                .Property(s => s.SideLength)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }
    }
}
