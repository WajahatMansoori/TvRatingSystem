using MediaVoirAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MediaVoirAdmin.DAL
{
    public class MediaVoirContext : DbContext
    {
        public MediaVoirContext() : base("MediaVoirContext")
        {
        }

        public DbSet<AdminUsers> adminuser { get; set; }
        public DbSet<RatingData> ratingdata { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}