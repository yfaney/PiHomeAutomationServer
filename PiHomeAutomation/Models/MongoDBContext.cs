using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PiHomeAutomation.Models
{
    class MongoDBContext : DbContext
    {
        public MongoDBContext() { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // To remove the requests to the Migration History table
            Database.SetInitializer<MongoDBContext>(null);
            // To remove the plural names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //For versions of EF before 6.0, uncomment the following line to remove calls to EdmTable, a metadata table
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }
    }
}