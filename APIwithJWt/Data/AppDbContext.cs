using APIwithJWt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace APIwithJWt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
            
               // public DbSet<User> Users { get; set; }
               
              public DbSet<User> Users { get; set; }
            }

        }

    
    

