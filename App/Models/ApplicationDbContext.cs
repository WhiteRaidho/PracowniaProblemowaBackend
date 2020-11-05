﻿using Microsoft.EntityFrameworkCore;
﻿using FitKidCateringApp.Models.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FitKidCateringApp.Models.Offers;

namespace FitKidCateringApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        #region OnModelCreating()
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region ApplicationDbContext()
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        #endregion

        public DbSet<CoreUser> CoreUsers { get; set; }
        public DbSet<CoreRole> CoreRoles { get; set; }
        public DbSet<CorePermission> CorePermissions { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Institution> Institutions { get; set; }
    }
}
