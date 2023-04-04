using EasyGift_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyGift_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }
        public DbSet<Product> Products { get; set; }
    }
}
