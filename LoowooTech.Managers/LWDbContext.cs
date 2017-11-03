using LoowooTech.Models;
using System.Data.Entity;

namespace LoowooTech.Managers
{
    public class LWDbContext:DbContext
    {
        public LWDbContext() : base("name=DbConnection")
        {
            Database.SetInitializer<LWDbContext>(null);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<LWSystem> LWSystems { get; set; }
    }
}
