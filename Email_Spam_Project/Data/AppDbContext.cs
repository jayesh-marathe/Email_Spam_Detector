using Email_Spam_Project.Models;
using Microsoft.ML;
using Microsoft.EntityFrameworkCore;

namespace Email_Spam_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Email> Emails { get; set; }
    }
}
