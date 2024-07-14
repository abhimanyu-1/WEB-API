using Microsoft.EntityFrameworkCore;
using WEB_API.Model;

namespace WEB_API.Data1
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<properties> Properties { get; set; }   
    }
}
