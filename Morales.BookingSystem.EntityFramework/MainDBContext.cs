using Microsoft.EntityFrameworkCore;
using Morales.BookingSystem.EntityFramework.Entities;

namespace Morales.BookingSystem.EntityFramework
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options){}
        
        public virtual DbSet<AccountEntity> Accounts { get; set; }
    }
}