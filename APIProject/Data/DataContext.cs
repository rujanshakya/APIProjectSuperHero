using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace APIProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
            public DbSet<SuperHero> SuperHeroes { get; set; }
    
        
    }
}
