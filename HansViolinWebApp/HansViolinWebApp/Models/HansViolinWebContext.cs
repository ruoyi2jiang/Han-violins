using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HansViolinWebApp.Models
{
    public partial class HansViolinWebContext : DbContext
    {
        public HansViolinWebContext(DbContextOptions<HansViolinWebContext> options) : base(options)
        {
            //Database.EnsureDeleted();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<BusinessDetail> BusinessDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(i => i.Items)
                .WithOne(c => c.Category);

            modelBuilder.Entity<Item>()
                .HasMany(im => im.Images)
                .WithOne(it => it.Item);
        }
    }
}
