﻿using FiorelloTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FiorelloTask.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }      
        public DbSet<PageIntro> PageIntros { get; set; }    
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Product> Products { get; set; }    
        public DbSet<Bio> Bios { get; set; }        
        public DbSet<Blog> Blogs { get; set; }
     
    }
}
