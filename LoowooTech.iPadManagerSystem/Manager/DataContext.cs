using LoowooTech.iPadManagerSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Manager
{
    public class DataContext:DbContext
    {
        public DataContext() : base("name=DbConnection")
        {
            Database.SetInitializer<DataContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<iPad> iPads { get; set; }

        public DbSet<iPadInvoice> iPad_Invoices { get; set; }

        public DbSet<iPadRegister> iPad_Registers { get; set; }

        public DbSet<Register_iPad> Register_iPads { get; set; }

        public DbSet<iPadContract> iPad_Contracts { get; set; }

        public DbSet<iPadAccount> iPad_Accounts { get; set; }

        public DbSet<iPadContact> iPad_Contacts { get; set; }
        public DbSet<iPadDatum> iPad_Datums { get; set; }
        public DbSet<iPadModel> iPad_Models { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<iPadSpace> iPad_Spaces { get; set; }
    }
}