﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using inz.Models;

namespace inz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<inz.Models.Song> Song { get; set; }
        public DbSet<inz.Models.Artist> Artist { get; set; }
        public DbSet<inz.Models.Album> Album { get; set; }
        public DbSet<inz.Models.Producer> Producer { get; set; }
        public DbSet<inz.Models.Opinion> Opinion { get; set; }
        public DbSet<inz.Models.Favorite> Favorite { get; set; }
        public DbSet<inz.Models.Changelog> Changelog { get; set; }
    }
}
