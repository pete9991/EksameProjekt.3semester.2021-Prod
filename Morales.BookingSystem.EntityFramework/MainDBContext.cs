using System.Linq;
using Microsoft.EntityFrameworkCore;
using Morales.BookingSystem.EntityFramework.Entities;

namespace Morales.BookingSystem.EntityFramework
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentEntity>().HasOne(ae => ae.Customer)
                .WithMany(c => c.Appointments).HasForeignKey(a => new {a.CustomerId});
            modelBuilder.Entity<AppointmentEntity>().HasOne(ae => ae.Employee)
                .WithMany(c => c.Appointments).HasForeignKey(a => new {a.EmployeeId});
        }

        public virtual DbSet<AccountEntity> Accounts { get; set; }

        public virtual DbSet<AppointmentEntity> Appointments { get; set; }
        
        public virtual DbSet<TreatmentEntity> Treatments { get; set; }
    }
}