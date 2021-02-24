using DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    /*Config*/
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions Options) : base(Options)
        {
            
        }
    }
    
    /*Entity*/
    public partial class DatabaseContext
    {
        public DbSet<Video> Videos     { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
    }
    
    /*Relation*/
    public partial class DatabaseContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new Video());
            builder.ApplyConfiguration(new Chapter());
        }
    }
}