using Ecommerce.DAL.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.DbContexts
{
    public class MSSQLDBContext:DbContext
    {
        public MSSQLDBContext(DbContextOptions<MSSQLDBContext> opt):base(opt)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(ho => ho.Category).WithMany(wm => wm.Products).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>().HasOne(ho => ho.Brand).WithMany(wm => wm.Products).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderDetail>().HasOne(ho => ho.Product).WithMany(wm => wm.OrderDetails).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Orders>().HasIndex(hi => hi.OrderNumber).IsUnique();

            //data seed
            modelBuilder.Entity<Admin>().HasData(new Admin {
                ID=1,LastDate=DateTime.Now,LastIPNO="",Name="Bekir",Password= "202cb962ac59075b964b07152d234b70",Surname="Oturakçı",UserName="bekir"
            });
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Slide> Slide { get; set; }    
        public DbSet<Product> Product { get; set; } 
        public DbSet<Picture> Picture { get; set; } 
        public DbSet<Orders> Orders { get; set; } 
        public DbSet<OrderDetail> OrderDetail { get; set; } 
    }
}
