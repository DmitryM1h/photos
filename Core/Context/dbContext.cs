using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.entities;
using Microsoft.EntityFrameworkCore;


namespace Core.Context
{
    public class PhotosContext(DbContextOptions<PhotosContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Photo> Photos { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=UsersPhotoDb;Trusted_Connection=True;");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                             
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photos");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.HasOne(p => p.Publisher)
                      .WithMany(u => u.Photos)
                      .HasForeignKey(p => p.PublisherId)
                      .IsRequired();
            });
        }



    }
}
